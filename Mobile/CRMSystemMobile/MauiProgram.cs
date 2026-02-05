using CRMSystemMobile.Extentions;
using CRMSystemMobile.Services;
using CRMSystemMobile.View;
using CRMSystemMobile.ViewModel;
using CRMSystemMobile.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if IOS
using UIKit;
#endif
#if ANDROID
using Android.OS;
using Android.Views;
#endif

namespace CRMSystemMobile
{
    public static class MauiProgram
    {
        [Obsolete]
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

#if ANDROID
            builder.ConfigureLifecycleEvents(static events =>
            {
                events.AddAndroid(static android => android.OnCreate(static (activity, bundle) =>
                {
                    if (bundle == null)
                    {
                        var window = activity.Window;
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        {
#if NET8_0_OR_GREATER
                            if (Build.VERSION.SdkInt < (BuildVersionCodes)35)
                            {
                                window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#112347"));
                            }
                            else
                            {
                                var insetsController = window.InsetsController;
                                if (insetsController != null)
                                {
                                    window.SetDecorFitsSystemWindows(true);
                                    window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#112347"));
                                }
                            }
#else
                            window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#112347"));
#endif
                        }
                    }

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                    {
                        var decor = activity.Window?.DecorView;
                        if (decor != null)
                        {
                            decor.SystemUiVisibility &= ~(StatusBarVisibility)SystemUiFlags.LightStatusBar;
                        }
                    }
                }));
            });
#endif
#if IOS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddiOS(iOS => iOS.FinishedLaunching((application, launchOptions) =>
                {
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

                    return true;
                }));
            });
#endif

            builder.Services.AddTransient<AuthHttpMessageHandler>();
            builder.Services.AddSingleton<IdentityService>();

            builder.Services.AddHttpClient<OrderService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();

            builder.Services.AddHttpClient<ClientService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();

            builder.Services.AddHttpClient<BillService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();

            builder.Services.AddHttpClient<LoginService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();
            builder.Services.AddHttpClient<RegistrationService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();
            builder.Services.AddHttpClient<CarService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();
            builder.Services.AddHttpClient<PaymentService>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            })
.AddHttpMessageHandler<AuthHttpMessageHandler>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MyCarsViewModel>();
            builder.Services.AddTransient<MyCarsPage>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<AddCarViewModel>();
            builder.Services.AddTransient<AddCarPage>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddTransient<CarDetailsPage>();
            builder.Services.AddTransient<BillsViewModel>();
            builder.Services.AddTransient<BillsPage>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<BookingPage>();
            builder.Services.AddTransient<PaymentsViewModel>();
            builder.Services.AddTransient<PaymentsPage>();

            return builder.Build();
        }
    }
}
