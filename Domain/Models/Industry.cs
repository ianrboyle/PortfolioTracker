using System.Data.Common;

namespace Domain.Models
{
  public class Industry
  {

    public Industry() { }
    public Industry(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["result_id"].ToString());
      this.SectorId = Int32.Parse(rdr["result_sectorid"].ToString());
      this.IndustryName = rdr["result_industryname"].ToString();

    }
    public int Id { get; set; }
    public string IndustryName { get; set; }
    public int SectorId { get; set; }
  }
}