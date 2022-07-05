namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class AddressResponseDto
    {
        public string Addressee { get; set; }
        public string CoAddress { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }


        internal AddressResponse Map()
        {
            return new AddressResponse(this);
        }
    }
}