using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Model
{
    public class CustomDbContext : DbContext
    {

        public DbSet<CustomEvent> CustomEvents { get; set; }

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }

    }
}
