using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using SwedbankPay.Sdk.PaymentOrders.V3;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V3.PaymentOrder.Update
{
    public class UpdateTestsV3 : Base.PaymentTestsV3
    {
        public UpdateTestsV3(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card, Checkout.Seamless })]
        public void Update_PaymentOrder(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToPayexPaymentFrame<PayexSwishFramePage>(products.Skip(1).ToArray(), checkout)
                .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Skip(1).First().UnitPrice / 100 * products.Skip(1).First().Quantity))} kr")
                .SwitchToRoot<HomePage>()
                .Header.Products.ClickAndGo();

                GoToOrdersPage(products, payexInfo, checkout)
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Cancel)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Cancel)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.Capture)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.ViewCheckout)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                await SwedbankPayClient.CheckoutV3.PaymentOrders.Get(_link, PaymentOrderExpand.All);
            });
        }
    }
}