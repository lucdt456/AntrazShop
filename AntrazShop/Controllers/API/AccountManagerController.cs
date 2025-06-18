using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountManagerController : ControllerBase
	{
		private readonly IAccountManagerService _accountManagerService;
		public AccountManagerController(IAccountManagerService accountManagerService)
		{
			_accountManagerService = accountManagerService;
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
		[HttpGet("Worker/{page}/{take}")]
		public async Task<IActionResult> GetWorkerAccounts(string? search, int page = 1, int take = 10)
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
		public async Task<IActionResult> GetCustomerAccounts(string? search, int page = 1, int take = 10)
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
		[HttpPost("create")]
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
		[HttpPut("{idUser}")]
		public async Task<IActionResult> UpdateAccount(int idUser, [FromForm] AccountDTO accountDTO)
		{
			var response = await _accountManagerService.EditAccount(idUser, accountDTO);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
