using AntrazShop.Data;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IProductCCService
	{
		Task<ServiceResponse<ColorCapacity>> EditColorCapacity(string productFolder, int id, ProductColorCapacityDTO productCCDTO);
	}
}
