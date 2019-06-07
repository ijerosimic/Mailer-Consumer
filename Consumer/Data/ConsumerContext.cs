using Consumer.Data;
using Consumer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.IO;

namespace Consumer
{
    public class ConsumerContext : DbContext
    {
        public ICredentialsRepository _credentialsRepo { get; }
        public ConsumerContext(ICredentialsRepository credentialsRepo) => _credentialsRepo = credentialsRepo;

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _credentialsRepo.GetConnectionString();

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}