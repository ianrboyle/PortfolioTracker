using Microsoft.Extensions.Options;
using Domain.Models.FinancialModelingPrep;
using System.Net.Http.Json;
using Domain.Exceptions;

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
      try
      {
        var clientFactory = _client.CreateClient();
        var path = _fmpSettings.Url + "/api/v3/quote/" + symbol + "?limit=120&apikey=" + _fmpSettings.ApiKey;
        var stockQuotes = await clientFactory.GetFromJsonAsync<List<StockQuote>>(path);
        return stockQuotes;
      }
      catch (System.Exception ex)
      {
        string exceptionString = $"Error calling FinancialModelingPrep API.";
        CustomException cex = new CustomException(exceptionString, 400, ex);
        throw cex;
      }

    }
    public async Task<List<CompanyProfile>> GetCompanyProfile(string symbol)
    {

      var clientFactory = _client.CreateClient();
      var path = _fmpSettings.Url + "/api/v3/profile/" + symbol + "?limit=120&apikey=" + _fmpSettings.ApiKey;
      var profiles = await clientFactory.GetFromJsonAsync<List<CompanyProfile>>(path);
      return profiles;
    }
  }
}