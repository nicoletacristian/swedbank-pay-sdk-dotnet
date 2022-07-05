namespace SwedbankPay.Sdk
{
    /// <summary>
    /// Holds shared information about a address.
    /// </summary>
    public interface IAddressResponse
    {
        /// <summary>
        /// The name of the addressee – the receiver of the shipped goods.
        /// </summary>
        string Addressee { get; }

        /// <summary>
        /// The C/O address of the payer, if applicable.
        /// </summary>
        string CoAddress { get; }

        /// <summary>
        /// Payers street address in their city of residence.
        /// </summary>
        string StreetAddress { get; }

        /// <summary>
        /// Payers zip code of their country of residence.
        /// </summary>
        string ZipCode { get; }

        /// <summary>
        /// The payers city of residence.
        /// </summary>
        string City { get; }

        /// <summary>
        /// The country code of the payers country of residence.
        /// </summary>
        CountryCode CountryCode { get; }
    }
}
