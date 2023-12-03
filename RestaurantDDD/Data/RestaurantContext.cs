using Microsoft.EntityFrameworkCore;
using RestaurantDDD.Aggregate;

namespace RestaurantDDD.Data;

public class RestaurantContext : DbContext
{
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<ClientReview> ClientReviews { get; set; }
        public virtual DbSet<StatusOfOrder> StatusOfOrders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=Restaurant;User Id=postgres;Password=1243");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(op => new { op.OrderId, op.ProductId });
            });
            modelBuilder.Entity<ClientReview>(entity =>
            {
                entity.HasKey(op => new { op.ClientId, op.ProductId });
            });
        }
}