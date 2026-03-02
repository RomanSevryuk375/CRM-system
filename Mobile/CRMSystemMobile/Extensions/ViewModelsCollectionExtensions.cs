using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.Extensions;

public static class ViewModelsCollectionExtensions
{
    extension(IServiceCollection services)
    {
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
            services.AddTransient<OrderAcceptanceViewModel>();

            return services;
        }
    }
}