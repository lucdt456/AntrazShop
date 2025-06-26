using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserDTO newUser)
		{
			var response = await _accountService.CreateUser(newUser);

			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Login userlg)
		{
			var response = await _accountService.AuthenticateAsync(userlg);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(new { Token = response.Data });
		}
	}
}
