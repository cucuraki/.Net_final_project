using Microsoft.EntityFrameworkCore;

namespace test1.Controllers.Data
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
