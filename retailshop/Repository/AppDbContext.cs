using Microsoft.EntityFrameworkCore;
using retailshop.Models;

namespace retailshop.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product {  get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<Cart> Cart { get; set; }  
        public DbSet<UserModel> UserModel { get; set; }
    }
}
