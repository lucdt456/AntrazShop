using AntrazShop.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AntrazShop.Authorization
{
	public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
	{
		private readonly IPermissionService _permissionService;
		public PermissionHandler(IPermissionService permissionService)
		{
			_permissionService = permissionService;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
			if(userIdClaim == null)
			{
				context.Fail();
				return;
			}
			int userId = int.Parse(userIdClaim.Value);

			var permissions = await _permissionService.GetUserPermissions(userId);
			if (permissions.Contains(requirement.Permission))
			{
				context.Succeed(requirement);
			}
			else context.Fail();

		}
	}
}
