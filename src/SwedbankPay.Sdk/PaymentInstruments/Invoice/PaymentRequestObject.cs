﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace SwedbankPay.Sdk.Payments.InvoicePayments
{
    public class PaymentRequestObject : IPaymentRequestObject
    {
        public PaymentRequestObject(Operation operation,
                                                    Intent intent,
                                                    CurrencyCode currency,
                                                    List<IPrice> prices,
                                                    string description,
                                                    string payerReference,
                                                    bool generatePaymentToken,
                                                    bool generateReccurenceToken,
                                                    string userAgent,
                                                    CultureInfo language,
                                                    IUrls urls,
                                                    PayeeInfo payeeInfo,
                                                    Dictionary<string, object> metadata = null,
                                                    string paymentToken = null,
                                                    PrefillInfo prefillInfo = null)
        {
            Operation = operation ?? throw new ArgumentNullException(nameof(operation));
            Intent = intent;
            Currency = currency;
            Prices = prices;
            Description = description;
            PayerReference = payerReference;
            UserAgent = userAgent;
            Language = language;
            Urls = urls;
            PayeeInfo = payeeInfo;
            Metadata = metadata;
            GeneratePaymentToken = generatePaymentToken;
            GenerateReccurenceToken = generateReccurenceToken;
            PaymentToken = paymentToken;
            PrefillInfo = prefillInfo;
        }


        public CurrencyCode Currency { get; set; }
        public string Description { get; set; }
        public bool GeneratePaymentToken { get; set; }
        public bool GenerateReccurenceToken { get; set; }
        public Intent Intent { get; set; }
        public CultureInfo Language { get; set; }
        public Dictionary<string, object> Metadata { get; }
        public Operation Operation { get; set; }
        public PayeeInfo PayeeInfo { get; internal set; }
        public string PayerReference { get; set; }
        public string PaymentToken { get; set; }
        public List<IPrice> Prices { get; set; }
        public IUrls Urls { get; }
        public string UserAgent { get; set; }
        public PrefillInfo PrefillInfo { get; set; }
    }

}