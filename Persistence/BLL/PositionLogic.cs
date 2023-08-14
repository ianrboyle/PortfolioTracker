using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Persistence.DAL;

namespace Persistence.BLL
{
  public class PositionLogic : IPositionLogic
  {
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;
    public PositionLogic(IPositionRepository repository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = repository;

    }
    public async Task<PositionDto> GetPositionById(int positionId)
    {

      var position = await _repository.GetPositionById(positionId);
      var positionDto = MapPosition(position);
      return positionDto;

    }

    public Task<List<Position>> GetUserPositions(int appUserId)
    {
      return _repository.GetUserPositions(appUserId);
    }

    private PositionDto MapPosition(Position position)
    {
      PositionDto positionDto = new();
      return _mapper.Map<Position, PositionDto>(position);
    }


  }
}