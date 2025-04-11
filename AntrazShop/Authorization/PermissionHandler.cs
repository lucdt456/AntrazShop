using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace AntrazShop.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // Lấy tất cả claim có type = "Permission"
            var userPermissions = context.User.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value);

            // Kiểm tra xem có chứa quyền yêu cầu không
            if (userPermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(); // Không có quyền → từ chối
            }

            return Task.CompletedTask;
        }
    }
}
