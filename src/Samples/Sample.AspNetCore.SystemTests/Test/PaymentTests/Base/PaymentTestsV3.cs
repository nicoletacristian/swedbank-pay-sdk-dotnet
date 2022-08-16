using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Base
{
    public abstract class PaymentTestsV3 : PaymentTests
    {
        public PaymentTestsV3(string driverAlias)
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

            return GoToPaymentFramePage(products, checkout)
                    .PaymentMethods[x => x.Name == paymentMethod].IsVisible.WaitTo.BeTrue()
                    .PaymentMethods[x => x.Name == paymentMethod].Click()
                    .PaymentMethods[x => x.Name == paymentMethod].PaymentFrame.SwitchTo<T>();
        }

        protected PaymentFramePage GoToPaymentFramePage(Product[] products, Checkout checkout)
        {
            PaymentFramePage frame = checkout switch
            {
                Checkout.Seamless => GoToPayexPaymentPage(products, checkout)
                            .PaymentMethodsFrameSeamless.IsVisible.WaitTo.BeTrue()
                            .PaymentMethodsFrameSeamless.SwitchTo(),
                _ => GoToPayexPaymentPage(products, checkout)
                        .PaymentMethodsFrameRedirect.IsVisible.WaitTo.BeTrue()
                        .PaymentMethodsFrameRedirect.SwitchTo()
            };

            _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

            return frame;
        }

        protected static PaymentPageV3 GoToPayexPaymentPage(Product[] products, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Seamless => SelectProducts(products).SeamlessCheckout.ClickAndGo(),
                _ => SelectProducts(products).RedirectCheckout.ClickAndGo(),
            };
        }
    }
}
