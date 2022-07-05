using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    /// <summary>
    /// The failed attempt response.
    /// </summary>
    public interface IFailedAttemptsResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }


        /// <summary>
        /// List of failed attempts.
        /// </summary>
        IList<IFailedAttemptItem> FailedAttemptList { get; }
    }
}