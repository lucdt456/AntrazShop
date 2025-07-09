using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly ShopDbContext _context;

		public DashboardRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<int> GetTotalNewCustomersThisMonth(bool thisMonth)
		{
			DateTime startDate, endDate;

			if (thisMonth)
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}
			else
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}

			return await _context.Users
				.AsNoTracking()
				.Where(u => !u.workerAccount)
				.Where(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate)
				.CountAsync();
		}

		public async Task<int> GetTotalOrdersThisMonth(bool thisMonth)
		{
			DateTime startDate, endDate;

			if (thisMonth)
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}
			else
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}

			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.CreatedAt >= startDate && or.CreatedAt <= endDate)
				.Where(or => or.Status == "Đã giao" || or.Status == "Hoàn thành")
				.CountAsync();
		}

		public async Task<int> GetTotalProductsSoldThisMonth(bool thisMonth)
		{
			DateTime startDate, endDate;

			if (thisMonth)
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}
			else
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}

			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.CreatedAt >= startDate && or.CreatedAt <= endDate)
				.Where(or => or.Status == "Đã giao" || or.Status == "Hoàn thành")
				.SelectMany(or => or.OrderDetails)
				.SumAsync(od => od.Quantity);
		}

		public async Task<decimal> GetTotalRevenueThisMonth(bool thisMonth)
		{
			DateTime startDate, endDate;

			if (thisMonth)
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}
			else
			{
				startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
				endDate = startDate.AddMonths(1).AddDays(-1);
			}

			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.CreatedAt >= startDate && or.CreatedAt <= endDate)
				.Where(or => or.Status == "Đã giao" || or.Status == "Hoàn thành")
				.SumAsync(od => od.Total);
		}
		public async Task<int> GetOrderCount(DateTime day)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(o => o.CreatedAt.Year == day.Year && o.CreatedAt.Month == day.Month && o.CreatedAt.Day == day.Day)
				.CountAsync();
		}

		public async Task<int> GetOrderCompleted(DateTime startDate)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.Status == "Đã giao" || or.Status == "Hoàn thành")
				.Where(o => o.CreatedAt >= startDate).CountAsync();
		}

		public async Task<int> GetOrderFalsed(DateTime startDate)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.Status == "Giao thất bại" || or.Status == "Đã huỷ")
				.Where(o => o.CreatedAt >= startDate).CountAsync();
		}

		public async Task<decimal> getTotalRevenuebyMonth(int month)
		{
			var now = DateTime.Now;
			return await _context.Orders
				.AsNoTracking()
				.Where(or => or.CreatedAt.Month == month && or.CreatedAt.Year == now.Year)
				.Where(or => or.Status == "Đã giao" || or.Status == "Hoàn thành")
				.SumAsync(od => od.Total);
		}
	}
}