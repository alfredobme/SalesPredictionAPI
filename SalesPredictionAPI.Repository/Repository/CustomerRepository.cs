using Microsoft.Extensions.Configuration;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;
using System.Data.SqlClient;

namespace SalesPredictionAPI.Repository.Repository
{
    public class CustomerRepository(IConfiguration configuration): ICustomerRepository
    {
        private readonly string? _connectionSQL = configuration.GetConnectionString("SQLConnection");

        public async Task<ResponseDTO<List<CustomerDTO>>> GetCustomers()
        {
            var response = new ResponseDTO<List<CustomerDTO>>
            {
                Success = true,
                Data = []
            };

            string sqlQuery = @"
                SELECT 
                    Customers.custid AS CustomerId,
                    Customers.companyname AS CustomerName,         
                    MAX(Orders.orderdate) AS LastOrderDate,
                    DATEADD(DAY, (DATEDIFF(DAY, MIN(Orders.orderdate), MAX(Orders.orderdate)) / COUNT(Orders.custid)), MAX(Orders.orderdate)) AS NextPredictedOrder
                FROM [StoreSample].[Sales].[Orders] AS Orders
                INNER JOIN [StoreSample].[Sales].[Customers] AS Customers ON Customers.custid = Orders.custid
                GROUP BY Customers.custid, Customers.companyname";

            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();
                using SqlCommand command = new(sqlQuery, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data.Add(new CustomerDTO
                    {
                        CustomerId = Convert.ToInt32(reader["CustomerId"]).ToString(),
                        CustomerName = reader["CustomerName"].ToString() ?? "",
                        LastOrderDate = Convert.ToDateTime(reader["LastOrderDate"]),
                        NextPredictedOrder = Convert.ToDateTime(reader["NextPredictedOrder"])
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
