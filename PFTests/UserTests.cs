// Test Fixture
using Persistence.DAL;
using Persistence.Logger;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using Persistence.BLL;
using Domain;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

public class UserRepositoryTests

{
  private readonly Mock<IUserLogic> _userLogic;
  private readonly Mock<ILogger> _logger;

  public UserRepositoryTests()
  {
    _userLogic = new Mock<IUserLogic>();
    _logger = new Mock<ILogger>();
  }

  // Write tests for UserRepository methods
  [Fact]
  public async Task GetUserById_Should_Return_User_IfExists()
  {

    // var mockUserLogic = new Mock<IUserLogic>();
    List<User> mockUsers = new List<User>
        {
            new User { Id = 1, UserName = "John" },
            new User { Id = 2, UserName = "Jane" }
        };

    _userLogic.Setup(ul => ul.GetUsers()).ReturnsAsync(mockUsers);

    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    var result = await controller.GetUsers();

    // Assert
    Assert.IsType<OkObjectResult>(result.Result);

    var okResult = result.Result as OkObjectResult;
    Assert.Equal(mockUsers, okResult.Value);
  }
  [Fact]
  public async Task GetUsers_ReturnsBadRequestOnException()
  {
    // var mockUserLogic = new Mock<IUserLogic>();
    List<User> mockUsers = new List<User>
    {
    };

    _userLogic.Setup(ul => ul.GetUsers()).ReturnsAsync(mockUsers);

    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    var result = await controller.GetUsers();

    // Assert
    Assert.IsType<OkObjectResult>(result.Result);

    var okResult = result.Result as OkObjectResult;
    Assert.Equal(mockUsers, okResult.Value);
  }

  [Fact]
  public async Task GetUserById_ReturnsUserIfExists()
  {
    // var mockUserLogic = new Mock<IUserLogic>();
    User user = new User
    {
      Id = 1,
      UserName = "Jogn"
    };

    _userLogic.Setup(ul => ul.GetUserById(1)).ReturnsAsync(user);

    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    var result = await controller.GetUser(1);

    // Assert
    Assert.IsType<OkObjectResult>(result.Result);

    var okResult = result.Result as OkObjectResult;
    Assert.Equal(user, okResult.Value);
  }

  [Fact]
  public async Task GetUserById_ReturnsBadRequest()
  {
    // var mockUserLogic = new Mock<IUserLogic>();
    User user = new User
    {
      Id = 1,
      UserName = "Jogn"
    };

    // Assuming _userLogic is a mock of IUserLogic
    _userLogic.Setup(ul => ul.GetUserById(0)).ThrowsAsync(new Exception("An error occurred while fetching the user."));

    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    var result = await controller.GetUser(0);
    var statusCode = result.Result.ToString();
    // Assert
    Assert.Contains("Bad", statusCode);
    // var badRequestResult = result as BadRequestObjectResult;
    // Assert.Equal("An error occurred while fetching the user.", result.Value);
  }

}