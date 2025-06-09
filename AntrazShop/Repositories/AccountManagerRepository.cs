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
				.Include(u => u.UserRoles)
				.ThenInclude(u => u.Role)
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

		public async Task<User> CreateAccount(User newUser)
		{
			await _context.Users.AddAsync(newUser);
			await _context.SaveChangesAsync();
			return newUser;
		}

		public async Task AddRoles(int userId, List<int> roleIds)
		{
			foreach (int roleId in roleIds)
			{
				var ur = new UserRole
				{
					UserId = userId,
					RoleId = roleId
				};
				await _context.UserRoles.AddAsync(ur);
			}
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRoles(int userId, List<int> roleIds)
		{
			foreach (int roleId in roleIds)
			{
				var ur = new UserRole
				{
					UserId = userId,
					RoleId = roleId
				};
				_context.UserRoles.Remove(ur);
			}
			await _context.SaveChangesAsync();
		}

		public async Task<bool> CheckExistEmail(string email)
		{
			return _context.Users.Any(u => u.Email.ToLower() == email.ToLower());
		}
	}
}
