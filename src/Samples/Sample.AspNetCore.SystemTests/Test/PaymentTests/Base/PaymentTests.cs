using Microsoft.Extensions.Configuration;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.PageObjectModels.Products;
using Sample.AspNetCore.SystemTests.PageObjectModels.ThankYou;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

[assembly: LevelOfParallelism(1)]
namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Base
{
    [Parallelizable(scope: ParallelScope.All)]
    public abstract class PaymentTests : TestBase
    {
        protected string _referenceLink;
        protected Uri _link;

        public PaymentTests(string driverAlias)
            : base(driverAlias)
        {
        }

        protected SwedbankPayClient SwedbankPayClient { get; private set; }


        [OneTimeSetUp]
        public void Setup()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.local.json", true)
                .AddEnvironmentVariables()
                .Build();

            if(SwedbankPayClient == null)
            {
                var baseAddress = configRoot.GetSection("SwedbankPay:ApiBaseUrl").Value;
                var authHeader = configRoot.GetSection("SwedbankPay:Token").Value;
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeader);

                SwedbankPayClient = new SwedbankPayClient(httpClient);
            }
        }

        protected OrdersPage GoToOrdersPage(Product[] products, PayexInfo payexInfo, Checkout checkout = Checkout.Anonymous)
        {
            ThankYouPage page = null;

            switch (payexInfo)
            {
                case PayexCardInfo cardInfo:
                    page = GoToPayexPaymentFrame<PayexCardFramePage>(products, checkout)
                        .PayWithPayexCard(cardInfo, products, checkout);
                    break;
                case PayexSwishInfo swishInfo:
                    page = GoToPayexPaymentFrame<PayexSwishFramePage>(products, checkout)
                        .PayWithPayexSwish(swishInfo, products, checkout);
                    break;
                case PayexInvoiceInfo invoiceInfo:
                    page = GoToPayexPaymentFrame<PayexInvoiceFramePage>(products, checkout)
                           .PayWithPayexInvoice(invoiceInfo, products, checkout); 
                    break;
                case PayexTrustlyInfo trustlyInfo:
                    page = GoToPayexPaymentFrame<PayexTrustlyFramePage>(products, checkout)
                           .PayWithPayexTrustly();
                    break;
            }

            return page ?
                .ThankYou.IsVisible.WaitTo.WithinSeconds(120).BeTrue()
                .Header.Orders.ClickAndGo()
                .Do(x =>
                {
                    _link = new Uri(_referenceLink, UriKind.RelativeOrAbsolute);
                });
        }

        protected static ProductsPage SelectProducts(Product[] products)
        {
            return Go.To<ProductsPage>()
                .Do((x) =>
                {
                    if (x.Header.ClearOrders.Exists(new SearchOptions { Timeout = new TimeSpan(0, 0, 0, 0, 500), IsSafely = true }))
                    {
                        x
                        .Header.ClearOrders.Click()
                        .Header.Products.Click();
                    }

                    foreach (var product in products)
                    {
                        x
                        .Products.Rows[y => y.Name == product.Name].AddToCart.Click()
                        .Products.Rows[y => y.Name == product.Name].Price.StorePrice(out var price);

                        product.UnitPrice = price;

                        if (product.Quantity != 1)
                        {
                            x
                            .CartProducts.Rows[y => y.Name == product.Name].Quantity.Set(product.Quantity)
                            .CartProducts.Rows[y => y.Name == product.Name].Update.Click();
                        }
                    }
                });
        }

        protected abstract T GoToPayexPaymentFrame<T>(Product[] products, Checkout checkout) where T : Page<T>;

        protected static IEnumerable TestData(bool? singleProduct, string paymentMethod, Checkout checkout)
        {
            var data = new List<object>();

            if (!singleProduct.HasValue || singleProduct.Value)
            {
                data.Add(new[]
                {
                    new Product { Name = Products.Product1, Quantity = 1 }
                });
            }
            else
            {
                data.Add(new[]
                {
                    new Product { Name = Products.Product1, Quantity = 3 },
                    new Product { Name = Products.Product2, Quantity = 2 }
                });
            }

            switch (paymentMethod)
            {
                case PaymentMethods.Card:
                    data.Add(new PayexCardInfo(TestDataService.CreditCardNumber, TestDataService.CreditCardExpirationDate,
                                               TestDataService.CreditCardCvc));
                    break;

                case PaymentMethods.Swish:
                    data.Add(new PayexSwishInfo(TestDataService.SwishPhoneNumber));
                    break;

                case PaymentMethods.Invoice:
                    data.Add(new PayexInvoiceInfo(TestDataService.PersonalNumberShort, TestDataService.Email, TestDataService.PhoneNumber,
                                                  TestDataService.ZipCode));
                    break;

                case PaymentMethods.Trustly:
                    data.Add(new PayexTrustlyInfo());
                    break;
            }

            data.Add(checkout);

            yield return data.ToArray();
        }
    }
}
