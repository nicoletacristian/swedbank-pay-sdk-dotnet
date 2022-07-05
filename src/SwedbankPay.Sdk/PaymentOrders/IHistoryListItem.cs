namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    /// <summary>
    /// The history object.
    /// </summary>
    public interface IHistoryListItem
    {
        /// <summary>
        /// The ISO-8601 date of when the history event was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Name of the history event. See list below for information.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The payment instrument used when the event occurred.
        /// </summary>
        public string Instrument { get; }

        /// <summary>
        /// Payment number associated with the event.
        /// </summary>
        public long Number { get; }

        /// <summary>
        /// Indicates if payment info was prefilled or not.
        /// </summary>
        public bool Prefill { get; }

        /// <summary>
        /// Consumer, Merchant or System. The party that initiated the event.
        /// </summary>
        InitiatedByType InitiatedBy { get;}
    }
}
