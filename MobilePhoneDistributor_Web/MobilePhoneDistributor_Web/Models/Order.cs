using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MobilePhoneDistributor_Web.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }

        [Display(Name = "Ordered on")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(250)]
        public string OrderStatus { get; set; }
        [Required]
        [StringLength(250)]
        public string PaymentMethod { get; set; }
        [Required]
        [StringLength(250)]
        public string PaymentStatus { get; set; }
        [Required]
        [ForeignKey("Agent")]
        public string AgentId { get; set; }
        public Agent Agent { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    
    public class DeliveryNote
    {
        [Key]
        public string DeliveryNoteId { get; set; }
        [Required]
        [Display(Name = "Delivery on")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        [Required]
        [Display(Name ="Order ID")]
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("PhoneModel")]
        public string PhoneModelId { get; set; }
        public PhoneModel PhoneModel { get; set; }
    }
    
    [NotMapped]
    public class OrderDetailCreateViewModel
    {
        [Required]
        public int Quantity { get; set; }

    }
}