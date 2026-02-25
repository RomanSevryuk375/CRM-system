using Foundation;

namespace CRMSystemMobile;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    [Obsolete("Obsolete")]
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}