namespace SalesPredictionAPI.DataBase.Models
{
    public class Order
    {
        public int Custid { get; set; }
        public int Empid { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime? Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public int Shipperid { get; set; }
        public int Freight { get; set; }
        public string Shipname { get; set; }
        public string Shipaddress { get; set; }
        public string Shipcity { get; set; }
        public string Shipcountry { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
