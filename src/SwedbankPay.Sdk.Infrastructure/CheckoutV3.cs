using SwedbankPay.Sdk.PaymentOrders;
using System;
using System.Net;
using System.Net.Http;

using SwedbankPay.Sdk.PaymentOrders.V3;

namespace SwedbankPay.Sdk
{
    public class CheckoutV3 : ICheckoutV3
    {
        public CheckoutV3(HttpClient httpClient, IPaymentOrdersResource paymentOrders)
        {
            if (!ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12))
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpClient.BaseAddress == null)
            {
                throw new ArgumentNullException(nameof(httpClient), $"{nameof(httpClient.BaseAddress)} cannot be null.");
            }

            if (httpClient.DefaultRequestHeaders?.Authorization?.Parameter == null)
            {
                throw new ArgumentException($"Please configure the {nameof(httpClient)} with an Authorization header.");
            }

            if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent.Default);
            }

            PaymentOrders = paymentOrders ?? throw new ArgumentNullException(nameof(paymentOrders));
        }

        public IPaymentOrdersResource PaymentOrders { get; }
    }
}
