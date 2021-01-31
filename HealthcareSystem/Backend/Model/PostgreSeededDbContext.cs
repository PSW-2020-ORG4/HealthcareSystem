using Microsoft.EntityFrameworkCore;

namespace Backend.Model
{
    class PostgreSeededDbContext : DataSeededDbContext
    {
        private static readonly DbContextOptions<MyDbContext> options =
            new DbContextOptionsBuilder<MyDbContext>().UseNpgsql(
                "server=dummy;userid=dummy;pwd=dummy;port=5432;database=dummy").Options;

        public PostgreSeededDbContext() : base(options)
        {

        }
    }
}
