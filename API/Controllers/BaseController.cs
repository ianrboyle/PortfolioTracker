using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BaseController : ControllerBase
  {
    private Persistence.Logger.ILogger _logger;

    protected Persistence.Logger.ILogger Logger => _logger ??=
      HttpContext.RequestServices.GetService<Persistence.Logger.ILogger>();
  }
}