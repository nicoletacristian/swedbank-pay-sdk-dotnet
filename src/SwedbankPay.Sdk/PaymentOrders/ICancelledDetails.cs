#region License

// --------------------------------------------------
// Copyright © Swedbank Pay. All Rights Reserved.
// 
// This software is proprietary information of Swedbank Pay.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

namespace SwedbankPay.Sdk.PaymentOrders;

/// <summary>
/// Details connected to the payment.
/// </summary>
public interface ICancelledDetails
{
    /// <summary>
    /// The result of our own card tokenization. Activated in POS for the merchant or merchant group.
    /// </summary>
    string NonPaymentToken { get; }

    /// <summary>
    /// The result of an external tokenization. This value will vary depending on card types, acquirers, customers, etc. For Mass Transit merchants, transactions redeemed by Visa will be populated with PAR. For Mastercard and Amex, it will be our own token.
    /// </summary>
    string ExternalNonPaymentToken { get; }
}