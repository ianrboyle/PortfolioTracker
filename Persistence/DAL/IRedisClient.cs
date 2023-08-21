using Domain.Models;

namespace Persistence.DAL
{
  public interface IRedisClient
  {
    Task<CompanyInformation> GetCompanyInfoBySymbol(string symbol);
    Task AddCompanyInfoToRedis(CompanyInformation companyInfo);
  }
}