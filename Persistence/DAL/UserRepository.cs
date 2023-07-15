using Domain;
using Npgsql;
using Persistence.Logger;

namespace Persistence.DAL
{
  public class UserRepository : IUserRepository
  {
    IConnections _connections;

    ILogger _logger;

    public UserRepository(IConnections connections, ILogger logger)
    {
      _connections = connections;
      _logger = logger;
    }

    public async Task<User> GetUser(int userId)
    {
      var user = new User();
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT * FROM get_user_by_id({userId})";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
          cmd.Parameters.AddWithValue("id", userId);
          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            user = new User(rdr);
            return user;
          }
        }
        catch (Exception ex)
        {
          await _logger.Log(ex);
        }

      }
      return user;
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

    public async Task<User> SignUpUser(User user)
    {
      await using NpgsqlConnection conn = _connections.GetConnection();
      try
      {
        if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
        await using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM get_all_users()", conn);

        // cmd.CommandType = System.Data.CommandType.StoredProcedure;

        await using var rdr = await cmd.ExecuteReaderAsync();

        while (await rdr.ReadAsync())
        {
          user = new User(rdr);
        }
      }
      catch (Exception ex)
      {
        await _logger.Log(ex);
      }
      return user;
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