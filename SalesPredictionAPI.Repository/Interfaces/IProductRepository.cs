using SalesPredictionAPI.DTO.Models;

namespace SalesPredictionAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<ResponseDTO<List<ProductDTO>>> GetProducts();
    }
}
