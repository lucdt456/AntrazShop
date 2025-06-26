using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IBrandRepository
	{
		Task<IEnumerable<Brand>> GetBrands();
		Task<Brand> GetBrand(int id);
		Task<Brand> CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(Brand brandUpdate);
		Task DeleteBrand(int id);
	}
}
