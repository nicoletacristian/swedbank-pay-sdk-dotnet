namespace SwedbankPay.Sdk
{
    public interface IDeviceResponse
    {
        int DetectionAccuracy { get; }
        string IpAddress { get; }
        string UserAgent { get; }
        string DeviceType { get; }
        string HardwareFamily { get; }
        string HardwareName { get; }
        string HardwareVendor { get; }
        string PlatformName { get; }
        string PlatformVendor { get; }
        string PlatformVersion { get; }
        string BrowserName { get; }
        string BrowserVendor { get; }
        string BrowserVersion { get; }
        bool BrowserJavaEnabled { get; }
    }
}
