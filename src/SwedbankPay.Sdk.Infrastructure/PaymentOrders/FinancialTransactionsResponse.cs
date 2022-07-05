using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class FinancialTransactionsResponse : Identifiable, IFinancialTransactionsResponse
    {
        internal FinancialTransactionsResponse(FinancialTransactionsResponseDto dto) : base(dto.Id)
        {
            var list = new List<IFinancialTransactionItem>();
            foreach (var financialTransactionItem in dto.FinancialTransactionList)
            {
                list.Add(financialTransactionItem.Map());
            }

            FinancialTransactionList = list;
        }

        /// <summary>
        /// The financial transactions object.
        /// </summary>
        public IList<IFinancialTransactionItem> FinancialTransactionList { get; }
    }
}
