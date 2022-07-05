namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class TokenListItemDto
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string ExpiryDate { get; set; }


        internal ITokenItem Map()
        {
            return new TokenListItem(this);
        }
    }
}
