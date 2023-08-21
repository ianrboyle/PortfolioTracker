using System.Data.Common;
using Newtonsoft.Json;

namespace Domain.Models
{
  public class CompanyInformation
  {
    public CompanyInformation() { }
    public CompanyInformation(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["id"].ToString());
      this.Symbol = rdr["symbol"].ToString();
      this.CompanyName = rdr["company_name"].ToString();
      this.SectorId = Int32.Parse(rdr["sector_id"].ToString());
      this.IndustryId = Int32.Parse(rdr["industry_id"].ToString());
      this.CurrentPrice = Int32.Parse(rdr["current_price"].ToString());
      this.Country = rdr["country"].ToString();
    }
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string CompanyName { get; set; }
    public int SectorId { get; set; }
    public int IndustryId { get; set; }
    public double CurrentPrice { get; set; }
    public string Country { get; set; }
    public DateTime Created { get; set; }

    public global::StackExchange.Redis.RedisValue CompanyInfoToJson()
    {
      string json = JsonConvert.SerializeObject(this);
      return json;
    }
    public static CompanyInformation CompanyInfoFromJson(string json)
    {
      return JsonConvert.DeserializeObject<CompanyInformation>(json);
    }

  }
}