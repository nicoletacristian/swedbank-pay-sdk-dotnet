using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Products;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = PaymentPageV3;

    public class PaymentPageV3 : BasePage<_>
    {
        [FindById("pxhv-paymentmenu")] public Frame<PaymentFramePage, _> PaymentMethodsFrameRedirect { get; set; }

        [FindById("swedbankpay-checkout")] public Frame<PaymentFramePage, _> PaymentMethodsFrameSeamless { get; set; }

        [FindByClass("cancel")]
        public Clickable<ProductsPage, _> AbortPayment { get; set; }
    }
}