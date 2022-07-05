namespace SwedbankPay.Sdk.PaymentOrders
{
    using System;
    using System.Collections.Generic;

    internal class FailedAttemptsResponseDto
    {
        public Uri Id { get; set; }
        public IList<FailedAttemptItemDto> FailedAttemptList { get; set; } = new List<FailedAttemptItemDto>();


        internal IFailedAttemptsResponse Map()
        {
            return new FailedAttemptsResponse(this);
        }
    }
}