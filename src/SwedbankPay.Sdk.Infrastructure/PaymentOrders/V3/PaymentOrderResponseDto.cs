using System.Net.Http;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    internal class PaymentOrderResponseDto
    {
        public OperationListDto Operations { get; set; }

        public PaymentOrderDto PaymentOrder { get; set; }

        public IPaymentOrderResponse Map(HttpClient httpClient)
        {
            return new PaymentOrderResponse(this, httpClient);
        }
    }
}