using SalesPredictionAPI.DTO.Models;

namespace SalesPredictionAPI.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ResponseDTO<List<EmployeeDTO>>> GetEmployees();
    }
}
