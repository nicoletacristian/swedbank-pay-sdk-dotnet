using System.Collections.Generic;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    internal class PaymentOrderRequestDetailsDto
    {
        public PaymentOrderRequestDetailsDto(PaymentOrderRequestDetails paymentOrder)
        {
            Amount = paymentOrder.Amount.InLowestMonetaryUnit;
            Currency = paymentOrder.Currency.ToString();
            Description = paymentOrder.Description;
            Language = paymentOrder.Language.ToString();
            RequestDeliveryInfo = paymentOrder.RequestDeliveryInfo;
            RestrictedToDeliveryInfoInstruments = paymentOrder.RestrictedToDeliveryInfoInstruments;
            ProductName = paymentOrder.ProductName;
            Operation = paymentOrder.Operation.Value;            
            PayeeInfo = new PayeeInfoResponseDto(paymentOrder.PayeeInfo);
            Payer = new PayerDto(paymentOrder.Payer);
            RiskIndicator = new RiskIndicatorDto(paymentOrder.RiskIndicator);
            Urls = new UrlsDto(paymentOrder.Urls);
            UserAgent = paymentOrder.UserAgent;
            VatAmount = paymentOrder.VatAmount.InLowestMonetaryUnit;
            
            if(paymentOrder.Metadata != null)
            {
                Metadata = new MetadataDto(paymentOrder.Metadata);
            }

            if (paymentOrder.Items != null)
            {
                Items = new List<ItemDto>();
                foreach (var item in paymentOrder.Items)
                {
                    Items.Add(new ItemDto(item));
                }
            }

            if (paymentOrder.OrderItems != null)
            {
                OrderItems = new List<OrderItemDto>();
                foreach (var item in paymentOrder.OrderItems)
                {
                    OrderItems.Add(new OrderItemDto(item));
                }
            }
        }

        public long Amount { get; }

        public string Currency { get; }

        public string Description { get; }

        public List<ItemDto> Items { get; }

        public string Language { get; }

        public bool RequestDeliveryInfo { get; }

        public bool RestrictedToDeliveryInfoInstruments { get; }

        public string ProductName { get; }

        public string Operation { get; }

        public List<OrderItemDto> OrderItems { get; }

        public PayeeInfoResponseDto PayeeInfo { get; }

        public PayerDto Payer { get; }

        public RiskIndicatorDto RiskIndicator { get; }

        public UrlsDto Urls { get; }

        public string UserAgent { get; }

        public long VatAmount { get; }

        public MetadataDto Metadata { get; }
    }
}