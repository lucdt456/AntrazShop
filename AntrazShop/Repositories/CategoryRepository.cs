using AntrazShop.Data;
using AntrazShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ShopDbContext _context;
		public CategoryRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Category>> GetCategorys()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> GetCategory(int id)
		{
			return await _context.Categories.FindAsync(id);
		}

		public async Task CreateCategory(Category category)
		{
			if (category != null)
			{
				await _context.Categories.AddAsync(category);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<Category> UpdateCategory(int id, Category newCategory)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				category.Name = newCategory.Name;
				category.Description = newCategory.Description;
				category.Image = newCategory.Image;

				_context.Categories.Update(category);
				await _context.SaveChangesAsync();
				return category;
			}
			else return null;
		}

		public async Task<bool> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if(category != null)
			{
				_context.Remove(category);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
