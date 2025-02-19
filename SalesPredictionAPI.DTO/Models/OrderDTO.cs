namespace SalesPredictionAPI.DTO.Models
{
    public class OrderDTO
    {
        public int Orderid { get; set; }
        public DateTime? Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public string Shipname { get; set; }
        public string Shipaddress { get; set; }
        public string Shipcity { get; set; }
    }
}
