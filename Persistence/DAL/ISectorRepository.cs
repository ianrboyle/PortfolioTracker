using Domain.Models;

namespace Persistence.DAL
{
  public interface ISectorRepository
  {
    Task<Sector> GetSectorBySectorName(string sectorName);
    Task AddSector(string sectorName);
  }
}