namespace AntrazShop.Models.ViewModels
{
	public class DashboardStatsVM
	{
		public int TotalOrders { get; set; }
		public float OrdersGrowthRate { get; set; }

		public int TotalProductsSold { get; set; }
		public float ProductsSoldGrowthRate { get; set; }
		public decimal TotalRevenue { get; set; }
		public float RevenueGrowthRate { get; set; }
		public int TotalNewCustomers{ get; set; }
		public float NewCustomersGrowthRate { get; set; }
	}
}
