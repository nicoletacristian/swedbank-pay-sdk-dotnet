namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class HashedPayerFields
    {
        public HashedPayerFields(string emailHash, string msisdnHash)
        {
            EmailHash = emailHash;
            MsisdnHash = msisdnHash;
        }


        public string EmailHash { get; }
        public string MsisdnHash { get; }
    }
}
