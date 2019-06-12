using ApiCaller.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaller.Data
{
    public class ApiCallerContext : DbContext
    {
        public ApiCallerContext(
            DbContextOptions<ApiCallerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public virtual DbSet<MessageAttachment> Attachments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Identifiers> Identifiers { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
    }
}
