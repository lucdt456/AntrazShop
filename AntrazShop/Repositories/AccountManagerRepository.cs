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
				.AsNoTracking()
				.Include(u => u.UserAuthInfo)
				.Include(u => u.UserRoles)
				.ThenInclude(u => u.Role)
				.OrderBy(u => u.Id)
				.Skip(recSkip)
				.Take(take)
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> SearchWorkerAccounts(string search, int recSkip, int take)
		{
			return await _context.Users
				.AsNoTracking()
				.Include(u => u.UserAuthInfo)
				.Include(u => u.UserRoles)
				.ThenInclude(u => u.Role)
				.OrderBy(u => u.Id)
				.Where(u => u.workerAccount == true)
				.Where(u => u.Name.Contains(search) || u.Id.ToString().Contains(search) || u.Email.Contains(search))
				.Skip(recSkip)
				.Take(take)
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> SearchCustomerAccounts(string search, int recSkip, int take)
		{
			return await _context.Users
				.AsNoTracking()
				.Include(u => u.UserAuthInfo)
				.Include(u => u.UserRoles)
				.ThenInclude(u => u.Role)
				.OrderBy(u => u.Id)
				.Where(u => u.workerAccount == false)
				.Where(u => u.Name.Contains(search) || u.Id.ToString().Contains(search) || u.Email.Contains(search))
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

		public async Task<int> GetCountSearchWorkerAccounts(string search)
		{
			return await _context.Users
				.AsNoTracking()
				.Where(u => u.workerAccount == true)
				.Where(u => u.Name.Contains(search) || u.Id.ToString().Contains(search) || u.Email.Contains(search))
				.CountAsync();
		}

		public async Task<int> GetCountSearchCustomerAccounts(string search)
		{
			return await _context.Users
				.AsNoTracking()
				.Where(u => u.workerAccount == false)
				.Where(u => u.Name.Contains(search) || u.Id.ToString().Contains(search) || u.Email.Contains(search))
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

		public async Task SetRoleUser(int userId, List<int> roleIds)
		{
			foreach (int roleId in roleIds)
			{
				var ur = new UserRole
				{
					UserId = userId,
					RoleId = roleId
				};
				_context.UserRoles.Add(ur);
			}
			await _context.SaveChangesAsync();
		}

		public async Task<List<int>> GetRoleFromUser(int userId)
		{
			return await _context.UserRoles
				.AsNoTracking()
				.Where(ur => ur.UserId == userId)
				.Select(ur => ur.RoleId).ToListAsync();
		}

		public async Task<User> GetUser(int userId)
		{
			var user = await _context.Users
				.AsNoTracking()
				.Include(u => u.UserAuthInfo)
				.Include(u => u.UserRoles)
				.ThenInclude(ur => ur.Role)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null) throw new Exception("Không tìm thấy tài khoản");
			return user;
		}

		public async Task<User> UpdateUser(int userId, User userUpdate)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null) throw new Exception("Không tìm thấy tài khoản!!");

			user.Name = userUpdate.Name;
			user.Gender = userUpdate.Gender;
			user.PhoneNumber = userUpdate.PhoneNumber;
			user.Address = userUpdate.Address;
			user.Avatar = userUpdate.Avatar;
			user.Birthday = userUpdate.Birthday;
			user.Hometown = userUpdate.Hometown;
			user.workerAccount = userUpdate.workerAccount;

			await _context.SaveChangesAsync();

			return user;
		}
		public async Task AddLoginHistory(LoginHistory loginHistory)
		{
			_context.LoginHistories.Add(loginHistory);
			await _context.SaveChangesAsync();
		}

		public async Task<List<LoginHistory>> GetLoginHistories(int userId)
		{
			return await _context.LoginHistories
					.OrderByDescending(h => h.Id)
					.Where(h => h.UserId == userId)
					.Take(50)
					.ToListAsync();
		}

		public async Task DeleteAccount(int userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null) throw new Exception("Không tìm thấy tài khoản!");
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAuthAccount(int userId)
		{
			var auth = await _context.UserAuthInfos.FindAsync(userId);
			if (auth == null) throw new Exception("Không tìm thấy bảng Auth tài khoản!");
			_context.UserAuthInfos.Remove(auth);
			await _context.SaveChangesAsync();
		}
	}
}
