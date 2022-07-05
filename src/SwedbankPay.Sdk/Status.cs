namespace SwedbankPay.Sdk
{
    /// <summary>
    ///     Initialized, Completed, Failed Ready, Pending or Aborted, or Paid.
    ///     Indicates the current state.
    /// </summary>
    public class Status : TypeSafeEnum<Status>
    {
        /// <summary>
        /// Status of payment is Initialized.
        /// </summary>
        public static readonly Status Initialized = new Status(nameof(Initialized), "Initialized");

        /// <summary>
        /// Status of payment is Paid.
        /// </summary>
        public static readonly Status Paid = new Status(nameof(Paid), "Paid");

        /// <summary>
        /// Status of payment is Failed.
        /// </summary>
        public static readonly Status Failed = new Status(nameof(Failed), "Failed");

        /// <summary>
        /// Status of payment is Cancelled.
        /// </summary>
        public static readonly Status Cancelled = new Status(nameof(Cancelled), "Cancelled");

        /// <summary>
        /// Status of payment is Aborted.
        /// </summary>
        public static readonly Status Aborted = new Status(nameof(Aborted), "Aborted");




        /// <summary>
        /// Initializes a <see cref="Status"/> with the provided parameters.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        /// <param name="value">The API value of the state.</param>
        public Status(string name, string value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Converts from a <c>string</c> to a <see cref="State"/>.
        /// </summary>
        /// <param name="originalState">he API value of the state.</param>
        public static implicit operator Status(string originalState)
        {
            return originalState switch
            {
                "Initialized" => Initialized,
                "Paid" => Paid,
                "Failed" => Failed,
                "Cancelled" => Cancelled,
                "Aborted" => Aborted,
                _ => new Status(originalState, originalState),
            };
        }
    }
}