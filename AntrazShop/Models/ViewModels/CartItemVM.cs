namespace AntrazShop.Models.ViewModels
{
	public class CartItemVM
	{
		public int UserId { get; set; }
		public int ColorCapacityId { get; set; }
		public int Quantity { get; set; }
		public decimal Total { get; set; }

		// Product details
		public int ProductId { get; set; }
		public string FolderImage { get; set; }
		public string ProductName { get; set; }
		public string ProductImage { get; set; }
		public string ColorName { get; set; }
		public string CapacityValue { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
}
