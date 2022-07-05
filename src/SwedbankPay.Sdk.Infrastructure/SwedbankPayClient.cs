using SwedbankPay.Sdk.Consumers;
using SwedbankPay.Sdk.PaymentInstruments;
using SwedbankPay.Sdk.PaymentOrders;
using System;
using System.Net;
using System.Net.Http;

using SwedbankPay.Sdk.PaymentOrders.V2;
using SwedbankPay.Sdk.PaymentOrders.V3;

using PaymentOrdersResource = SwedbankPay.Sdk.PaymentOrders.V3.PaymentOrdersResource;

namespace SwedbankPay.Sdk
{
    public class SwedbankPayClient : ISwedbankPayClient
    {
        public SwedbankPayClient(HttpClient httpClient, CheckoutV2 checkoutV2, CheckoutV3 checkoutV3, IPaymentInstrumentsResource payments)
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

            Payments = payments ?? throw new ArgumentNullException(nameof(payments));
            CheckoutV2 = checkoutV2 ?? throw new ArgumentNullException(nameof(checkoutV2));
            CheckoutV3 = checkoutV3 ?? throw new ArgumentNullException(nameof(checkoutV3));
        }

        public SwedbankPayClient(HttpClient httpClient) :
            this(
                httpClient,
                new CheckoutV2(httpClient, new PaymentOrders.V2.PaymentOrdersResource(httpClient), new ConsumersResource(httpClient)),
                new CheckoutV3(httpClient, new PaymentOrdersResource(httpClient)),
                new PaymentsResource(httpClient))
        { }


        public IPaymentInstrumentsResource Payments { get; }
        public ICheckoutV2 CheckoutV2 { get; }
        public ICheckoutV3 CheckoutV3 { get; }
    }
}