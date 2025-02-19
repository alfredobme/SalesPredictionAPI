namespace SalesPredictionAPI.DataBase.Models
{
    public class OrderDetail
    {
        public int Productid { get; set; }
        public int Unitprice { get; set; }
        public int Qty { get; set; }
        public int Discount { get; set; }
    }
}