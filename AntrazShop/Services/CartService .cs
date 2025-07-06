using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;
using System.Linq;

namespace AntrazShop.Services
{
	public class CartService : ICartService
	{
		private readonly ICartRepository _cartRepository;
		private readonly IMapper _mapper;
		private readonly IEmailSender _emailSender;
		private readonly IAccountManagerRepository _accountManagerRepository;
		private readonly IOrderManagerRepository _orderManagerRepository;

		public CartService(ICartRepository cartRepository, IMapper mapper, IEmailSender emailSender, IAccountManagerRepository accountManagerRepository, IOrderManagerRepository orderManagerRepository)
		{
			_cartRepository = cartRepository;
			_mapper = mapper;
			_emailSender = emailSender;
			_accountManagerRepository = accountManagerRepository;
			_orderManagerRepository = orderManagerRepository;
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
				dto.Quantity = (dto.Quantity < 1) ? 1 : dto.Quantity;
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

		public async Task<ServiceResponse<string>> CreateOrder(int userId, OrderDTO dto)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var cartItems = await _cartRepository.GetCart(userId);
				if(!cartItems.Any())
				{
					response.IsSuccess = false;
					response.Errors.Add("Không có sản phẩm trong giỏ hàng!");
					return response;
				}
				var guid = Guid.NewGuid();

				decimal total = 0;
				decimal totalDiscountAmount = 0;

				var details = new List<OrderDetail>();
				foreach (var item in cartItems)
				{
					total += item.Total;
					totalDiscountAmount += item.ColorCapacity.Product.DiscountAmount * item.Quantity;
					var detail = new OrderDetail
					{
						OrderCode = guid,
						ColorCapacityId = item.ColorCapacityId,
						Quantity = item.Quantity
					};
					details.Add(detail);
				}

				var order = new Order
				{
					OrderCode = guid,
					UserId = userId,
					CreatedAt = DateTime.Now,
					Status = "Chờ xử lý",
					Adress = dto.Address,
					Name = dto.Name,
					PhoneNumber = dto.PhoneNumber,
					Total = (total - totalDiscountAmount)
				};

				await _cartRepository.CreateCheckout(order);
				var statusLog = new OrderStatusLog
				{
					OrderCode = guid,
					Status = "Đơn hàng được khởi tạo.",
					CreateAt = DateTime.Now
				};

				await _orderManagerRepository.CreateOrderStatusLog(statusLog);


				await _cartRepository.CreateOrderDetail(details);

				var user = await _accountManagerRepository.GetUser(userId);
				var emailBody = $@"
<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <h2 style='color: #333; text-align: center; margin-bottom: 30px;'>XÁC NHẬN ĐỚN HÀNG</h2>
    
    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin-bottom: 20px;'>
        <h3 style='color: #495057; margin-top: 0;'>Thông tin đơn hàng</h3>
        <p><strong>Mã đơn hàng:</strong> {order.OrderCode}</p>
        <p><strong>Ngày đặt:</strong> {order.CreatedAt:dd/MM/yyyy HH:mm}</p>
        <p><strong>Trạng thái:</strong> {order.Status}</p>
    </div>
    
    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin-bottom: 20px;'>
        <h3 style='color: #495057; margin-top: 0;'>Thông tin giao hàng</h3>
        <p><strong>Họ tên:</strong> {order.Name}</p>
        <p><strong>Số điện thoại:</strong> {order.PhoneNumber}</p>
        <p><strong>Địa chỉ:</strong> {order.Adress}</p>
    </div>
    
    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin-bottom: 20px;'>
        <h3 style='color: #495057; margin-top: 0;'>Chi tiết đơn hàng</h3>
        <table style='width: 100%; border-collapse: collapse;'>
            <thead>
                <tr style='background-color: #dee2e6;'>
                    <th style='padding: 12px; text-align: left; border-bottom: 1px solid #ccc;'>Sản phẩm</th>
                    <th style='padding: 12px; text-align: center; border-bottom: 1px solid #ccc;'>Số lượng</th>
                    <th style='padding: 12px; text-align: right; border-bottom: 1px solid #ccc;'>Thành tiền</th>
                </tr>
            </thead>
            <tbody>";

				foreach (var item in cartItems)
				{
					emailBody += $@"
                <tr>
                    <td style='padding: 12px; border-bottom: 1px solid #eee;'>
                        {item.ColorCapacity.Product.Name}<br>
                        <small style='color: #6c757d;'>{item.ColorCapacity.Color.NameColor} {item.ColorCapacity.Capacity.Value}</small>
                    </td>
                    <td style='padding: 12px; text-align: center; border-bottom: 1px solid #eee;'>×{item.Quantity}</td>
                    <td style='padding: 12px; text-align: right; border-bottom: 1px solid #eee;'>{item.Total:N0} VNĐ</td>
                </tr>";
				}

				emailBody += $@"
            </tbody>
        </table>
    </div>
    
    <div style='background-color: #e8f5e8; padding: 20px; border-radius: 8px; border-left: 4px solid #28a745;'>
        <table style='width: 100%; margin-bottom: 0;'>
            <tr>
                <td style='padding: 5px 0;'><strong>Tổng tiền:</strong></td>
                <td style='text-align: right; padding: 5px 0;'>{total:N0} VNĐ</td>
            </tr>
            <tr>
                <td style='padding: 5px 0;'><strong>Giảm giá:</strong></td>
                <td style='text-align: right; padding: 5px 0;'>-{totalDiscountAmount:N0} VNĐ</td>
            </tr>
            <tr>
                <td style='padding: 5px 0;'><strong>Phí giao hàng:</strong></td>
                <td style='text-align: right; padding: 5px 0;'>Miễn phí</td>
            </tr>
            <tr style='border-top: 2px solid #28a745; font-size: 18px; color: #28a745;'>
                <td style='padding: 10px 0 0 0;'><strong>TỔNG THANH TOÁN:</strong></td>
                <td style='text-align: right; padding: 10px 0 0 0;'><strong>{(total - totalDiscountAmount):N0} VNĐ</strong></td>
            </tr>
        </table>
    </div>
    
    <div style='text-align: center; margin-top: 30px; padding-top: 20px; border-top: 1px solid #dee2e6;'>
        <p style='color: #6c757d; margin-bottom: 10px;'>Cảm ơn bạn đã mua hàng tại AntrazShop!</p>
        <p style='color: #6c757d; font-size: 14px;'>Mọi thắc mắc xin liên hệ: contact@antrazshop.com</p>
    </div>
</div>";

				await _emailSender.SendEmailAsync(user.Email, "Xác nhận đặt hàng", emailBody);
				await _cartRepository.RemoveAllFromCart(userId);
				response.Data = "Đặt hàng thành công!";

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
