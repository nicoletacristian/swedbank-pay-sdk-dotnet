using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    internal class PaymentOrderDto
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Operation { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public long VatAmount { get; set; }
        public string Description { get; set; }
        public string InitiatingSystemUserAgent { get; set; }
        public string Language { get; set; }
        public IList<string> AvailableInstruments { get; set; }
        public string Implementation { get; set; }
        public string Integration { get; set; }
        public bool InstrumentMode { get; set; }
        public bool GuestMode { get; set; }
        public OrderItemListResponseDto OrderItems { get; set; }
        public UrlsDto Urls { get; set; }
        public PaymentOrderPayeeInfoDto PayeeInfo { get; set; }
        public PayerResponseDto Payer { get; set; }
        public HistoryResponseDto History { get; set; }
        public FailedResponseDto Failed { get; set; }
        public AbortedResponse Aborted { get; set; }
        public PaidResponseDto Paid { get; set; }
        public CancelledResponseDto Cancelled { get; set; }
        public FinancialTransactionsResponseDto FinancialTransactions { get; set; }
        public FailedAttemptsResponseDto FailedAttempts { get; set; }
        //public MetadataDto Metadata { get; set; }
    }
}