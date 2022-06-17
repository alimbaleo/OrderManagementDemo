using System.ComponentModel.DataAnnotations;
namespace OrderManagement.Domain.Entitities
{
    public class Order 
    {
        public Order(string createdBy, string product, string orderNo, decimal price, decimal totalPrice, decimal total, DateTime orderDate)
        {
         
           
            OrderDate = orderDate;
            CreatedBy = createdBy;
            OrderNo = orderNo;
            ProductName = product;
            Total = total;
            Price = price;
            TotalPrice = totalPrice;
        }

        public long Id { get; init; }
        [Required]
        public DateTime OrderDate { get; init; }
        [Required]
        public string CreatedBy { get; init; }
        [Required]
        public string OrderNo { get; init; }
        [Required]
        public string ProductName { get; init; }
        public decimal Total { get; init; }
        public decimal Price { get; init; }
        public decimal TotalPrice { get; init; }

      
    }
}
