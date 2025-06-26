using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IAccountRepository
	{
		Task<User> CreateUser(User newUser);
		Task<User> FindUser(string email);
		Task<bool> CheckExistEmail(string email);
		Task CreateUserAuthInfo(UserAuthInfo auth);
		Task SetUserAuthInfo(UserAuthInfo auth);
	}
}
