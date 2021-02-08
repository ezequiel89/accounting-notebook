using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Transaction> Transactions { get; set; }
    }
}
