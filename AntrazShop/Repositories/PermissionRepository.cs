using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class PermissionRepository : IPermissionRepository
	{
		private readonly ShopDbContext _context;
		public PermissionRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<List<string>> GetUserPermissions(int userId)
		{
			var rolePermissions = await _context.UserRoles
				.Where(ur => ur.UserId == userId)
				.SelectMany(ur => ur.Role.RolePermissions)
				.Select(rp => rp.Permission.Name)
				.ToListAsync();

			var userPermissions = await _context.UserPermissions
				.Where(up => up.UserId == userId)
				.Select(up => up.Permission.Name)
				.ToListAsync();

			var allPermissions = rolePermissions
				.Union(userPermissions)
				.Distinct()
				.ToList();

			return allPermissions;
		}
	}
}
