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
    public Task<Position> GetPositionById(int positionId)
    {
      return _repository.GetPositionById(positionId);
    }

    public Task<List<Position>> GetUserPositions(int appUserId)
    {
      return _repository.GetUserPositions(appUserId);
    }
  }
}