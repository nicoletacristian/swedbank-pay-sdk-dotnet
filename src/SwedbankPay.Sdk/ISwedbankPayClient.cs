using SwedbankPay.Sdk.PaymentInstruments;

namespace SwedbankPay.Sdk
{
    /// <summary>
    /// The entrypoint of this SDK!
    /// Used to access the different APIs'.
    /// </summary>
    public interface ISwedbankPayClient
    {
        /// <summary>
        /// Checkout V2
        /// </summary>
        ICheckoutV2 CheckoutV2 { get; }

        /// <summary>
        /// Checkout V3
        /// </summary>
        ICheckoutV3 CheckoutV3 { get; }

        /// <summary>
        /// Resource to create and get payments on several payment instruments.
        /// </summary>
        IPaymentInstrumentsResource Payments { get; }
    }
}
