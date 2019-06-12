using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaller.Models
{
    [Table("NotificationMessageCompanies")]
    public class Companies
    {
        public Companies()
        {
            Clients = new HashSet<Clients>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
