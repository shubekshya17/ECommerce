namespace ECommerce.Models.ViewModels
{
    public class OrderWithNameVM
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
