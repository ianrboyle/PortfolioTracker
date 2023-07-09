using System;
using Npgsql;

namespace Persistence.DAL
{
  public interface IConnections
  {
    NpgsqlConnection GetConnection();
  }
}
