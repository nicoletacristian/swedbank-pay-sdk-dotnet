using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class HistoryResponse : Identifiable, IHistoryResponse
    {
        internal HistoryResponse(HistoryResponseDto dto) : base(dto.Id)
        {
            var list = new List<IHistoryListItem>();
            foreach (var historyListItem in dto.HistoryList)
            {
                list.Add(historyListItem.Map());
            }

            HistoryList = list;
        }

        /// <summary>
        /// The array of history objects.
        /// </summary>
        public IEnumerable<IHistoryListItem> HistoryList { get; }
    }
}
 