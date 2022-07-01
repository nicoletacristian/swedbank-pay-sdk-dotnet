namespace SwedbankPay.Sdk.PaymentOrders
{
    /// <summary>
    /// API request to reverse captured funds.
    /// </summary>
    public class PaymentOrderReversalRequest
    {
        /// <summary>
        /// Instantiates a <see cref="PaymentOrderReversalRequest"/> with the provided parameters.
        /// </summary>
        /// <param name="amount">The amount to refund to the payer.</param>
        /// <param name="vatAmount">The amount of VAT to refund.</param>
        /// <param name="description">Textual description of the reversal.</param>
        /// <param name="payeeReference">Unique ID set by the merchant for this transaction.</param>
        /// <param name="receiptReference">A unique reference from the merchant system. It is used to supplement payeeReference as an additional receipt number.</param>
        public PaymentOrderReversalRequest(Amount amount, Amount vatAmount, string description, string payeeReference, string receiptReference)
        {
            Transaction = new PaymentOrderReversalTransaction(amount, vatAmount, description, payeeReference, receiptReference);
        }

        /// <summary>
        /// Transactional details about what is being reversed.
        /// </summary>
        public PaymentOrderReversalTransaction Transaction { get; }
    }
}