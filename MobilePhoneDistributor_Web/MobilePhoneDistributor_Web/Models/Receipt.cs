using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobilePhoneDistributor_Web.Models
{
    public class Receipt
    {
        [Key]
        public string ReceiptId { get; set; }
        [Required]
        [Display(Name = "Created on")]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
        [Required]

        public string StaffId { get; set;}

        public Staff Staff { get; set;}

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
    [NotMapped]
    public class ReceiptCreateViewModel
    {
/*        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMddyy}", ApplyFormatInEditMode = true)]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;*/
        [Required]
        [Display(Name ="Staff ID")]
        public string StaffId { get; set; }
    }
    public class ReceiptDetail
    {
        [Key]
        public int ReceiptDetailId { get; set; }
        [Required]
        public string ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("PhoneModel")]
        public string PhoneModelId { get; set; }
        public PhoneModel PhoneModel { get; set; }
        [Required]
        public double UnitAmmount { set; get; }

    }
    [NotMapped]
    public class ReceiptDetailCreateViewModel
    {
        [Required]
        [Display(Name = "Appended Phone Model")]
        public string PhoneModelId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitAmmount { get; set; }
    }
}