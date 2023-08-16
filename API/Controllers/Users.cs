using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;
using Persistence.Logger;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : BaseController
{


  private readonly IUserLogic _userLogic;
  private readonly Persistence.Logger.ILogger _logger;

  public Users(IUserLogic userLogic, Persistence.Logger.ILogger logger)
  {
    _logger = logger;
    _userLogic = userLogic;
  }
  [HttpPost]
  public async Task<IActionResult> SignUpUser([FromBody] User appUser)
  {
    try
    {
      await _userLogic.SignUpUser(appUser);
      return Ok("Sign-up successful.");

    }
    catch (CustomException ex)
    {
      var errorResponse = new ErrorResponse
      {
        Error = "An error occurred while signing up the user.",
        Details = ex.Message,
        StatusCode = ex.StatusCode
      };
      await _logger.Log(ex);
      return StatusCode(ex.StatusCode, errorResponse);
    }
  }

  [HttpGet]
  public async Task<ActionResult<List<User>>> GetUsers()
  {
    try
    {
      List<User> users = await _userLogic.GetUsers();

      return Ok(users);
    }
    catch (Exception ex)
    {
      await Logger.Log(ex);

      return BadRequest();
    }
  }

  [HttpGet("{userId}")]
  public async Task<ActionResult<User>> GetUser(int userId)
  {
    try
    {
      User user = await _userLogic.GetUserById(userId);

      return Ok(user);
    }
    catch (CustomException ex)
    {
      await _logger.Log(ex);

      var errorResponse = new ErrorResponse
      {
        Error = "An error occurred while fetching the user.",
        Details = ex.Message,
        StatusCode = ex.StatusCode
      };

      return StatusCode(ex.StatusCode, errorResponse);
    }
  }

  [HttpDelete("{userId}")]
  public async Task<IActionResult> DeleteUser(int userId)
  {
    await _userLogic.DeleteUser(userId);
    return Ok("Deletion successful.");
  }
}
