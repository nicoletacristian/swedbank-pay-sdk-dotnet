using System;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class FailedResponseDto
    {
        public Uri Id { get; set; }

        public ProblemDto Problem { get; set; }

        internal FailedResponse Map()
        {
            return new FailedResponse(Id)
            {
                Problem = Problem?.Map()
            };
        }
    }
}
