﻿using SwedbankPay.Sdk.Common;
using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentInstruments
{
    public class AuthorizationTransactionDto : List<TransactionDto>
    {
        internal IPaymentAuthorizationListResponse Map() => throw new NotImplementedException();
    }
}