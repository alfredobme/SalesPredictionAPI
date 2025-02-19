using Microsoft.AspNetCore.Mvc;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;

namespace SalesPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeRepository employeeRepository) : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            ResponseDTO<List<EmployeeDTO>> response = await _employeeRepository.GetEmployees();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
