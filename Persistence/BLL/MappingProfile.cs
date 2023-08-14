using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Persistence.BLL
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Position, PositionDto>()
           .ReverseMap();
    }

  }
}