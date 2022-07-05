namespace SwedbankPay.Sdk
{
    public interface IPayer
    {
        string Reference { get; }
        string Name { get; }
        EmailAddress Email { get; }
        Msisdn Msisdn { get; }
        IAddressResponse ShippingAddress { get; }
        IDeviceResponse Device { get; }
    }
}
