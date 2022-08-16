using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Products;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = PaymentPageV2;

    public class PaymentPageV2 : BasePage<_>
    {
        [FindByAutomation("button", "button-abort")]
        public ButtonDelegate<ProductsPage, _> Abort { get; set; }

        public Frame<IdentificationFramePage, _> IdentificationFrame { get; set; }

        [FindById("paymentMenuFrame")] public Frame<PaymentFramePage, _> PaymentMethodsFrame { get; set; }
    }
}