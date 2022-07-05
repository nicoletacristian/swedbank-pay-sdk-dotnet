using System;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Use to have sub-resources of a payment order be automatically expanded/filled
    /// when using a GET request for a payment order.
    /// </summary>
    [Flags]
    public enum PaymentOrderExpand
    {
        /// <summary>
        /// Don't expand any fields.
        /// </summary>
        None = 0,

        /// <summary>
        /// Expand the Urls sub-reasource.
        /// </summary>
        Urls = 1,

        /// <summary>
        /// Expand the PayeeInfo sub-reasource.
        /// </summary>
        PayeeInfo = 2,

        /// <summary>
        /// Expand the Payer sub-reasource.
        /// </summary>
        Payer = 4,

        /// <summary>
        /// Expand the History sub-reasource.
        /// </summary>
        History = 8,

        /// <summary>
        /// Expand the Failed sub-reasource.
        /// </summary>
        Failed = 16,

        /// <summary>
        /// Expand the Aborted sub-reasource.
        /// </summary>
        Aborted = 32,

        /// <summary>
        /// Expand the Paid sub-reasource.
        /// </summary>
        Paid = 64,

        /// <summary>
        /// Expand the Cancelled sub-reasource.
        /// </summary>
        Cancelled = 128,

        /// <summary>
        /// Expand the FinancialTransactions sub-reasource.
        /// </summary>
        FinancialTransactions = 256,


        /// <summary>
        /// Expand the FailedAttempts sub-reasource.
        /// </summary>
        FailedAttempts = 512,

        /// <summary>
        /// Expand the Metadata sub-reasource.
        /// </summary>
        Metadata = 1024,

        /// <summary>
        /// Expand the OrderItems sub-reasource.
        /// </summary>
        OrderItems = 2048,

        /// <summary>
        /// Expand All sub-reasources.
        /// </summary>
        All = 4095
    }
}