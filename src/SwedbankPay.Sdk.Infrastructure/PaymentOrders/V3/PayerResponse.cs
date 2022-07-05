namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Resource to access the Payer information on a payment order.
    /// </summary>
    internal class PayerResponse : Identifiable, IPayer
    {
        /// <summary>
        /// Instantiates a <see cref="PayerResponseDto"/> with the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        public PayerResponse(PayerResponseDto dto) : base(dto.Id)
        {
            Reference = dto.Reference;
            Name = dto.Name;
            Email = !string.IsNullOrWhiteSpace(dto.Email) ? new EmailAddress(dto.Email) : null;
            Msisdn = !string.IsNullOrWhiteSpace(dto.Msisdn) ? new Msisdn(dto.Msisdn) : null;
            HashedFields = dto.HashedFields;
            ShippingAddress = dto.ShippingAddress?.Map();
            Device = dto.Device?.Map();
        }

        public string Reference { get; }
        public string Name { get; }
        public EmailAddress Email { get; }
        public Msisdn Msisdn { get; }
        public HashedPayerFields HashedFields { get;}
        public IAddressResponse ShippingAddress { get; }
        public IDeviceResponse Device { get; }
    }
}