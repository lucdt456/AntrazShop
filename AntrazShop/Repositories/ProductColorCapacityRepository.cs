using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class ProductColorCapacityRepository : IProductColorCapacityRepository
	{
		private ShopDbContext _context;
		public ProductColorCapacityRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<int> AddCapacity(string capacityValue)
		{ 
			var capacity = await _context.Capacities.FirstOrDefaultAsync(c => c.Value == capacityValue);
			if (capacity != null)
			{
				return capacity.Id;
			}
			else
			{
				capacity = new Capacity
				{
					Value = capacityValue
				};
				await _context.Capacities.AddAsync(capacity);
				await _context.SaveChangesAsync();
				return capacity.Id;
			}
		}

		public async Task<int> AddColor(string colorName)
		{
			var color = await _context.Colors.FirstOrDefaultAsync(c => c.NameColor == colorName);
			if (color != null)
			{
				return color.Id;
			}
			else
			{
				color = new Color
				{
					NameColor = colorName
				};
				await _context.AddAsync(color);
				await _context.SaveChangesAsync();
				return color.Id;
			}

		}

		public async Task AddColorCapacity(ColorCapacity colorCapacity)
		{
			await _context.ColorCapacities.AddAsync(colorCapacity);
			await _context.SaveChangesAsync();
		}
	}
}
