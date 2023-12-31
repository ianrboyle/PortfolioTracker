using System.Globalization;
using Domain;
using Domain.Exceptions;
using Npgsql;


namespace Persistence.DAL
{
  public class UserRepository : IUserRepository
  {
    IConnections _connections;

    public UserRepository(IConnections connections)
    {
      _connections = connections;
    }

    public async Task DeleteUser(int userId)
    {
      await using NpgsqlConnection conn = _connections.GetConnection();
      try
      {
        if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }

        var sqlString = $" SELECT Delete_User_By_UserId('{userId}');";
        await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
        cmd.Parameters.AddWithValue("userId", userId);

        await using var rdr = await cmd.ExecuteReaderAsync();

      }
      catch (Exception ex)
      {
        CustomException cex = new CustomException(ex.Message, 400, ex);
        throw cex;
      }
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

          while (await rdr.ReadAsync())
          {
            user = new User(rdr);

          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
        return user;
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
          CustomException cex = new(ex.Message, 400, ex);
          throw cex;
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
            var user = MapUserFromDataReader(rdr);
            users.Add(user);
          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
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


        var sqlString = $"CALL insert_app_user('{user.UserName}');";
        await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
        cmd.Parameters.AddWithValue("user_name", user.UserName);

        await using var rdr = await cmd.ExecuteReaderAsync();

      }
      catch (Exception ex)
      {
        CustomException cex = new CustomException(ex.Message, 400, ex);
        throw cex;
      }
    }

    private User MapUserFromDataReader(NpgsqlDataReader rdr)
    {
      int id = Int32.Parse(rdr["id"].ToString());
      string userName = rdr["user_name"].ToString();
      var created = DateTime.Parse(rdr["created"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None);


      return new User
      {
        Id = id,
        UserName = userName,
        Created = created
      };
    }
  }
}


