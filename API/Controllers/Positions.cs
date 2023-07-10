using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;
using Persistence.Logger;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Positions : ControllerBase
{

  private readonly Persistence.Logger.ILogger _logger;
  private readonly IPositionLogic _positionLogic;

  public Positions(Persistence.Logger.ILogger logger, IPositionLogic positionLogic)
  {
    _positionLogic = positionLogic;
    _logger = logger;
  }

  [HttpGet("{appUserId}")]
  public async Task<ActionResult<List<Position>>> GetUserPositions(int appUserId)
  {
    try
    {
      List<Position> positions = await _positionLogic.GetUserPositions(appUserId);

      return Ok(positions);
    }
    catch (Exception ex)
    {
      await _logger.Log(ex);

      return BadRequest();
    }
  }
}
