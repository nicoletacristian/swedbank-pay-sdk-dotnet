#region License

// --------------------------------------------------
// Copyright © Swedbank Pay. All Rights Reserved.
// 
// This software is proprietary information of Swedbank Pay.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders;

/// <summary>
/// Information about the cancelled transactions
/// </summary>
public interface ICancelledResponse
{
    /// <summary>
    /// The relative URL and unique identifier
    /// </summary>
    Uri Id { get; }

    /// <summary>
    /// Why the payment was cancelled.
    /// </summary>
    string CancelReason { get; }

    /// <summary>
    /// Payment instrument used in the cancelled payment.
    /// </summary>
    string Instrument { get; }

    /// <summary>
    /// The transaction number, useful when there’s need to reference the transaction in human communication. Not usable for programmatic identification of the transaction, where id should be used instead.
    /// </summary>
    long Number { get; }

    /// <summary>
    /// A unique reference from the merchant system. It is set per operation to ensure an exactly-once delivery of a transactional operation. See payeeReference for details.
    /// </summary>
    string PayeeReference { get; }

    /// <summary>
    /// The order reference should reflect the order reference found in the merchant’s systems.
    /// </summary>
    string OrderReference { get; }

    /// <summary>
    /// The transaction amount (including VAT, if any).
    /// </summary>
    Amount Amount { get; }

    /// <summary>
    /// A list of tokens connected to the payment.
    /// </summary>
    IList<ITokenItem> Tokens { get; }

    /// <summary>
    /// Details connected to the payment.
    /// </summary>
    CancelledDetails Details { get; }
}