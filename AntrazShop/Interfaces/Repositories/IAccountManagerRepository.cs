using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IAccountManagerRepository
	{
		Task<IEnumerable<User>> GetUsers(int recSkip, int take);
		Task<User> CreateAccount(User newUser);
		Task<int> GetCountUsers();
		Task AddRoles(int userId, List<int> roleIds);
		Task DeleteRoles(int userId, List<int> roleIds);
		Task<bool> CheckExistEmail(string email);
	}
}
