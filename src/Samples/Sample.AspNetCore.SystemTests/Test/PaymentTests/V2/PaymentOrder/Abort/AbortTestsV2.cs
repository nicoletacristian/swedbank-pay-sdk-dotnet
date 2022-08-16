using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V2.PaymentOrder.Abort
{
    public class AbortTestsV2 : Base.PaymentTestsV2
    {
        public AbortTestsV2(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { true, null })]
        public void Abort_PaymentOrder(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToPayexPaymentPage(products, Checkout.Anonymous)
                    .Abort.ClickAndGo()
                    .Message.StoreValueAsUri(out var message)
                    .Header.Products.ClickAndGo();

                var orderLink = message.OriginalString.Substring(message.OriginalString.IndexOf("/")).Replace(" has been Aborted", "");

                var order = await SwedbankPayClient.CheckoutV2.PaymentOrders.Get(new Uri(orderLink, UriKind.RelativeOrAbsolute), SwedbankPay.Sdk.PaymentOrders.V2.PaymentOrderExpand.All);

                // Operations
                Assert.That(order.Operations[LinkRelation.AbortedPaymentOrder], Is.Not.Null);

                // Transactions
                Assert.That(order.PaymentOrder.CurrentPayment.Payment, Is.Null);
            });
        }
    }
}