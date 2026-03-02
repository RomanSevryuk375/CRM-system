using CRMSystemMobile.Services;

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

            services.AddApiClient<AbsenceService>()
                .AddApiClient<AcceptanceImgService>()
                .AddApiClient<AcceptanceService>()
                .AddApiClient<BillService>()
                .AddApiClient<CarService>()
                .AddApiClient<ClientService>()
                .AddApiClient<LoginService>()
                .AddApiClient<OrderService>()
                .AddApiClient<PartService>()
                .AddApiClient<PartSetService>()
                .AddApiClient<PaymentService>()
                .AddApiClient<PositionService>()
                .AddApiClient<RegistrationService>()
                .AddApiClient<ScheduleService>()
                .AddApiClient<ShiftService>()
                .AddApiClient<SkillService>()
                .AddApiClient<SpecializationService>()
                .AddApiClient<WorkerService>()
                .AddApiClient<WorkInOrderService>()
                .AddApiClient<WorkProposalService>()
                .AddApiClient<WorkService>();

            return services;
        }
    }
}