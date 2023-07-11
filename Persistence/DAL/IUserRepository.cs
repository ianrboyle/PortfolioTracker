using Domain;

namespace Persistence.DAL
{
  public interface IUserRepository
  {
    Task<List<User>> GetUsers();
  }
}