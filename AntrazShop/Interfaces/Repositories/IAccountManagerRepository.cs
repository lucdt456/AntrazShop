using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IAccountManagerRepository
	{
		Task<IEnumerable<User>> GetUsers(int recSkip, int take);
		Task<int> GetCountUsers();
		Task<List<string>> GetUserRoles(int userId);
	}
}
