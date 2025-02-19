using SalesPredictionAPI.DataBase.Models;
using SalesPredictionAPI.DTO.Models;

namespace SalesPredictionAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<ResponseDTO<List<OrderDTO>>> GetClientOrders(int idCustomer);

        Task<ResponseDTO<object>> AddNewOrder(Order order);
    }
}
