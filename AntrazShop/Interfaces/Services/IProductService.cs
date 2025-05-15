using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IProductService
	{
		Task<(IEnumerable<ProductVM>, Paginate)> GetProducts(int pg, int size);
		Task<(IEnumerable<ProductVM>, Paginate)> SearchProducts(string search, int pg, int size);
		Task<ProductVM> GetProduct(int id);
		Task<List<string>> AddProduct(ProductDTO newProduct);
		Task<Product> UpdateProduct(int id, ProductDTO productUpdaTe);
		Task<bool> DeleteProduct(int id);
	}
}
