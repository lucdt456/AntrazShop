using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IOrderManagerService
	{
		Task<(ServiceResponse<IEnumerable<OrderVM>>, Paginate)> GetOrders(string orderCode, string status, int currentPG, int Size);
		Task<ServiceResponse<string>> UpdateOrderStatus(Guid orderCode, string status);
		Task<ServiceResponse<IEnumerable<OrderVM>>> GetOrdersByUser(int userId);
	}
}
