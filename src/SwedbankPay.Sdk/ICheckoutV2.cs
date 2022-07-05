using SwedbankPay.Sdk.Consumers;
using SwedbankPay.Sdk.PaymentOrders.V2;

namespace SwedbankPay.Sdk
{
    /// <summary>
    /// Checkout v2
    /// </summary>
    public interface ICheckoutV2
    {
        /// <summary>
        /// Payment orders resource
        /// </summary>
        IPaymentOrdersResource PaymentOrders { get; }

        /// <summary>
        /// Consumers resource
        /// </summary>
        IConsumersResource Consumers { get; }
    }
}
