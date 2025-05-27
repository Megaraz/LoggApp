using AppLogic.Models;
using AppLogic.Models.Activity;
using AppLogic.Models.Intake;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;
using Microsoft.EntityFrameworkCore;
using Activity = AppLogic.Models.Activity.Activity;

namespace AppLogic
{
    public class LoggAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DayCard> DayCards { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<AirQualityData> AirQualityData { get; set; }
        public DbSet<CaffeineDrink> CaffeineDrinks { get; set; }
        public DbSet<WellnessCheckIn> WellnessCheckIns { get; set; }
        public DbSet<Sleep> Sleep { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<SupplementIngredient> SupplementIngredients { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Exercise> Exercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bas-tabell för Activity
            modelBuilder.Entity<Activity>().ToTable("Activities");

            // Egen tabell för Exercise
            modelBuilder.Entity<Exercise>().ToTable("Exercises");

            modelBuilder.Entity<AirQualityData>(entity => 
            { 
                entity.OwnsOne(a => a.HourlyBlock);
                entity.OwnsOne(w => w.HourlyUnits);
            });


            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.OwnsOne(w => w.HourlyBlock);
                entity.OwnsOne(w => w.HourlyUnits);
                
            });

            modelBuilder.Entity<DayCard>()
                .HasOne(dc => dc.User)
                .WithMany(u => u.DayCards)
                .HasForeignKey(dc => dc.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CaffeineDrink>()
                .HasOne(cd => cd.DayCard)
                .WithMany(dc => dc.CaffeineDrinks)
                .HasForeignKey(cd => cd.DayCardId)
                .OnDelete(DeleteBehavior.Cascade);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=HealthLogApp;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer(@"Server=tcp:rlack.database.windows.net,1433;Initial Catalog=HealthLogg;Persist Security Info=False;User ID=rlack;Password=Tellus46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }
    }
}
