using Persistence.Logger;
using Moq;
using Persistence.BLL;


namespace PFTests
{
  public class PositionTests
  {
    private readonly Mock<IUserLogic> _userLogic;
    private readonly Mock<ILogger> _logger;

    public PositionTests()
    {
      _userLogic = new Mock<IUserLogic>();
      _logger = new Mock<ILogger>();
    }

    [Fact]
    async Task GetPositionById_Success()
    {

    }

  }
}