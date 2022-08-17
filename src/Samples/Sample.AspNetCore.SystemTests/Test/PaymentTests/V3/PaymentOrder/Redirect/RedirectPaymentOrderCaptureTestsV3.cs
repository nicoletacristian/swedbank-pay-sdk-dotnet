using SwedbankPay.Sdk.PaymentOrders.V3;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Redirect
{
    public class RedirectPaymentOrderCaptureTestsV3 : Base.PaymentTestsV3
    {
        public RedirectPaymentOrderCaptureTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card, Checkout.Redirect })]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Invoice, Checkout.Redirect })]
        public void Anonymous_PaymentOrder_Capture(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, payexInfo, checkout)
                    .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].IsVisible, 60, 10)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].ExecuteAction.ClickAndGo()
                    .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].IsVisible, 60, 10)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);

                // Operations
                Assert.That(order.Operations.Cancel, Is.Null);
                Assert.That(order.Operations.Capture, Is.Null);
                Assert.That(order.Operations.View, Is.Not.Null);
                Assert.That(order.Operations.Reverse, Is.Not.Null);

                // Transactions
                Assert.That(order.PaymentOrder.History.HistoryList.Count(), Is.EqualTo(6));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Instrument, Is.EqualTo(payexInfo.Instrument.ToString()));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Name, Is.EqualTo("PaymentCaptured"));
            });
        }
    }
}
