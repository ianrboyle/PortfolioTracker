using Domain;

namespace Persistence.BLL
{
  public interface IUserLogic
  {
    Task<List<User>> GetUsers();
    Task<User> GetUser(int userId);
    Task<User> SignUpUser(User appUser);
  }
}