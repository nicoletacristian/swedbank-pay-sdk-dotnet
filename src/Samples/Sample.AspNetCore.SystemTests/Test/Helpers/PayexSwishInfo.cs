namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public class PayexSwishInfo : PayexInfo
    {
        public PayexSwishInfo(string swishNumber)
        {
            SwishNumber = swishNumber;
            Instrument = PaymentInstrument.Swish;
        }


        public string SwishNumber { get; }
    }
}