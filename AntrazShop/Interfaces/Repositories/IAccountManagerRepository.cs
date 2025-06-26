using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IAccountManagerRepository
	{
		Task<IEnumerable<User>> GetUsers(int recSkip, int take);
		Task<IEnumerable<User>> SearchWorkerAccounts(string search, int recSkip, int take);
		Task<IEnumerable<User>> SearchCustomerAccounts(string search, int recSkip, int take);

		Task<User> CreateAccount(User newUser);
		Task<int> GetCountUsers();
		Task<int> GetCountSearchWorkerAccounts(string search);
		Task<int> GetCountSearchCustomerAccounts(string search);

		Task AddRoles(int userId, List<int> roleIds);
		Task DeleteRoles(int userId, List<int> roleIds);
		Task<bool> CheckExistEmail(string email);
		Task SetRoleUser(int userId, List<int> roleIds);

		Task<List<int>> GetRoleFromUser(int userId);
		Task<User> GetUser(int userId);
		Task<User> UpdateUser(int userId, User userUpdate);

		Task AddLoginHistory(LoginHistory loginHistory);
		Task<List<LoginHistory>> GetLoginHistories(int userId);
	}
}
