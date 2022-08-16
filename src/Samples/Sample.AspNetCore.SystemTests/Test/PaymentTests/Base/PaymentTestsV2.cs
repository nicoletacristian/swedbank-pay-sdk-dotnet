using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Base
{
    public abstract class PaymentTestsV2 : PaymentTests
    {
        public PaymentTestsV2(string driverAlias)
            : base(driverAlias)
        {
        }

        protected override T GoToPayexPaymentFrame<T>(Product[] products, Checkout checkout)
        {
            var paymentMethod = typeof(T).Name switch
            {
                nameof(PayexCardFramePage) => PaymentMethods.Card,
                nameof(PayexSwishFramePage) => PaymentMethods.Swish,
                nameof(PayexTrustlyFramePage) => PaymentMethods.Trustly,
                _ => PaymentMethods.Invoice,
            };

            return checkout switch
            {
                Checkout.LocalPaymentMenu => GoToLocalPaymentPage(products)
                        .Do(x => 
                        {
                            return paymentMethod switch
                            {
                                PaymentMethods.Card => x.CreditCard.IsVisible.WaitTo.BeTrue().CreditCard.Click(),
                                PaymentMethods.Swish => x.Swish.IsVisible.WaitTo.BeTrue().Swish.Click(),
                                PaymentMethods.Trustly => x.Trustly.IsVisible.WaitTo.BeTrue().Trustly.Click(),
                                _ => x.Invoice.IsVisible.WaitTo.BeTrue().Invoice.Click(),
                            };
                        })
                        .PaymentFrame.IsVisible.WaitTo.BeTrue()
                        .PaymentFrame.SwitchTo<T>()
                        .PageSource.WaitTo.WithinSeconds(15).Contain("/psp/")
                        .Do(x => 
                            _referenceLink = x.PageSource.GetPaymentOrderFromBody()
                        ),
                _ => GoToPaymentFramePage(products, checkout)
                    .PaymentMethods[x => x.Name == paymentMethod].IsVisible.WaitTo.BeTrue()
                    .PaymentMethods[x => x.Name == paymentMethod].Click()
                    .PaymentMethods[x => x.Name == paymentMethod].PaymentFrame.SwitchTo<T>()
                    .Do(x => 
                    {
                        if (x is PayexCardFramePage cardFrame && cardFrame.CardTypeSelector.Exists())
                        {
                            cardFrame.CardTypeSelector.Check();
                        }
                    })
            };
        }

        protected PaymentFramePage GoToPaymentFramePage(Product[] products, Checkout checkout)
        {
            PaymentFramePage frame = checkout switch
            {
                Checkout.Standard => GoToPayexPaymentPage(products, checkout)
                                        .PerformIdentification()
                                        .SwitchToRoot<PaymentPageV2>().WaitSeconds(20)
                                        .PaymentMethodsFrame.SwitchTo(),
                _ => GoToPayexPaymentPage(products, checkout)
                                        .PaymentMethodsFrame.IsVisible.WaitTo.BeTrue()
                                        .PaymentMethodsFrame.SwitchTo(),
            };

            _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

            return frame;
        }

        protected static LocalPaymentMenuPage GoToLocalPaymentPage(Product[] products)
        {
            return SelectProducts(products)
                    .LocalPaymentMenu.ClickAndGo();
                    
        }

        protected static PaymentPageV2 GoToPayexPaymentPage(Product[] products, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Standard => SelectProducts(products).StandardCheckout.ClickAndGo(),
                _ => SelectProducts(products).AnonymousCheckout.ClickAndGo()
            };
        }
    }
}
