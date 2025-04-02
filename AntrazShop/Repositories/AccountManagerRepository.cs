using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class AccountManagerRepository : IAccountManagerRepository
	{
		private readonly ShopDbContext _context;
		public AccountManagerRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<User>> GetUsers(int recSkip, int take)
		{
			return await _context.Users
				.OrderBy(u => u.Id)
				.Skip(recSkip)
				.Take(take)
				.ToListAsync();
		}

		public async Task<int> GetCountUsers()
		{
			return await _context
				.Users
				.CountAsync();
		}

		public async Task<List<string>> GetUserRoles(int userId)
		{
			return await _context.UserRoles
				.Where(ur => ur.UserId == userId)
				.Select(ur => ur.Role.Name)
				.ToListAsync();
		}
	}
}
