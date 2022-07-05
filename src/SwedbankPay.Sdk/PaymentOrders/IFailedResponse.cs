namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    /// <summary>
    /// The failed object.
    /// </summary>
    public interface IFailedResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// The problem object.
        /// </summary>
        IProblem Problem { get; }
    }
}
