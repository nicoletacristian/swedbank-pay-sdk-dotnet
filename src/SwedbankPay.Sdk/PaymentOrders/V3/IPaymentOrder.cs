using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Detailed information about a payment order.
    /// </summary>
    public interface IPaymentOrder : IIdentifiable
    {
        /// <summary>
        /// The <seealso cref="DateTime"/> the payment order was created.
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// The <seealso cref="DateTime"/> this payment order was last updated.
        /// </summary>
        public DateTime Updated { get; }

        /// <summary>
        /// The <see cref="Operation"/> used to initiate this payment order.
        /// </summary>
        public string Operation { get; }

        /// <summary>
        /// Indicates the payment order’s current status.
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// The selected currency for this payment order.
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// The amount (including VAT, if any) to charge the payer,
        /// entered in the lowest monetary unit of the selected currency.
        /// </summary>
        public Amount Amount { get; }

        /// <summary>
        /// The payment’s VAT (Value Added Tax) amount, entered in the lowest monetary unit of the selected currency.
        /// The vatAmount entered will not affect the amount shown on the payment page, which only shows the total amount.
        /// This field is used to specify how much of the total amount the VAT will be.
        /// Set to 0 (zero) if there is no VAT amount charged.
        /// </summary>
        public Amount VatAmount { get; }

        /// <summary>
        /// A 40 character length textual description of the purchase.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The user agent of the HTTP client making the request, reflecting the value sent in the User-Agent header with the initial POST request which created the Payment Order.
        /// </summary>
        public string InitiatingSystemUserAgent { get; }

        /// <summary>
        /// Payers preferred language.
        /// </summary>
        public Language Language { get; }

        /// <summary>
        /// A list of instruments available for this payment.
        /// </summary>
        public IList<string> AvailableInstruments { get; }

        /// <summary>
        /// The merchant’s Checkout v3 implementation type. Business, Enterprise, PaymentsOnly or Starter. We ask that you don’t build logic around this field’s response. It is mainly for information purposes
        /// </summary>
        public string Implementation { get; }

        /// <summary>
        /// The merchant’s Checkout v3 integration type. HostedView (Seamless View) or Redirect. We ask that you don’t build logic around this field’s response.
        /// </summary>
        public string Integration { get; }

        /// <summary>
        /// Set to true or false. Indicates if the payment is initialized with only one payment instrument available.
        /// </summary>
        public bool InstrumentMode { get; }

        /// <summary>
        /// Set to true or false. Indicates if the payer chose to pay as a guest or not. When using the Payments Only implementation, this is triggered by not including a payerReference in the original paymentOrder request.
        /// </summary>
        public bool GuestMode { get; }

        /// <summary>
        /// Resource that gives access to the <seealso cref="IOrderItem"/>s this payment order contains.
        /// </summary>
        public OrderItemListResponse OrderItems { get; }

        /// <summary>
        /// Resource where all URIs related to the payment order can be retrieved.
        /// </summary>
        public IUrls Urls { get; }

        /// <summary>
        /// Identifies the merchant that initiated the payment.
        /// </summary>
        public IPayeeInfo PayeeInfo { get; }

        /// <summary>
        /// Resource where information about the payee of the payment order can be retrieved.
        /// </summary>
        public IPayer Payer { get; }

        /// <summary>
        /// Information about the payment’s history
        /// </summary>
        public IHistoryResponse History { get; }

        /// <summary>
        /// Information about the failed transactions
        /// </summary>
        public IFailedResponse Failed { get; }

        /// <summary>
        /// Information about the aborted transactions
        /// </summary>
        public IAbortedResponse Aborted { get; }

        /// <summary>
        /// Information about the paid transactions
        /// </summary>
        public IPaidResponse Paid { get; }

        /// <summary>
        /// Information about the cancelled transactions
        /// </summary>
        public ICancelledResponse Cancelled { get; }

        /// <summary>
        /// Information about the financial transactions 
        /// </summary>
        public IFinancialTransactionsResponse FinancialTransactions { get; }

        /// <summary>
        /// Information about the failed attempts
        /// </summary>
        public IFailedAttemptsResponse FailedAttempts { get; }

        /// <summary>
        /// Metadata can be used to store data associated to a payment order that can be retrieved later by performing a GET on the payment order.
        /// Swedbank Pay does not use or process metadata,
        /// it is only stored on the payment order so it can be retrieved later alongside the payment order.
        /// An example where metadata might be useful is when several internal systems are involved in the payment process and the payment creation is done in one system and post-purchases take place in another.
        /// In order to transmit data between these two internal systems,
        /// the data can be stored in metadata on the payment order so the internal systems do not need to communicate with each other directly.
        /// </summary>
        public Metadata Metadata { get; }
    }
}