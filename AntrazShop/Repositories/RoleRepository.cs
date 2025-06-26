using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class RoleRepository : IRoleResponsive
	{
		private readonly ShopDbContext _context;
		public RoleRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<Role> CreateRole(Role newRole)
		{
			await _context.Roles.AddAsync(newRole);
			await _context.SaveChangesAsync();
			return newRole;
		}
		public async Task<Role> EditRole(int id, Role roleUpdate)
		{
			var role = await _context.Roles.FindAsync(id);
			if (role == null)
			{
				throw new Exception("Không tìm thấy Role");
			}
			else
			{
				role.Name = roleUpdate.Name;
				role.Description = roleUpdate.Description;
				_context.Roles.Update(role);
				await _context.SaveChangesAsync();
				return role;
			}
		}

		public async Task<IEnumerable<Role>> GetRoles(int recSkip, int take)
		{
			return await _context.Roles
				.Skip(recSkip)
				.Take(take)
				.Include(r => r.RolePermissions)
					.ThenInclude(rp => rp.Permission)
				.Include(r => r.UserRoles)
					.ThenInclude(ur => ur.User)
				.ToListAsync();
		}


		public async Task<Role> GetRole(int id)
		{
			var role = await _context.Roles
				.Include(r => r.RolePermissions)
					.ThenInclude(rp => rp.Permission)
				.Include(r => r.UserRoles)
					.ThenInclude(ur => ur.User)
				.FirstOrDefaultAsync(r => r.Id == id);

			if (role == null)
			{
				throw new Exception("Không tìm thấy role!");
			}
			else
			{
				return role;
			}

		}
		public async Task<int> GetRoleCount()
		{
			return await _context.Roles.CountAsync();
		}

		public async Task DeleteRole(int id)
		{
			var role = await _context.Roles.FindAsync(id);
			if (role == null)
			{
				throw new Exception("Không tìm thấy role!");
			}
			else
			{
				_context.Remove(role);
				await _context.SaveChangesAsync();
			}
		}

		public async Task AddRolePermissions(int roleId, List<int> permissionIds)
		{
			foreach (int permissionId in permissionIds)
			{
				var rp = new RolePermission
				{
					RoleId = roleId,
					PermissionId = permissionId,
				};
				_context.RolePermissions.Add(rp);
			}
			;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRolePermission(int roleId, List<int> permissionIds)
		{
			foreach (int permissionId in permissionIds)
			{
				var rolePermission = await _context.RolePermissions
					   .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

				if (rolePermission == null) throw new Exception("Không tìm thấy phân quyền cho role");
				_context.Remove(rolePermission);
			}
			await _context.SaveChangesAsync();
		}

		public async Task AddRolePermission(RolePermission rp)
		{
			var newRp = new RolePermission
			{
				RoleId = rp.RoleId,
				PermissionId = rp.PermissionId
			};

			await _context.RolePermissions.AddAsync(rp);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> CheckExistRole(string roleName)
		{
			return _context.Roles.Any(r => r.Name.ToLower() == roleName.ToLower());
		}

		public async Task<int> getRoleIdFromRoleName(string roleName)
		{
			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
			if (role == null) throw new Exception($"Không tìm thấy role {roleName} trong danh sách role!");
			return role.Id;
		}

		public async Task AddRoleUser(UserRole ur)
		{
			_context.UserRoles.Add(ur);
			await _context.SaveChangesAsync();
		}
	}
}
