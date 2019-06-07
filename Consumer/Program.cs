using Consumer.Data;
using Consumer.Factories;
using Consumer.Factories.Context;
using Consumer.Messaging;
using Consumer.Service;
using Consumer.Validation;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Consumer
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().AsSelf();
            builder.RegisterType<ContextFactory>().As<IContextFactory>();
            builder.RegisterType<ConfigurationBuilder>().AsSelf();
            builder.RegisterType<ConnectionCreator>().As<IConnectionCreator>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<CredentialsRepository>().As<ICredentialsRepository>();
            builder.RegisterType<NetworkCredentialsFactory>().As<INetworkCredentialsFactory>();
            builder.RegisterType<MailMessageFactory>().As<IMailMessageFactory>();
            builder.RegisterType<MailRepositoryFactory>().As<IMailRepositoryFactory>();
            builder.RegisterType<SmtpClientFactory>().As<ISmtpClientFactory>();
            builder.RegisterType<MailCreator>().As<IMailCreator>();
            builder.RegisterType<MailSender>().As<IMailSender>();
            builder.RegisterType<EmailAddressValidator>().As<IEmailAddressValidator>();
            builder.RegisterType<InputValidator>().As<IInputValidator>();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var container = CompositionRoot();
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<Application>().Run();
            }
        }
    }
}
