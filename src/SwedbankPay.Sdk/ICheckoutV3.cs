using SwedbankPay.Sdk.PaymentOrders.V3;

namespace SwedbankPay.Sdk
{
    /// <summary>
    /// Checkout v3
    /// </summary>
    public interface ICheckoutV3
    {
        /// <summary>
        /// Payment orders resource
        /// </summary>
        IPaymentOrdersResource PaymentOrders { get; }
    }
}
