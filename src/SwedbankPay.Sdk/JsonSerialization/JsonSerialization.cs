﻿namespace SwedbankPay.Sdk.JsonSerialization
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using SwedbankPay.Sdk.PaymentOrders;
    using SwedbankPay.Sdk.Payments;

    public class JsonSerialization
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(),
                new CustomEmailAddressConverter(typeof(EmailAddress)),
                new TypedSafeEnumValueConverter<ShipIndicator, string>(),
                new TypedSafeEnumValueConverter<DeliveryTimeFrameIndicator, string>(),
                new TypedSafeEnumValueConverter<PreOrderPurchaseIndicator, string>(),
                new TypedSafeEnumValueConverter<ReOrderPurchaseIndicator, string>(),
                new TypedSafeEnumValueConverter<AccountAgeIndicator, string>(),
                new TypedSafeEnumValueConverter<AccountChangeIndicator, string>(),
                new TypedSafeEnumValueConverter<AccountPwdChangeIndicator, string>(),
                new TypedSafeEnumValueConverter<ShippingAddressUsageIndicator, string>(),
                new TypedSafeEnumValueConverter<ShippingNameIndicator, string>(),
                new TypedSafeEnumValueConverter<SuspiciousAccountActivity, string>(),
                new TypedSafeEnumValueConverter<Operation, string>(),
                new TypedSafeEnumValueConverter<State, string>(),
                new TypedSafeEnumValueConverter<PaymentOrderLanguage, string>(),
                new CustomCurrencyCodeConverter(typeof(CurrencyCode))
            },
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyyMMdd"
        };
    }
}