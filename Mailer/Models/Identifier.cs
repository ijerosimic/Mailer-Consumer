using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Models
{
    [Table("NotificationMessageIdentifiers")]
    public class Identifier
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GUID { get; set; }
        public int ClientID { get; set; }
        [ForeignKey("ClientID")]
        public virtual Client Clients { get; set; }
    }
}
