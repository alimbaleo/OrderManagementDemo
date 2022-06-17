using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Application.Contract.Orders
{
    public class CreateOrderDto 
    {
        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order number is required")]
        public string OrderNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
