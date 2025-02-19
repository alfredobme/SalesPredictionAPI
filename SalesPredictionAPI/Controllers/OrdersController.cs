using Microsoft.AspNetCore.Mvc;
using SalesPredictionAPI.DataBase.Models;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;

namespace SalesPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderRepository orderRepository) : ControllerBase
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        [HttpGet("GetClientOrders")]
        public async Task<IActionResult> GetClientOrders(int idCustomer)
        {
            ResponseDTO<List<OrderDTO>> response = await _orderRepository.GetClientOrders(idCustomer);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddNewOrder")]
        public async Task<IActionResult> AddNewOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest(new ResponseDTO<bool> { Success = false, Message = "Datos inválidos" });
            }

            ResponseDTO<object> response = await _orderRepository.AddNewOrder(order);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
