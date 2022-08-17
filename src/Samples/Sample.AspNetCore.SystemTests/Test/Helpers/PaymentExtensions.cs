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
                .Email.Set(TestDataService.Email)
                .PhoneNumber.Set(TestDataService.SwedishPhoneNumber)
                .Next.Click()
                .WaitSeconds(1)
                .Do(x =>
                {
                    if (x.SaveMyInformation.IsVisible)
                    {
                        x.SaveMyInformation.Click();
                        x.PersonalNumber.Set(TestDataService.PersonalNumber);
                        x.Next.Click();
                        x.FirstName.Set(TestDataService.FirstName);
                        x.LastName.Set(TestDataService.LastName);
                        x.Address.Set(TestDataService.Street);
                        x.ZipCode.Set(TestDataService.ZipCode);
                        x.City.Set(TestDataService.City);
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
                                .Cvc.Set(info.Cvc);
                            }

                            x.AddNewCard.Click();
                        }
                    }

                    return x.CreditCardNumber.Set(TestDataService.CreditCardNumber)
                            .ExpiryDate.Set(TestDataService.CreditCardExpirationDate)
                            .Cvc.Set(TestDataService.CreditCardCvc);

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
                        .SwishNumber.Clear()
                        .SwishNumber.Set(info.SwishNumber);
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
                        x.PersonalNumber.Set(info.PersonalNumber.Substring(info.PersonalNumber.Length - 4));
                    }
                    else
                    {
                        x
                        .PersonalNumber.Clear()
                        .PersonalNumber.Set(info.PersonalNumber)
                        .Email.Clear()
                        .Email.Set(info.Email)
                        .PhoneNumber.Clear()
                        .PhoneNumber.Set(info.PhoneNumber)
                        .ZipCode.Clear()
                        .ZipCode.Set(info.ZipCode)
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
