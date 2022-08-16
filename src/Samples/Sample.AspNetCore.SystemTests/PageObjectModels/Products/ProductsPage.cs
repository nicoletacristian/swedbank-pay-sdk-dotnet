using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Products
{
    using _ = ProductsPage;

    public class ProductsPage : BasePage<_>
    {
        [FindByClass("alert alert-success")] public Text<_> Message { get; set; }

        
        /*
         * Product Table
         */

        public class ProductBasketItem : TableRow<_>
        {
            [FindByXPath("td[2]")] public Text<_> Name { get; set; }

            [FindByXPath("td[3]")] public Text<_> Price { get; set; }

            [FindByName("Quantity")] public NumberInput<_> Quantity { get; set; }

            [WaitSeconds(0.5, TriggerEvents.AfterClick)]
            [FindByXPath("button[1]")]
            public Button<_> Update { get; set; }
        }

        public class ProductItem : TableRow<_>
        {
            [FindByAutomation("a", "button-addtocart")]
            public Link<_> AddToCart { get; set; }

            [FindByXPath("td[1]")] public Text<_> Name { get; set; }

            [FindByXPath("a[1]")] public Link<_> Open { get; set; }

            [FindByXPath("td[5]")] public Text<_> Price { get; set; }
        }

        [FindByXPath("table[1]")] public Table<ProductItem, _> Products { get; set; }
        
        [FindByXPath("table[2]")] public Table<ProductBasketItem, _> CartProducts { get; set; }

        [FindByXPath("table[2]//tfoot[1]//td[2]")]
        public Text<_> TotalAmount { get; set; }

        /*
         * Checkout buttons
         */

        [FindByAutomation("a", "button-checkout-v2", Index = 0)]
        public LinkDelegate<PaymentPageV2, _> StandardCheckout { get; set; }

        [FindByAutomation("a", "button-checkout-v2", Index = 1)]
        public LinkDelegate<PaymentPageV2, _> AnonymousCheckout { get; set; }

        [FindByAutomation("a", "button-checkout-v2", Index = 2)]
        public LinkDelegate<LocalPaymentMenuPage, _> LocalPaymentMenu { get; set; }

        [FindByAutomation("a", "button-checkout-v3", Index = 0)]
        public LinkDelegate<PaymentPageV3, _> SeamlessCheckout { get; set; }

        [FindByAutomation("a", "button-checkout-v3", Index = 1)]
        public LinkDelegate<PaymentPageV3, _> RedirectCheckout { get; set; }

    }
}