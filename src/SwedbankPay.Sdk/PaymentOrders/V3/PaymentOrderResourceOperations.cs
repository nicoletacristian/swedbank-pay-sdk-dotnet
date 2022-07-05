namespace SwedbankPay.Sdk.PaymentOrders.V3
{
    /// <summary>
    /// Known operations for PaymentOrder.
    /// </summary>
    public static class PaymentOrderResourceOperations
    {

        /// <summary>
        /// Updates the order with a change in orderItems, amount and/or vatAmount.
        /// </summary>
        public const string UpdateOrder = "update-order";

        /// <summary>
        /// Aborts the payment order before any financial transactions are performed.
        /// </summary>
        public const string Abort = "abort";

        /// <summary>
        /// Used to cancel authorized and not yet captured transactions. If a cancellation is performed after doing a part-capture, it will only affect the not yet captured authorization amount.
        /// </summary>
        public const string Cancel = "cancel";

        /// <summary>
        /// The second part of a two-phase transaction where the authorized amount is sent from the payer to the payee. It is possible to do a part-capture on a subset of the authorized amount. Several captures on the same payment are possible, up to the total authorization amount.
        /// </summary>
        public const string Capture = "capture";

        /// <summary>
        /// Used to reverse a payment. It is only possible to reverse a payment that has been captured and not yet reversed.
        /// </summary>
        public const string Reversal = "reversal";

        /// <summary>
        /// Contains the URL used to redirect the payer to the Swedbank Pay Payments containing the Payment Menu.
        /// </summary>t
        public const string RedirectCheckout = "redirect-checkout";

        /// <summary>
        /// Contains the JavaScript href that is used to embed the Payment Menu UI directly on the webshop/merchant site.
        /// </summary>
        public const string ViewCheckout = "view-checkout";
    }
}
