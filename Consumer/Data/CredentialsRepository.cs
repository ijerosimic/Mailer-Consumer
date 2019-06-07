using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Consumer.Data
{
    public class CredentialsRepository : ICredentialsRepository
    {
        public ConfigurationBuilder _configBuilder { get; }
        public CredentialsRepository(ConfigurationBuilder configBuilder) => _configBuilder = configBuilder;

        public IConfigurationRoot GetConfigBuilder()
        {
           return _configBuilder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", false, true)
              .Build();
        }

        public string GetConfigData(string section, string key)
        {
            return GetConfigBuilder().GetSection(section)[key];
        }
        
        public string GetConnectionString()
        {
            return GetConfigData("ConnectionStrings", "DefaultConnection");
        }

        public string GetSenderEmailAddress()
        {
            return GetConfigData("Email", "SenderMailAddress");
        }

        public string GetSenderEmailPassword()
        {
            return GetConfigData("Email", "SenderMailPassword");
        }

        public string GetSmtpClientHost()
        {
            return GetConfigData("SmtpClient", "Host");
        }

        public int GetSmtpPort()
        {
            var port = GetConfigData("SmtpClient", "Port");
            return Convert.ToInt32(port);
        }
    }
}
