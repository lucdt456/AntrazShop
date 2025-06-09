using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IPermissionService
	{
		Task<(ServiceResponse<List<PermissionVM>>, Paginate)> GetPermissions(int pg, int take);
		Task<(ServiceResponse<List<PermissionVM>>, Paginate)> GetAllPermissionsInController(string controller, int pg, int take);
		Task<ServiceResponse<PermissionVM>> CreatePermission(PermissionDTO newPermission);
	}
}
