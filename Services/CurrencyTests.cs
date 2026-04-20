using System;
using System.Collections.Generic;
using System.Text;
using TechMoveApp.Patterns.Strategy;

namespace TechMoveApp.Tests.Services
{
    public class CurrencyTests
    {
        [Fact]
        public void Convert_UsdToZar_ReturnsCorrectValue()
        {
           
            var strategy = new ApiCurrencyConversionStrategy();
            var context = new CurrencyConversionContext(strategy);

            decimal usd = 10;
            decimal rate = 18;
   
            var result = context.Convert(usd, rate);

            Assert.Equal(180, result);
        }
    }
}

