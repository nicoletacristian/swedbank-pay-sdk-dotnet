using System;
using System.Net.Http;
using System.Threading.Tasks;

using SwedbankPay.Sdk.Extensions;
using SwedbankPay.Sdk.PaymentInstruments;

namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    public class PaymentOrderOperations : OperationsBase, IPaymentOrderOperations
    {
        public PaymentOrderOperations(IOperationList operations, HttpClient client)
        {
            foreach (var httpOperation in operations)
            {
                switch (httpOperation.Rel.Value)
                {
                    case PaymentOrderResourceOperations.Capture:
                        Capture = async payload =>
                        {
                            var requestDto = new PaymentOrderCaptureRequestDto(payload);
                            var dto = await client.SendAsJsonAsync<CaptureResponseDto>(httpOperation.Method, httpOperation.Href, requestDto);
                            return new CaptureResponse(dto);
                        };
                        break;
                    case PaymentOrderResourceOperations.Cancel:
                        Cancel = async payload =>
                        {
                            var requestDto = new PaymentOrderCancelRequestDto(payload);
                            var dto = await client.SendAsJsonAsync<CancelResponseDto>(httpOperation.Method, httpOperation.Href, requestDto);
                            return new CancellationResponse(dto.Payment, dto.Cancellation.Map());
                        };
                        break;
                    case PaymentOrderResourceOperations.Reversal:
                        Reverse = async payload =>
                        {
                            var url = httpOperation.Href.GetUrlWithQueryString(PaymentOrderExpand.All);
                            var requestDto = new PaymentOrderReversalRequestDto(payload);
                            var paymentOrderResponseContainer = await client.SendAsJsonAsync<ReversalResponseDto>(httpOperation.Method, url, requestDto);
                            return new ReversalResponse(paymentOrderResponseContainer.Payment, paymentOrderResponseContainer.Reversal.Map());
                        };
                        break;
                    case PaymentOrderResourceOperations.UpdateOrder:
                        Update = async payload =>
                        {
                            var url = httpOperation.Href.GetUrlWithQueryString(PaymentOrderExpand.All);
                            var requestDto = new PaymentOrderUpdateRequestDto(payload);
                            var paymentOrderResponseContainer = await client.SendAsJsonAsync<PaymentOrderResponseDto>(httpOperation.Method, url, requestDto);
                            return new PaymentOrderResponse(paymentOrderResponseContainer, client);
                        };
                        break;
                    case PaymentOrderResourceOperations.Abort:
                        Abort = async payload =>
                        {
                            var url = httpOperation.Href.GetUrlWithQueryString(PaymentOrderExpand.All);
                            var paymentOrderResponseContainer = await client.SendAsJsonAsync<PaymentOrderResponseDto>(httpOperation.Method, url, payload);
                            return new PaymentOrderResponse(paymentOrderResponseContainer, client);
                        };
                        break;
                    case PaymentOrderResourceOperations.ViewCheckout:
                        View = httpOperation;
                        break;
                    case PaymentOrderResourceOperations.RedirectCheckout:
                        Redirect = httpOperation;
                        break;
                }
                Add(httpOperation.Rel, httpOperation);
            }
        }

        public Func<PaymentOrderAbortRequest, Task<IPaymentOrderResponse>> Abort { get; }
        public Func<PaymentOrderCancelRequest, Task<CancellationResponse>> Cancel { get; }
        public Func<PaymentOrderCaptureRequest, Task<ICaptureResponse>> Capture { get; }
        public Func<PaymentOrderReversalRequest, Task<IReversalResponse>> Reverse { get; }
        public Func<PaymentOrderUpdateRequest, Task<IPaymentOrderResponse>> Update { get; }
        public HttpOperation View { get; }
        public HttpOperation Redirect { get; }
    }
}
