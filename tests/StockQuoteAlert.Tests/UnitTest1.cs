using System;
using Xunit;

namespace StockQuoteAlert.Tests
{
    public class StockValuesTest
    {
        [Fact]
        public void ParseValuesTest()
        {
           StockQuote stockAlert = new StockQuote();

           double parsedValue =StockQuote.ParseValue("22.45");
            Assert.Equal(22.45,parsedValue);

            double parsedValue2 =StockQuote.ParseValue("26");
            Assert.Equal(26,parsedValue2);

        }
    }
}
