using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;
using Persistence.Logger;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : BaseController
{


  private readonly IUserLogic _userLogic;

  public Users(IUserLogic userLogic)
  {
    _userLogic = userLogic;
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
      User user = await _userLogic.GetUser(userId);

      return Ok(user);
    }
    catch (Exception ex)
    {
      await Logger.Log(ex);

      return BadRequest();
    }
  }
}
