namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;
    using System.Collections.Generic;

    internal class HistoryResponseDto
    {
        public Uri Id { get; set; }
        public List<HistoryListItemDto> HistoryList { get; set; } = new List<HistoryListItemDto>();

        internal IHistoryResponse Map()
        {
            return new HistoryResponse(this);
        }
    }
}
