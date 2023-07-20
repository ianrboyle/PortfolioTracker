using System.Net.Http;
using Domain;
using Newtonsoft;
using Newtonsoft.Json;
using Moq;
using Npgsql;
using Persistence.DAL;
using System.Text;

namespace PFTests;

public class UserTests
{
  private readonly HttpClient _client;
  private readonly Mock<IConnections> _connections;

  public UserTests()
  {
    _client = new HttpClient();
    _connections = new Mock<IConnections>();
  }
  [Fact]
  public async Task GetUserTest_Success()
  {
    var user = new User
    {
      Id = 13,
      UserName = "whatever"
    };
    var response = await _client.GetAsync("https://localhost:5001/users/13");
    var responseString = await response.Content.ReadAsStringAsync();
    User getUser = JsonConvert.DeserializeObject<User>(responseString);
    Console.WriteLine();
    Assert.Equal(user.Id, getUser.Id);
    Assert.Equal(user.UserName, getUser.UserName);
  }

  [Fact]
  public async Task GetUserTest_Fail()
  {
    var user = new User
    {
      Id = 16,
      UserName = "whatever"
    };
    var response = await _client.GetAsync("https://localhost:5001/users/16");
    var responseString = await response.Content.ReadAsStringAsync();
    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
    Assert.Equal(error.Status, 400);
    Assert.Equal(error.Title, "Bad Request");
  }

  [Fact]
  public async Task SignUpUser_Success()
  {
    var user = new User
    {
      UserName = "boyleee"
    };
    var userJson = JsonConvert.SerializeObject(user);

    // Convert the JSON string to HttpContent
    HttpContent httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("https://localhost:5001/users", httpContent);
    var responseString = await response.Content.ReadAsStringAsync();
    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
    Assert.Equal(error.Status, 400);
    Assert.Equal(error.Title, "Bad Request");


  }

  public class ErrorResponse
  {
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
  }
}
