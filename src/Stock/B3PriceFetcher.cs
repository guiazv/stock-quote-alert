using System.Net.Http.Json;

namespace Stock
{
  public class B3PriceFetcher : PriceFetcher
  {
    private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://cotacao.b3.com.br/mds/api/v1/instrumentQuotation/") };

    public async Task<double> GetCurrentPrice(string symbol)
    {
      var response = await client.GetFromJsonAsync<ResponseContent>(symbol);
      var currentPrice = response.CurrentPrice;

      return currentPrice;
    }

    struct ResponseContent
    {
      public double CurrentPrice { get => Trad[0].Scty.SctyQtn.CurPrc; }
      public Trad[] Trad { get; set; }
      public BizSts BizSts
      {
        set
        {
          if (value.Cd != "OK")
          {
            throw new Exception(value.Desc);
          }
        }
      }
    }

    struct Trad
    {
      public Scty Scty { get; set; }
    }
    struct Scty
    {
      public SctyQtn SctyQtn { get; set; }
    }

    struct SctyQtn
    {
      public double CurPrc { get; set; }
    }
    struct BizSts
    {
      public string Cd { get; set; }
      public string Desc { get; set; }
    }
  }
}
