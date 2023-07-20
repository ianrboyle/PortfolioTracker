using Domain;
using Persistence.DAL;

namespace Persistence.BLL
{
  public class UserLogic : IUserLogic
  {
    private readonly IUserRepository _repository;
    public UserLogic(IUserRepository repository)
    {
      _repository = repository;

    }

    public Task<User> GetUserById(int userId)
    {
      return _repository.GetUserById(userId);
    }

    public Task<List<User>> GetUsers()
    {
      return _repository.GetUsers();
    }

    public Task SignUpUser(User appUser)
    {
      return _repository.SignUpUser(appUser);
    }

  }
}