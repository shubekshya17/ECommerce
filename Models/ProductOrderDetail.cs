using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class ProductOrderDetail
    {
        [Key]
        public int ProductOrderDetailId { get; set; }
        public int ProductOrderMasterId { get; set; }
        public int ProductItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ProductOrderMasterId")]
        public virtual ProductOrderMaster ProductOrderMaster { get; set; }
        [ForeignKey("ProductItemId")]
        public virtual ProductItem ProductItem { get; set; }
    }
}
