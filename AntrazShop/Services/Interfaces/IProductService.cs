using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductVM>> GetProducts();
		Task<ProductVM?> GetProduct(int id);
		Task<Product> AddProduct(ProductDTO newProduct);
		Task<Product> UpdateProduct(int id, ProductDTO productUpdaTe);
		Task<bool> DeleteProduct(int id);
	}
}
