using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;

namespace AntrazShop.Services
{
	public class CartService : ICartService
	{
		private readonly ICartRepository _cartRepository;
		private readonly IMapper _mapper;

		public CartService(ICartRepository cartRepository, IMapper mapper)
		{
			_cartRepository = cartRepository;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<CartItemVM>> AddToCart(AddToCartDto dto)
		{
			var response = new ServiceResponse<CartItemVM>();
			try
			{
				// Kiểm tra sản phẩm có tồn tại trong giỏ hàng không
				var existingItem = await _cartRepository.GetCartItem(dto.UserId, dto.ColorCapacityId);

				Cart cartItem;
				if (existingItem != null)
				{
					// Nếu đã có trong giỏ hàng, cập nhật số lượng
					cartItem = await _cartRepository.UpdateCartItemQuantity(
						dto.UserId,
						dto.ColorCapacityId,
						existingItem.Quantity + dto.Quantity
					);
				}
				else
				{
					// Thêm mới vào giỏ hàng
					cartItem = await _cartRepository.AddToCart(dto);
				}

				response.Data = _mapper.Map<CartItemVM>(cartItem);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<List<CartItemVM>>> GetCart(int userId)
		{
			var response = new ServiceResponse<List<CartItemVM>>();
			try
			{
				var cartItems = await _cartRepository.GetCart(userId);
				response.Data = _mapper.Map<List<CartItemVM>>(cartItems);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<CartItemVM>> UpdateCartItem(UpdateCartDto dto)
		{
			var response = new ServiceResponse<CartItemVM>();
			try
			{
				var cartItem = await _cartRepository.UpdateCartItemQuantity(
					dto.UserId,
					dto.ColorCapacityId,
					dto.Quantity
				);
				response.Data = _mapper.Map<CartItemVM>(cartItem);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<bool>> RemoveFromCart(int userId, int colorCapacityId)
		{
			var response = new ServiceResponse<bool>();
			try
			{
				var result = await _cartRepository.RemoveFromCart(userId, colorCapacityId);
				response.Data = result;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}
	}
}
