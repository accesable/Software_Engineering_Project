using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MobilePhoneDistributor_Web.Models
{
    
    public class UserAccount
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}