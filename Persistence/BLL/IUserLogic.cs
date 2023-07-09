using Domain;

namespace Persistence.BLL
{
  public interface IUserLogic
  {
    Task<List<User>> GetUsers();
  }
}