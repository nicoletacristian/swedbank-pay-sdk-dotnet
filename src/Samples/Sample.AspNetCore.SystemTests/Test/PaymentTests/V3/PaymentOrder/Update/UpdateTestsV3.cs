using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using SwedbankPay.Sdk.PaymentOrders.V2;

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
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card })]
        public void Update_PaymentOrder(Product[] products, PayexInfo payexInfo)
        {
            Assert.DoesNotThrowAsync(async () =>
            {

                GoToPayexSwishPaymentFrame(products.Skip(1).ToArray(), Checkout.Anonymous)
                .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Skip(1).First().UnitPrice / 100 * products.Skip(1).First().Quantity))} kr")
                .SwitchToRoot<HomePage>()
                .Header.Products.ClickAndGo();

                GoToOrdersPage(products, payexInfo, Checkout.Anonymous)
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.CreatePaymentOrderCancel)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.CreatePaymentOrderCancel)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.CreatePaymentOrderCapture)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentOrderResourceOperations.PaidPaymentOrder)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentorderlink"] == _referenceLink].Clear.ClickAndGo();

                await SwedbankPayClient.CheckoutV2.PaymentOrders.Get(_link, PaymentOrderExpand.All);
            });
        }
    }
}