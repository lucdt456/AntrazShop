using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.ViewModels;
using AutoMapper;

namespace AntrazShop.Services
{
	public class OrderManagerService : IOrderManagerService
	{
		private readonly IOrderManagerRepository _orderManagerRepository;
		private readonly IMapper _mapper;

		public OrderManagerService(IOrderManagerRepository orderManagerRepository, IMapper mapper)
		{
			_orderManagerRepository = orderManagerRepository;
			_mapper = mapper;
		}
		public async Task<(ServiceResponse<IEnumerable<OrderVM>>, Paginate)> GetOrders(string orderCode, string status, int currentPG, int Size)
		{
			var response = new ServiceResponse<IEnumerable<OrderVM>>();
			try
			{
				var count = await _orderManagerRepository.GetTotalOrdersCount(orderCode, status);

				var pagination = new Paginate(count, currentPG, Size);
				int recskip = (currentPG - 1) * Size;
				recskip = (recskip < 0) ? 1 : recskip;

				var orders = await _orderManagerRepository.GetOrders(orderCode, status, recskip, Size);

				response.Data = _mapper.Map<List<OrderVM>>(orders);

				return (response, pagination);
			} 
			catch(Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
				return (response, null);
			}
		}

		public async Task<ServiceResponse<IEnumerable<OrderVM>>> GetOrdersByUser(int userId)
		{
			var response = new ServiceResponse<IEnumerable<OrderVM>>();
			try
			{
				var orders = await _orderManagerRepository.GetOrdersByUser(userId);
				
				response.Data = _mapper.Map<List<OrderVM>>(orders);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> UpdateOrderStatus(Guid orderCode, string status)
		{
			var response = new ServiceResponse<string>();
			try
			{
				await _orderManagerRepository.UpdateStatusOrder(orderCode, status);

				var statusLog = new OrderStatusLog
				{
					OrderCode = orderCode,
					Status = "Đơn hàng đã được chuyển sang trạng thái " + status,
					CreateAt = DateTime.Now
				};

				await _orderManagerRepository.CreateOrderStatusLog(statusLog);

				if(status == "Đã giao")
				{
					var orderDetails = await _orderManagerRepository.GetOderDetails(orderCode);
					await _orderManagerRepository.UpdateProductCCSoldAmount(orderDetails);
				}

				response.Data="Cập nhật trạng thái thành công";
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
