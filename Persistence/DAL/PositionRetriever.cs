using Domain.Models;
using Npgsql;
using Persistence.Logger;

namespace Persistence.DAL
{
  public class PositionRetriever : IPositionRetriever
  {
    IConnections _connections;

    ILogger _logger;

    public PositionRetriever(IConnections connections, ILogger logger)
    {
      _connections = connections;
      _logger = logger;
    }
    public Task<Position> GetUserPosition(int positionId)
    {
      throw new NotImplementedException();
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
          await _logger.Log(ex);
        }
      }

      return positions;
    }
  }
}
