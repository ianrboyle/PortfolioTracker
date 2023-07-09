using System;
using System.Threading.Tasks;
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

    public async Task LogError(Exception exception, string userId = null)
    {
      using (NpgsqlConnection conn = _connections.GetConnection())
      {
        try
        {
          //   if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          //   NpgsqlCommand cmd = new NpgsqlCommand("log_error", conn);
          //   cmd.CommandType = System.Data.CommandType.StoredProcedure;
          //   cmd.Parameters.AddWithValue("userid", string.IsNullOrEmpty(userId) ? "" : userId);
          //   cmd.Parameters.AddWithValue("message", exception.Message);
          //   cmd.Parameters.AddWithValue("stacktrace", exception.StackTrace ?? "");
          //   cmd.Parameters.AddWithValue("innerexception", exception.InnerException == null ? "" : exception.InnerException.Message);
          //   cmd.Parameters.AddWithValue("source", exception.Source);

          //   await cmd.ExecuteScalarAsync();
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
