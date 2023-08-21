using Domain.Exceptions;
using Domain.Models;
using Npgsql;

namespace Persistence.DAL
{
  public class SectorRepository : ISectorRepository
  {
    private readonly IConnections _connections;

    public SectorRepository(IConnections connections)
    {
      _connections = connections;

    }

    public async Task AddSector(string sectorName)
    {
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT insert_sector_if_not_exists('{sectorName}')";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
          cmd.Parameters.AddWithValue("sectorname", sectorName);
          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
      }
    }

    public async Task<Sector> GetSectorBySectorName(string sectorName)
    {
      Sector sector = new();
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT * FROM get_sector_by_name('{sectorName}')";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
          cmd.Parameters.AddWithValue("sectorname", sectorName);
          // cmd.CommandType = System.Data.CommandType.StoredProcedure;

          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            sector = new Sector(rdr);
          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
        return sector;
      }
    }
  }
}