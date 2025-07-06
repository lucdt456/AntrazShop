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

		public async Task<List<int>> GetRolePermissions(int roleId)
		{
			return await _context.RolePermissions
											.Where(rp => rp.RoleId == roleId)
											.Select(rp => rp.Permission.Id).ToListAsync();
		}

		public async Task<Permission> CreatePermissions(Permission newP)
		{
			await _context.Permissions.AddAsync(newP);
			await _context.SaveChangesAsync();
			return newP;
		}

		public async Task<IEnumerable<Permission>> GetPermissions(int recSkip, int take)
		{
			return await _context.Permissions.Skip(recSkip).Take(take).ToListAsync();
		}

		public async Task<int> GetPermissionCount()
		{
			return await _context.Permissions.CountAsync();
		}

		public async Task<IEnumerable<PermissionGroup>> GetPermissionGroup()
		{
			return await _context.PermissionGroups
				.AsNoTracking()
				.Include(pg => pg.Permissions)
				.ToListAsync();
		}
	}
}
