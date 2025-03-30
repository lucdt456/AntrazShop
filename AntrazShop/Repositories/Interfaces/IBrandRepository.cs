using AntrazShop.Data;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Repositories.Interfaces
{
	public interface IBrandRepository
	{
		Task<IEnumerable<Brand>> GetBrands(int recskip, int take);
		Task<BrandVM> GetBrand(int id);
		Task CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(int id, Brand newBrand);
		Task<bool> DeleteBrand(int id);
		Task<int> GetTotalBrands();
		Task<int> GetBrandProductCounts(int id);
	}
}
