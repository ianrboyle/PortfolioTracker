using Domain.Models;

namespace Application.Extensions
{
  public class PositionExtensions
  {
    public static float CalculateTotalCostBasis(Position position)
    {
      return position.SharesOwned * position.AverageCostBasis;
    }
  }
}