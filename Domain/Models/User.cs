using System.Data.Common;
using System.Globalization;

namespace Domain
{
  public class User
  {

    public User(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["id"].ToString());
      this.UserName = rdr["username"].ToString();
      this.DateOfBirth = DateTime.Parse(rdr["dateofbirth"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None);
      // this.Created = DateTime.Parse(rdr["dateofbirth"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None);
      // this.InvestingStyle = rdr["investingstyle"].ToString();
      // this.City = rdr["city"].ToString();
      // this.Country = rdr["country"].ToString();
    }
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime Created { get; set; }
    public string InvestingStyle { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
  }
}