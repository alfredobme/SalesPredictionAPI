using SalesPredictionAPI.DTO.Models;

namespace SalesPredictionAPI.Repository.Interfaces
{
    public interface IShipperRepository
    {
        Task<ResponseDTO<List<ShipperDTO>>> GetShippers();
    }
}
