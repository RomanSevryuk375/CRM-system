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
                .AddApiClient<SkillService>()
                .AddApiClient<SpecializationService>()
                .AddApiClient<ScheduleService>();

            return services;
        }
    }
}