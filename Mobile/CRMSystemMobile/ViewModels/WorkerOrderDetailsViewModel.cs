using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Order;
using Shared.Contracts.PartSet;
using Shared.Contracts.WorkInOrder;
using Shared.Contracts.WorkProposal;
using Shared.Enums;
using Shared.Filters;
using System.Collections.ObjectModel;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerOrderDetailsViewModel(
    WorkInOrderService workInOrderService,
    PartSetService partSetService,
    WorkProposalService workProposalService,
    IdentityService identityService,
    OrderService orderService)
    : ObservableObject, IQueryAttributable
{
    [ObservableProperty] public partial OrderResponse? Order { get; set; }

    [ObservableProperty] public partial bool IsBusy { get; set; }

    [ObservableProperty] public partial bool IsRefreshing { get; set; }

    [ObservableProperty] public partial int SelectedTab { get; set; } = 0;

    public ObservableCollection<WorkInOrderResponse> MyWorks { get; } = [];
    public ObservableCollection<PartSetResponse> OrderParts { get; } = [];
    public ObservableCollection<WorkProposalResponse> MyProposals { get; } = [];

    public bool HasParts => OrderParts.Count > 0;
    public bool HasProposals => MyProposals.Count > 0;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Order", out var value))
        {
            return;
        }

        Order = (OrderResponse)value;
        LoadAllDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadAllData()
    {
        IsBusy = true;

        try
        {
            await Task.WhenAll(
                LoadWorksInternal(),
                LoadPartsInternal(),
                LoadProposalsInternal()
            );
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"Не удалось загрузить данные. {ex}", "ОК");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    private async Task LoadWorksInternal()
    {
        if (Order != null)
        {
            var filter = new WorkInOrderFilter(
                OrderIds: [Order.Id],
                WorkerIds: null,
                JobIds: null,
                StatusIds: null,
                SortBy: null,
                Page: 1,
                Limit: 10,
                IsDescending: true
            );
            var (items, _) = await workInOrderService.GetWorksInOrder(filter);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MyWorks.Clear();
                if (items == null)
                {
                    return;
                }

                foreach (var i in items)
                {
                    MyWorks.Add(i);
                }
            });
        }
    }

    private async Task LoadPartsInternal()
    {
        var filter = new PartSetFilter(
            OrderIds: [Order?.Id],
            PositionIds: [],
            ProposalIds: [],
            SortBy: null,
            Page: 1,
            Limit: 100,
            IsDescending: true
        );
        var (items, _) = await partSetService.GetPartSets(filter);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            OrderParts.Clear();
            if (items == null)
            {
                return;
            }

            foreach (var i in items)
            {
                OrderParts.Add(i);
            }
        });
    }

    private async Task LoadProposalsInternal()
    {
        var (profileId, _) = await identityService.GetProfileIdAsync();
        var filter = new WorkProposalFilter(
            OrderIds: [Order!.Id],
            WorkerIds: [(int)profileId],
            JobIds: [], StatusIds: [], SortBy: null, Page: 1, Limit: 100, IsDescending: true
        );
        var (items, _) = await workProposalService.GetWorkProposals(filter);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            MyProposals.Clear();
            if (items == null)
            {
                return;
            }

            foreach (var i in items)
            {
                MyProposals.Add(i);
            }
        });
    }

    [RelayCommand]
    private void SelectTab(string tabIndex)
    {
        if (int.TryParse(tabIndex, out var index))
        {
            SelectedTab = index;
        }
    }

    [RelayCommand]
    private async Task GoToAddPart()
    {
        if (Order != null)
        {
            var navParam = new Dictionary<string, object> { { "OrderId", Order.Id } };
            await Shell.Current.GoToAsync("AddPartPage", navParam);
        }
    }

    [RelayCommand]
    private async Task GoToAddProposal()
    {
        if (Order != null)
        {
            var navParam = new Dictionary<string, object> { { "OrderId", Order.Id } };
            await Shell.Current.GoToAsync("AddProposalPage", navParam);
        }
    }

    [RelayCommand]
    private async Task ChangeStatus(WorkInOrderResponse? work)
    {
        if (work == null)
        {
            return;
        }

        WorkStatusEnum newStatus;
        string actionName;

        switch (work.StatusId)
        {
            case (int)WorkStatusEnum.Pending:
                newStatus = WorkStatusEnum.InProgress;
                actionName = "Начата";
                break;
            case (int)WorkStatusEnum.InProgress:
                newStatus = WorkStatusEnum.Completed;
                actionName = "Завершена";
                break;
            default:
                return;
        }

        var confirm = await Shell.Current.DisplayAlert("Подтверждение",
            $"Работа будет {actionName}. Продолжить?", "Да", "Нет");

        if (!confirm)
        {
            return;
        }

        var request = new WorkInOrderUpdateRequest
        {
            WorkerId = null,
            StatusId = newStatus,
            TimeSpent = null
        };

        var error = await workInOrderService.UpdateWorkInOrder(work.Id, request);

        if (error == null)
        {
            await LoadAllData();
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }
    
    [RelayCommand]
    public async Task DownloadPdf()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            if (Order != null)
            {
                var pdfBytes = await orderService.GetOrderPdf(Order.Id);

                if (pdfBytes == null || pdfBytes.Length == 0)
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Файл заказ-наряда еще не сформирован или недоступен.", "ОК");
                    return;
                }

                var fileName = $"Order_{Order.Id}.pdf";
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

                await File.WriteAllBytesAsync(filePath, pdfBytes);

                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    Title = "Заказ-наряд",
                    File = new ReadOnlyFile(filePath)
                });
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"Не удалось открыть файл: {ex.Message}", "ОК");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteWork(WorkInOrderResponse? item)
    {
        if (item == null)
        {
            return;
        }

        var confirm = await Shell.Current.DisplayAlert("Удаление",
            $"Удалить работу \"{item.Job}\"?", "Да", "Нет");

        if (!confirm)
        {
            return;
        }

        var error = await workInOrderService.DeleteWorkInOrder(item.Id);

        if (error == null)
        {
            MyWorks.Remove(item);
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "OK");
        }
    }

    [RelayCommand]
    private async Task DeletePart(PartSetResponse? item)
    {
        if (item == null)
        {
            return;
        }

        var confirm = await Shell.Current.DisplayAlert("Удаление",
            $"Удалить запчасть \"{item.Position}\"?", "Да", "Нет");

        if (!confirm)
        {
            return;
        }

        var error = await partSetService.DeletePartSet(item.Id);

        if (error == null)
        {
            OrderParts.Remove(item);
            OnPropertyChanged(nameof(HasParts));
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "OK");
        }
    }

    [RelayCommand]
    private async Task DeleteProposal(WorkProposalResponse? item)
    {
        if (item == null)
        {
            return;
        }

        var confirm = await Shell.Current.DisplayAlert("Удаление",
            $"Удалить предложение \"{item.Job}\"?", "Да", "Нет");

        if (!confirm)
        {
            return;
        }

        var error = await workProposalService.DeleteWorkProposal(item.Id);

        if (error == null)
        {
            MyProposals.Remove(item);
            OnPropertyChanged(nameof(HasProposals));
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "OK");
        }
    }

    [RelayCommand]
    private static async Task GoBack() => await Shell.Current.GoToAsync("..");
    
    [RelayCommand]
    private async Task GoToAcceptance()
    {
        if (Order != null)
        {
            var navParam = new Dictionary<string, object> { { "Order", Order } };
            await Shell.Current.GoToAsync("OrderAcceptancePage", navParam);
        }
    }
}