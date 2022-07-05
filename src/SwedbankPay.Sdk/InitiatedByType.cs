namespace SwedbankPay.Sdk
{
    /// <summary>
    /// The different types of initiated by
    /// </summary>
    public sealed class InitiatedByType : TypeSafeEnum<InitiatedByType>
    {
        /// <summary>
        /// Initiated by consumer
        /// </summary>
        public static readonly InitiatedByType Consumer = new InitiatedByType(nameof(Consumer), "Consumer");

        /// <summary>
        /// Initiated by merchant
        /// </summary>
        public static readonly InitiatedByType Merchant = new InitiatedByType(nameof(Merchant), "Merchant");

        /// <summary>
        /// Initiated by system
        /// </summary>
        public static readonly InitiatedByType System = new InitiatedByType(nameof(System), "System");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public InitiatedByType(string name, string value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="InitiatedByType"/>.
        /// </summary>
        /// <param name="type">The API value to convert.</param>
        public static implicit operator InitiatedByType(string type)
        {
            return type switch
            {
                "Consumer" => Consumer,
                "Merchant" => Merchant,
                "System" => System,
                _ => new InitiatedByType(type, type),
            };
        }
    }
}
