using AntrazShop.Data;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface ICartRepository
	{
		Task<Cart> AddToCart(AddToCartDto dto);
		Task<List<Cart>> GetCart(int userId);
		Task<Cart> GetCartItem(int userId, int colorCapacityId);
		Task<Cart> UpdateCartItemQuantity(int userId, int colorCapacityId, int quantity);
		Task<bool> RemoveFromCart(int userId, int colorCapacityId);
	}
}
