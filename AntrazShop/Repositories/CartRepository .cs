using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Models.DTOModels;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly ShopDbContext _context;

		public CartRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<Cart> AddToCart(AddToCartDto dto)
		{
			var colorCapacity = await _context.ColorCapacities
				.Include(cc => cc.Product)
				.FirstOrDefaultAsync(cc => cc.Id == dto.ColorCapacityId);

			if (colorCapacity == null)
				throw new Exception("Sản phẩm không tồn tại");

			if (colorCapacity.Stock < dto.Quantity)
				throw new Exception("Không đủ hàng trong kho");

			// Tính total price
			var total = colorCapacity.Price * dto.Quantity;

			var cartItem = new Cart
			{
				UserId = dto.UserId,
				ColorCapacityId = dto.ColorCapacityId,
				Quantity = dto.Quantity,
				Total = total
			};

			_context.Carts.Add(cartItem);
			await _context.SaveChangesAsync();
			await _context.SaveChangesAsync();

			return await GetCartItemWithDetails(cartItem.UserId, cartItem.ColorCapacityId);
		}

		public async Task<List<Cart>> GetCart(int userId)
		{
			var cartItems = await _context.Carts
				.AsNoTracking()
				.Where(c => c.UserId == userId)
				.OrderByDescending(c => c.Id)
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Product)
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Color)
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Capacity)
				.ToListAsync();

			return cartItems;
		}

		public async Task<Cart> GetCartItem(int userId, int colorCapacityId)
		{
			return await _context.Carts
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.UserId == userId && c.ColorCapacityId == colorCapacityId);
		}

		public async Task<Cart> UpdateCartItemQuantity(int userId, int colorCapacityId, int quantity)
		{
			var cartItem = await _context.Carts
				.FirstOrDefaultAsync(c => c.UserId == userId && c.ColorCapacityId == colorCapacityId);

			if (cartItem == null)
				throw new Exception("Sản phẩm không có trong giỏ hàng");

			// Kiểm tra stock
			var colorCapacity = await _context.ColorCapacities
				.FirstOrDefaultAsync(cc => cc.Id == colorCapacityId);

			if (colorCapacity.Stock < quantity)
				throw new Exception("Không đủ hàng trong kho");

			// Cập nhật quantity và total
			cartItem.Quantity = quantity;
			cartItem.Total = colorCapacity.Price * quantity;

			await _context.SaveChangesAsync();

			return await GetCartItemWithDetails(userId, colorCapacityId);
		}

		public async Task<bool> RemoveFromCart(int userId, int colorCapacityId)
		{
			var cartItem = await _context.Carts
				.FirstOrDefaultAsync(c => c.UserId == userId && c.ColorCapacityId == colorCapacityId);

			if (cartItem == null)
				throw new Exception("Sản phẩm không có trong giỏ hàng");

			_context.Carts.Remove(cartItem);
			await _context.SaveChangesAsync();

			return true;
		}

		private async Task<Cart> GetCartItemWithDetails(int userId, int colorCapacityId)
		{
			return await _context.Carts
				.AsNoTracking()
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Product)
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Color)
				.Include(c => c.ColorCapacity)
					.ThenInclude(cc => cc.Capacity)
				.FirstOrDefaultAsync(c => c.UserId == userId && c.ColorCapacityId == colorCapacityId);
		}
	}
}
