using System;
using System.Threading.Tasks;

namespace Persistence.DAL
{
  public interface IDataLogger
  {
    Task LogError(Exception exception, string userId = null);
  }
}
