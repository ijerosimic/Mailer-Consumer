using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Consumer.Factories
{
    public interface INetworkCredentialsFactory
    {
        NetworkCredential GetNetworkCredentials();
    }
}
