namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class SwishItemDto
    {
        public SwishItemDto(Swish swish)
        {
            if(swish == null)
            {
                return;
            }

            EnableEcomOnly = swish.EnableEcomOnly;
            PaymentRestrictedToAgeLimit = swish.PaymentRestrictedToAgeLimit;
            PaymentRestrictedToSocialSecurityNumber = swish.PaymentRestrictedToSocialSecurityNumber;
        }

        public bool EnableEcomOnly { get; set; }
        public int PaymentRestrictedToAgeLimit { get; set; }
        
        public string PaymentRestrictedToSocialSecurityNumber { get; set; }
    }
}