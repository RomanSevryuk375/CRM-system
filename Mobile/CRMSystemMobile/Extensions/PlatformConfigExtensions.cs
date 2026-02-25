#if ANDROID
using Android.OS;
using Android.Views;
#endif
using Microsoft.Maui.LifecycleEvents;
using Color = Android.Graphics.Color;
#if IOS
using UIKit;
#endif

namespace CRMSystemMobile.Extensions;

public static class PlatformConfigExtensions
{
    [Obsolete("Obsolete")]
    public static MauiAppBuilder ConfigurePlatformLifecycle(this MauiAppBuilder builder)
    {
#if ANDROID
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddAndroid(android => android.OnCreate((activity, _) =>
            {
                if (activity.Window is not { } window)
                    return;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    window.SetStatusBarColor(Color.ParseColor("#112347"));
                }


                switch (Build.VERSION.SdkInt)
                {
                    case >= BuildVersionCodes.R:
                    {
                        var controller = window.InsetsController;
                        controller?.SetSystemBarsAppearance(0, (int)WindowInsetsControllerAppearance.LightStatusBars);

                        break;
                    }
                    case >= BuildVersionCodes.M:
                    {
                        var decorView = window.DecorView;
                        var currentFlags = (int)decorView.SystemUiVisibility;

                        decorView.SystemUiVisibility =
                            (StatusBarVisibility)(currentFlags & ~(int)SystemUiFlags.LightStatusBar);

                        break;
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
            handler.PlatformView.SetBackgroundColor(Color.Transparent);
            handler.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Color.Transparent);
#endif
        });
        return builder;
    }
}