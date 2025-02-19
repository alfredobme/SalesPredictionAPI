using Microsoft.Extensions.Configuration;
using SalesPredictionAPI.DataBase.Models;
using SalesPredictionAPI.DTO.Models;
using SalesPredictionAPI.Repository.Interfaces;
using System.Data.SqlClient;

namespace SalesPredictionAPI.Repository.Repository
{
    public class OrderRepository(IConfiguration configuration) : IOrderRepository
    {
        private readonly string? _connectionSQL = configuration.GetConnectionString("SQLConnection");

        public async Task<ResponseDTO<List<OrderDTO>>> GetClientOrders(int idCustomer)
        {
            var response = new ResponseDTO<List<OrderDTO>>
            {
                Success = true,
                Data = []
            };

            string sqlQuery = @"
                SELECT 
	                [orderid],
	                [requireddate],
	                [shippeddate],
	                [shipname],
	                [shipaddress],
	                [shipcity]
                FROM
	                [Sales].[Orders]
                WHERE 
	                [Sales].[Orders].custid = @idCustomer";
            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();
                using SqlCommand command = new(sqlQuery, connection);
                command.Parameters.AddWithValue("@idCustomer", idCustomer);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    response.Data.Add(new OrderDTO
                    {
                        Orderid = Convert.ToInt32(reader["orderid"]),
                        Requireddate = reader["requireddate"] == DBNull.Value ? null : Convert.ToDateTime(reader["requireddate"]),
                        Shippeddate = reader["shippeddate"] == DBNull.Value ? null : Convert.ToDateTime(reader["shippeddate"]),
                        Shipname = reader["shipname"]?.ToString() ?? string.Empty,
                        Shipaddress = reader["shipaddress"]?.ToString() ?? string.Empty,
                        Shipcity = reader["shipcity"]?.ToString() ?? string.Empty,
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
        public async Task<ResponseDTO<object>> AddNewOrder(Order order)
        {
            var response = new ResponseDTO<object>
            {
                Success = true,
                Data = new List<object>()
            };

            string sqlOrderQuery = @" 
                INSERT INTO [Sales].[Orders] (custid, empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
                OUTPUT INSERTED.Orderid                
                VALUES (@custid, @empid, @shipperid, @shipname, @shipaddress, @shipcity, @orderdate, @requireddate, @shippeddate, @freight, @shipcountry)";

            string sqlOrderDetailQuery = @"
                INSERT INTO [Sales].[OrderDetails] (orderid, productid, unitprice, qty, discount)
                VALUES (@orderid, @productid, @unitprice, @qty, @discount)";

            try
            {
                using SqlConnection connection = new(_connectionSQL);
                await connection.OpenAsync();

                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Insertar la orden
                    using SqlCommand orderCommand = new(sqlOrderQuery, connection, transaction);
                    orderCommand.Parameters.AddWithValue("@custid", order.Custid);
                    orderCommand.Parameters.AddWithValue("@empid", order.Empid);
                    orderCommand.Parameters.AddWithValue("@shipperid", order.Shipperid);
                    orderCommand.Parameters.AddWithValue("@shipname", order.Shipname);
                    orderCommand.Parameters.AddWithValue("@shipaddress", order.Shipaddress);
                    orderCommand.Parameters.AddWithValue("@shipcity", order.Shipcity);
                    orderCommand.Parameters.AddWithValue("@orderdate", order.Orderdate);
                    orderCommand.Parameters.AddWithValue("@requireddate", order.Requireddate);
                    orderCommand.Parameters.AddWithValue("@shippeddate", order.Shippeddate);
                    orderCommand.Parameters.AddWithValue("@freight", order.Freight);
                    orderCommand.Parameters.AddWithValue("@shipcountry", order.Shipcountry);

                    object? orderIdObj = await orderCommand.ExecuteScalarAsync();
                    if (orderIdObj == null) throw new Exception("No se pudo obtener el ID de la orden.");

                    // Insertar el detalle
                    using SqlCommand detailCommand = new(sqlOrderDetailQuery, connection, transaction);
                    detailCommand.Parameters.AddWithValue("@orderid", Convert.ToInt32(orderIdObj));
                    detailCommand.Parameters.AddWithValue("@productid", order.OrderDetail.Productid);
                    detailCommand.Parameters.AddWithValue("@unitprice", order.OrderDetail.Unitprice);
                    detailCommand.Parameters.AddWithValue("@qty", order.OrderDetail.Qty);
                    detailCommand.Parameters.AddWithValue("@discount", order.OrderDetail.Discount);
                    await detailCommand.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                    response.Message = "Orden y detalle insertados correctamente";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.Success = false;
                    response.Message = $"Error en la transacción: {ex.Message}";
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
