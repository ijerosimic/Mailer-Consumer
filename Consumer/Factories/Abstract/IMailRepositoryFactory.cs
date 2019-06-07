using Consumer.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Consumer.Factories
{
   public  interface IMailRepositoryFactory
    {
        MailRepository CreateMailRepository(int mailID, MailMessage mailContent);
    }
}
