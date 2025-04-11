using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Http;
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

		[HttpPost("register")]
		public async Task<IActionResult> Register(UserDTO newUser)
		{
			if (newUser == null)
			{
				return BadRequest("Invalid data.");
			}

			return Ok(await _accountService.CreateUser(newUser));
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Login userlg)
		{
			if (userlg == null)
			{
				return BadRequest("Không có data.");
			}

			try
			{
				var token = await _accountService.AuthenticateAsync(userlg);
				return Ok(new { Token = token });
			}
			catch (UnauthorizedAccessException ex)
			{
				return Unauthorized(ex.Message);
			}

		}
	}
}
