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

internal class FailedAttemptItem : IFailedAttemptItem
{
    public FailedAttemptItem(FailedAttemptItemDto dto)
    {
        Created = dto.Created;
        Instrument = dto.Instrument;
        Number = dto.Number;
        Status = Status.FromValue(dto.Status);
        Problem = new Problem(dto.Problem);
    }


    public DateTime Created { get; }
    public string Instrument { get; }
    public long Number { get; }
    public Status Status { get; }
    public IProblem Problem { get; }
}