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
  private readonly FinancialModelingPrepApiService _fmpService;

  public Positions(IPositionLogic positionLogic, Persistence.Logger.ILogger logger, FinancialModelingPrepApiService fmpService)
  {
    _fmpService = fmpService;
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

  [HttpGet("{symbol}/statement")]
  public async Task<ActionResult<List<FinancialStatement>>> GetCompanyStatement(string symbol)
  {
    var statements = await _fmpService.GetFinancialStatements(symbol);
    return statements;
  }
  [HttpGet("{symbol}/quote")]
  public async Task<ActionResult<List<StockQuote>>> GetStockQuote(string symbol)
  {
    var quote = await _fmpService.GetStockQuote(symbol);
    return quote;
  }
}

