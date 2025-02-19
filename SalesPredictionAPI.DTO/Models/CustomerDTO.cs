namespace SalesPredictionAPI.DTO.Models
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
