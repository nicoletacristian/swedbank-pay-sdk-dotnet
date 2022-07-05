namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;

    internal class HistoryListItemDto
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Instrument { get; set; }
        public long Number { get; set; }
        public bool Prefill { get; set; }
        public string InitiatedBy { get; set; }

        internal IHistoryListItem Map()
        {
            return new HistoryListItem(this);
        }
    }
}