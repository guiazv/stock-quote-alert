namespace Stock
{
  public interface PriceFetcher
  {
    public Task<double> GetCurrentPrice(string symbol);
  }
}