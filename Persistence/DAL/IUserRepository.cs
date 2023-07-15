using Domain;

namespace Persistence.DAL
{
  public interface IUserRepository
  {
    Task<List<User>> GetUsers();
    Task<User> GetUser(int userId);
    Task<User> SignUpUser(User user);
  }
}