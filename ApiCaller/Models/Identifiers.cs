using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaller.Models
{
    [Table("NotificationMessageIdentifiers")]
    public class Identifiers
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GUID { get; set; }
        public int ClientID { get; set; }
        [ForeignKey("ClientID")]
        public virtual Clients Clients{ get; set; }
    }
}
