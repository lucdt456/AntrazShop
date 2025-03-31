using AntrazShop.Models;
using AntrazShop.Models.DTOModels;


namespace AntrazShop.Interfaces.Services
{
	public interface IAccountService
	{
		Task<UserDTO> CreateUser(UserDTO newUser);
		Task<string> AuthenticateAsync(Login loginRequest);
	}
}
