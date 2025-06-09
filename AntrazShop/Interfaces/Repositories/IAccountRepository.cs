using AntrazShop.Data;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IAccountRepository
	{
		Task<User> CreateUser(User newUser);
		Task<User> FindUser(string email);
	}
}
