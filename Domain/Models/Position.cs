using System.Data.Common;

namespace Domain.Models
{
  public class Position
  {

    public Position() { }
    public Position(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["position_id"].ToString());
      this.Symbol = rdr["symbol"].ToString();
      this.SharesOwned = float.Parse(rdr["shares_owned"].ToString());
      this.AverageCostBasis = float.Parse(rdr["average_cost_basis"].ToString());

      this.SectorName = rdr["sector_name"].ToString();
      this.IndustryName = rdr["industry_name"].ToString();
      //   this.AppUserId = Int32.Parse(rdr["app_user_id"].ToString());
      // this.Created = DateTime.Parse(rdr["dateofbirth"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None);
      // this.InvestingStyle = rdr["investingstyle"].ToString();
      // this.City = rdr["city"].ToString();
      // this.Country = rdr["country"].ToString();
    }
    public int Id { get; set; }
    public string Symbol { get; set; }
    public float SharesOwned { get; set; }
    public float AverageCostBasis { get; set; }
    public int AppUserId { get; set; }
    public int SectorId { get; set; }
    public int IndustryId { get; set; }
    public string SectorName { get; set; }
    public string IndustryName { get; set; }
  }
}

