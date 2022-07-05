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

internal class FailedAttemptItemDto
{
    public DateTime Created { get; set; }
    public string Instrument { get; set; }
    public long Number { get; set; }
    public string Status { get; set; }
    public ProblemDto Problem { get; set; }


    internal FailedAttemptItem Map()
    {
        return new FailedAttemptItem(this);
    }
}