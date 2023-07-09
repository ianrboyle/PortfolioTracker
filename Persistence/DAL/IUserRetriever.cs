using Domain;

namespace Persistence.DAL
{
  public interface IUserRetriever
  {
    Task<List<User>> GetUsers();
  }
}