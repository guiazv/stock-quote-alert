
namespace Stock
{
  public class AdviceMailNotifier : AdviceNotifier
  {
    private string from;
    private string to;

    public AdviceMailNotifier(string from, string to)
    {
      this.from = from;
      this.to = to;
    }

    public Task SendBuyAdvice(string stock, double stockPrice, double buyPrice)
    {
      Console.WriteLine("Sending buy stock recommendation mail!");
      return Utils.Mailer.Send(new System.Net.Mail.MailMessage()
      {
        From = new System.Net.Mail.MailAddress(from),
        To = { to },
        Subject = $"Buy {stock}",
        Body = $"<p>Current price: {stockPrice:C2}; Configured buy price: {buyPrice:C2}</p>",
        IsBodyHtml = true,
      });
    }

    public Task SendSellAdvice(string stock, double stockPrice, double sellPrice)
    {
      Console.WriteLine("Sending sell stock recommendation mail!");
      return Utils.Mailer.Send(new System.Net.Mail.MailMessage()
      {
        From = new System.Net.Mail.MailAddress(from),
        To = { to },
        Subject = $"Sell {stock}",
        Body = $"<p>Current price: {stockPrice:C2}; Configured sell price: {sellPrice:C2}</p>",
        IsBodyHtml = true,
      });
    }
  }
}
