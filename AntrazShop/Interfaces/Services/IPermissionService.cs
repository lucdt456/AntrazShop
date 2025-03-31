namespace AntrazShop.Interfaces.Services
{
	public interface IPermissionService
	{
		Task<List<string>> GetUserPermissions(int userId);
	}
}
