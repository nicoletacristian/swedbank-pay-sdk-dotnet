namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    ///  Token connected to the payment.
    /// </summary>
    public interface ITokenItem
    {
        /// <summary>
        /// The different types of available tokens.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The token guid.
        /// </summary>
        TokenType Token { get; }

        /// <summary>
        /// The name of the token.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The expiry date of the token.
        /// </summary>
        string ExpiryDate { get; }
    }
}
