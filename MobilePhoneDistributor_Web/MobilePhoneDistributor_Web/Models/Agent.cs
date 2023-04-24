using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobilePhoneDistributor_Web.Models
{
    [Table("Agents")]
    public class Agent : UserAccount
    {
        [Key]
        public string AgentId { get; set; }
    }
}