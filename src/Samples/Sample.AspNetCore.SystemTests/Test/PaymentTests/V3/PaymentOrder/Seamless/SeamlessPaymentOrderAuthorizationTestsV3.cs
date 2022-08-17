using SwedbankPay.Sdk.PaymentOrders.V3;
using System.Threading.Tasks;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Seamless
{
    public class SeamlessPaymentOrderAuthorizationTestsV3 : Base.PaymentTestsV3
    {
        public SeamlessPaymentOrderAuthorizationTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card, Checkout.Seamless })]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Invoice, Checkout.Seamless })]
        public void Seamless_PaymentOrder_Authorization(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, payexInfo, checkout)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Cancel)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.RedirectCheckout)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);

                // Global Order
                Assert.That(order.PaymentOrder.Amount.InLowestMonetaryUnit, Is.EqualTo(products.Select(x => x.UnitPrice * x.Quantity).Sum()));
                Assert.That(order.PaymentOrder.Currency.ToString(), Is.EqualTo("SEK"));
                Assert.That(order.PaymentOrder.Status, Is.EqualTo(Status.Paid));

                // Operations
                Assert.That(order.Operations.Cancel, Is.Not.Null);
                Assert.That(order.Operations.Capture, Is.Not.Null);
                Assert.That(order.Operations.Redirect, Is.Not.Null);
                Assert.That(order.Operations.View, Is.Not.Null);

                Assert.That(order.Operations.Reverse, Is.Null);
                Assert.That(order.Operations.Update, Is.Null);
                Assert.That(order.Operations.Abort, Is.Null);

                // Transactions
                Assert.That(order.PaymentOrder.History.HistoryList.Count, Is.EqualTo(5));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Instrument, Is.EqualTo(payexInfo.Instrument.ToString()));
                Assert.That(order.PaymentOrder.History.HistoryList.Last().Name, Is.EqualTo("PaymentPaid"));

                // Order Items

                Assert.That(order.PaymentOrder.OrderItems.OrderItemList.Count, Is.EqualTo(products.Length));
                for (var i = 0; i < products.Length; i++)
                {
                    Assert.That(order.PaymentOrder.OrderItems.OrderItemList.ElementAt(i).Name, Is.EqualTo(products[i].Name));
                    Assert.That(order.PaymentOrder.OrderItems.OrderItemList.ElementAt(i).UnitPrice.InLowestMonetaryUnit, Is.EqualTo(products[i].UnitPrice));
                    Assert.That(order.PaymentOrder.OrderItems.OrderItemList.ElementAt(i).Quantity, Is.EqualTo(products[i].Quantity));
                    Assert.That(order.PaymentOrder.OrderItems.OrderItemList.ElementAt(i).Amount.InLowestMonetaryUnit, Is.EqualTo(products[i].UnitPrice * products[i].Quantity));
                }
            });
        }

    }
}
