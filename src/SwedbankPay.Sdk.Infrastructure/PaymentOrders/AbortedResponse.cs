namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    internal class AbortedResponse : Identifiable, IAbortedResponse
    {
        public AbortedResponse(Uri id, string abortReason) : base(id)
        {
            AbortReason = abortReason;
        }

        /// <summary>
        /// Why the payment was aborted.
        /// </summary>
        public string AbortReason { get; }
    }
}
