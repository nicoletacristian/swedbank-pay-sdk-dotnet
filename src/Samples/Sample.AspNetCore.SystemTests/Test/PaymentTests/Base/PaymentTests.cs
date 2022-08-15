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
                    page = PayWithPayexCard(products, cardInfo, checkout);
                    break;
                case PayexSwishInfo swishInfo:
                    page = PayWithPayexSwish(products, swishInfo, checkout);
                    break;
                case PayexInvoiceInfo invoiceInfo:
                    page = PayWithPayexInvoice(products, invoiceInfo, checkout);
                    break;
                case PayexTrustlyInfo trustlyInfo:
                    page = PayWithPayexTrustly(products, checkout);
                    break;
            }

            return page?
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

        protected abstract PayexCardFramePage GoToPayexCardPaymentFrame(Product[] products, Checkout checkout);

        protected abstract PayexInvoiceFramePage GoToPayexInvoicePaymentFrame(Product[] products, Checkout checkout);

        protected abstract PayexSwishFramePage GoToPayexSwishPaymentFrame(Product[] products, Checkout checkout);

        protected abstract PayexTrustlyFramePage GoToPayexTrustlyPaymentFrame(Product[] products, Checkout checkout);

        protected ThankYouPage PayWithPayexCard(Product[] products, PayexCardInfo info, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Standard => GoToPayexCardPaymentFrame(products, checkout)
                    .Do(x =>
                    {
                        if (x.PreFilledCards.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(3) }))
                        {
                            if (x.PreFilledCards.Items[y => y.CreditCardNumber.Value.Contains(info.CreditCardNumber.Substring(info.CreditCardNumber.Length - 4))].Exists())
                            {
                                x
                                .PreFilledCards
                                .Items[
                                    y => y.CreditCardNumber.Value.Contains(
                                        info.CreditCardNumber.Substring(info.CreditCardNumber.Length - 4))].Click()
                                .Cvc.SetWithSpeed(info.Cvc, interval: 0.1);
                            }
                            else
                            {
                                x
                                .AddNewCard.Click()
                                .CreditCardNumber.SetWithSpeed(TestDataService.CreditCardNumber, interval: 0.1)
                                .ExpiryDate.SetWithSpeed(TestDataService.CreditCardExpirationDate, interval: 0.1)
                                .Cvc.SetWithSpeed(TestDataService.CreditCardCvc, interval: 0.1);
                            }
                        }
                        else if (x.CreditCardNumber.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(3) }))
                        {
                            x
                            .CreditCardNumber.SetWithSpeed(info.CreditCardNumber, interval: 0.1)
                            .ExpiryDate.SetWithSpeed(info.ExpiryDate, interval: 0.1)
                            .Cvc.SetWithSpeed(info.Cvc, interval: 0.1);
                        }
                    })
                    .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                    .Pay.ClickAndGo(),
                _ => GoToPayexCardPaymentFrame(products, checkout)
                    .CreditCardNumber.IsVisible.WaitTo.BeTrue()
                    .CreditCardNumber.SetWithSpeed(info.CreditCardNumber, interval: 0.1)
                    .ExpiryDate.SetWithSpeed(info.ExpiryDate, interval: 0.1)
                    .Cvc.SetWithSpeed(info.Cvc, interval: 0.1)
                    .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                    .Pay.ClickAndGo(),
            };
        }

        protected ThankYouPage PayWithPayexInvoice(Product[] products, PayexInvoiceInfo info, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Standard => GoToPayexInvoicePaymentFrame(products, checkout)
                                       .PersonalNumber.IsVisible.WaitTo.BeTrue()
                                       .PersonalNumber.SetWithSpeed(info.PersonalNumber.Substring(info.PersonalNumber.Length - 4), interval: 0.15)
                                       .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                                       .Pay.ClickAndGo(),
                _ => GoToPayexInvoicePaymentFrame(products, checkout)
                    .PersonalNumber.IsVisible.WaitTo.BeTrue()
                    .PersonalNumber.SetWithSpeed(info.PersonalNumber, interval: 0.1)
                    .Email.SetWithSpeed(info.Email, interval: 0.1)
                    .PhoneNumber.SetWithSpeed(info.PhoneNumber, interval: 0.1)
                    .ZipCode.SetWithSpeed(info.ZipCode, interval: 0.1)
                    .Next.Click()
                    .WaitSeconds(5)
                    .Pay.IsVisible.WaitTo.WithinSeconds(20).BeTrue()
                    .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                    .Pay.ClickAndGo(),
            };
        }

        protected ThankYouPage PayWithPayexSwish(Product[] products, PayexSwishInfo info, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Standard => GoToPayexSwishPaymentFrame(products, checkout)
                                   .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                                   .Pay.ClickAndGo(),
                _ => GoToPayexSwishPaymentFrame(products, checkout)
                      .SwishNumber.IsVisible.WaitTo.BeTrue()
                      .SwishNumber.SetWithSpeed(info.SwishNumber, interval: 0.1)
                      .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                      .Pay.ClickAndGo(),
            };
        }

        protected ThankYouPage PayWithPayexTrustly(Product[] products, Checkout checkout)
        {

            return GoToPayexTrustlyPaymentFrame(products, checkout)
                .Submit.IsVisible.WaitTo.WithinSeconds(15).BeTrue()
                .Submit.ClickAndGo<TrustlyPaymentPage>()
                .Banks[0].IsVisible.WaitTo.WithinSeconds(15).BeTrue()
                .Banks[0].Click()
                .Next.Click()
                .PersonalNumber.Set(TestDataService.PersonalNumber)
                .SecurityCodeOption.Click()
                .Next.Click()
                .MessageCode.StoreValue(out string code)
                .Code.Set(code)
                .Next.Click()
                .AccountOptions.IsVisible.WaitTo.WithinSeconds(60).BeTrue()
                .Next.Click()
                .MessageCode.StoreValue(out code)
                .Code.Set(code)
                .Next.Click()
                .PageUrl.Should.WithinSeconds(60).Contain("Thankyou")
                .SwitchToRoot<ThankYouPage>()
                .ThankYou.IsVisible.WaitTo.BeTrue();
        }

        protected static IEnumerable TestData(bool singleProduct = true, string paymentMethod = PaymentMethods.Card)
        {
            var data = new List<object>();

            if (singleProduct)
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

            yield return data.ToArray();
        }
    }
}
