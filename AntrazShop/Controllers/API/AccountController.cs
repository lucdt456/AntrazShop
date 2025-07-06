using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IEmailService _emailService;
		public AccountController(IAccountService accountService, IEmailService emailService)
		{
			_accountService = accountService;
			_emailService = emailService;
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

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
		{
			var response = await _emailService.SendEmailCode(request.Email);
			if (response.IsSuccess)
			{
				return Ok(response.Data);
			}
			return BadRequest(response.Errors);
		}

		[HttpPost("verify-code")]
		public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDTO dto)
		{
			var response = await _emailService.VerifyCodeRequest(dto);
			if (response.IsSuccess)
			{
				return Ok(response.Data);
			}
			return BadRequest(response.Errors);
		}

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
		{
			var response = await _accountService.ChangePassword(dto);
			if (response.IsSuccess)
			{
				return Ok(response.Data);
			}
			return BadRequest(response.Errors);
		}
	}
}
