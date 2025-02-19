using Microsoft.AspNetCore.Mvc;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;

namespace SalesPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerRepository customerRepository) : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        [HttpGet("SalesDatePrediction")]
        public async Task<IActionResult> GetCustomers()
        {
            ResponseDTO<List<CustomerDTO>> response = await _customerRepository.GetCustomers();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
