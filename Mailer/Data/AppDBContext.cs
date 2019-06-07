using AprossMailer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageAttachment> MessageAttachments { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Identifier> Identifiers { get; set; }
    }
}
