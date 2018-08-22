using GetTestData.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTestData.Api
{
    public class TestDataContext : DbContext
    {
        public DbSet<TestItem> TestItems { get; set; }

        public TestDataContext(DbContextOptions<TestDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TestItemConfiguration());
        }
    }
}
