using AntrazShop.Data;
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
		Task<ServiceResponse<string>> SetUserAuthInfo(int userid, bool isActive);
	}
}
