namespace AntrazShop.Models.ViewModels
{
	public class OrderOverviewVM
	{
		public List<int> NewOrdersCountDetails { get; set; }
		public int ProcessOrderCount { get; set; }
		public int FailedOrdersCount { get; set; }
		public int CompletedOrderCount { get; set; }
	}
}
