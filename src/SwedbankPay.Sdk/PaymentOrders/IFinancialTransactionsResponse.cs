using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// Information about the financial transactions
    /// </summary>
    public interface IFinancialTransactionsResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// The financial transactions object.
        /// </summary>
        IList<IFinancialTransactionItem> FinancialTransactionList { get; }
    }
}
