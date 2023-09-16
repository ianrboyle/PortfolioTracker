using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using StackExchange.Redis;
using Application.Services;
using Domain.Models;
using Persistence.DAL;
using Persistence.Logger;
using Xunit.Sdk;

namespace PFTests
{
  public class RedisClientTests
  {
    private readonly Mock<IConnectionMultiplexer> _redisMock;

    public RedisClientTests()
    {

      _redisMock = new Mock<IConnectionMultiplexer>();
    }

    [Fact]
    public async Task AddCompanyInfoToRedis_ShouldAddCompanyInfoToRedis()
    {
      // Arrange
      var connectionMultiplexerMock = new Mock<IConnectionMultiplexer>();
      var databaseMock = new Mock<IDatabase>();
      var companyInfo = new CompanyInformation
      {
        Symbol = "BTU",
        Country = "USA",
        SectorId = 2,
        IndustryId = 2,
        CompanyName = "Peabody",
        CurrentPrice = 1.2
      };
      connectionMultiplexerMock.Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(databaseMock.Object);
      databaseMock.Setup(db => db.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), null, It.IsAny<When>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);

      var redisClient = new RedisClient(connectionMultiplexerMock.Object);


      // Act
      await redisClient.AddCompanyInfoToRedis(companyInfo);

      // Assert
      databaseMock.Verify(db => db.StringSetAsync("BTU", It.IsAny<RedisValue>(), null, It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Once);
    }
    [Fact]
    public async Task AddCompanyInfoToRedis_ValidCompanyInfo_ShouldAddToRedis()
    {
      // Arrange
      var companyInfo = new CompanyInformation
      {
        Symbol = "BTU",
        Country = "USA",
        SectorId = 2,
        IndustryId = 2,
        CompanyName = "Peabody",
        CurrentPrice = 1.2
      };
      var serializedCompanyInfo = companyInfo.CompanyInfoToJson();
      // string serializedCompanyInfo = "{\"Symbol\":\"BTU\",\"CompanyName\":\"Peabody\",\"SectorId\":2,\"IndustryId\":2,\"CurrentPrice\":0.0,\"Country\":\"USA\"}";


      var redisDatabaseMock = new Mock<IDatabase>();
      redisDatabaseMock.Setup(db => db.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), null, When.Always, CommandFlags.None))
                       .Callback<RedisKey, RedisValue, TimeSpan?, When, CommandFlags>((key, value, _, __, ___) =>
                       {
                         Console.WriteLine($"Key: {key}, Value: {value}");
                       })
                       .Returns(Task.FromResult(true));

      var connectionMultiplexerMock = new Mock<IConnectionMultiplexer>();
      connectionMultiplexerMock.Setup(cm => cm.GetDatabase(It.IsAny<int>(), null))
                               .Returns(redisDatabaseMock.Object);

      var redisClient = new RedisClient(connectionMultiplexerMock.Object);

      // Act
      await redisClient.AddCompanyInfoToRedis(companyInfo);

      // Assert
      redisDatabaseMock.Verify(db => db.StringSetAsync("BTU", serializedCompanyInfo, null, false, When.Always, CommandFlags.None), Times.Once);
    }

  }
}
