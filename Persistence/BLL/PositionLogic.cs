using Domain.Models;
using Persistence.DAL;

namespace Persistence.BLL
{
  public class PositionLogic : IPositionLogic
  {
    private readonly IPositionRetriever _retriever;
    public PositionLogic(IPositionRetriever retriever)
    {
      _retriever = retriever;

    }
    public Task<Position> GetUserPosition(int positionId)
    {
      throw new NotImplementedException();
    }

    public Task<List<Position>> GetUserPositions(int appUserId)
    {
      return _retriever.GetUserPositions(appUserId);
    }
  }
}