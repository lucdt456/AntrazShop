using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly IDashboardRepository _dashboardRepository;

		public DashboardService(IDashboardRepository dashboardRepository)
		{
			_dashboardRepository = dashboardRepository;
		}

		public async Task<ServiceResponse<DashboardStatsVM>> GetDashboardStatsThisMonth()
		{
			var response = new ServiceResponse<DashboardStatsVM>();
			try
			{
				//Lấy thống kê tháng này
				var totalOrdersThisMonth = await _dashboardRepository.GetTotalOrdersThisMonth(true);
				var totalProductsSoldThisMonth = await _dashboardRepository.GetTotalProductsSoldThisMonth(true);
				var totalRevenueThisMonth = await _dashboardRepository.GetTotalRevenueThisMonth(true);
				var totalNewCustomersThisMonth = await _dashboardRepository.GetTotalNewCustomersThisMonth(true);

				//Lấy thống kê tháng trước
				var totalOrdersLastMonth = await _dashboardRepository.GetTotalOrdersThisMonth(false);
				totalOrdersLastMonth = (totalOrdersLastMonth == 0) ? 1 : totalOrdersLastMonth;

				var totalProductsSoldLastMonth = await _dashboardRepository.GetTotalProductsSoldThisMonth(false);
				totalProductsSoldLastMonth = (totalProductsSoldLastMonth == 0) ? 1 : totalProductsSoldLastMonth;

				var totalRevenueLastMonth = await _dashboardRepository.GetTotalRevenueThisMonth(false);
				totalRevenueLastMonth = (totalRevenueLastMonth == 0) ? 1 : totalRevenueLastMonth;

				var totalNewCustomersLastMonth = await _dashboardRepository.GetTotalNewCustomersThisMonth(false);
				totalNewCustomersLastMonth = (totalNewCustomersLastMonth == 0) ? 1 : totalNewCustomersLastMonth;

				//tính số ngày đã qua trong tháng
				var today = DateTime.Now;
				var startOfMonth = new DateTime(today.Year, today.Month, 1);
				var countThisMonthDay = (today - startOfMonth).Days + 1;

				//Tính số ngày tháng trước
				var startLastMonth = startOfMonth.AddMonths(-1);
				var counLastMountDay = (startOfMonth.AddDays(-1) - startLastMonth).Days + 1;

				//Trung bình ngày tháng này
				var AverageOrdersThisMonth = (float)totalOrdersThisMonth / countThisMonthDay;
				var AverageProductsSoldThisMonth = (float)totalProductsSoldThisMonth / countThisMonthDay;
				var AverageRevenueThisMonth = totalRevenueThisMonth / countThisMonthDay;
				var AverageNewCustomersThisMonth = (float)totalNewCustomersThisMonth / countThisMonthDay;

				//Trung bình ngày tháng trước
				var AverageOrdersLastMonth = (float)totalOrdersLastMonth / counLastMountDay;
				var AverageProductsSoldLastMonth = (float)totalProductsSoldLastMonth / counLastMountDay;
				var AverageRevenueLastMonth = totalRevenueLastMonth / counLastMountDay;
				var AverageNewCustomersLastMonth = (float)totalNewCustomersLastMonth / counLastMountDay;

				// Tránh chia cho 0 trong growth rate
				AverageOrdersLastMonth = (AverageOrdersLastMonth == 0) ? 1 : AverageOrdersLastMonth;
				AverageProductsSoldLastMonth = (AverageProductsSoldLastMonth == 0) ? 1 : AverageProductsSoldLastMonth;
				AverageRevenueLastMonth = (AverageRevenueLastMonth == 0) ? 1000 : AverageRevenueLastMonth;
				AverageNewCustomersLastMonth = (AverageNewCustomersLastMonth == 0) ? 1 : AverageNewCustomersLastMonth;

				response.Data = new DashboardStatsVM()
				{
					TotalNewCustomers = totalNewCustomersThisMonth,
					TotalOrders = totalOrdersThisMonth,
					TotalProductsSold = totalProductsSoldThisMonth,
					TotalRevenue = totalRevenueThisMonth,

					NewCustomersGrowthRate = (float)Math.Round((100 * (AverageNewCustomersThisMonth - AverageNewCustomersLastMonth) / AverageNewCustomersLastMonth), 2),
					OrdersGrowthRate = (float)Math.Round((100 * (AverageOrdersThisMonth - AverageOrdersLastMonth) / AverageOrdersLastMonth), 2),
					ProductsSoldGrowthRate = (float)Math.Round((100 * (AverageProductsSoldThisMonth - AverageProductsSoldLastMonth) / AverageProductsSoldLastMonth), 2),
					RevenueGrowthRate = (float)Math.Round((100 * (AverageRevenueThisMonth - AverageRevenueLastMonth) / AverageRevenueLastMonth), 2)
				};
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<OrderOverviewVM>> GetOrderOverview(int days)
		{
			var response = new ServiceResponse<OrderOverviewVM>();
			try
			{
				var timeNow = DateTime.Now;
				var startDate = timeNow.AddDays(-days).Date;

				var listOrderCountDetails = new List<int>();
				for (int i = days - 1; i >= 0; i--)
				{
					var day = timeNow.AddDays(-i).Date;
					var count = await _dashboardRepository.GetOrderCount(day);
					listOrderCountDetails.Add(count);
				}

				var countCompleted = await _dashboardRepository.GetOrderCompleted(startDate);
				var countFailedOrders = await _dashboardRepository.GetOrderFalsed(startDate);

				response.Data = new OrderOverviewVM()
				{
					NewOrdersCountDetails = listOrderCountDetails,
					CompletedOrderCount = countCompleted,
					FailedOrdersCount = countFailedOrders,
					ProcessOrderCount = listOrderCountDetails.Sum() - countCompleted - countFailedOrders
				};

			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<List<decimal>>> GetRevenueOverview()
		{
			var response = new ServiceResponse<List<decimal>>();
			try
			{
				var now = DateTime.Now;
				response.Data = new List<decimal>();
				for (int i = 1; i <= now.Month; i++)
				{
					var total = await _dashboardRepository.getTotalRevenuebyMonth(i);
					response.Data.Add(total);
				}
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