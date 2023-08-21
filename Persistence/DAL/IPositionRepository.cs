using Domain.Models;

namespace Persistence.DAL
{
  public interface IPositionRepository
  {
    Task<List<Position>> GetUserPositions(int appUserId);
    Task<Position> GetPositionById(int positionId);
    Task AddNewPosition(Position position);
  }
}