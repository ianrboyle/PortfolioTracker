using Domain.Models;

namespace Persistence.DAL
{
  public interface IIndustryRepository
  {
    Task<Industry> GetIndustryByIndustryName(string industryName);
    Task AddIndustry(string industryName, int sectorId);
  }
}