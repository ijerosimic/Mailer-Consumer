using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaller.Models
{
    [Table("NotificationMessageClients")]
    public class Clients
    {
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Email { get; set; }
        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Companies Companies { get; set; }
    }
}