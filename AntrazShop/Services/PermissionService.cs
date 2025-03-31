using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;

namespace AntrazShop.Services
{
	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;
		public PermissionService(IPermissionRepository permissionRepository)
		{
			_permissionRepository = permissionRepository;
		}

		public async Task<List<string>> GetUserPermissions(int userId)
		{
			return await _permissionRepository.GetUserPermissions(userId);
		}
	}
}
