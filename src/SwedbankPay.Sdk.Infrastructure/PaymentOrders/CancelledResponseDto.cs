using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class CancelledResponseDto
    {
        public Uri Id { get; set; }
        public string CancelReason { get; set; }
        public string Instrument { get; set; }
        public long Number { get; set; }
        public string PayeeReference { get; set; }
        public string OrderReference { get; set; }
        public long Amount { get; set; }
        public IList<TokenListItemDto> Tokens { get; set; } = new List<TokenListItemDto>();
        public CancelledDetailsDto Details { get; set; }

        internal CancelledResponse Map()
        {
            return new CancelledResponse(this);
        }
    }

    internal class CancelledDetailsDto
    {
        public string NonPaymentToken { get; set; }
        public string ExternalNonPaymentToken { get; set; }


        internal CancelledDetails Map()
        {
            return new CancelledDetails(NonPaymentToken, ExternalNonPaymentToken);
        }
    }
}
