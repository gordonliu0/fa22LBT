using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using fa22LBT.Models;

namespace fa22LBT.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //this code makes sure the database is re-created on the $5/month Azure tier
            builder.HasPerformanceLevel("Basic");
            builder.HasServiceTier("Basic");

            // Add the shadow property to the model
            builder.Entity<StockPortfolio>()
            .Property<String>("AppUserForeignKey");


            //this code configures the 1:1 relationship between AppUser and StockPortfolio
            builder.Entity<AppUser>()
            .HasOne(sp => sp.StockPortfolio)
            .WithOne(u => u.AppUser)
            .HasForeignKey<StockPortfolio>("AppUserForeignKey");

            base.OnModelCreating(builder);
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockPortfolio> StockPortfolios { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
