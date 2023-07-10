using Domain;
using Npgsql;
using Persistence.Logger;

namespace Persistence.DAL
{
  public class UserRetriever : IUserRetriever
  {
    IConnections _connections;

    ILogger _logger;

    public UserRetriever(IConnections connections, ILogger logger)
    {
      _connections = connections;
      _logger = logger;
    }
    public async Task<List<User>> GetUsers()
    {
      List<User> users = new List<User>();

      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM get_all_users()", conn);

          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            var user = new User(rdr);
            users.Add(user);
          }
        }
        catch (Exception ex)
        {
          await _logger.Log(ex);
        }
      }

      return users;
    }
  }
}


// while (await reader.ReadAsync())
// {
//   var user = new User(reader);
//   users.Add(user);
// }

// await using var command = new NpgsqlCommand(sqlQuery, conn);
// await using var reader = await command.ExecuteReaderAsync();