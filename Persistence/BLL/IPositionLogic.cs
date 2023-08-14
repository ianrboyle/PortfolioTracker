using Application.DTOs;
using Domain.Models;

namespace Persistence.BLL
{
  public interface IPositionLogic
  {
    Task<List<PositionDto>> GetUserPositions(int appUserId);
    Task<PositionDto> GetPositionById(int positionId);
  }
}