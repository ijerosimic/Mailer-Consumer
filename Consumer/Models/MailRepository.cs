using System.Net.Mail;

namespace Consumer
{
    public class MailRepository
    {
        public int MailID { get; set; }
        public MailMessage MailContent { get; set; }
    }
}
