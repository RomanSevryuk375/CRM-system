using CRMSystemMobile.Extentions;
using CRMSystemMobile.Services;
using CRMSystemMobile.View;
using CRMSystemMobile.ViewModel;
using CRMSystemMobile.ViewModels;
using Microsoft.Extensions.Logging;

namespace CRMSystemMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            builder.Services.AddTransient<AuthHttpMessageHandler>();
            builder.Services.AddSingleton<IdentityService>();

            builder.Services.AddHttpClient<OrderService>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.100.52:5066/");
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();
            builder.Services.AddHttpClient<LoginService>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.100.52:5066/");
            });
            builder.Services.AddHttpClient<RegistrationService>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.100.52:5066/");
            });
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
