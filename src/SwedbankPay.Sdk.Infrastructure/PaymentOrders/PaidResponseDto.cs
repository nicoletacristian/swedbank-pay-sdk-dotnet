using System;
using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class PaidResponseDto
    {
        public Uri Id { get; set; }
        public string Instrument { get; set; }
        public long Number { get; set; }
        public string PayeeReference { get; set; }
        public string OrderReference { get; set; }
        public long Amount { get; set; }
        public IList<TokenListItemDto> Tokens { get; set; }
        public PaidDetailsDto Details { get; set; }

        internal PaidResponse Map()
        {
            var paidResponse = new PaidResponse(this);
            return paidResponse;
        }
    }
}
