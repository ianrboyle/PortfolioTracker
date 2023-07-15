using System.Data.Common;
using System.Globalization;

namespace Domain
{
  public class User
  {
    public User() { }
    public User(DbDataReader rdr)
    {
      this.Id = Int32.Parse(rdr["id"].ToString());
      this.UserName = rdr["user_name"].ToString();
      this.Created = DateTime.Parse(rdr["created"].ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None);

    }
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime Created { get; set; }
  }
}