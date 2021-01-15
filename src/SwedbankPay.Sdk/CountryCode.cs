﻿using System;
using System.Globalization;

namespace SwedbankPay.Sdk
{
    /// <summary>
    /// Object to hold a valid ISO 3166-1 alpha-2 country code.
    /// </summary>
    public class CountryCode
    {
        private string Value { get; }

        /// <summary>
        /// Instantiates a new <see cref="CountryCode"/> with the provided <paramref name="countryCode"/>.
        /// </summary>
        /// <param name="countryCode">A country code following ISO 3166-1 alpha-2.</param>
        public CountryCode(string countryCode)
        {
            try
            {
                _ = new RegionInfo(countryCode);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid country code: {countryCode}", nameof(countryCode));
            }

            Value = countryCode;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
