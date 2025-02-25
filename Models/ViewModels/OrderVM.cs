namespace ECommerce.Models.ViewModels
{
    public class OrderVM
    {
        public ProductOrderMaster master { get; set; }
        public List<ProductOrderDetail> detail { get; set; }
    }
}
