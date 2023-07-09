using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Persistence.DAL
{
  public class Connections : IConnections
  {
    private IConfiguration _config;
    public Connections(IConfiguration configuration)
    {
      _config = configuration;
    }

    public NpgsqlConnection GetConnection()
    {
      return new NpgsqlConnection(_config.GetConnectionString("PORTFOLIOTRACKER"));
    }
  }
}