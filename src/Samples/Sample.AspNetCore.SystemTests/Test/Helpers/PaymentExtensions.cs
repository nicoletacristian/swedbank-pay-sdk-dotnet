using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.PageObjectModels.ThankYou;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class PaymentExtensions
    {
        public static IdentificationFramePage PerformIdentification(this PaymentPageV2 page)
        {
            return page
                .IdentificationFrame.SwitchTo()
                .Email.IsVisible.WaitTo.BeTrue()
                .Email.SetWithSpeed(TestDataService.Email, interval: 0.1)
                .PhoneNumber.SetWithSpeed(TestDataService.SwedishPhoneNumber, interval: 0.1)
                .Next.Click()
                .WaitSeconds(1)
                .Do(x =>
                {
                    if (x.SaveMyInformation.IsVisible)
                    {
                        x.SaveMyInformation.Click();
                        x.PersonalNumber.SetWithSpeed(TestDataService.PersonalNumber, interval: 0.1);
                        x.Next.Click();
                        x.FirstName.SetWithSpeed(TestDataService.FirstName, interval: 0.1);
                        x.LastName.SetWithSpeed(TestDataService.LastName, interval: 0.1);
                        x.Address.SetWithSpeed(TestDataService.Street, interval: 0.1);
                        x.ZipCode.SetWithSpeed(TestDataService.ZipCode, interval: 0.1);
                        x.City.SetWithSpeed(TestDataService.City, interval: 0.1);
                        x.Next.Click();
                    }
                });
        }

        public static ThankYouPage PayWithPayexCard(this PayexCardFramePage frame, PayexCardInfo info, Product[] products, Checkout checkout)
        {
            return frame
                .Do(x =>
                {
                    if (checkout == Checkout.Standard)
                    {
                        if (x.PreFilledCards.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(3) }))
                        {
                            if (x.PreFilledCards.Items[y => y.CreditCardNumber.Value.Contains(info.CreditCardNumber.Substring(info.CreditCardNumber.Length - 4))].Exists())
                            {
                                return x.PreFilledCards
                                .Items[
                                    y => y.CreditCardNumber.Value.Contains(
                                        info.CreditCardNumber.Substring(info.CreditCardNumber.Length - 4))].Click()
                                .Cvc.SetWithSpeed(info.Cvc, interval: 0.1);
                            }

                            x.AddNewCard.Click();
                        }
                    }

                    return x.CreditCardNumber.SetWithSpeed(TestDataService.CreditCardNumber, interval: 0.1)
                            .ExpiryDate.SetWithSpeed(TestDataService.CreditCardExpirationDate, interval: 0.1)
                            .Cvc.SetWithSpeed(TestDataService.CreditCardCvc, interval: 0.1);

                })
                .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                .Pay.ClickAndGo();
        }

        public static ThankYouPage PayWithPayexSwish(this PayexSwishFramePage frame, PayexSwishInfo info, Product[] products, Checkout checkout)
        {
            return frame
                .Do(x =>
                {
                    if (checkout != Checkout.Standard)
                    {
                        x
                        .SwishNumber.IsVisible.WaitTo.BeTrue()
                        .SwishNumber.SetWithSpeed(info.SwishNumber, interval: 0.1);
                    }

                    return x
                        .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                        .Pay.ClickAndGo();
                });
        }

        public static ThankYouPage PayWithPayexInvoice(this PayexInvoiceFramePage frame, PayexInvoiceInfo info, Product[] products, Checkout checkout)
        {
            return frame
                .Do(x =>
                {
                    x
                    .PersonalNumber.IsVisible.WaitTo.BeTrue();
                    
                    if (checkout == Checkout.Standard)
                    {
                        x.PersonalNumber.SetWithSpeed(info.PersonalNumber.Substring(info.PersonalNumber.Length - 4), interval: 0.15);
                    }
                    else
                    {
                        x
                        .PersonalNumber.SetWithSpeed(info.PersonalNumber, interval: 0.15)
                        .Email.SetWithSpeed(info.Email, interval: 0.1)
                        .PhoneNumber.SetWithSpeed(info.PhoneNumber, interval: 0.1)
                        .ZipCode.SetWithSpeed(info.ZipCode, interval: 0.1)
                        .Next.Click();
                    }

                    return x;
                })
                .WaitSeconds(5)
                .Pay.IsVisible.WaitTo.WithinSeconds(20).BeTrue()
                .Pay.Content.Should.BeEquivalent($"Betala {string.Format("{0:N2}", Convert.ToDecimal(products.Sum(x => x.UnitPrice / 100 * x.Quantity)))} kr")
                .Pay.ClickAndGo();
        }

        public static ThankYouPage PayWithPayexTrustly(this PayexTrustlyFramePage frame)
        {
            return frame
                .Submit.IsVisible.WaitTo.WithinSeconds(15).BeTrue()
                .Submit.ClickAndGo<TrustlyPaymentPage>()
                .Banks[0].IsVisible.WaitTo.WithinSeconds(15).BeTrue()
                .Banks[0].Click()
                .Next.Click()
                .PersonalNumber.Set(TestDataService.PersonalNumber)
                .SecurityCodeOption.Click()
                .Next.Click()
                .MessageCode.StoreValue(out string code)
                .Code.Set(code)
                .Next.Click()
                .AccountOptions.IsVisible.WaitTo.WithinSeconds(60).BeTrue()
                .Next.Click()
                .MessageCode.StoreValue(out code)
                .Code.Set(code)
                .Next.Click()
                .PageUrl.Should.WithinSeconds(60).Contain("Thankyou")
                .SwitchToRoot<ThankYouPage>()
                .ThankYou.IsVisible.WaitTo.BeTrue();
        }
    }
}
