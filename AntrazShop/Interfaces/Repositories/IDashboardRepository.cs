namespace AntrazShop.Interfaces.Repositories
{
	public interface IDashboardRepository
	{
		/// <summary>
		/// Hàm lấy số đơn hàng trong tháng
		/// </summary>
		/// <param name="lastMonth"></param>
		/// <returns>Tháng này nếu true. Tháng trước nếu false</returns>
		Task<int> GetTotalOrdersThisMonth(bool thisMonth);
		/// <summary>
		/// Hàm lấy số đơn phẩm bán trong tháng
		/// </summary>
		/// <param name="lastMonth"></param>
		/// <returns>Tháng này nếu true. Tháng trước nếu false</returns>
		Task<int> GetTotalProductsSoldThisMonth(bool thisMonth);
		/// <summary>
		/// Hàm lấy doanh số trong tháng
		/// </summary>
		/// <param name="lastMonth"></param>
		/// <returns>Tháng này nếu true. Tháng trước nếu false</returns>
		Task<decimal> GetTotalRevenueThisMonth(bool thisMonth);
		/// <summary>
		/// Hàm lấy số tài khoản khách hàng mới trong tháng
		/// </summary>
		/// <param name="lastMonth"></param>
		/// <returns>Tháng này nếu true. Tháng trước nếu false</returns>
		Task<int> GetTotalNewCustomersThisMonth(bool thisMonth);
		/// <summary>
		/// Lấy số lượng đơn hàng trong ngày
		/// </summary>
		/// <param name="day">Nhập ngày cần lấy</param>
		/// <returns></returns>
		Task<int> GetOrderCount(DateTime day);
		/// <summary>
		/// Đếm đơn hàng hoàn thành trong khoảng thời gian
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		Task<int> GetOrderCompleted(DateTime startDate);
		/// <summary>
		/// Đếm đơn hàng thất bại trong khoảng thời gian
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		Task<int> GetOrderFalsed(DateTime startDate);
		Task<decimal> getTotalRevenuebyMonth(int month);
	}
}
