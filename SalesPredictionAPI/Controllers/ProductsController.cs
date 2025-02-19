using Microsoft.AspNetCore.Mvc;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;

namespace SalesPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            ResponseDTO<List<ProductDTO>> response = await _productRepository.GetProducts();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
