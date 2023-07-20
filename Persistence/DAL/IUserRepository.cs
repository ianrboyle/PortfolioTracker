using Domain;

namespace Persistence.DAL
{
  public interface IUserRepository
  {
    Task<List<User>> GetUsers();
    Task<User> GetUserById(int userId);
    Task<User> GetUserByUserName(string userName);
    Task SignUpUser(User user);
  }
}