using Domain;
using Domain.Exceptions;
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

    public async Task<User> GetUserById(int userId)
    {
      var user = await _repository.GetUserById(userId);
      if (user.Id == 0 || user == null)
      {
        string exceptionString = $"User with ID: {userId} not found.";
        CustomException ex = new CustomException(exceptionString, 404);
        throw ex;
      }
      return user;
    }

    public async Task<List<User>> GetUsers()
    {
      return await _repository.GetUsers();
    }

    public Task SignUpUser(User appUser)
    {
      return _repository.SignUpUser(appUser);
    }
    public Task DeleteUser(int userId)
    {
      return _repository.DeleteUser(userId);
    }



  }
}