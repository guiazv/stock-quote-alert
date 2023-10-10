namespace Stock.Tests
{
    public class AdvisorTests
    {
        [Theory]
        [InlineData(0.0, 10.0, "sellPrice")]
        [InlineData(-1.0, 10.0, "sellPrice")]
        [InlineData(10.0, 0.0, "buyPrice")]
        [InlineData(10.0, -1.0, "buyPrice")]
        [InlineData(10.0, 10.0, "sellPrice")]
        public void Advisor_ShouldThrowWithInvalidArguments(double sellPrice, double buyPrice, string paramName)
        {
            // Arrange
            // Act
            var t = Assert.Throws<ArgumentException>(
                () => new Advisor(
                    "PETR4",
                    sellPrice,
                    buyPrice,
                    new MockPriceFetcher(0),
                    new MockAdviceNotifier()
                )
            );
            // Assert
            Assert.Equal(paramName, t.ParamName);
        }

        [Theory]
        [InlineData(10.00, 9.00, 10.00, "sell")]
        [InlineData(10.10, 9.00, 10.10, "sell")]
        [InlineData(20.00, 9.00, 100.00, "sell")]
        [InlineData(20.00, 9.00, 999.99, "sell")]
        [InlineData(100.00, 9.00, 999.99, "sell")]
        [InlineData(10.00, 9.00, 9.00, "buy")]
        [InlineData(10.00, 9.00, 8.00, "buy")]
        [InlineData(10.00, 5.00, 1.00, "buy")]
        [InlineData(10.00, 2.00, 0.10, "buy")]
        [InlineData(10.00, 1.00, 0.01, "buy")]
        [InlineData(10.00, 9.00, 9.99, null)]
        [InlineData(10.00, 9.00, 9.01, null)]
        [InlineData(10.00, 9.00, 9.50, null)]
        public void Advisor_ShouldSendCorrectAdvice(double sellPrice, double buyPrice, double currentPrice, string? expectedAdvice)
        {
            // Arrange
            string? advice = null;
            var advisor = new Advisor(
                "PETR4",
                sellPrice,
                buyPrice,
                new MockPriceFetcher(currentPrice),
                new MockAdviceNotifier(
                    (_, _, _) => { advice = "buy"; },
                    (_, _, _) => { advice = "sell"; }
                )
            );
            // Act
            advisor.Advise();
            // Assert
            Assert.Equal(expectedAdvice, advice);
        }
    }

    class MockPriceFetcher : PriceFetcher
    {
        private double _returned;
        public MockPriceFetcher(double returned)
        {
            _returned = returned;
        }
        public Task<double> GetCurrentPrice(string symbol)
        {
            return Task.FromResult(_returned);
        }
    }

    class MockAdviceNotifier : AdviceNotifier
    {
        private Action<string, double, double>? _onSendBuyAdvice;
        private Action<string, double, double>? _onSendSellAdvice;
        public MockAdviceNotifier() { }
        public MockAdviceNotifier(Action<string, double, double> onSendBuyAdvice, Action<string, double, double> onSendSellAdvice)
        {
            _onSendBuyAdvice = onSendBuyAdvice;
            _onSendSellAdvice = onSendSellAdvice;
        }
        public Task SendBuyAdvice(string stock, double stockPrice, double buyPrice)
        {
            if (_onSendBuyAdvice == null)
            {
                return Task.CompletedTask;
            }
            _onSendBuyAdvice(stock, stockPrice, buyPrice);
            return Task.CompletedTask;
        }

        public Task SendSellAdvice(string stock, double stockPrice, double sellPrice)
        {
            if (_onSendSellAdvice == null)
            {
                return Task.CompletedTask;
            }
            _onSendSellAdvice(stock, stockPrice, sellPrice);
            return Task.CompletedTask;
        }
    }
}

