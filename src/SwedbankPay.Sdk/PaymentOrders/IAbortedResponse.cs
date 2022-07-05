using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// Information about the aborted transactions
    /// </summary>
    public interface IAbortedResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// Why the payment was aborted.
        /// </summary>
        string AbortReason { get; }
    }
}
