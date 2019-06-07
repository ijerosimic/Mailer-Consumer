using Consumer.Factories;
using Consumer.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Consumer.Data;

namespace Consumer.Service
{
    public class MailCreator : IMailCreator
    {
        public IInputValidator _inputValidator { get; }
        public IEmailAddressValidator _emailValidator { get; }
        public IMailMessageFactory _mailMessageFactory { get; }
        public IMailRepositoryFactory _mailRepositoryFactory { get; }
        public IMailSender _sender { get; }
        public ICredentialsRepository _credentialsRepo { get; }

        public MailCreator(IInputValidator inputValidator,
            IEmailAddressValidator emailAddressValidator,
            IMailMessageFactory mailMessageFactory,
            IMailRepositoryFactory mailRepositoryFactory,
            IMailSender sender,
            ICredentialsRepository credentialsRepo)
        {
            _inputValidator = inputValidator;
            _emailValidator = emailAddressValidator;
            _mailMessageFactory = mailMessageFactory;
            _mailRepositoryFactory = mailRepositoryFactory;
            _sender = sender;
            _credentialsRepo = credentialsRepo;
        }

        public async Task CreateMail(string messageContent)
        {
            var message = _inputValidator.ProcessMessage(messageContent);
            var validRecipients = _emailValidator.ValidateAndProcessEmailAddress(message.MailTo);
            var validSecondaryRecipients = _emailValidator.ValidateAndProcessEmailAddress(message.Cc);
            var validHiddenRecipients = _emailValidator.ValidateAndProcessEmailAddress(message.Bcc);
            var mailContent = _mailMessageFactory.CreateMailMessage(_credentialsRepo.GetSenderEmailAddress(), message.Subject, message.Body, message.Attachments);

            validRecipients.ForEach(r => mailContent.To.Add(r));
            validSecondaryRecipients.ForEach(s => mailContent.CC.Add(s));
            validHiddenRecipients.ForEach(h => mailContent.Bcc.Add(h));

            var mail = _mailRepositoryFactory.CreateMailRepository(message.ID, mailContent);
            await _sender.SendMail(mail);
        }
    }
}
