namespace ECommerce.Models.ViewModels
{
    public class PayVM
    {
        public ProductOrderMaster master { get; set; }
        public List<OrderWithNameVM> detail { get; set; }
    }
}
