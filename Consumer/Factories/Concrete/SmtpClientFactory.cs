using Consumer.Data;
using System;
using System.Net;
using System.Net.Mail;

namespace Consumer.Factories
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        public INetworkCredentialsFactory _networkCredentialsFactory { get; }
        public ICredentialsRepository _credentialsRepo { get; }
        public SmtpClientFactory(INetworkCredentialsFactory networkCredentialsFactory, ICredentialsRepository credentialsRepo)
        {
            _networkCredentialsFactory = networkCredentialsFactory;
            _credentialsRepo = credentialsRepo;
        }

        public SmtpClient GetSmtpClient()
        {
            var host = _credentialsRepo.GetSmtpClientHost();
            var port = _credentialsRepo.GetSmtpPort();
            var credentials = _networkCredentialsFactory.GetNetworkCredentials();

            if (host == null && port == 0 && credentials == null)
                throw new ArgumentNullException("Smtp credentials must cannot be null!");

            return new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = credentials
            };
        }
    }
}
