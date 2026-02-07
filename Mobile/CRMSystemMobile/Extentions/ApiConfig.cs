namespace CRMSystemMobile.Extentions;

public static class ApiConfig
{
    public const string LocalIpAddress = "192.168.43.34";

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
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                return $"http://localhost:{Port}/";
            }

            // 3. Real IP
            return $"http://{LocalIpAddress}:{Port}/";
        }
    }
}
