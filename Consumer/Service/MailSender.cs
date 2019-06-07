using Consumer.Data;
using Consumer.Factories;
using Consumer.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Consumer
{
    public class MailSender : IMailSender
    {
        public ISmtpClientFactory _smtpClientFactory { get; }
        public IRepository _repo { get; }

        public MailSender(ISmtpClientFactory smtpClientFactory, IRepository repo)
        {
            _smtpClientFactory = smtpClientFactory;
            _repo = repo;
        }

        public async Task SendMail(MailRepository mail)
        {
            using (var smtpClient = _smtpClientFactory.GetSmtpClient())
            {
                try
                {
                    await smtpClient.SendMailAsync(mail.MailContent);
                    Console.WriteLine($"Succesfully sent mail ID = {mail.MailID}");
                }
                catch (SmtpException e)
                {
                    throw new SmtpException("Error sending e-mail", e);
                }
            }

            try
            {
                await _repo.FlagMessageAsSentAsync(mail.MailID);
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException($"Failed to update message as sent, message ID: {mail.MailID}", e);
            }
        }
    }
}
