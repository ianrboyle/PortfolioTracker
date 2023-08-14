using Persistence.Logger;
using Moq;
using Persistence.BLL;
using Application.DTOs;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;

namespace PFTests
{
  public class PositionTests
  {
    private readonly Mock<IPositionLogic> _positionLogic;
    private readonly Mock<ILogger> _logger;

    public PositionTests()
    {
      _positionLogic = new Mock<IPositionLogic>();
      _logger = new Mock<ILogger>();
    }

    [Fact]
    async Task GetPositionById_Success()
    {
      PositionDto position = new()
      {
        Id = 1,
        Symbol = "BTU",
        AverageCostBasis = 15,
        SharesOwned = 10,
        SectorName = "Energy",
        IndustryName = "Coal"
      };

      _positionLogic.Setup(ul => ul.GetPositionById(1)).ReturnsAsync(position);

      var controller = new Positions(_positionLogic.Object, _logger.Object);

      // Act
      var result = await controller.GetPositionById(1);

      // Assert
      Assert.IsType<OkObjectResult>(result.Result);

      var okResult = result.Result as OkObjectResult;
      Assert.Equal(position, okResult.Value);
      Assert.Equal(position.CurrentTotalValue, 150);
    }
    [Fact]
    async Task GetPositionById_Failure()
    {
      PositionDto position = new()
      {
        Id = 0,
        Symbol = null,
        AverageCostBasis = 0,
        SharesOwned = 0,
        SectorName = null,
        IndustryName = null
      };

      string exceptionString = $"Position with ID: 0 not found.";
      NotFoundException ex = new NotFoundException(exceptionString);

      _positionLogic.Setup(ul => ul.GetPositionById(1)).ThrowsAsync(ex);

      var controller = new Positions(_positionLogic.Object, _logger.Object);

      // Act
      var result = await controller.GetPositionById(1);

      // Assert
      Assert.IsType<BadRequestResult>(result.Result);

      // var okResult = result.Result as OkObjectResult;
      var statusCode = result.Result.ToString();
      // Assert
      Assert.Contains("Bad", statusCode);
      // Assert.Equal(position.CurrentTotalValue, 150);
    }

  }
}