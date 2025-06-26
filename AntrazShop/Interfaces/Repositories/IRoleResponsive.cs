using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IRoleResponsive
	{
		Task<Role> CreateRole(Role newRole);
		Task<Role> EditRole(int id, Role role);
		Task<IEnumerable<Role>> GetRoles(int recSkkip, int take);
		Task<Role> GetRole(int id);
		Task<int> GetRoleCount();
		Task DeleteRole(int id);
		Task AddRolePermission(RolePermission rp);
		Task AddRolePermissions(int roleId, List<int> permissionIds);
		Task DeleteRolePermission(int roleId, List<int> permissionIds);
		Task<bool> CheckExistRole(string roleName);
		Task<int> getRoleIdFromRoleName(string roleName);
		Task AddRoleUser(UserRole ur);
	}
}
	