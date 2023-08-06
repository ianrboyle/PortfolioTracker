using Domain;

namespace Persistence.BLL
{
  public interface IUserLogic
  {
    Task<List<User>> GetUsers();
    Task<User> GetUserById(int userId);
    Task SignUpUser(User appUser);
    Task DeleteUser(int userId);

  }
}