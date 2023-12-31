// Test Fixture
using Persistence.Logger;
using Moq;
using Persistence.BLL;
using Domain;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Exceptions;

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
    string exceptionString = "User with ID: 0 not found.";
    CustomException ex = new CustomException(exceptionString, 404);
    _userLogic.Setup(ul => ul.GetUserById(0)).ThrowsAsync(ex);

    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    var result = await controller.GetUser(0);
    var badRequestResult = Assert.IsType<ObjectResult>(result.Result);
    Assert.Equal(404, badRequestResult.StatusCode);

    var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);

    Assert.Equal("An error occurred while fetching the user.", errorResponse.Error);
    Assert.Equal("User with ID: 0 not found.", errorResponse.Details);
    Assert.Equal(404, errorResponse.StatusCode);

  }
  [Fact]
  public async Task SignupUser_Success()
  {
    // var mockUserLogic = new Mock<IUserLogic>();
    User user = new User
    {
      UserName = "Jogn"
    };

    // Assuming _userLogic is a mock of IUserLogic
    _userLogic.Setup(ul => ul.SignUpUser(user)).Returns(Task.CompletedTask);
    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    await controller.SignUpUser(user);
    // Assert
    _userLogic.Verify(ul => ul.SignUpUser(user), Times.Once);

    // Act
    IActionResult result = await controller.SignUpUser(user);

    // Assert
    Assert.IsType<OkObjectResult>(result);
    var okResult = (OkObjectResult)result;
    Assert.Equal("Sign-up successful.", okResult.Value);
  }


  [Fact]
  public async Task SignUpUser_UsernameAlreadyExists()
  {
    // Arrange
    User user = new User
    {
      UserName = "John"
    };

    var _userLogicMock = new Mock<IUserLogic>();
    string exceptionString = "Username 'John' already exists.";
    CustomException ex = new CustomException(exceptionString, 409);
    _userLogicMock.Setup(ul => ul.SignUpUser(user)).ThrowsAsync(ex);


    var controller = new Users(_userLogicMock.Object, _logger.Object);

    // Act
    var result = await controller.SignUpUser(user);

    // Assert
    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(409, objectResult.StatusCode);

    var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);

    Assert.Equal("An error occurred while signing up the user.", errorResponse.Error);
    Assert.Equal("Username 'John' already exists.", errorResponse.Details);
    Assert.Equal(409, errorResponse.StatusCode);
    Assert.IsType<ObjectResult>(result);

    _userLogicMock.Verify(ul => ul.SignUpUser(user), Times.Once);
    _logger.Verify(logger => logger.Log(It.IsAny<CustomException>()), Times.Once);
  }

  [Fact]
  public async Task DeleteUser_Success()
  {
    // Arrange

    var _userLogicMock = new Mock<IUserLogic>();
    _userLogicMock.Setup(ul => ul.DeleteUser(1)).Returns(Task.CompletedTask);
    var controller = new Users(_userLogic.Object, _logger.Object);

    // Act
    await controller.DeleteUser(1);
    // Assert
    _userLogic.Verify(ul => ul.DeleteUser(1), Times.Once);

    // Act
    IActionResult result = await controller.DeleteUser(1);

    // Assert
    Assert.IsType<OkObjectResult>(result);
    var okResult = (OkObjectResult)result;
    Assert.Equal("Deletion successful.", okResult.Value);
  }

}
