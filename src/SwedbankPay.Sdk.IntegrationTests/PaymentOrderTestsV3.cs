using SwedbankPay.Sdk.Exceptions;
using SwedbankPay.Sdk.PaymentOrders;
using SwedbankPay.Sdk.Tests.TestBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using SwedbankPay.Sdk.PaymentOrders.V3;

using Xunit;

namespace SwedbankPay.Sdk.Tests
{
    public class PaymentOrderTestsV3 : ResourceTestsBase
    {
        private readonly PaymentOrderRequestBuilder paymentOrderRequestBuilder = new PaymentOrderRequestBuilder();


        [Fact]
        public async Task CreateAndAbortPaymentOrder_ShouldReturnAbortedStatus()
        {
            //ARRANGE
            var paymentOrderRequest = this.paymentOrderRequestBuilder
                .WithTestValues(this.payeeId)
                .WithOrderItems()
                .BuildV3();

            //ACT
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder);
            Assert.NotEmpty(paymentOrder.Operations);
            Assert.NotNull(paymentOrder.Operations.Abort);

            var responseContainer = await paymentOrder.Operations.Abort(new PaymentOrderAbortRequest("testAbortReason"));

            Assert.NotNull(responseContainer);
            Assert.NotNull(responseContainer.PaymentOrder);
            Assert.Equal(Status.Aborted, responseContainer.PaymentOrder.Status);

            var paymentOrder2 = await this.Sut.CheckoutV3.PaymentOrders.Get(paymentOrder.PaymentOrder.Id, PaymentOrderExpand.All);

            Assert.Equal("testAbortReason", paymentOrder2.PaymentOrder.Aborted.AbortReason);
        }


        //[Fact]
        //public async Task ABC()
        //{
        //    var paymentOrderV3Response = await this.Sut.CheckoutV3.PaymentOrders.Get(new Uri("/psp/paymentorders/f0337fea-5983-4586-5c0b-08da5aa857f7",
        //                                                                                 UriKind.RelativeOrAbsolute), PaymentOrderExpand.All);
        //    Assert.NotNull(paymentOrderV3Response);
        //    Assert.NotNull(paymentOrderV3Response.PaymentOrder);
        //    Assert.NotNull(paymentOrderV3Response.PaymentOrder.FailedAttempts);
        //    Assert.NotNull(paymentOrderV3Response.PaymentOrder.FailedAttempts.FailedAttemptList);
        //    Assert.NotEmpty(paymentOrderV3Response.PaymentOrder.FailedAttempts.FailedAttemptList);
        //    Assert.NotEmpty(paymentOrderV3Response.PaymentOrder.FailedAttempts.FailedAttemptList.FirstOrDefault().Problem.Problems);
        //}


        [Fact]
        public async Task CreateAndGetPaymentOrder_ShouldReturnPaymentOrderWithSameAmountAndMetadata()
        {
            var paymentOrderRequest = this.paymentOrderRequestBuilder
                .WithTestValues(this.payeeId)
                .WithOrderItems()
                .WithPayer().BuildV3();
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder);
            Assert.NotNull(paymentOrder.PaymentOrder);
            var amount = paymentOrder.PaymentOrder.Amount;

            var paymentOrder2 = await this.Sut.CheckoutV3.PaymentOrders.Get(paymentOrder.PaymentOrder.Id, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder2);
            Assert.NotNull(paymentOrder2.PaymentOrder);
            Assert.Equal(amount.InLowestMonetaryUnit, paymentOrder2.PaymentOrder.Amount.InLowestMonetaryUnit);

            //Assert.Equal(paymentOrderRequest.PaymentOrder.Metadata["key1"], paymentOrder2.PaymentOrder.Metadata["key1"]);
        }


        [Fact]
        public async Task CreateAndUpdateOnlyAmountOnPaymentOrder_ShouldThrowHttpResponseException()
        {
            var paymentOrderRequest = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId)
                .WithOrderItems()
                .WithPayer().BuildV3();
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder);
            Assert.NotNull(paymentOrder.PaymentOrder);

            var newAmount = 50000;
            var updateRequest = new PaymentOrderUpdateRequest(new Amount(newAmount), null);

            await Assert.ThrowsAsync<HttpResponseException>(() => paymentOrder.Operations.Update?.Invoke(updateRequest));
        }

        [Fact]
        public async Task CreateAndUpdatePaymentOrder_WithOrderItems_ShouldNotThrowHttpResponseException()
        {
            var paymentOrderRequest = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId).WithOrderItems().WithPayer().BuildV3();
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder);
            Assert.NotNull(paymentOrder.PaymentOrder);

            OrderItem updateOrderitem = new OrderItem("p3", "Product3", OrderItemType.Product, "ProductGroup3", 1, "pcs", 50000, 0,
                                          50000, 0)
            {
                ItemUrl = "https://example.com/products/1234",
                ImageUrl = "https://example.com/products/1234.jpg"
            };
            var updateRequest = new PaymentOrderUpdateRequest(updateOrderitem.Amount, updateOrderitem.VatAmount);
            updateRequest.PaymentOrder.OrderItems.Add(updateOrderitem);
            _ = await paymentOrder.Operations.Update.Invoke(updateRequest);
        }


        [Fact]
        public async Task CreateAndUpdatePaymentOrder_ShouldReturnPaymentOrderWithNewAmounts()
        {
            var paymentOrderRequest = this.paymentOrderRequestBuilder
                .WithTestValues(this.payeeId)
                .WithOrderItems()
                .WithPayer().BuildV3();

            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest, PaymentOrderExpand.All);
            Assert.NotNull(paymentOrder);
            Assert.NotNull(paymentOrder.PaymentOrder);

            var newAmount = 2500;
            var newVatAmount = 375;
            var updateRequest = new PaymentOrderUpdateRequest(new Amount(newAmount), new Amount(newVatAmount));

            var updateOrderItems = new List<IOrderItem>
            {
                new OrderItem("p1", "Product1", OrderItemType.Product, "ProductGroup1", 5, "pcs", new Amount(300), 25, new Amount(1500), new Amount(375))
                {
                    ItemUrl ="https://example.com/products/123",
                    ImageUrl = "https://example.com/products/123.jpg"
                },
                new OrderItem("p2", "Product2", OrderItemType.Product, "ProductGroup1", 2, "pcs", new Amount(500), 0, new Amount(1000), new Amount(0))
            };

            updateRequest.PaymentOrder.OrderItems = updateOrderItems;

            Assert.NotNull(paymentOrder.Operations.Update);

            var response = await paymentOrder.Operations.Update(updateRequest);

            Assert.Equal(updateRequest.PaymentOrder.Amount, response.PaymentOrder.Amount);
            Assert.Equal(updateRequest.PaymentOrder.VatAmount, response.PaymentOrder.VatAmount);
        }

        [Fact]
        public async Task CreatePaymentOrder_ShouldReturnPaymentOrderWithCorrectAmount()
        {
            var paymentOrderRequest = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId).WithPayer().BuildV3();
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequest);
            Assert.NotNull(paymentOrder.PaymentOrder);
            Assert.Equal(paymentOrderRequest.PaymentOrder.Amount, paymentOrder.PaymentOrder.Amount);
        }


        [Fact]
        public async Task CreatePaymentOrder_WithOrderItems_ShouldReturnOrderItemsIfExpanded()
        {
            //ARRANGE
            var paymentOrderRequestContainer = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId)
                    .WithPayer()
                    .WithOrderItems()
                    .BuildV3();

            //ACT
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequestContainer, PaymentOrderExpand.OrderItems);

            //ASSERT
            Assert.NotNull(paymentOrder.PaymentOrder);
            Assert.Equal(UserAgent.Default, paymentOrder.PaymentOrder.InitiatingSystemUserAgent);
            Assert.NotNull(paymentOrder.PaymentOrder.OrderItems);
            Assert.NotEmpty(paymentOrder.PaymentOrder.OrderItems.OrderItemList);
        }



        [Fact]
        public async Task CreatePaymentOrder_With_RestrictedToInstruments_ShouldMapCorrectly()
        {
            //ARRANGE
            var paymentOrderRequestContainer = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId)
                    .WithOrderItems()
                    .WithPayer()
                    .WithOrderItemRestrictedToInstruments(OrderItemInstrument.Invoice, OrderItemInstrument.InvoicePayExFinancingSe, OrderItemInstrument.InvoicePayExFinancingNo)
                    .BuildV3();

            //ACT
            var paymentOrder = await this.Sut.CheckoutV3.PaymentOrders.Create(paymentOrderRequestContainer, PaymentOrderExpand.OrderItems);

            //ASSERT
            Assert.NotNull(paymentOrder.PaymentOrder);
            Assert.NotNull(paymentOrder.PaymentOrder.OrderItems);
            Assert.NotEmpty(paymentOrder.PaymentOrder.OrderItems.OrderItemList);
            var orderItemRestrictedToInstruments = paymentOrder.PaymentOrder.OrderItems.OrderItemList.Last();
            Assert.Collection(orderItemRestrictedToInstruments.RestrictedToInstruments,
                item => Assert.Equal(OrderItemInstrument.Invoice, item),
                item => Assert.Equal(OrderItemInstrument.InvoicePayExFinancingSe, item),
                item => Assert.Equal(OrderItemInstrument.InvoicePayExFinancingNo, item));
        }



        //[Fact]
        //public async Task GetPaymentOrder_WithPayment_ShouldReturnCurrentPaymentIfExpanded()
        //{
        //    //ARRANGE
        //    _ = this.paymentOrderRequestBuilder.WithTestValues(this.payeeId)
        //            .WithOrderItems()
        //            .Build();

        //    //ACT
        //    var paymentOrder = await this.Sut.PaymentOrders.Get(new Uri("/psp/paymentorders/472e6f26-a9b5-4e91-1b70-08d756b9b7d8", UriKind.Relative),
        //                                                        PaymentOrderExpand.CurrentPayment);

        //    //ASSERT
        //    Assert.NotNull(paymentOrder);
        //    Assert.NotNull(paymentOrder.PaymentOrder.CurrentPayment);
        //    Assert.NotNull(paymentOrder.PaymentOrder.CurrentPayment.Payment);
        //}

        [Fact]
        public async Task GetUnknownPaymentOrder_ShouldThrowHttpResponseException()
        {
            var id = new Uri("/psp/paymentorders/56a45c8a-9605-437a-fb80-08d742822747", UriKind.Relative);

            var thrownException = await Assert.ThrowsAsync<HttpResponseException>(() => this.Sut.CheckoutV3.PaymentOrders.Get(id));
            Assert.Equal(HttpStatusCode.NotFound, thrownException.HttpResponse.StatusCode);
        }
    }
}