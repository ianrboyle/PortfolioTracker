using System.Data.Common;

namespace Domain.Models
{
  public class Sector
  {
    public Sector() { }
    public Sector(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["result_id"].ToString());
      this.SectorName = rdr["result_sectorname"].ToString();
    }
    public int Id { get; set; }
    public string SectorName { get; set; }
  }
}