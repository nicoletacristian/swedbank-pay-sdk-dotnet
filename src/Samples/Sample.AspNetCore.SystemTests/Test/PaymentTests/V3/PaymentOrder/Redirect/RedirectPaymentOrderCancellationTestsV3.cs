using SwedbankPay.Sdk.PaymentOrders.V3;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Redirect
{
    public class RedirectPaymentOrderCancellationTestsV3 : Base.PaymentTestsV3
    {
        public RedirectPaymentOrderCancellationTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }

        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card, Checkout.Redirect })]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Invoice, Checkout.Redirect })]
        public void Anonymous_PaymentOrder_Cancellation(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, payexInfo, checkout)
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Cancel)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Cancel)].ExecuteAction.ClickAndGo()
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.RedirectCheckout)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);

                // Operations
                Assert.That(order.Operations.Cancel, Is.Null);
                Assert.That(order.Operations.Capture, Is.Null);
                Assert.That(order.Operations.Reverse, Is.Null);
                Assert.That(order.Operations.View, Is.Not.Null);

                // Transactions
                Assert.That(order.PaymentOrder.History.HistoryList.Count(), Is.EqualTo(6));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Instrument, Is.EqualTo(payexInfo.Instrument.ToString()));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Name, Is.EqualTo("PaymentCancelled"));
            });
        }
    }
}
