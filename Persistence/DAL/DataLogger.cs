using Npgsql;

namespace Persistence.DAL
{
  public class DataLogger : IDataLogger
  {
    private IConnections _connections;

    public DataLogger(IConnections connections)
    {
      _connections = connections;
    }

    public async Task LogError(Exception exception, string appUserId = null)
    {
      using (NpgsqlConnection conn = _connections.GetConnection())
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          NpgsqlCommand cmd = new NpgsqlCommand("log_error", conn);
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("app_user_id", string.IsNullOrEmpty(appUserId) ? (object)DBNull.Value : int.Parse(appUserId));
          cmd.Parameters.AddWithValue("message", exception.Message);
          cmd.Parameters.AddWithValue("stack_trace", exception.StackTrace ?? "");
          cmd.Parameters.AddWithValue("inner_exception", exception.InnerException?.Message ?? "");
          cmd.Parameters.AddWithValue("source", exception.Source);

          await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          Console.WriteLine(ex.StackTrace ?? "no stack trace");
        }
      }
    }
  }
}
