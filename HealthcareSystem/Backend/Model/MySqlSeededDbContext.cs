using Microsoft.EntityFrameworkCore;

namespace Backend.Model
{
    class MySqlSeededDbContext : DataSeededDbContext
    {
        private static readonly DbContextOptions<MyDbContext> options =
            new DbContextOptionsBuilder<MyDbContext>().UseMySql(
                "server=dummy;userid=dummy;pwd=dummy;port=3306;database=dummy").Options;

        public MySqlSeededDbContext() : base(options)
        {

        }
    }
}
