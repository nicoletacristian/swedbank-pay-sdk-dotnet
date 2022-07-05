namespace SwedbankPay.Sdk.PaymentOrders;

/// <summary>
/// Details connected to the payment.
/// </summary>
public class CancelledDetails : ICancelledDetails
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nonPaymentToken"></param>
    /// <param name="externalNonPaymentToken"></param>
    protected internal CancelledDetails(string nonPaymentToken, string externalNonPaymentToken)
    {
        NonPaymentToken = nonPaymentToken;
        ExternalNonPaymentToken = externalNonPaymentToken;
    }

    /// <summary>
    /// The result of our own card tokenization. Activated in POS for the merchant or merchant group.
    /// </summary>
    public string NonPaymentToken { get; }

    /// <summary>
    /// The result of an external tokenization. This value will vary depending on card types, acquirers, customers, etc. For Mass Transit merchants, transactions redeemed by Visa will be populated with PAR. For Mastercard and Amex, it will be our own token.
    /// </summary>
    public string ExternalNonPaymentToken { get; }
}