using SwedbankPay.Sdk.PaymentOrders;
using System;
using System.Collections.Generic;
using System.Linq;

using SwedbankPay.Sdk.PaymentOrders.V2;
using SwedbankPay.Sdk.PaymentOrders.V3;

using Payer = SwedbankPay.Sdk.PaymentOrders.V3.Payer;
using PaymentOrderRequest = SwedbankPay.Sdk.PaymentOrders.V3.PaymentOrderRequest;

namespace SwedbankPay.Sdk.Tests.TestBuilders
{
    public class PaymentOrderRequestBuilder
    {
        private Amount amount;
        private Currency currency;
        private string description;
        private bool generateRecurrenceToken;
        private Language language;
        private Metadata metadata;
        private Operation operation;
        private List<IOrderItem> orderItems;
        private IPayeeInfo payeeInfo;
        private IUrls urls;
        private string userAgent;
        private Amount vatAmount;
        private bool requestDeliveryInfo;
        private bool restrictedToDeliveryInfoInstruments;
        private Payer payer;


        public PaymentOrders.V2.PaymentOrderRequest Build()
        {
            var req = new PaymentOrders.V2.PaymentOrderRequest(this.operation, this.currency, this.amount, this.vatAmount, this.description, this.userAgent, this.language, this.generateRecurrenceToken, this.urls, this.payeeInfo);
            req.PaymentOrder.OrderItems = orderItems;
            req.PaymentOrder.Metadata = metadata;
            
            return req;
        }

        public PaymentOrderRequest BuildV3()
        {
            var req = new PaymentOrderRequest(this.operation, this.currency, this.amount, this.vatAmount, this.description,
                                                this.userAgent, this.language, requestDeliveryInfo, restrictedToDeliveryInfoInstruments, "Checkout3", this.urls, this.payeeInfo);
            req.PaymentOrder.OrderItems = orderItems;
            req.PaymentOrder.Metadata = metadata;
            req.PaymentOrder.Payer = this.payer;

            return req;
        }


        public PaymentOrderRequestBuilder WithAmounts(long longAmount = 30000, long longVatAmount = 7500)
        {
            if (longAmount - longVatAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(longVatAmount), $"{longVatAmount} cant be greater than {longAmount}");
            }

            this.amount = new Amount(longAmount);
            this.vatAmount = new Amount(longVatAmount);

            return this;
        }


        public PaymentOrderRequestBuilder WithLanguageCode(string code)
        {
            this.language = new Language(code);
            return this;
        }

        public PaymentOrderRequestBuilder WithPayer()
        {
            this.payer = new Payer("Leia", "Ahlström")
            {
                Email = new EmailAddress("leia@payex.com"),
                Msisdn = new Msisdn("+46787654321"),
                PayerReference = "AB1234",
                ShippingAddress = new Address
                {
                    FirstName = "firstname/companyname",
                    LastName = "lastname",
                    Email = new EmailAddress("karl.andersson@mail.se"),
                    Msisdn = new Msisdn("+46759123456"),
                    StreetAddress = "string",
                    CoAddress = "string",
                    City = "Solna",
                    ZipCode = "17674",
                    CountryCode = new CountryCode("SE")
                },
                BillingAddress = new Address
                {
                    FirstName = "firstname/companyname",
                    LastName = "lastname",
                    Email = new EmailAddress("karl.andersson@mail.se"),
                    Msisdn = new Msisdn("+46759123456"),
                    StreetAddress = "string",
                    CoAddress = "string",
                    City = "Solna",
                    ZipCode = "17674",
                    CountryCode = new CountryCode("SE")
                },
                AccountInfo = new AccountInfo
                {
                    AccountAgeIndicator = AccountAgeIndicator.ThirtyToSixtyDays,
                    AccountChangeIndicator = AccountChangeIndicator.MoreThanSixtyDays,
                    AccountPwdChangeIndicator = AccountPwdChangeIndicator.NoChange,
                    ShippingAddressUsageIndicator = ShippingAddressUsageIndicator.ThisTransaction,
                    ShippingNameIndicator = ShippingNameIndicator.AccountNameIdenticalToShippingName,
                    SuspiciousAccountActivity = SuspiciousAccountActivity.NoSuspiciousActivityObserved
                }
            };
            return this;
        }



        public PaymentOrderRequestBuilder WithMetadata()
        {
            this.metadata = new Metadata
            {
                ["testvalue"] = 3,
                ["testvalue2"] = "test"
            };
            return this;
        }


        public PaymentOrderRequestBuilder WithOrderItems()
        {
            this.orderItems = new List<IOrderItem>
            {
                new OrderItem("p1", "Product1", OrderItemType.Product, "ProductGroup1", 5, "pcs", new Amount(300), 25,
                              new Amount(1500), new Amount(375))
                {
                    ItemUrl ="https://example.com/products/123",
                    ImageUrl = "https://example.com/products/123.jpg"
                },
                new OrderItem("p2", "Product2", OrderItemType.Product, "ProductGroup1", 1, "pcs", new Amount(500), 0,
                              new Amount(500), new Amount(0))
            };
            this.amount = new Amount(this.orderItems.Sum(s => s.Amount));
            this.vatAmount = new Amount(this.orderItems.Sum(s => s.VatAmount));

            return this;
        }


        public PaymentOrderRequestBuilder WithOrderItemRestrictedToInstruments(params OrderItemInstrument[] instruments)
        {
            if (!this.orderItems.Any())
            {
                this.orderItems = new List<IOrderItem>();
            }

            this.orderItems.Add(new OrderItem("F1", "PaymentFee", OrderItemType.PaymentFee, "Fees", 1, "pcs", 1900, 0, 1900, 0)
            {
                RestrictedToInstruments = instruments
            });

            this.amount = new Amount(this.orderItems.Where(o => o.RestrictedToInstruments is null).Sum(s => s.Amount));
            this.vatAmount = new Amount(this.orderItems.Where(o => o.RestrictedToInstruments is null).Sum(s => s.VatAmount));

            return this;
        }


        public PaymentOrderRequestBuilder WithTestValues(string payeeId)
        {
            this.operation = Operation.Purchase;
            this.currency = new Currency("SEK");
            this.amount = new Amount(1700);
            this.vatAmount = new Amount(0);
            this.description = "Test Description";
            this.generateRecurrenceToken = false;
            this.urls = new UrlsResponse(new UrlsDto
            {
                HostUrls = new List<Uri> { new Uri("https://example.com") },
                PaymentUrl = new Uri("https://example.com/payment"),
                CompleteUrl = new Uri("https://example.com/payment-completed"),
                TermsOfServiceUrl = new Uri("https://example.com/termsandconditoons.pdf"),
                CancelUrl = new Uri("https://example.com/payment-canceled"),
                Id = "https://example.com",
                CallbackUrl = new Uri("https://example.com/callback")
            });
            this.userAgent = "useragent";
            this.language = new Language("sv-SE");
            this.payeeInfo = new PayeeInfo(payeeId, DateTime.Now.Ticks.ToString());
            this.metadata = new Metadata { { "key1", "value1" }, { "key2", 2 }, { "key3", 3.1 }, { "key4", false } };
            this.requestDeliveryInfo = false;
            this.restrictedToDeliveryInfoInstruments = false;
            return this;
        }
    }
}