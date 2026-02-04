namespace CRMSystemMobile
{
    internal static class AppHelpers
    {

        private static async void CheckAuthAndRedirect()
        {
            try
            {
                var token = await SecureStorage.Default.GetAsync("jwt_token");

                if (!string.IsNullOrEmpty(token))
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Auth check failed: {ex.Message}");
            }
        }
    }
}