using Domain.Exceptions;
using Domain.Models;
using Npgsql;
using Persistence.Logger;

namespace Persistence.DAL
{
  public class PositionRepository : IPositionRepository
  {
    IConnections _connections;

    ILogger _logger;

    public PositionRepository(IConnections connections, ILogger logger)
    {
      _connections = connections;
      _logger = logger;
    }
    public async Task<Position> GetPositionById(int positionId)
    {
      Position position = new();

      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = "SELECT * FROM get_position_by_position_id(@p_id)";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

          cmd.Parameters.AddWithValue("p_id", positionId);

          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            position = new Position(rdr);
          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
      }

      return position;
    }

    public async Task<List<Position>> GetUserPositions(int appUserId)
    {
      List<Position> positions = new List<Position>();

      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = "SELECT * FROM get_positions_by_appuserid(@appUserId)";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

          cmd.Parameters.AddWithValue("appUserId", appUserId);

          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            var position = new Position(rdr);
            positions.Add(position);
          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
      }

      return positions;
    }
  }
}
