using AntrazShop.Data;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services.Interfaces
{
	public interface IBrandService
	{
		Task<IEnumerable<BrandVM>> GetBrands();
		Task<BrandVM> GetBrand(int id);
		Task CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(int id, Brand newBrand);
		Task<bool> DeleteBrand(int id);
	}
}
