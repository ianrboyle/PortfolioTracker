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

    public async Task<User> GetUserById(int userId)
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

          if (await rdr.ReadAsync())
          {
            user = new User(rdr);
            return user;
          }
          {
            // No user found for the given ID
            throw new ArgumentException("Invalid user ID.");
          }
        }
        catch (NpgsqlException ex)
        {
          // Handle PostgreSQL-specific exceptions
          await _logger.Log(ex);
          throw; // Re-throw the exception to propagate it further
        }
        catch (Exception ex)
        {
          // Handle general exceptions
          await _logger.Log(ex);
          throw new Exception("An error occurred while fetching the user."); // Throw a custom exception message
        }
      }
    }
    public async Task<User> GetUserByUserName(string userName)
    {
      var user = new User();
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT * FROM app_users WHERE user_name = '{userName}';";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
          cmd.Parameters.AddWithValue("user_name", userName);
          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          if (await rdr.ReadAsync())
          {
            var existingUser = new User(rdr);
            return existingUser;
          }

          return null;
        }
        catch (Exception ex)
        {
          await _logger.Log(ex);
          return null;

        }
      }
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

    public async Task SignUpUser(User user)
    {
      await using NpgsqlConnection conn = _connections.GetConnection();
      try
      {
        if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }

        var existingUser = await GetUserByUserName(user.UserName);
        if (existingUser != null)
        {
          // Username already exists, handle the error or return an appropriate response
          // For example, you can throw an exception or return a specific result indicating the error
          throw new Exception("Username already exists.");
        }

        var sqlString = $"CALL insert_app_user('{user.UserName}');";
        await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
        cmd.Parameters.AddWithValue("user_name", user.UserName);

        await using var rdr = await cmd.ExecuteReaderAsync();

      }
      catch (Exception ex)
      {
        await _logger.Log(ex);
      }
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