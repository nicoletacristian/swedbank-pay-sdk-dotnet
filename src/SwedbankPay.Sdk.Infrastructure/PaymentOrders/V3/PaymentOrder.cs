using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    internal class PaymentOrder : IPaymentOrder
    {
        public PaymentOrder(PaymentOrderDto paymentOrder)
        {
            Id = new Uri(paymentOrder.Id, UriKind.RelativeOrAbsolute);
            Created = paymentOrder.Created;
            Updated = paymentOrder.Updated;
            Operation = paymentOrder.Operation;
            Status = paymentOrder.Status;
            Currency = new Currency(paymentOrder.Currency);
            Amount = paymentOrder.Amount;
            VatAmount = paymentOrder.VatAmount;
            Description = paymentOrder.Description;
            InitiatingSystemUserAgent = paymentOrder.InitiatingSystemUserAgent;
            Language = new Language(paymentOrder.Language);
            AvailableInstruments = paymentOrder.AvailableInstruments;
            Implementation = paymentOrder.Implementation;
            InstrumentMode = paymentOrder.InstrumentMode;
            GuestMode = paymentOrder.GuestMode;

            Payer = paymentOrder.Payer?.Map();
            Urls = paymentOrder.Urls.Map();
            PayeeInfo = paymentOrder.PayeeInfo.Map();
            OrderItems = paymentOrder.OrderItems?.Map();
            History = paymentOrder.History?.Map();
            Failed = paymentOrder.Failed.Map();
            Aborted = paymentOrder.Aborted;
            Paid = paymentOrder.Paid?.Map();
            Cancelled = paymentOrder.Cancelled.Map();
            FinancialTransactions = paymentOrder.FinancialTransactions?.Map();
            FailedAttempts = paymentOrder.FailedAttempts.Map();
            //Metadata = paymentOrder.Metadata?.Map();
        }

        public Uri Id { get; }
        public DateTime Created { get; }
        public DateTime Updated { get; }
        public string Operation { get; }
        public Status Status { get; }
        public Currency Currency { get; }
        public Amount Amount { get; }
        public Amount VatAmount { get; }
        public string Description { get; }
        public string InitiatingSystemUserAgent { get; }
        public Language Language { get; }
        public IList<string> AvailableInstruments { get; }
        public string Implementation { get; }
        public string Integration { get; }
        public bool InstrumentMode { get; }
        public bool GuestMode { get;}
        public OrderItemListResponse OrderItems { get; }
        public IUrls Urls { get; }
        public IPayeeInfo PayeeInfo { get; }
        public IPayer Payer { get; }
        public IHistoryResponse History { get; }
        public IFailedResponse Failed { get; }
        public IAbortedResponse Aborted { get; }
        public IPaidResponse Paid { get; }
        public ICancelledResponse Cancelled { get; }
        public IFinancialTransactionsResponse FinancialTransactions { get; }
        public IFailedAttemptsResponse FailedAttempts { get; }
        public Metadata Metadata { get; }
    }
}