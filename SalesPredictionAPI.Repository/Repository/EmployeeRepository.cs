using Microsoft.Extensions.Configuration;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;
using System.Data.SqlClient;

namespace SalesPredictionAPI.Repository.Repository
{
    public class EmployeeRepository(IConfiguration configuration): IEmployeeRepository
    {
        private readonly string? _connectionSQL = configuration.GetConnectionString("SQLConnection");

        public async Task<ResponseDTO<List<EmployeeDTO>>> GetEmployees()
        {
            var response = new ResponseDTO<List<EmployeeDTO>>
            {
                Success = true,
                Data = []
            };

            string sqlQuery = @"
                SELECT empid,
                       CONCAT(firstname, ' ', lastname) AS FullName
                FROM [HR].[Employees]
                ORDER BY FullName";

            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();
                using SqlCommand command = new(sqlQuery, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data.Add(new EmployeeDTO
                    {
                        EmpId = Convert.ToInt32(reader["empid"]),
                        FullName = reader["FullName"].ToString() ?? "",
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
