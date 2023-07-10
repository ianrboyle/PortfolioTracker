using Domain.Models;

namespace Persistence.DAL
{
  public interface IPositionRetriever
  {
    Task<List<Position>> GetUserPositions(int appUserId);
    Task<Position> GetUserPosition(int positionId);
  }
}