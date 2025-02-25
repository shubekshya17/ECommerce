namespace ECommerce.Models.ViewModels
{
    public class OrderWithCountVM
    {
        public int ProductOrderMasterId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int TotalItems { get; set; }
    }
}
