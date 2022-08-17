using System.Threading.Tasks;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V2.Payment
{
    public class PaymentSaleTestsV2 : Base.PaymentTestsV2
    {
        public PaymentSaleTestsV2(string driverAlias)
            : base(driverAlias)
        {
        }

        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Swish, Checkout.LocalPaymentMenu })]
        public async Task Payment_Swish_Sale(Product[] products, PayexInfo payexInfo, Checkout checkout)
        {
            GoToOrdersPage(products, payexInfo, checkout)
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateReversal)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateReversal)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.PaidPayment)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.ViewPayment)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Clear.ClickAndGo();

            var swishPayment = await SwedbankPayClient.Payments.SwishPayments.Get(new Uri(_referenceLink, UriKind.RelativeOrAbsolute), PaymentExpand.All);

            // Global Order
            Assert.That(swishPayment.Payment.Amount.InLowestMonetaryUnit, Is.EqualTo(products.Select(x => x.UnitPrice * x.Quantity).Sum()));
            Assert.That(swishPayment.Payment.Currency.ToString(), Is.EqualTo("SEK"));
            Assert.That(swishPayment.Payment.State, Is.EqualTo(State.Ready));

            // Operations
            Assert.That(swishPayment.Operations[LinkRelation.CreateCancellation], Is.Null);
            Assert.That(swishPayment.Operations[LinkRelation.CreateCapture], Is.Null);
            Assert.That(swishPayment.Operations[LinkRelation.CreateReversal], Is.Not.Null);
            Assert.That(swishPayment.Operations[LinkRelation.ViewPayment], Is.Not.Null);

            // Transactions
            Assert.That(swishPayment.Payment.Transactions.TransactionList.Count, Is.EqualTo(1));
            Assert.That(swishPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Sale).State,
                        Is.EqualTo(State.Completed));
        }
    }
}
