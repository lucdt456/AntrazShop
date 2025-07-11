using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpPost("Create")]
		public async Task<IActionResult> CreateRole([FromBody] RoleDTO dto)
		{
			var response = await _roleService.CreateRole(dto);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		[Authorize(Policy = "CanEditRole")]
		[HttpPut("{id}")]
		public async Task<IActionResult> EditRole(int id, [FromBody] RoleDTO dTo)
		{
			var response = await _roleService.EditRole(id, dTo);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		[Authorize(Policy = "CanGetRoles")]
		[HttpGet("{pg}/{size}")]
		public async Task<IActionResult> GetRoles(int pg = 1, int size = 10)
		{
			var (response, pagination) = await _roleService.GetRoles(pg, size);
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
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetRole(int id)
		{
			var response = await _roleService.GetRole(id);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		[Authorize(Policy = "CanDeleteRole")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRole(int id)
		{
			var response = await _roleService.DeleteRole(id);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}

		[Authorize(Policy = "CanEditRolePermission")]
		[HttpPut("EditRolePermission/{roleId}")]
		public async Task<IActionResult> EditRolePermission(int roleId,[FromBody] List<int> permissionIds)
		{
			var response = await _roleService.EditRolePermission(roleId, permissionIds);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			else return Ok(response.Data);
		}
	}
}
