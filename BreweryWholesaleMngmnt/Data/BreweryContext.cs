using BreweryWholesaleMngmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleMngmnt.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options) { }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }
        public DbSet<Sale> Sales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WholesalerStock>()
                .HasKey(ws => new { ws.WholesalerID, ws.BeerID });

            modelBuilder.Entity<Beer>()
                 .Property(b => b.AlcoholContent)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<Beer>()
                .Property(b => b.Price)
                .HasColumnType("decimal(10, 2)"); 
        }
    }
}
