using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UnderstandingDI
{
    public class UnderstandingDIContext : DbContext
    {
        private readonly string _connectionString = Program.ConnectionString;

        public DbSet<UnderstandingDIModel> UnderstandingDITable { get; set; }

        public UnderstandingDIContext(DbContextOptions<UnderstandingDIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
