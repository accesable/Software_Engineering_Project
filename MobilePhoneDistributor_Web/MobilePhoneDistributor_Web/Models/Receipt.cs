﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobilePhoneDistributor_Web.Models
{
    public class Receipt
    {
        [Key]
        public string ReceiptId { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
        [Required]
        public string StaffId { get; set;}

        public Staff Staff { get; set;}

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
    public class ReceiptDetail
    {
        [Key]
        public string ReceiptDetailId { get; set; }
        [Required]
        public string ReceiptId { get; set; }
        [Required]
        public string PhoneModelId { set; get; }
        [Required]
        public int Quantity { get; set; }
        [StringLength(50)]
        [Required]
        public string UnitAmmount { set; get; }


        public Receipt Receipt { get; set; }

        public PhoneModel PhoneModel { get; set; }


    }
}