namespace AntrazShop.Interfaces.Repositories
{
	public interface IPermissionRepository
	{
		Task<List<string>> GetUserPermissions(int userId);
	}
}
