using System.Collections.Generic;
using System.Linq;

namespace SwedbankPay.Sdk.PaymentOrders
{
    internal class CancelledResponse : Identifiable, ICancelledResponse
    {
        public CancelledResponse(CancelledResponseDto dto)
            : base(dto.Id)
        {
            CancelReason = dto.CancelReason;
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

        /// <summary>
        /// Why the payment was cancelled.
        /// </summary>
        public string CancelReason { get; }

        /// <summary>
        /// Payment instrument used in the cancelled payment.
        /// </summary>
        public string Instrument { get; }

        /// <summary>
        /// The transaction number, useful when there’s need to reference the transaction in human communication. Not usable for programmatic identification of the transaction, where id should be used instead.
        /// </summary>
        public long Number { get; }

        /// <summary>
        /// A unique reference from the merchant system. It is set per operation to ensure an exactly-once delivery of a transactional operation. See payeeReference for details.
        /// </summary>
        public string PayeeReference { get; }

        /// <summary>
        /// The order reference should reflect the order reference found in the merchant’s systems.
        /// </summary>
        public string OrderReference { get; }

        /// <summary>
        /// The transaction amount (including VAT, if any).
        /// </summary>
        public Amount Amount { get; }

        /// <summary>
        /// A list of tokens connected to the payment.
        /// </summary>
        public IList<ITokenItem> Tokens { get; }

        /// <summary>
        /// Details connected to the payment.
        /// </summary>
        public CancelledDetails Details { get; }
    }
}
