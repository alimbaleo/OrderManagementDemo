namespace OrderManagement.Application.Contract.Orders
{
    public class OrdersDto 
    {

        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CreatedBy { get; set; }
        public string OrderNo { get; set; }
        public string ProductName { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
