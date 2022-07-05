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

internal class PaidDetails
{
    internal PaidDetails(PaidDetailsDto dto)
    {
        NonPaymentToken = dto.NonPaymentToken;
        ExternalNonPaymentToken = dto.ExternalNonPaymentToken;
        CardBrand = dto.CardBrand;
        CardType = dto.CardType;
        ExpiryDate = dto.ExpiryDate;
        IssuerAuthorizationApprovalCode = dto.IssuerAuthorizationApprovalCode;
        AcquirerTransactionType = dto.AcquirerTransactionType;
        AcquirerStan = dto.AcquirerStan;
        AcquirerTerminalId = dto.AcquirerTerminalId;
        AcquirerTransactionTime = dto.AcquirerTransactionTime;
        TransactionInitiator = dto.TransactionInitiator;
    }


    public string NonPaymentToken { get; }
    public string ExternalNonPaymentToken { get; }
    public string CardBrand { get; }
    public string CardType { get; }
    public string ExpiryDate { get; }
    public string IssuerAuthorizationApprovalCode { get; }
    public string AcquirerTransactionType { get; }
    public string AcquirerStan { get; }
    public string AcquirerTerminalId { get; }
    public DateTime AcquirerTransactionTime { get; }
    public string TransactionInitiator { get; }

}