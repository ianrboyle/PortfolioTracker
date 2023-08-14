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
      return _mapper.Map<Position, PositionDto>(position);

    }

    public async Task<List<PositionDto>> GetUserPositions(int appUserId)
    {
      var positions = await _repository.GetUserPositions(appUserId);
      return _mapper.Map<List<Position>, List<PositionDto>>(positions);
    }


  }
}