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

      //   using (NpgsqlConnection conn = _connections.GetConnection())
      // var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connections.GetConnection());
      // var connectionString = dataSourceBuilder.ConnectionString;
      await using var conn = _connections.GetConnection();
      string sqlQuery = "SELECT * FROM appusers";
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          await using var command = new NpgsqlCommand(sqlQuery, conn);
          await using var reader = await command.ExecuteReaderAsync();
          //   if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          //   NpgsqlCommand cmd = new NpgsqlCommand("get_users", conn);
          //   cmd.CommandType = System.Data.CommandType.StoredProcedure;

          //   var rdr = await command.ExecuteReaderAsync();
          while (await reader.ReadAsync())
          {
            var user = new User(reader);
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