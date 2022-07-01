namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// API detail for setting Swish specific details.
    /// </summary>
    public class Swish
    {
        /// <summary>
        ///  Set enable Swish on ecommerce transactions.
        /// </summary>
        public bool EnableEcomOnly { get; set; }

        /// <summary>
        /// Positive number that sets the required age needed to fulfill the payment. To use this feature it has to be configured in the contract.
        /// </summary>
        public int PaymentRestrictedToAgeLimit { get; set; }

        /// <summary>
        /// When provided, the payment will be restricted to a specific social security number to make sure its the same logged in customer who is also the payer. Format: yyyyMMddxxxx. To use this feature it has to be configured in the contract.
        /// </summary>
        public string PaymentRestrictedToSocialSecurityNumber { get; set; }
    }
}