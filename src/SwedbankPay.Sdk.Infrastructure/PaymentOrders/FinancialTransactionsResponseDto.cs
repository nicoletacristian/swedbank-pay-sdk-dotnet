namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;
    using System.Collections.Generic;

    internal class FinancialTransactionsResponseDto
    {
        public Uri Id { get; set; }
        public List<FinancialTransactionItemDto> FinancialTransactionList { get; set; } = new List<FinancialTransactionItemDto>();


        internal IFinancialTransactionsResponse Map()
        {
            return new FinancialTransactionsResponse(this);
        }
    }
}
