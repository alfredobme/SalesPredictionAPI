using Microsoft.Extensions.Configuration;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;
using System.Data.SqlClient;

namespace SalesPredictionAPI.Repository.Repository
{
    public class ShipperRepository(IConfiguration configuration): IShipperRepository
    {
        private readonly string? _connectionSQL = configuration.GetConnectionString("SQLConnection");

        public async Task<ResponseDTO<List<ShipperDTO>>> GetShippers()
        {
            var response = new ResponseDTO<List<ShipperDTO>>
            {
                Success = true,
                Data = []
            };

            string sqlQuery = @"
                SELECT shipperid,
                       companyname
                FROM [Sales].[Shippers]";

            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();
                using SqlCommand command = new(sqlQuery, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data.Add(new ShipperDTO
                    {
                        ShipperId = Convert.ToInt32(reader["shipperid"]),
                        CompanyName = reader["companyname"].ToString() ?? "",
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
