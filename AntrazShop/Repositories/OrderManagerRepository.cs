using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class OrderManagerRepository : IOrderManagerRepository
	{
		private readonly ShopDbContext _context;

		public OrderManagerRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Order>> GetOrders(string ordercode, string status, int skip, int take)
		{
			return await _context.Orders
				.AsNoTracking()
				.OrderByDescending(o => o.CreatedAt)
				.Where(o => o.OrderCode.ToString().Contains(ordercode) && o.Status.Contains(status))
				.Include(o => o.User)
				.Include(o => o.OrderStatusLogs)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Product)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Color)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Capacity)
				.Skip(skip)
				.Take(take)
				.ToListAsync();
		}

		public async Task<int> GetTotalOrdersCount(string orderCode, string status)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(o => o.OrderCode.ToString().Contains(orderCode) && o.Status.Contains(status))
				.CountAsync();
		}
		public async Task UpdateStatusOrder(Guid orderCode, string status)
		{
			var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode.ToString() == orderCode.ToString());
			if (order == null) throw new Exception("Không tìm thấy đơn hàng");
			order.Status = status;
			await _context.SaveChangesAsync();
		}
		public async Task CreateOrderStatusLog(OrderStatusLog log)
		{
			_context.OrderStatusLogs.Add(log);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<OrderDetail>> GetOderDetails(Guid orderCode)
		{
			return await _context.OrderDetails
				.AsNoTracking()
				.Where(od => od.OrderCode == orderCode).ToListAsync();
		}

		public async Task UpdateProductCCSoldAmount(IEnumerable<OrderDetail> orderDetails)
		{
			foreach(var orderDetail in orderDetails)
			{
				var productCC = _context.ColorCapacities
					.Find(orderDetail.ColorCapacityId);
				if (productCC != null)
				{
					productCC.SoldAmount += orderDetail.Quantity;
					productCC.Stock -= orderDetail.Quantity;
				}
			}
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> GetOrdersByUser(int userId)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(o => o.User.Id == userId)
				.OrderByDescending(o => o.CreatedAt)
				.Include(o => o.OrderStatusLogs)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Product)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Color)
				.Include(o => o.OrderDetails)
					.ThenInclude(od => od.ColorCapacity)
						.ThenInclude(cc => cc.Capacity)
				.ToListAsync();
		}
	}
}