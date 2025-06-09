using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IProductCCService
	{
		Task<ServiceResponse<ColorCapacity>> EditColorCapacity(string productFolder, int id, ProductColorCapacityDTO dTO);
		Task<ServiceResponse<ColorCapacity>> CreateColorCapacity(int idProduct, string productFolder, ProductColorCapacityDTO dTO);
		Task<ServiceResponse<bool>> DeleteColorCapacity(int id, string productFolder);
	}
}
