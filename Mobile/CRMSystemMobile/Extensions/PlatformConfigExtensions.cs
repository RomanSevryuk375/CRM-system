#if ANDROID
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
#endif
using Microsoft.Maui.LifecycleEvents;
#if IOS
using UIKit;
#endif

namespace CRMSystemMobile.Extensions;

public static class PlatformConfigExtensions
{
    public static MauiAppBuilder ConfigurePlatformLifecycle(this MauiAppBuilder builder)
    {
#if ANDROID
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddAndroid(android => android.OnCreate(static (activity, bundle) =>
            {
                var window = activity.Window;
                if (window == null) return;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#112347"));
                }

                var controller = WindowCompat.GetInsetsController(window, window.DecorView);

                if (controller == null)
                {
                    return;
                }
                controller.AppearanceLightStatusBars = false;
            }));
        });
#endif
#if IOS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddiOS(iOS => iOS.FinishedLaunching((application, launchOptions) =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
                
                return true;
            }));
        });
#endif
        return builder;
    }

    public static MauiAppBuilder ConfigureCustomHandlers(this MauiAppBuilder builder)
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, _) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            handler.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
        return builder;
    }
}