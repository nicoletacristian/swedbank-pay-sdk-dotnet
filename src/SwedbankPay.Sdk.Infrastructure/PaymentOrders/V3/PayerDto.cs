using SwedbankPay.Sdk.PaymentInstruments.Card;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    internal class PayerDto
	{
		public PayerDto() { }

		public PayerDto(Payer payer)
		{
			if (payer == null)
			{
				return;
			}

            DigitalProducts = payer.DigitalProducts;
            FirstName = payer.FirstName;
            LastName = payer.LastName;
            Email = payer.Email?.ToString();
            Msisdn = payer.Msisdn?.ToString();
            PayerReference = payer.PayerReference;
			ShippingAddress = new AddressDto(payer.ShippingAddress);
			BillingAddress = new AddressDto(payer.BillingAddress);
            AccountInfo = new AccountInfoDto(payer.AccountInfo);
        }

        public bool DigitalProducts { get; set; }
		public string Id { get; set; }
		public AccountInfoDto AccountInfo { get; set; }
		public AddressDto BillingAddress { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Msisdn { get; set; }
        public string PayerReference { get; set; }
		public AddressDto ShippingAddress { get; set; }
    }
}