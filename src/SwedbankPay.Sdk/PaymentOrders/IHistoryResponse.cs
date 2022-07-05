using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// The history object.
    /// </summary>
    public interface IHistoryResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// The array of history objects.
        /// </summary>
        IEnumerable<IHistoryListItem> HistoryList { get; }
    }
}
