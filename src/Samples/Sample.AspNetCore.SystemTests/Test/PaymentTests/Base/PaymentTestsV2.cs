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

        protected override PayexCardFramePage GoToPayexCardPaymentFrame(Product[] products, Checkout checkout)
        {
            PayexCardFramePage frame = null;

            switch (checkout)
            {
                case Checkout.LocalPaymentMenu:
                    frame = GoToLocalPaymentPage(products)
                        .CreditCard.IsVisible.WaitTo.BeTrue()
                        .CreditCard.Click()
                        .PaymentFrame.IsVisible.WaitTo.BeTrue()
                        .PaymentFrame.SwitchTo<PayexCardFramePage>()
                        .PageSource.WaitTo.WithinSeconds(15).Contain("/psp/");

                    _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

                    break;

                default:

                    frame = GoToPaymentFramePage(products, checkout)
                        .PaymentMethods[x => x.Name == PaymentMethods.Card].IsVisible.WaitTo.BeTrue()
                        .PaymentMethods[x => x.Name == PaymentMethods.Card].Click()
                        .PaymentMethods[x => x.Name == PaymentMethods.Card].PaymentFrame.SwitchTo<PayexCardFramePage>();

                    if (frame.CardTypeSelector.Exists())
                    {
                        frame.CardTypeSelector.Check();
                    }

                    break;
            }

            return frame;
        }

        protected override PayexInvoiceFramePage GoToPayexInvoicePaymentFrame(Product[] products, Checkout checkout)
        {
            PayexInvoiceFramePage frame = null;

            switch (checkout)
            {
                case Checkout.LocalPaymentMenu:
                    frame = GoToLocalPaymentPage(products)
                        .Invoice.IsVisible.WaitTo.BeTrue()
                        .Invoice.Click()
                        .PaymentFrame.IsVisible.WaitTo.BeTrue()
                        .PaymentFrame.SwitchTo<PayexInvoiceFramePage>()
                        .PageSource.WaitTo.WithinSeconds(15).Contain("/psp/");

                    _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

                    break;
                default:
                    frame = GoToPaymentFramePage(products, checkout)
                        .PaymentMethods[x => x.Name == PaymentMethods.Invoice].IsVisible.WaitTo.BeTrue()
                        .PaymentMethods[x => x.Name == PaymentMethods.Invoice].Click()
                        .PaymentMethods[x => x.Name == PaymentMethods.Invoice].PaymentFrame.SwitchTo<PayexInvoiceFramePage>();
                    break;
            }

            return frame;
        }

        protected override PayexSwishFramePage GoToPayexSwishPaymentFrame(Product[] products, Checkout checkout)
        {
            PayexSwishFramePage frame = null;

            switch (checkout)
            {
                case Checkout.LocalPaymentMenu:
                    frame = GoToLocalPaymentPage(products)
                                .Swish.IsVisible.WaitTo.BeTrue()
                                .Swish.Click()
                                .PaymentFrame.IsVisible.WaitTo.BeTrue()
                                .PaymentFrame.SwitchTo<PayexSwishFramePage>()
                                .PageSource.WaitTo.WithinSeconds(15).Contain("/psp/");

                    _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

                    break;
                default:
                    frame = GoToPaymentFramePage(products, checkout)
                        .PaymentMethods[x => x.Name == PaymentMethods.Swish].IsVisible.WaitTo.BeTrue()
                        .PaymentMethods[x => x.Name == PaymentMethods.Swish].Click()
                        .PaymentMethods[x => x.Name == PaymentMethods.Swish].PaymentFrame.SwitchTo<PayexSwishFramePage>();
                    break;
            }

            return frame;
        }

        protected override PayexTrustlyFramePage GoToPayexTrustlyPaymentFrame(Product[] products, Checkout checkout)
        {
            PayexTrustlyFramePage frame = null;

            switch (checkout)
            {
                case Checkout.LocalPaymentMenu:
                    frame = GoToLocalPaymentPage(products)
                        .Trustly.IsVisible.WaitTo.BeTrue()
                        .Trustly.Click()
                        .PaymentFrame.IsVisible.WaitTo.BeTrue()
                        .PaymentFrame.SwitchTo<PayexTrustlyFramePage>()
                        .PageSource.WaitTo.WithinSeconds(15).Contain("/psp/");

                    _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

                    break;
                default:
                    frame = GoToPaymentFramePage(products, checkout)
                        .PaymentMethods[x => x.Name == PaymentMethods.Trustly].IsVisible.WaitTo.BeTrue()
                        .PaymentMethods[x => x.Name == PaymentMethods.Trustly].Click()
                        .PaymentMethods[x => x.Name == PaymentMethods.Trustly].PaymentFrame.SwitchTo<PayexTrustlyFramePage>();
                    break;
            }

            return frame;
        }

        protected PaymentFramePageV2 GoToPaymentFramePage(Product[] products, Checkout checkout)
        {
            PaymentFramePageV2 frame = null;

            frame = checkout switch
            {
                Checkout.Standard => GoToPayexPaymentPageV2(products, checkout)
                                        .IdentificationFrame.SwitchTo()
                                        .Email.IsVisible.WaitTo.BeTrue()
                                        .Email.SetWithSpeed(TestDataService.Email, interval: 0.1)
                                        .PhoneNumber.SetWithSpeed(TestDataService.SwedishPhoneNumber, interval: 0.1)
                                        .Next.Click()
                                        .WaitSeconds(1)
                                        .Do(x =>
                                        {
                                            if (x.SaveMyInformation.IsVisible)
                                            {
                                                x.SaveMyInformation.Click();
                                                x.PersonalNumber.SetWithSpeed(TestDataService.PersonalNumber, interval: 0.1);
                                                x.Next.Click();
                                                x.FirstName.SetWithSpeed(TestDataService.FirstName, interval: 0.1);
                                                x.LastName.SetWithSpeed(TestDataService.LastName, interval: 0.1);
                                                x.Address.SetWithSpeed(TestDataService.Street, interval: 0.1);
                                                x.ZipCode.SetWithSpeed(TestDataService.ZipCode, interval: 0.1);
                                                x.City.SetWithSpeed(TestDataService.City, interval: 0.1);
                                                x.Next.Click();
                                            }
                                        })
                                        .SwitchToRoot<PaymentPage>().WaitSeconds(20)
                                        .PaymentMethodsFrameV2.SwitchTo(),
                _ => GoToPayexPaymentPageV2(products, checkout)
                                        .PaymentMethodsFrameV2.IsVisible.WaitTo.BeTrue()
                                        .PaymentMethodsFrameV2.SwitchTo(),
            };
            _referenceLink = frame.PageSource.GetPaymentOrderFromBody();

            return frame;
        }

        protected static LocalPaymentMenuPage GoToLocalPaymentPage(Product[] products)
        {
            return SelectProducts(products)
                    .LocalPaymentMenu.ClickAndGo();
                    
        }

        protected static PaymentPage GoToPayexPaymentPageV2(Product[] products, Checkout checkout)
        {
            return checkout switch
            {
                Checkout.Standard => SelectProducts(products).StandardCheckout.ClickAndGo(),
                _ => SelectProducts(products).AnonymousCheckout.ClickAndGo()
            };
        }
    }
}
