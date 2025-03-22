using AntrazShop.Data;

namespace AntrazShop.Repositories.Interfaces
{
	public interface IAccountRepository
	{
		Task<User> CreateUser(User newUser);
		Task<User> FindUser(string email);
	}
}
