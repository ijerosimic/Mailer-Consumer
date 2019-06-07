using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Models
{
    [Table("NotificationMessageClients")]
    public class Client
    {
        public Client()
        {
            Identifiers = new HashSet<Identifier>();
        }
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public ICollection<Identifier> Identifiers { get; set; }
    }
}
