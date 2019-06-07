using Consumer.Data;
using Consumer.Factories;
using Consumer.Messaging;
using Consumer.Service;
using Consumer.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer
{
    public class Application
    {
        public IConnectionCreator _rabbit { get; }
        public Application(IConnectionCreator rabbit)
        {
            _rabbit = rabbit;
        }

        public void Run()
        {
            _rabbit.CreateConnection();
        }
    }
}
