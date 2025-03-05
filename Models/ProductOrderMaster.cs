using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class ProductOrderMaster
    {
        [Key]
        public int ProductOrderMasterId { get; set; }
        public string FullName { get; set; }
        public string Email {  get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string PaymentOperator { get; set; }
        public string RefNo { get; set; }
    }
}
