namespace AntrazShop.Models.DTOModels
{
	public class ProductFilter
	{
		public List<int> BrandIds { get; set; }
		public List<int> CategoryIds { get; set; }
		public string SearchText { get; set; }
		public decimal MinPrice { get; set; }
		public decimal MaxPrice { get; set; }
		public bool? AscendingPrice { get; set; }
	}
}
