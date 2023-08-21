using Domain.Models;
using StackExchange.Redis;
using Application.Services;
using Domain.Exceptions;

namespace Persistence.DAL
{
  public class RedisClient : IRedisClient
  {

    private readonly IDatabase _redisDatabase;

    public RedisClient(IConnectionMultiplexer connectionMultiplexer)
    {
      _redisDatabase = connectionMultiplexer.GetDatabase();
    }

    public async Task AddCompanyInfoToRedis(CompanyInformation companyInfo)
    {
      try
      {
        await _redisDatabase.StringSetAsync(companyInfo.Symbol, ConvertCompanyInformationToRedisValue(companyInfo));
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
        var redisValue = await _redisDatabase.StringGetAsync(symbol);

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


    private RedisValue ConvertCompanyInformationToRedisValue(CompanyInformation companyInfo)
    {
      return companyInfo.CompanyInfoToJson();
    }


  }
}