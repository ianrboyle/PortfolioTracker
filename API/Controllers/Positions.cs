using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Positions : BaseController
{

  private readonly IPositionLogic _positionLogic;
  private readonly Persistence.Logger.ILogger _logger;

  public Positions(IPositionLogic positionLogic, Persistence.Logger.ILogger logger)
  {
    _logger = logger;
    _positionLogic = positionLogic;
  }

  [HttpGet("{appUserId}")]
  public async Task<ActionResult<List<PositionDto>>> GetUserPositions(int appUserId)
  {
    try
    {
      List<PositionDto> positions = await _positionLogic.GetUserPositions(appUserId);

      return Ok(positions);
    }
    catch (Exception ex)
    {
      await _logger.Log(ex, appUserId.ToString());

      return BadRequest();
    }
  }
  [HttpGet("position/{positionId}")]
  public async Task<ActionResult<PositionDto>> GetPositionById(int positionId)
  {
    try
    {
      PositionDto position = await _positionLogic.GetPositionById(positionId);

      return Ok(position);
    }
    catch (Exception ex)
    {
      await _logger.Log(ex);

      return BadRequest();
    }
  }
}

