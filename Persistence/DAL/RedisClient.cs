using Domain.Models;
using Persistence.Logger;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using Application.Services;
using Domain.Exceptions;

namespace Persistence.DAL
{
  public class RedisClient : IRedisClient
  {
    private readonly ConnectionMultiplexer _redis;
    private readonly FinancialModelingPrepApiService _fmpService;
    public RedisClient(FinancialModelingPrepApiService fmpService)
    {
      _fmpService = fmpService;
      _redis = ConnectionMultiplexer.Connect("localhost");
    }

    public async Task AddCompanyInfoToRedis(CompanyInformation companyInfo)
    {
      try
      {
        await _redis.GetDatabase().StringSetAsync(companyInfo.Symbol, ConvertCompanyInformationToRedisValue(companyInfo));
      }
      catch (System.Exception ex)
      {
        CustomException cex = new CustomException(ex.Message, 400, ex);
        throw cex;
      }

    }

    public async Task<CompanyInformation> GetCompanyInfoBySymbol(string symbol)
    {
      try
      {
        var redisValue = await _redis.GetDatabase().StringGetAsync(symbol);

        if (!redisValue.IsNullOrEmpty)
        {
          return CompanyInformation.CompanyInfoFromJson(redisValue);
        }
        return new CompanyInformation();
      }
      catch (System.Exception ex)
      {
        CustomException cex = new CustomException(ex.Message, 400, ex);
        throw cex;
      }





    }



    // Implement methods to convert between RedisValue and CompanyInformation
    private RedisValue ConvertCompanyInformationToRedisValue(CompanyInformation companyInfo)
    {
      // Convert CompanyInformation to JSON string or other format as needed
      return companyInfo.CompanyInfoToJson();  // Example, assuming ToJson() method converts to JSON string
    }


  }
}