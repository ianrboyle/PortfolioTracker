using System;
using System.Threading.Tasks;

namespace Persistence.Logger
{
  public interface ILogger
  {
    Task Log(Exception exception);

    Task Log(Exception exception, string userId);
  }
}
