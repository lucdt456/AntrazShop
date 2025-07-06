using AntrazShop.Helper;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;


namespace AntrazShop.Interfaces.Services
{
	public interface IAccountService
	{
		Task<ServiceResponse<AccountVM>> CreateUser(UserDTO dto);
		Task<ServiceResponse<string>> AuthenticateAsync(Login loginRequest);
		Task<ServiceResponse<string>> SetUserAuthInfo(int userId, bool isActive);
		Task<ServiceResponse<string>> SetPassword(int userId, string password);
		Task<ServiceResponse<string>> ChangePassword(ChangePasswordDTO dto);
	}
}
