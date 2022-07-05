namespace SwedbankPay.Sdk.PaymentOrders
{
    using System.Collections.Generic;

    internal class FailedAttemptsResponse : Identifiable, IFailedAttemptsResponse
    {
        internal FailedAttemptsResponse(FailedAttemptsResponseDto dto)
            : base(dto.Id)
        {
            var list = new List<IFailedAttemptItem>();
            foreach (var failedAttemptItemDto in dto.FailedAttemptList)
            {
                list.Add(failedAttemptItemDto.Map());
            }

            FailedAttemptList = list;
        }

        public IList<IFailedAttemptItem> FailedAttemptList { get; }
    }
}
