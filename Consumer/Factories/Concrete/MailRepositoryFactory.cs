using System;
using System.Net.Mail;

namespace Consumer.Factories
{
    public class MailRepositoryFactory : IMailRepositoryFactory
    {
        public MailRepository CreateMailRepository(int mailID, MailMessage mailContent)
        {
            if (mailID > 0 && mailContent != null)
            {
                return new MailRepository()
                {
                    MailID = mailID,
                    MailContent = mailContent
                };
            }

            return new MailRepository();
        }
    }
}
