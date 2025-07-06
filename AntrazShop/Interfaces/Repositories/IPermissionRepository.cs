using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IPermissionRepository
	{
		Task<List<string>> GetUserPermissions(int userId);
		Task<List<int>> GetRolePermissions(int roleId);
		Task<Permission> CreatePermissions(Permission newP);
		Task<IEnumerable<Permission>> GetPermissions(int recSkip, int take);
		Task<int> GetPermissionCount();
		Task<IEnumerable<PermissionGroup>> GetPermissionGroup();
	}
}
