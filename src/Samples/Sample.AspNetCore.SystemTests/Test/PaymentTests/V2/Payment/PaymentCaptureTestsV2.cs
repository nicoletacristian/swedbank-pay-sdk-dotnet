using System.Threading.Tasks;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.V2.Payment
{
    public class PaymentCaptureTestsV2 : Base.PaymentTestsV2
    {
        public PaymentCaptureTestsV2(string driverAlias)
            : base(driverAlias)
        {
        }


        [Test]
        [Retry(2)]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card })]
        public async Task Payment_Card_Capture(Product[] products, PayexInfo payexInfo)
        {
            GoToOrdersPage(products, payexInfo, Checkout.LocalPaymentMenu)
                .RefreshPageUntil(x => x.Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateCapture)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateCapture)].ExecuteAction.ClickAndGo()
                .RefreshPageUntil(x => x.Orders[y => y.Content.Value.Contains(_referenceLink)].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateReversal)].IsVisible, 60, 10)
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.CreateReversal)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.PaidPayment)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Actions.Rows[y => y.Name.Value.Contains(PaymentResourceOperations.ViewPayment)].Should.BeVisible()
                .Orders[y => y.Attributes["data-paymentlink"] == _referenceLink].Clear.ClickAndGo();

            var cardPayment = await SwedbankPayClient.Payments.CardPayments.Get(new Uri(_referenceLink, UriKind.RelativeOrAbsolute), PaymentExpand.All);

            // Operations
            Assert.That(cardPayment.Operations[LinkRelation.CreateCancellation], Is.Null);
            Assert.That(cardPayment.Operations[LinkRelation.CreateCapture], Is.Null);
            Assert.That(cardPayment.Operations[LinkRelation.PaidPayment], Is.Not.Null);
            Assert.That(cardPayment.Operations[LinkRelation.CreateReversal], Is.Not.Null);

            // Transactions
            Assert.That(cardPayment.Payment.Transactions.TransactionList.Count, Is.EqualTo(2));
            Assert.That(cardPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Authorization).State,
                        Is.EqualTo(State.Completed));
            Assert.That(cardPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Capture).State,
                        Is.EqualTo(State.Completed));
        }

    }
}

