using CRMSystemMobile.View;

namespace CRMSystemMobile.Extensions;

public static class ViewsCollectionExtensions
{
    extension(IServiceCollection services)
    {
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