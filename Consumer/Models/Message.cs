using Consumer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consumer
{
    [Table("NotificationMessageHistory")]
    public class Message
    {
        public Message() => Attachments = new HashSet<MessageAttachment>();
        public int ID { get; set; }
        public string MailTo { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime TriggerTime { get; set; }
        public bool IsSent { get; set; } = false;
        public virtual ICollection<MessageAttachment> Attachments { get; set; }
        [NotMapped]
        public Guid Guid { get; set; }
    }
}