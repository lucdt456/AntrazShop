using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services
{
	public class BrandService : IBrandService
	{
		private readonly IBrandRepository _brandRepository;
		public BrandService(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;
		}

		public async Task<(IEnumerable<BrandVM>, Paginate)> GetBrands(int pg, int size)
		{
			var brandCount = await _brandRepository.GetTotalBrands();

			var pagination = new Paginate(brandCount, pg, size);
			int recskip = (pg - 1) * size;
			var brands = await _brandRepository.GetBrands(recskip, size);
			var brandVMs = new List<BrandVM>();
			foreach (var b in brands)
			{
				var productCount = await _brandRepository.GetBrandProductCounts(b.Id);
				brandVMs.Add(new BrandVM
				{
					Id = b.Id,
					Name = b.Name,
					Description = b.Description,
					Logo = b.Logo,
					ProductCount = productCount
				});
			}
			return (brandVMs, pagination);
		}

		public async Task CreateBrand(Brand brand)
		{
			await _brandRepository.CreateBrand(brand);
		}

		public async Task<bool> DeleteBrand(int id)
		{
			return await _brandRepository.DeleteBrand(id);
		}

		public async Task<BrandVM> GetBrand(int id)
		{
			return await _brandRepository.GetBrand(id);
		}



		public async Task<Brand> UpdateBrand(int id, Brand newBrand)
		{
			return await _brandRepository.UpdateBrand(id, newBrand);
		}

	}
}
