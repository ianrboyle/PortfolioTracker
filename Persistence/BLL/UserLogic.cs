using Domain;
using Persistence.DAL;

namespace Persistence.BLL
{
  public class UserLogic : IUserLogic
  {
    private readonly IUserRetriever _retriever;
    public UserLogic(IUserRetriever retriever)
    {
      _retriever = retriever;

    }
    public Task<List<User>> GetUsers()
    {
      return _retriever.GetUsers();
    }
  }
}