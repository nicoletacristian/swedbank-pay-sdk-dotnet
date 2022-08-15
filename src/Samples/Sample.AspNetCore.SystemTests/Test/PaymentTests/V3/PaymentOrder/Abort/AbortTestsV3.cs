using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Abort
{
    public class AbortTestsV3 : Base.PaymentTestsV3
    {
        public AbortTestsV3(string driverAlias)
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
                var frame = GoToPayexPaymentPage(products, Checkout.Redirect)
                    .PaymentMethodsFrameV3.SwitchTo<PaymentFramePageV2>();

                var orderLink = frame.PageSource.GetPaymentOrderFromBody();

                frame
                    .SwitchToRoot<PaymentPage>()
                    .AbortPayment.ClickAndGo()
                    .Header.Products.ClickAndGo();

                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(new Uri(orderLink, UriKind.RelativeOrAbsolute), SwedbankPay.Sdk.PaymentOrders.V3.PaymentOrderExpand.All);

                // Status
                Assert.That(order.PaymentOrder.Status, Is.EqualTo(Status.Aborted));
            });
        }
    }
}