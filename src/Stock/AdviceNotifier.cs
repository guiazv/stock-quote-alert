namespace Stock
{
  public interface AdviceNotifier
  {
    public Task SendBuyAdvice(string stock, double stockPrice, double buyPrice);
    public Task SendSellAdvice(string stock, double stockPrice, double sellPrice);
  }
}
