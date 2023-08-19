using Microsoft.Extensions.Options;
using Domain.Models.FinancialModelingPrep;
using System.Net.Http.Json;


namespace Application.Services
{
  public class FinancialModelingPrepApiService
  {
    private readonly IOptions<FinancialModelingPrepSettings> _config;
    private readonly IHttpClientFactory _client;
    private readonly FinancialModelingPrepSettings _fmpSettings;

    public FinancialModelingPrepApiService(IHttpClientFactory client, IOptions<FinancialModelingPrepSettings> config)
    {
      _client = client;
      _config = config;
      _fmpSettings = config.Value;
    }

    public async Task<T> GetAsync<T>(T fmpReturnObject, string fmpApiSelector, string symbol)
    {
      var clientFactory = _client.CreateClient();
      var path = _fmpSettings.Url + "/api/v3/income-statement/" + symbol + "?limit=120&apikey=" + _fmpSettings.ApiKey;
      var fmpObejctToReturn = await clientFactory.GetFromJsonAsync<T>(path);
      return fmpObejctToReturn;
    }


    public async Task<List<FinancialStatement>> GetFinancialStatements(string symbol)
    {
      var clientFactory = _client.CreateClient();
      var path = _fmpSettings.Url + "/api/v3/income-statement/" + symbol + "?limit=120&apikey=" + _fmpSettings.ApiKey;
      var financialStatements = await clientFactory.GetFromJsonAsync<List<FinancialStatement>>(path);
      return financialStatements;
    }
    public async Task<List<StockQuote>> GetStockQuote(string symbol)
    {

      var clientFactory = _client.CreateClient();
      var path = _fmpSettings.Url + "/api/v3/quote/" + symbol + "?limit=120&apikey=" + _fmpSettings.ApiKey;
      var stockQuote = await clientFactory.GetFromJsonAsync<List<StockQuote>>(path);
      return stockQuote;
    }
  }
}