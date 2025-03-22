using AntrazShop.Models;
using AntrazShop.Models.DTOModels;


namespace AntrazShop.Services.Interfaces
{
	public interface IAccountService
	{
		Task<UserDTO> CreateUser(UserDTO newUser);
		Task<string> AuthenticateAsync(Login loginRequest);
	}
}
