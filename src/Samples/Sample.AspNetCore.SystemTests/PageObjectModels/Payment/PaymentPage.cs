using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Products;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = PaymentPage;

    public class PaymentPage : BasePage<_>
    {
        [FindByAutomation("button", "button-abort")]
        public ButtonDelegate<ProductsPage, _> Abort { get; set; }

        [FindByName("paymentOrderId", Visibility = Visibility.Any)]
        public HiddenInput<_> PaymentOrder { get; private set; }


        public Frame<IdentificationFramePage, _> IdentificationFrame { get; set; }

        [FindById("paymentMenuFrame")] public Frame<PaymentFramePageV2, _> PaymentMethodsFrameV2 { get; set; }

        [FindById("pxhv-paymentmenu")] public Frame<PaymentFramePageV2, _> PaymentMethodsFrameV3 { get; set; }

        [FindByClass("cancel")]
        public Clickable<ProductsPage, _> AbortPayment { get; set; }
    }
}