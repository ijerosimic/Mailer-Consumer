using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Data
{
    public interface ICredentialsRepository
    {
        IConfigurationRoot GetConfigBuilder();
        string GetConnectionString();
        string GetSenderEmailAddress();
        string GetSenderEmailPassword();
        string GetSmtpClientHost();
        int GetSmtpPort();
    }
}
