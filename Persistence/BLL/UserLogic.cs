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

    public Task<User> GetUser(int userId)
    {
      return _repository.GetUser(userId);
    }

    public Task<List<User>> GetUsers()
    {
      return _repository.GetUsers();
    }

    public Task<User> SignUpUser(User appUser)
    {
      throw new NotImplementedException();
    }
  }
}