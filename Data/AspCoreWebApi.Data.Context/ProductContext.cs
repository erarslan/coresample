using Microsoft.EntityFrameworkCore;
using AspCoreWebApi.Data.Model;

namespace AspCoreWebApi.Data.Context
{
    public class ProductContext : DbContext
    {

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
