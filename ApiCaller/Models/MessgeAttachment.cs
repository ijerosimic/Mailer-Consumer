using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaller.Models
{
    [Table("NotificationMessageAttachmentHistory")]
    public class MessageAttachment
    {
        public int ID { get; set; }
        public int MessageID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Body { get; set; }
        [ForeignKey("MessageID")]
        public virtual Message Message { get; set; }
    }
}
