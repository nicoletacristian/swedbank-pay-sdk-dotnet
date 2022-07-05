using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class FinancialTransactionItemDto
    {
        public Uri Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Type { get; set; }
        public long Number { get; set; }
        public long Amount { get; set; }
        public long VatAmount { get; set; }
        public string Description { get; set; }
        public string PayeeReference { get; set; }
        public string ReceiptReference { get; set; }
        public Identifiable OrderItems { get; set; }


        internal IFinancialTransactionItem Map()
        {
            return new FinancialTransactionItem(this);
        }
    }
}
