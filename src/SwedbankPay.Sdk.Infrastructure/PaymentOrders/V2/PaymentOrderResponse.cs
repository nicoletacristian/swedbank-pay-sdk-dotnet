using System.Net.Http;

using SwedbankPay.Sdk.PaymentOrders.V3;

namespace SwedbankPay.Sdk.PaymentOrders.V2
{
    internal class PaymentOrderResponse : IPaymentOrderResponse
    {
        public PaymentOrderResponse(PaymentOrderResponseDto paymentOrder, HttpClient httpClient)
        {
            Operations = new PaymentOrderOperations(paymentOrder.Operations.Map(), httpClient);
            PaymentOrder = new PaymentOrder(paymentOrder.PaymentOrder);
        }

        public IPaymentOrderOperations Operations { get; }

        public IPaymentOrder PaymentOrder { get; }
    }
}