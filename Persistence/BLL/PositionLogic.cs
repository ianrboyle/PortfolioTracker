using Domain.Models;
using Persistence.DAL;

namespace Persistence.BLL
{
  public class PositionLogic : IPositionLogic
  {
    private readonly IPositionRepository _repository;
    public PositionLogic(IPositionRepository repository)
    {
      _repository = repository;

    }
    public Task<Position> GetUserPosition(int positionId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Position>> GetUserPositions(int appUserId)
    {
      return _repository.GetUserPositions(appUserId);
    }
  }
}