using AntrazShop.Data;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountManagerController : ControllerBase
	{
		private readonly IAccountManagerService _accountManagerService;
		private readonly IAccountService _accountService;
		public AccountManagerController(IAccountManagerService accountManagerService, IAccountService accountService)
		{
			_accountManagerService = accountManagerService;
			_accountService = accountService;
		}

		//Lấy tất cả tài khoản
		[HttpGet("{page}/{take}")]
		public async Task<IActionResult> GetUsers(int page = 1, int take = 10)
		{
			var (response, pagination) = await _accountManagerService.GetUsers(page, take);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(new
			{
				Users = response.Data,
				Pagination = pagination
			});
		}

		//Lấy - Tìm kiếm tài khoản nhân viên
		[Authorize(Policy = "CanGetUserWorkers")]
		[HttpGet("Worker/{page}/{take}")]
		public async Task<IActionResult> GetWorkerAccounts(string search = "", int page = 1, int take = 10)
		{
			var (response, pagination) = await _accountManagerService.GetWorkerAccounts(search, page, take);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(new
			{
				Users = response.Data,
				Pagination = pagination
			});
		}

		//Lấy - Tìm kiếm tài khoản khách hàng
		[HttpGet("Customer/{page}/{take}")]
		public async Task<IActionResult> GetCustomerAccounts(string search = "", int page = 1, int take = 10)
		{
			var (response, pagination) = await _accountManagerService.GetCustomerAccounts(search, page, take);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(new
			{
				Users = response.Data,
				Pagination = pagination
			});
		}

		//Tạo tài khoản
		[HttpPost("Create")]
		public async Task<IActionResult> CreateAccount([FromForm] AccountDTO accountDTO)
		{
			var response = await _accountManagerService.CreateAccount(accountDTO);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Sửa tài khoản
		[HttpPut("Edit/{idUser}")]
		public async Task<IActionResult> UpdateAccount(int userId, [FromForm] AccountDTO accountDTO)
		{
			var response = await _accountManagerService.EditAccount(userId, accountDTO);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Lấy tài khoản
		[HttpGet("{idUser}")]
		public async Task<IActionResult> GetUser(int idUser)
		{
			var response = await _accountManagerService.GetUser(idUser);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Lấy tài khoản
		[HttpGet("LoginHistory/{idUser}")]
		public async Task<IActionResult> GetLoginHistories(int idUser)
		{
			var response = await _accountManagerService.GetLoginHistories(idUser);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Chỉnh sửa roles
		[HttpPut("EditUserRoles/{userId}")]
		public async Task<IActionResult> EditUserRole(int userId, [FromBody] List<int> roleIds)
		{
			var response = await _accountManagerService.EditUserRoles(userId, roleIds);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		//Chỉnh sửa trạng thái active của tài khoản
		[HttpPut("EditUserAuth/{userId}")]
		public async Task<IActionResult> EditAuthUser(int userId, [FromBody] bool isActive)
		{
			var response = await _accountService.SetUserAuthInfo(userId, isActive);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
