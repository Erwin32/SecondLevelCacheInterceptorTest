using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L2CacheTest
{
    public class TestContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public TestContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>(e =>
            {
                e.HasData(new List<Entry>{new Entry{Id = 1, Data = 1245, Name = "Test"}});
            });   
            
            base.OnModelCreating(modelBuilder);
        }
    }
}