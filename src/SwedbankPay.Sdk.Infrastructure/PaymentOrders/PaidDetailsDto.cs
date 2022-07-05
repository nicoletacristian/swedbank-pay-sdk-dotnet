#region License

// --------------------------------------------------
// Copyright © Swedbank Pay. All Rights Reserved.
// 
// This software is proprietary information of Swedbank Pay.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;

namespace SwedbankPay.Sdk.PaymentOrders;

internal class PaidDetailsDto
{
    public string NonPaymentToken { get; set; }
    public string ExternalNonPaymentToken { get; set; }
    public string CardBrand { get; set; }
    public string CardType { get; set; }
    public string ExpiryDate { get; set; }
    public string IssuerAuthorizationApprovalCode { get; set; }
    public string AcquirerTransactionType { get; set; }
    public string AcquirerStan { get; set; }
    public string AcquirerTerminalId { get; set; }
    public DateTime AcquirerTransactionTime { get; set; }
    public string TransactionInitiator { get; set; }


    internal PaidDetails Map()
    {
        return new PaidDetails(this);
    }
}