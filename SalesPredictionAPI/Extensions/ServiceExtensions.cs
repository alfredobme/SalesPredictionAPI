using SalesPredictionAPI.Repository.Interfaces;
using SalesPredictionAPI.Repository.Repository;

namespace SalesPredictionAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            // Registro de los repositorios
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
   }
}


