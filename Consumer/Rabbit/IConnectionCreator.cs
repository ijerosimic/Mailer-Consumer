using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Messaging
{
    public interface IConnectionCreator
    {
        void CreateConnection();
        void EstablishConnectionToQueue();
    }
}
