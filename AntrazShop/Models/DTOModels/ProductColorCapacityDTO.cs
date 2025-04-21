namespace AntrazShop.Models.DTOModels
{
	public class ProductColorCapacityDTO
	{
		public string ColorName { get; set; }
		public string CapacityValue { get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
		public int Status { get; set; }
		public IFormFile Image { get; set; }
	}
}
