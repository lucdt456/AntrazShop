using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface ICartService
	{
		Task<ServiceResponse<CartItemVM>> AddToCart(AddToCartDto dto);
		Task<ServiceResponse<List<CartItemVM>>> GetCart(int userId);
		Task<ServiceResponse<CartItemVM>> UpdateCartItem(UpdateCartDto dto);
		Task<ServiceResponse<bool>> RemoveFromCart(int userId, int colorCapacityId);
		Task<ServiceResponse<string>> CreateOrder(int userId, OrderDTO dto);
	}
}
