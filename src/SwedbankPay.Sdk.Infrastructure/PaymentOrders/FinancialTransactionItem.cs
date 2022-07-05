using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class FinancialTransactionItem : Identifiable, IFinancialTransactionItem
    {
        internal FinancialTransactionItem(FinancialTransactionItemDto dto) : base(dto.Id)
        {
            Created = dto.Created;
            Updated = dto.Updated;
            Type = dto.Type;
            Number = dto.Number;
            Amount = dto.Amount;
            VatAmount = dto.VatAmount;
            Description = dto.Description;
            PayeeReference = dto.PayeeReference;
            ReceiptReference = dto.ReceiptReference;
            OrderItems = dto.OrderItems;
        }


        public DateTime Created { get; }
        public DateTime Updated { get; }
        public string Type { get; }
        public long Number { get; }
        public Amount Amount { get; }
        public Amount VatAmount { get; }
        public string Description { get; }
        public string PayeeReference { get; }
        public string ReceiptReference { get; }
        public Identifiable OrderItems { get; }
    }
}
