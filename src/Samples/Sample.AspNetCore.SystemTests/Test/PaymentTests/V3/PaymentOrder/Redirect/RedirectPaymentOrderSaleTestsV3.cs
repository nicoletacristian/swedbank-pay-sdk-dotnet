using SwedbankPay.Sdk.PaymentOrders.V3;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Redirect
{
    public class RedirectPaymentOrderSaleTestsV3 : Base.PaymentTestsV3
    {
        public RedirectPaymentOrderSaleTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Swish, Checkout.Redirect })]
        public void Redirect_PaymentOrder_Swish_Sale(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, payexInfo, checkout)
                    .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].IsVisible, 60, 10)
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Reversal)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                    .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                var order = await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);

                // Global Order
                Assert.That(order.PaymentOrder.Amount.InLowestMonetaryUnit, Is.EqualTo(products.Select(x => x.UnitPrice * x.Quantity).Sum()));
                Assert.That(order.PaymentOrder.Currency.ToString(), Is.EqualTo("SEK"));
                Assert.That(order.PaymentOrder.Status, Is.EqualTo(Status.Paid));

                // Operations
                Assert.That(order.Operations.Cancel, Is.Null);
                Assert.That(order.Operations.Capture, Is.Null);
                Assert.That(order.Operations.Reverse, Is.Not.Null);
                Assert.That(order.Operations.View, Is.Not.Null);

                // Transactions
                Assert.That(order.PaymentOrder.History.HistoryList.Count, Is.EqualTo(6));
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
