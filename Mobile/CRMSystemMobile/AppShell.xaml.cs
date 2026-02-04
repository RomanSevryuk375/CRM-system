using CRMSystemMobile.View;

namespace CRMSystemMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("MyCarsPage", typeof(MyCarsPage));
        Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
        Routing.RegisterRoute("AddCarPage", typeof(AddCarPage));
        Routing.RegisterRoute("CarDetailsPage", typeof(CarDetailsPage));
        Routing.RegisterRoute("HistoryPage", typeof(HistoryPage));
        Routing.RegisterRoute("BillsPage", typeof(BillsPage));
        Routing.RegisterRoute("BookingPage", typeof(BookingPage));
    }
}
