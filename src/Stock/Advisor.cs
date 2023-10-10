namespace Stock
{
  public class Advisor
  {
    private string stock;
    private double sellPrice;
    private double buyPrice;
    private PriceFetcher fetcher;
    private AdviceNotifier notifier;

    public Advisor(string stock, double sellPrice, double buyPrice, PriceFetcher fetcher, AdviceNotifier notifier)
    {
      if (sellPrice <= 0)
      {
        throw new ArgumentException("Sell price must be greater than zero", nameof(sellPrice));
      }
      if (buyPrice <= 0)
      {
        throw new ArgumentException("Buy price must be greater than zero", nameof(buyPrice));
      }
      if (sellPrice <= buyPrice)
      {
        throw new ArgumentException("Sell price must be greater than buy price", nameof(sellPrice));
      }
      this.stock = stock;
      this.sellPrice = sellPrice;
      this.buyPrice = buyPrice;
      this.fetcher = fetcher;
      this.notifier = notifier;
    }

    public void Advise()
    {
      var currentPrice = fetcher.GetCurrentPrice(stock).Result;
      if (currentPrice >= sellPrice)
      {
        notifier.SendSellAdvice(stock, currentPrice, sellPrice);
      }
      if (currentPrice <= buyPrice)
      {
        notifier.SendBuyAdvice(stock, currentPrice, buyPrice);
      }
    }
  }
}