namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    internal class FailedResponse : Identifiable, IFailedResponse
    {
        public FailedResponse(Uri id)
            : base(id)
        {
        }

        /// <summary>
        /// The problem object.
        /// </summary>
        public IProblem Problem { get; set; }
    }
}
