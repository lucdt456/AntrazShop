using Microsoft.AspNetCore.Authorization;

namespace AntrazShop.Authorization
{
	public class PermissionRequirement: IAuthorizationRequirement
	{
		public string Permission { get; }
		public PermissionRequirement(string permission)
		{
			Permission = permission;
		}
	}
}
