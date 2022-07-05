namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    /// <summary>
    /// The failed attempt object.
    /// </summary>
    public interface IFailedAttemptItem
    {
        /// <summary>
        /// The ISO-8601 date of when the payment order was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Payment instrument used in the failed payment.
        /// </summary>
        string Instrument { get; }

        /// <summary>
        /// The transaction number, useful when there’s need to reference the transaction in human communication
        /// </summary>
        long Number { get; }

        /// <summary>
        /// The status of the payment attempt. Failed or Aborted.
        /// </summary>
        Status Status { get; }

        /// <summary>
        /// The problem object.
        /// </summary>
        IProblem Problem { get; }
    }
}
