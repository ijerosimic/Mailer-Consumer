using Consumer.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Factories.Context
{
    public class ContextFactory : IContextFactory
    {
        public ICredentialsRepository _credentialsRepo { get; }
        public ContextFactory(ICredentialsRepository credentialsRepo) => _credentialsRepo = credentialsRepo;

        public ConsumerContext CreateContext()
        {
            return new ConsumerContext(_credentialsRepo);
        }
    }
}
