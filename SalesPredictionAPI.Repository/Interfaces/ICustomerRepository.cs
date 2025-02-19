using SalesPredictionAPI.DTO.Models;

namespace SalesPredictionAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ResponseDTO<List<CustomerDTO>>> GetCustomers();
    }
}
