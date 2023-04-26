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
        [ForeignKey("Agent")]
        public string AgentId { get; set; }

        public Agent Agent { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        [StringLength(250)]
        public string DeliveryStatus{ get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
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
        public Order Order { get; set; }

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