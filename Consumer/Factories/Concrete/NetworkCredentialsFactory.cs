using Consumer.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Consumer.Factories
{
    public class NetworkCredentialsFactory : INetworkCredentialsFactory
    {
        public ICredentialsRepository _credentialsRepo { get; }
        public NetworkCredentialsFactory(ICredentialsRepository credentialsRepo)
        {
            _credentialsRepo = credentialsRepo;
        }

        public NetworkCredential GetNetworkCredentials()
        {
            var userName = _credentialsRepo.GetSenderEmailAddress();
            var password = _credentialsRepo.GetSenderEmailPassword();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("User name and password cannot be null!");

            return new NetworkCredential(userName, password);
        }
    }
}
