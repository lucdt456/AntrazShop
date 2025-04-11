using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Models.DTOModels;
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
			await _context.Users.AddAsync(newUser);
			await _context.SaveChangesAsync();
			return newUser;
		}

		public async Task<User> FindUser(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
