namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class TokenListItem : ITokenItem
    {
        public TokenListItem(TokenListItemDto dto)
        {
            Type = dto.Type;
            Token = TokenType.FromValue(dto.Token);
            Name = dto.Name;
            ExpiryDate = dto.ExpiryDate;
        }


        public string Type { get; }
        public TokenType Token { get; }
        public string Name { get; }
        public string ExpiryDate { get; }
    }
}
