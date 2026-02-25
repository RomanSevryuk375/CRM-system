using CRMSystemMobile.Services;
using CRMSystemMobile.View;
using CRMSystemMobile.ViewModels;
using LoginViewModel = CRMSystemMobile.ViewModels.LoginViewModel;

namespace CRMSystemMobile.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        private IServiceCollection AddApiClient<TService>()
            where TService : class
        {
            services.AddHttpClient<TService>(client => { client.BaseAddress = new Uri(ApiConfig.BaseUrl); })
                .AddHttpMessageHandler<AuthHttpMessageHandler>();

            return services;
        }

        public IServiceCollection AddApplicationServices()
        {
            services.AddTransient<AuthHttpMessageHandler>();
            services.AddSingleton<IdentityService>();

            services.AddApiClient<OrderService>()
                .AddApiClient<ClientService>()
                .AddApiClient<BillService>()
                .AddApiClient<LoginService>()
                .AddApiClient<RegistrationService>()
                .AddApiClient<CarService>()
                .AddApiClient<PaymentService>()
                .AddApiClient<PartSetService>()
                .AddApiClient<WorkInOrderService>()
                .AddApiClient<WorkProposalService>()
                .AddApiClient<WorkerService>()
                .AddApiClient<PositionService>()
                .AddApiClient<WorkService>()
                .AddApiClient<ScheduleService>();

            return services;
        }

        public IServiceCollection AddViewModels()
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MyCarsViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<AddCarViewModel>();
            services.AddTransient<CarDetailsViewModel>();
            services.AddTransient<BillsViewModel>();
            services.AddTransient<BookingViewModel>();
            services.AddTransient<OrderDetailsViewModel>();
            services.AddTransient<BillDetailsViewModel>();
            services.AddTransient<WorkerMainViewModel>();
            services.AddTransient<WorkerProfileViewModel>();
            services.AddTransient<WorkerOrderDetailsViewModel>();
            services.AddTransient<AddPartViewModel>();
            services.AddTransient<AddProposalViewModel>();
            services.AddTransient<WorkerScheduleViewModel>();

            return services;
        }

        public IServiceCollection AddViews()
        {
            services.AddSingleton<AppShell>();
            services.AddSingleton<App>();

            services.AddTransient<LoginPage>();
            services.AddTransient<RegistrationPage>();
            services.AddTransient<MainPage>();
            services.AddTransient<MyCarsPage>();
            services.AddTransient<ProfilePage>();
            services.AddTransient<AddCarPage>();
            services.AddTransient<CarDetailsPage>();
            services.AddTransient<BillsPage>();
            services.AddTransient<BookingPage>();
            services.AddTransient<OrderDetailsPage>();
            services.AddTransient<BillDetailsPage>();
            services.AddTransient<WorkerMainPage>();
            services.AddTransient<WorkerProfilePage>();
            services.AddTransient<WorkerOrderDetailsPage>();
            services.AddTransient<AddPartPage>();
            services.AddTransient<AddProposalPage>();
            services.AddTransient<WorkerSchedulePage>();

            return services;
        }
    }
}