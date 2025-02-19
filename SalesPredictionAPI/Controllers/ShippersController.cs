using Microsoft.AspNetCore.Mvc;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;

namespace SalesPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController(IShipperRepository shipperRepository) : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository = shipperRepository;

        [HttpGet("GetShippers")]
        public async Task<IActionResult> GetShippers()
        {
            ResponseDTO<List<ShipperDTO>> response = await _shipperRepository.GetShippers();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
