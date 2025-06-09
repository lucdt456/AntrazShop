using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

		[HttpGet("Permission/{pg}/{size}")]
		public async Task<IActionResult> GetPermissions(int pg = 1, int size = 10)
		{
			var (response, pagination) = await _permissionService.GetPermissions(pg, size);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(new
			{
				Roles = response.Data,
				Pagination = pagination
			});
		}

		[HttpGet("Permission/{controllerName}/{pg}/{size}")]
		public async Task<IActionResult> GetPermissions(string controllerName, int pg = 1, int size = 10)
		{
			var (response, pagination) = await _permissionService.GetAllPermissionsInController(controllerName, pg, size);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(new
			{
				Roles = response.Data,
				Pagination = pagination
			});
		}

		[HttpPost]
		public async Task<IActionResult> CreatePermissions([FromBody] PermissionDTO dTO)
		{
			var response = await _permissionService.CreatePermission(dTO);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}
	}
}
