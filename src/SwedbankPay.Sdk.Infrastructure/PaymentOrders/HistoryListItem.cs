using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class HistoryListItem : IHistoryListItem
    {
        internal HistoryListItem(HistoryListItemDto dto)
        {
            Created = dto.Created;
            Name = dto.Name;
            Instrument = dto.Instrument;
            Number = dto.Number;
            Prefill = dto.Prefill;
            InitiatedBy = InitiatedByType.FromValue(dto.InitiatedBy);
        }

        
        public DateTime Created { get; }
        public string Name { get; }
        public string Instrument { get; }
        public long Number { get; }
        public bool Prefill { get; }
        public InitiatedByType InitiatedBy { get; }
    }
}
