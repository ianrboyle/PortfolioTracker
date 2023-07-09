using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence.BLL;
using Persistence.Logger;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : ControllerBase
{

  private readonly Persistence.Logger.ILogger _logger;
  private readonly IUserLogic _userLogic;

  public Users(Persistence.Logger.ILogger logger, IUserLogic userLogic)
  {
    _userLogic = userLogic;
    _logger = logger;
  }

  [HttpGet]
  public async Task<ActionResult<List<User>>> GetUsers()
  {
    try
    {
      List<User> investors = await _userLogic.GetUsers();

      return Ok(investors);
    }
    catch (Exception ex)
    {
      await _logger.Log(ex);

      return BadRequest();
    }
  }
}
