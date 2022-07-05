using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// The financial transactions object.
    /// </summary>
    public interface IFinancialTransactionItem
    {
        /// <summary>
        /// The id of the financial transaction.
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// The ISO-8601 date of when the payment order was created.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// The ISO-8601 date of when the payment order was updated.
        /// </summary>
        DateTime Updated { get; }

        /// <summary>
        /// The type of transaction. Capture, Authorization, Cancellation, Reversal, Sale.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The transaction number, useful when there’s need to reference the transaction in human communication. Not usable for programmatic identification of the transaction, where id should be used instead.
        /// </summary>
        long Number { get; }

        /// <summary>
        /// The transaction amount (including VAT, if any)
        /// </summary>
        Amount Amount { get; }

        /// <summary>
        /// The payment’s VAT (Value Added Tax)
        /// </summary>
        Amount VatAmount { get; }

        /// <summary>
        /// The description of the payment order.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// A unique reference from the merchant system. It is set per operation to ensure an exactly-once delivery of a transactional operation
        /// </summary>
        string PayeeReference { get; }

        /// <summary>
        /// A unique reference from the merchant system. It is used to supplement payeeReference as an additional receipt number.
        /// </summary>
        string ReceiptReference { get; }

        /// <summary>
        /// The array of items being purchased with the order. Note that authorization orderItems will not be printed on invoices, so lines meant for print must be added in the Capture request.
        /// </summary>
        Identifiable OrderItems { get; }
    }
}
