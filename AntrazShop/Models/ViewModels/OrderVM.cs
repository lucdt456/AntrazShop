using AntrazShop.Data;

namespace AntrazShop.Models.ViewModels
{
	public class OrderVM
	{
		public string OrderCode { get; set; }
		public  string Name { get; set; }
		public DateTime CreateAt { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public decimal Total { get; set; }
		public DateTime LastStatusDate { get; set; }
		public string Email { get; set; }
		public string Status { get; set; }
		public List<CartItemVM> OrderDetails { get; set; }
		public List<OrderStatusLogVM> OrderStatusLogVMs { get; set; }
	}
}
