namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class AddressResponse : IAddressResponse
    {
        internal AddressResponse(AddressResponseDto dto)
        {
            Addressee = dto.Addressee;
            CoAddress = dto.CoAddress;
            StreetAddress = dto.StreetAddress;
            ZipCode = dto.ZipCode;
            City = dto.City;
            CountryCode = !string.IsNullOrWhiteSpace(dto.CountryCode) ? new CountryCode(dto.CountryCode) : null;
        }


        public string Addressee { get; }
        public string CoAddress { get; }
        public string StreetAddress { get; }
        public string ZipCode { get; }
        public string City { get; }
        public CountryCode CountryCode { get; }
    }
}