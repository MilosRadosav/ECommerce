using ECommerce.Core.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.Core.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
