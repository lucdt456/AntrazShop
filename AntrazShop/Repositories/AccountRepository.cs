using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ShopDbContext _context;
		public AccountRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<User> CreateUser(User newUser)
		{
			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();
			return newUser;
		}
		public async Task<User> FindUser(string email)
		{
			var user = await _context.Users
				.AsNoTracking()
				.Include(u => u.UserAuthInfo)
				.FirstOrDefaultAsync(u => u.Email == email);
			if (user == null) throw new Exception("Không tìm thấy tài khoản");
			return user;
		}
		public async Task<bool> CheckExistEmail(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email == email);
		}

		public async Task CreateUserAuthInfo(UserAuthInfo auth)
		{
			_context.UserAuthInfos.Add(auth);
			await _context.SaveChangesAsync();
		}

		public async Task SetUserAuthInfo(UserAuthInfo authUpdate)
		{
			var auth = await _context.UserAuthInfos.FindAsync(authUpdate.UserId);
			if (auth == null) throw new Exception("Không tìm thấy tài khoản");

			auth.IsActive = authUpdate.IsActive;
			auth.FailedAttempts = authUpdate.FailedAttempts;

			await _context.SaveChangesAsync();
		}
	}
}
