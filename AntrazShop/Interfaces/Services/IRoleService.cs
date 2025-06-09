using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IRoleService
	{
		Task<ServiceResponse<RoleVM>> CreateRole(RoleDTO roleDTO);
		Task<ServiceResponse<RoleVM>> EditRole(int id, RoleDTO dTO);
		Task<(ServiceResponse<List<RoleVM>>, Paginate)> GetRoles(int pg, int take);
		Task<ServiceResponse<RoleVM>> GetRole(int id);
		Task<ServiceResponse<string>> DeleteRole(int id);
		Task<ServiceResponse<RoleVM>> EditRolePermission(int roleId, List<int> permissionIds);
	}
}
