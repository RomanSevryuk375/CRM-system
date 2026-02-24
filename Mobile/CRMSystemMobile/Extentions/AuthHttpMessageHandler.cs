namespace CRMSystemMobile.Extentions;

public class AuthHttpMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await SecureStorage.Default.GetAsync("jwt_token");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized) return response;
        SecureStorage.Default.Remove("jwt_token");

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Shell.Current.GoToAsync("//LoginPage");
        });

        return response;
    }
}
