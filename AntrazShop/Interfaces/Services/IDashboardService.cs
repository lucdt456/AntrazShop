using AntrazShop.Helper;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IDashboardService
	{
		/// <summary>
		/// Lấy số liệu cho 4 ô trên cùng của dashboard
		/// </summary>
		/// <returns></returns>
		Task<ServiceResponse<DashboardStatsVM>> GetDashboardStatsThisMonth();
		Task<ServiceResponse<OrderOverviewVM>> GetOrderOverview(int days);
		Task<ServiceResponse<List<decimal>>> GetRevenueOverview();
	}
}
