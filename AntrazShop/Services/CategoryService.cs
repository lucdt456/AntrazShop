using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<(IEnumerable<CategoryVM>, Paginate)> GetCategories( int pg, int size)
		{
			var categoryCount = await _categoryRepository.getTotalCategories();
			var pagination = new Paginate(categoryCount, pg, size);
			int recskip = (pg - 1) * size;
			var categpories = await _categoryRepository.GetCategorys(recskip, size);
			var categoryVMs = new List<CategoryVM>();
			foreach (var c in categpories)
			{
				var productCount = await _categoryRepository.getCategoryProductCounts(c.Id);
				categoryVMs.Add(new CategoryVM
				{
					Id = c.Id,
					Name  = c.Name,
					Description = c.Description,
					Image = c.Image,
					ProductCount = productCount
				});
			};
			return (categoryVMs, pagination);
		}

		public async Task CreateCategory(Category category)
		{
			await _categoryRepository.CreateCategory(category);
		}

		public async Task<bool> DeleteCategory(int id)
		{
			return await _categoryRepository.DeleteCategory(id);
		}

		public async Task<Category> GetCategory(int id)
		{
			return await _categoryRepository.GetCategory(id);
		}



		public async Task<Category> UpdateCategory(int id, Category newCategory)
		{
			return await _categoryRepository.UpdateCategory(id, newCategory);
		}
	}
}
