namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Detailed information about a payer for a payment order.
    /// </summary>
    public class Payer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public Payer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


        /// <summary>
        /// 
        /// </summary>
        public bool DigitalProducts { get; set; }

        /// <summary>
        ///     The first name of the payer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name of the payer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Optional (increases chance for challenge flow if not set)
        /// </summary>

        public EmailAddress Email { get; set; }

        /// <summary>
        ///     Optional (increases chance for challenge flow if not set)
        /// </summary>
        public Msisdn Msisdn { get; set; }

        /// <summary>
        /// A reference used in the Enterprise and Payments Only implementations to recognize the payer when no SSN is stored.
        /// </summary>
        public string PayerReference { get; set; }

        /// <summary>
        /// Payers shipping address for this payment order.
        /// </summary>
        public Address ShippingAddress { get; set; }

        /// <summary>
        /// Payers billing address for this payment order.
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Account information about the payer if such is known by the merchant system.
        /// </summary>
        public AccountInfo AccountInfo { get; set; }
    }
}