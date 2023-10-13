using System.Globalization;

internal class Program
{
  const int INTERVAL_IN_SECONDS = 15;
  const int MILLISECONDS_IN_A_SECOND = 1000;

  private static void Main(string[] args)
  {
    Utils.DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

    if (args.Length != 3)
    {
      Console.Error.WriteLine("Invalid number of arguments");
      Console.WriteLine("Usage: <stock symbol> <sell price> <buy price>");
      Environment.Exit(1);
    }

    var (stock, rawSellPrice, rawPurchasePrice) = (args[0].ToUpper(), args[1], args[2]);
    if (!double.TryParse(rawSellPrice, CultureInfo.InvariantCulture, out var sellPrice))
    {
      Console.Error.WriteLine($"Invalid sell price: {rawSellPrice}");
      Environment.Exit(1);
    }
    if (!double.TryParse(rawPurchasePrice, CultureInfo.InvariantCulture, out var purchasePrice))
    {
      Console.Error.WriteLine($"Invalid purchase price: {rawPurchasePrice}", rawPurchasePrice);
      Environment.Exit(1);
    }
    Console.WriteLine($"Stock: {stock}; sell price: {sellPrice:C2}; Buy price: {purchasePrice:C2}");

    var mailTo = Environment.GetEnvironmentVariable("MAIL_TO");
    if (string.IsNullOrEmpty(mailTo))
    {
      Console.Error.WriteLine("MAIL_TO environment variable is not set");
      Environment.Exit(1);
    }

    var advisor = new Stock.Advisor(
      stock,
      sellPrice,
      purchasePrice,
      new Stock.B3PriceFetcher(),
      new Stock.AdviceMailNotifier("stockquotealert@example.com", mailTo)
    );

    while (true)
    {
      var delay = Task.Delay(INTERVAL_IN_SECONDS * MILLISECONDS_IN_A_SECOND);
      advisor.Advise();
      delay.Wait();
    }
  }
}