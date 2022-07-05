using System.Collections.Generic;
using System.Linq;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class PaidResponse : Identifiable, IPaidResponse
    {
        public PaidResponse(PaidResponseDto dto)
            : base(dto.Id)
        {
            Instrument = dto.Instrument;
            Number = dto.Number;
            PayeeReference = dto.PayeeReference;
            OrderReference = dto.OrderReference;
            Amount = dto.Amount;
            var tokens = new List<ITokenItem>();
            if (dto.Tokens != null && dto.Tokens.Any())
            {
                tokens.AddRange(dto.Tokens.Select(tokenListItemDto => tokenListItemDto.Map()));
            }

            Tokens = tokens;
            Details = dto.Details?.Map();
        }

        public string Instrument { get; }
        public long Number { get; }
        public string PayeeReference { get; }
        public string OrderReference { get; }
        public Amount Amount { get; }
        public IList<ITokenItem> Tokens { get; }
        public PaidDetails Details { get; }
    }
}
