using SwedbankPay.Sdk.PaymentOrders.V3;
using System.Threading;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Seamless
{
    public class SeamlessPaymentOrderReversalTestsV3 : Base.PaymentTestsV3
    {
        public SeamlessPaymentOrderReversalTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card, Checkout.Seamless })]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Swish, Checkout.Seamless })]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Invoice, Checkout.Seamless })]
        public void Seamless_PaymentOrder_Reversal(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {

                GoToOrdersPage(products, payexInfo, checkout)
                    .Do(x =>
                    {
                        if (payexInfo.Instrument != PaymentInstrument.Swish)
                        {
                            x
                            .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].IsVisible, 60, 10)
                            .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].ExecuteAction.ClickAndGo();
                        }
                    })
                    .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].IsVisible, 60, 10)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].ExecuteAction.ClickAndGo()
                    .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].IsVisible, 60, 10)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                var counter = 0;
                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);

                while (order.PaymentOrder.History.HistoryList.Last().Name != "PaymentReversed" && counter <= 15)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);
                    }
                    catch { }
                    counter++;
                }

                // Operations
                Assert.That(order.Operations.Cancel, Is.Null);
                Assert.That(order.Operations.Capture, Is.Null);
                Assert.That(order.Operations.Reverse, Is.Null);
                Assert.That(order.Operations.View, Is.Not.Null);

                // Transactions
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Instrument, Is.EqualTo(payexInfo.Instrument.ToString()));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Name, Is.EqualTo("PaymentReversed"));
            });
        }
    }
}
