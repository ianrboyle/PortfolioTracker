using Application.DTOs;
using Application.Services;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.FinancialModelingPrep;
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
    catch (CustomException ex)
    {
      await _logger.Log(ex);

      var errorResponse = new ErrorResponse
      {
        Error = "An error occurred while fetching the position.",
        Details = ex.Message,
        StatusCode = ex.StatusCode
      };

      return StatusCode(ex.StatusCode, errorResponse);
    }
  }

  [HttpPost("add/{symbol}")]
  public async Task<IActionResult> AddPosition([FromBody] Position position)
  {
    try
    {
      await _positionLogic.AddPosition(position);
      return Ok("Success");
    }

    catch (CustomException ex)
    {
      await _logger.Log(ex);

      var errorResponse = new ErrorResponse
      {
        Error = "An error occurred while adding the position.",
        Details = ex.Message,
        StatusCode = ex.StatusCode
      };

      return StatusCode(ex.StatusCode, errorResponse);
    }

  }


}

