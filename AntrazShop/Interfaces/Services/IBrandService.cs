using AntrazShop.Data;
using AntrazShop.Models;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IBrandService
	{
		Task<(IEnumerable<BrandVM>, Paginate)> GetBrands(int pg, int size);
		Task<BrandVM> GetBrand(int id);
		Task CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(int id, Brand newBrand);
		Task<bool> DeleteBrand(int id);
	}
}
