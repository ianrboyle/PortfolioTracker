using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;
using Persistence.Logger;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Positions : BaseController
{

  private readonly IPositionLogic _positionLogic;

  public Positions(IPositionLogic positionLogic)
  {
    _positionLogic = positionLogic;
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
      await Logger.Log(ex, appUserId.ToString());

      return BadRequest();
    }
  }
}
