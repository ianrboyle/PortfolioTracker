using Domain.Exceptions;
using Domain.Models;
using Npgsql;
using NpgsqlTypes;

namespace Persistence.DAL
{
  public class PositionRepository : IPositionRepository
  {
    IConnections _connections;

    public PositionRepository(IConnections connections)
    {
      _connections = connections;
    }

    public async Task AddNewPosition(Position position)
    {

      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }


          await using var cmd = new NpgsqlCommand("insert_position", conn);
          cmd.CommandType = System.Data.CommandType.StoredProcedure;

          cmd.Parameters.Add(new NpgsqlParameter("p_symbol", NpgsqlDbType.Text) { Value = position.Symbol });
          cmd.Parameters.Add(new NpgsqlParameter("p_sharesowned", NpgsqlDbType.Numeric) { Value = position.SharesOwned });
          cmd.Parameters.Add(new NpgsqlParameter("p_averagecostbasis", NpgsqlDbType.Numeric) { Value = position.AverageCostBasis });
          cmd.Parameters.Add(new NpgsqlParameter("p_sectorid", NpgsqlDbType.Integer) { Value = position.SectorId });
          cmd.Parameters.Add(new NpgsqlParameter("p_industryid", NpgsqlDbType.Integer) { Value = position.IndustryId });
          cmd.Parameters.Add(new NpgsqlParameter("p_appuserid", NpgsqlDbType.Integer) { Value = position.AppUserId });
          await using var rdr = await cmd.ExecuteReaderAsync();

        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
      }

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
