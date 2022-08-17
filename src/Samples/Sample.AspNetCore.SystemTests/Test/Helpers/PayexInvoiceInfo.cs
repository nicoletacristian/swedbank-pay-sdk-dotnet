namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public class PayexInvoiceInfo : PayexInfo
    {
        public PayexInvoiceInfo(string personalNumber, string email, string phoneNumber, string zipCode)
        {
            PersonalNumber = personalNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            ZipCode = zipCode;
            Instrument = PaymentInstrument.Invoice;
        }


        public string Email { get; }
        public string PersonalNumber { get; }
        public string PhoneNumber { get; }
        public string ZipCode { get; }
    }
}