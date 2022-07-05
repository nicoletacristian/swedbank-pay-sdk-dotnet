namespace SwedbankPay.Sdk
{
    /// <summary>
    /// The different types of available tokens.
    /// </summary>
    public sealed class TokenType : TypeSafeEnum<TokenType>
    {
        /// <summary>
        /// Token of payment type
        /// </summary>
        public static readonly TokenType Payment = new TokenType(nameof(Payment), "payment");

        /// <summary>
        /// Token of recurrence type
        /// </summary>
        public static readonly TokenType Recurrence = new TokenType(nameof(Recurrence), "recurrence");

        /// <summary>
        /// Token of transactionOnFile type
        /// </summary>
        public static readonly TokenType TransactionOnFile = new TokenType(nameof(TransactionOnFile), "transactionOnFile");

        /// <summary>
        /// Token of unscheduled type
        /// </summary>
        public static readonly TokenType Unscheduled = new TokenType(nameof(Unscheduled), "unscheduled");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public TokenType(string name, string value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="InitiatedByType"/>.
        /// </summary>
        /// <param name="type">The API value to convert.</param>
        public static implicit operator TokenType(string type)
        {
            return type switch
            {
                "payment" => Payment,
                "recurrence" => Recurrence,
                "transactionOnFile" => TransactionOnFile,
                "unscheduled" => Unscheduled,
                _ => new TokenType(type, type),
            };
        }
    }
}
