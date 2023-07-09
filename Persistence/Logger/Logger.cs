using Persistence.DAL;

namespace Persistence.Logger
{
  public class Logger : ILogger
  {

    private IDataLogger _dataLogger;

    public Logger(IDataLogger dataLogger)
    {
      _dataLogger = dataLogger;
    }

    public async Task Log(Exception exception)
    {
      await _dataLogger.LogError(exception);
    }

    public async Task Log(Exception exception, string userId)
    {
      await _dataLogger.LogError(exception, userId);
    }
  }
}
