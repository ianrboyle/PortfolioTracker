using Domain.Exceptions;
using Domain.Models;
using Npgsql;

namespace Persistence.DAL
{

  public class IndustryRepository : IIndustryRepository
  {
    private readonly IConnections _connections;

    public IndustryRepository(IConnections connections)
    {
      _connections = connections;

    }
    public async Task AddIndustry(string industryName, int sectorId)
    {
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT insert_industry_if_not_exists('{industryName}', {sectorId})";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

          cmd.Parameters.AddWithValue("industry_name_param", industryName);
          cmd.Parameters.AddWithValue("sector_id_param", sectorId);


          await using var rdr = await cmd.ExecuteReaderAsync();

        }
        catch (Exception ex)
        {
          CustomException cex = new(ex.Message, 400, ex);
          throw cex;
        }
      }
    }

    public async Task<Industry> GetIndustryByIndustryName(string industryName)
    {
      Industry industry = new();
      await using NpgsqlConnection conn = _connections.GetConnection();
      {
        try
        {
          if (conn.State != System.Data.ConnectionState.Open) { await conn.OpenAsync(); }
          var sqlString = $"SELECT * FROM get_industry_by_name('{industryName}')";
          await using NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
          cmd.Parameters.AddWithValue("industryname", industryName);


          await using var rdr = await cmd.ExecuteReaderAsync();

          while (await rdr.ReadAsync())
          {
            industry = new Industry(rdr);
          }
        }
        catch (Exception ex)
        {
          CustomException cex = new CustomException(ex.Message, 400, ex);
          throw cex;
        }
        return industry;
      }
    }
  }
}