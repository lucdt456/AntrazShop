using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;

namespace AntrazShop.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IConfiguration _configuration;
		private readonly IPermissionRepository _permissionRepository;
		private readonly IRoleResponsive _roleResponsive;
		private readonly IAccountManagerRepository _accountManagerRepository;

		public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IPermissionRepository permissionRepository, IRoleResponsive roleResponsive, IAccountManagerRepository accountManagerRepository)
		{
			_accountRepository = accountRepository;
			_configuration = configuration;
			_permissionRepository = permissionRepository;
			_roleResponsive = roleResponsive;
			_accountManagerRepository = accountManagerRepository;
		}

		public async Task<ServiceResponse<AccountVM>> CreateUser(UserDTO newUser)
		{
			var response = new ServiceResponse<AccountVM>();
			try
			{
				var isEmailExist = await _accountRepository.CheckExistEmail(newUser.Email);
				if (isEmailExist)
				{
					response.IsSuccess = false;
					response.Errors.Add("Email đã tổn tại!!");
					return response;
				}

				var passwordHasher = new PasswordHasher<User>();
				var user = new User
				{
					Name = newUser.Name,
					Gender = newUser.Gender,
					Email = newUser.Email,
					PhoneNumber = newUser.PhoneNumber,
					Address = newUser.Address,
					PasswordHash = passwordHasher.HashPassword(new User(), newUser.Password),
					Birthday = newUser.Birthday,
					Avatar = "defaultAvt.png",
					workerAccount = false,
					CreatedAt = DateTime.Now
				};

				user = await _accountRepository.CreateUser(user);

				//Thêm role khách hàng
				var roleIdCustomer = await _roleResponsive.getRoleIdFromRoleName("Khách hàng");
				var ur = new UserRole()
				{
					UserId = user.Id,
					RoleId = roleIdCustomer
				};

				await _roleResponsive.AddRoleUser(ur);

				//Thêm bảng Auth cho tài khoản
				var auth = new UserAuthInfo()
				{
					UserId = user.Id,
					IsActive = true,
					FailedAttempts = 5
				};

				await _accountRepository.CreateUserAuthInfo(auth);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}


		public async Task<ServiceResponse<string>> AuthenticateAsync(Login loginRequest)
		{
			var response = new ServiceResponse<string>();
			try
			{
				//Lấy địa chỉ ip
				var ip = GetLocalIPAddress();

				var passwordHasher = new PasswordHasher<User>();
				var user = await _accountRepository.FindUser(loginRequest.Email);
				//Check tồn tại
				if (user == null)
				{
					response.IsSuccess = false;
					response.Errors.Add("Email không tồn tại!");
					return response;
				}

				var history = new LoginHistory();

				if (!user.UserAuthInfo.IsActive)
				{
					history = new LoginHistory
					{
						UserId = user.UserAuthInfo.UserId,
						IPAddress = ip,
						Time = DateTime.Now,
						StatusLogin = "Thất bại! Tài khoản đang bị khoá"
					};

					await _accountManagerRepository.AddLoginHistory(history);
					response.IsSuccess = false;
					response.Errors.Add("Tài khoản đang bị khoá! Liên hệ quản trị viên qua email 'Lucdt456@gmail.com' để được giải quyết!");
					return response;
				}

				//Check mật khẩu
				var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

				if (result == PasswordVerificationResult.Failed)
				{
					user.UserAuthInfo.FailedAttempts--;

					response.IsSuccess = false;
					response.Errors.Add("Mật khẩu không chính xác!");

					if (user.UserAuthInfo.FailedAttempts < 1)
					{
						user.UserAuthInfo.IsActive = false;
						response.Errors.Add("Bạn đã nhập sai mật khẩu quá 5 lần. Tài khoản đã bị khoá!!");
					}
					await _accountRepository.SetUserAuthInfo(user.UserAuthInfo);

					history = new LoginHistory
					{
						UserId = user.UserAuthInfo.UserId,
						IPAddress = ip,
						Time = DateTime.Now,
						StatusLogin = "Thất bại, Sai mật khẩu!"
					};

					await _accountManagerRepository.AddLoginHistory(history);

					return response;
				}

				history = new LoginHistory
				{
					UserId = user.UserAuthInfo.UserId,
					IPAddress = ip,
					Time = DateTime.Now,
					StatusLogin = "Thành công, Đăng nhập thành công!"
				};

				await _accountManagerRepository.AddLoginHistory(history);

				user.UserAuthInfo.FailedAttempts = 5;

				await _accountRepository.SetUserAuthInfo(user.UserAuthInfo);
				var claims = new List<Claim>
				{
				new Claim("Name", user.Name),
				new Claim("Email", user.Email),
				new Claim("Id", user.Id.ToString()),
				new Claim("Avatar", user.Avatar??="defaultAvt.png"),
				new Claim("IsWorkerAccount", user.workerAccount.ToString())
				};

				List<string> permissions = await _permissionRepository.GetUserPermissions(user.Id);
				foreach (var permission in permissions)
				{
					claims.Add(new Claim("Permission", permission));
				}

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
				response.Data = new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		/// <summary>
		/// Get IPv4
		/// </summary>
		/// <returns></returns>
		private string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());

			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}

			return "Unknown";
		}

		public async Task<ServiceResponse<string>> SetUserAuthInfo(int userid, bool isActive)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var auth = new UserAuthInfo
				{
					UserId = userid,
					IsActive = isActive,
					FailedAttempts = 5
				};
				await _accountRepository.SetUserAuthInfo(auth);

				response.Data = (isActive) ? "Kích hoạt thành công!" : "Khoá tài khoản thành công!";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> SetPassword(int userId, string password)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var user = await _accountManagerRepository.GetUser(userId);
				var passwordHasher = new PasswordHasher<User>();
				string stringPasswordHash = passwordHasher.HashPassword(new User(), password);
				await _accountRepository.SetUserPassword(userId, stringPasswordHash);
				response.Data = "Đổi mật khẩu thành công";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> ChangePassword(ChangePasswordDTO dto)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var passwordHasher = new PasswordHasher<User>();
				var user = await _accountRepository.FindUser(dto.Email);
				//Check tồn tại
				if (user == null)
				{
					response.IsSuccess = false;
					response.Errors.Add("Email không tồn tại!");
					return response;
				}
				var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

				if (result == PasswordVerificationResult.Failed)
				{
					response.IsSuccess = false;
					response.Errors.Add("Mật khẩu không chính xác!");
					return response;
				};

				return  await SetPassword(user.Id, dto.NewPassword);
			}

			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
				return response;
			}		
		}
	}
}
