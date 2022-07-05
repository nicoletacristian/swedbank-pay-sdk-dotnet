using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// Information about the paid transactions
    /// </summary>
    public interface IPaidResponse
    {
        /// <summary>
        /// The relative URL and unique identifier
        /// </summary>
        Uri Id { get; }

        /// <summary>
        /// Payment instrument used in the payment.
        /// </summary>
        string Instrument { get; }

        /// <summary>
        /// The transaction number , useful when there’s need to reference the transaction in human communication.
        /// </summary>
        long Number { get; }

        /// <summary>
        /// 	A unique reference from the merchant system. It is set per operation to ensure an exactly-once delivery of a transactional operation
        /// </summary>
        string PayeeReference { get; }

        /// <summary>
        /// The order reference should reflect the order reference found in the merchant’s systems.
        /// </summary>
        string OrderReference { get; }

        /// <summary>
        /// The transaction amount (including VAT, if any)
        /// </summary>
        Amount Amount { get; }

        /// <summary>
        /// A list of tokens connected to the payment.
        /// </summary>
        IList<ITokenItem> Tokens { get; }
    }
}
