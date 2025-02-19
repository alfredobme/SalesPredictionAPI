using Microsoft.Extensions.Configuration;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;
using System.Data.SqlClient;

namespace SalesPredictionAPI.Repository.Repository
{
    public class ProductRepository(IConfiguration configuration) : IProductRepository
    {
        private readonly string? _connectionSQL = configuration.GetConnectionString("SQLConnection");
        public async Task<ResponseDTO<List<ProductDTO>>> GetProducts()
        {
            var response = new ResponseDTO<List<ProductDTO>>
            {
                Success = true,
                Data = []
            };

            string sqlQuery = @"
                SELECT productid,
                       productname
                FROM [Production].[Products]
                ORDER BY productname";

            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();
                using SqlCommand command = new(sqlQuery, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data.Add(new ProductDTO
                    {
                        ProductId = Convert.ToInt32(reader["productid"]),
                        ProductName = reader["productname"].ToString() ?? "",
                    });
                }
            }
            catch (SqlException ex)
            {
                response.Success = false;
                response.Message = $"Error de SQL: {ex.Message}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error inesperado: {ex.Message}";
            }

            return response;
        }
    }
}
