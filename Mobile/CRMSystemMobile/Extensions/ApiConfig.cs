namespace CRMSystemMobile.Extensions;

public static class ApiConfig
{
    public const string LocalIpAddress = "172.23.201.67";

    public const string Port = "5066";

    public static string BaseUrl
    {
        get
        {
            // Android Emulator
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.DeviceType == DeviceType.Virtual)
            {
                return $"http://10.0.2.2:{Port}/";
            }

            // Win application
            return DeviceInfo.Platform == DevicePlatform.WinUI
                ? $"http://localhost:{Port}/"
                :
                // 3. Real IP
                $"http://{LocalIpAddress}:{Port}/";
        }
    }
}