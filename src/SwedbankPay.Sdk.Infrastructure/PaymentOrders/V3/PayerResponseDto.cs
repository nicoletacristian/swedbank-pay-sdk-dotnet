namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Resource to access the Payer information on a payment order.
    /// </summary>
    internal class PayerResponseDto
    {
        public string Id { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Msisdn { get; set; }
        public HashedPayerFields HashedFields { get; set; }
        public AddressResponseDto ShippingAddress { get; set; }
        public DeviceResponseDto Device { get; set; }

        internal PayerResponse Map()
        {
            return new PayerResponse(this);
        }
    }
}
