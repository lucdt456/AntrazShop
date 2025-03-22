using AntrazShop.Data;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Repositories.Interfaces;
using AntrazShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AntrazShop.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IConfiguration _configuration;

		public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
		{
			_accountRepository = accountRepository;
			_configuration = configuration;
		}

		
		public async Task<UserDTO> CreateUser(UserDTO newUser)
		{
			var passwordHasher = new PasswordHasher<User>();
			var user = new User
			{
				Name = newUser.Name,
				Gender = newUser.Gender,
				Email = newUser.Email,
				PhoneNumber = newUser.PhoneNumber,
				Address = newUser.Address,
				PasswordHash = passwordHasher.HashPassword(new User(), newUser.Password)
			};
			await _accountRepository.CreateUser(user);
			return newUser;
		}

		
		public async Task<string> AuthenticateAsync(Login loginRequest)
		{
			var passwordHasher = new PasswordHasher<User>();
			var user = await _accountRepository.FindUser(loginRequest.Email);
			if (user == null)
			{
				throw new UnauthorizedAccessException("Email không tồn tại");
			}

			
			var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

			if (result == PasswordVerificationResult.Failed)
			{
				throw new UnauthorizedAccessException("Mật khẩu không chính xác");
			}

		
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			
			var jwtConfig = _configuration.GetSection("JwtConfig");

			var key = jwtConfig["Key"];
			if (string.IsNullOrEmpty(key))
			{
				throw new InvalidOperationException("Key cấu hình lỗi.");
			}
			var keyBytes = Encoding.UTF8.GetBytes(key);
			var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);


			
			var token = new JwtSecurityToken(
				issuer: jwtConfig["Issuer"],
				audience: jwtConfig["Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtConfig["TokenValidityMins"])),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
