using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IOrderManagerRepository
	{
		Task<IEnumerable<Order>> GetOrders(string ordercode, string status, int skip, int take);
		Task<int> GetTotalOrdersCount(string orderCode, string status);
		Task UpdateStatusOrder(Guid orderCode, string status);
		Task CreateOrderStatusLog(OrderStatusLog log);
		Task<IEnumerable<OrderDetail>> GetOderDetails(Guid orderCode);
		Task UpdateProductCCSoldAmount(IEnumerable<OrderDetail> orderDetails);
		Task<IEnumerable<Order>> GetOrdersByUser(int userId);
	}
}
