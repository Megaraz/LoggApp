using AppLogic.Models.Entities;
using AppLogic.Models.Entities.WeatherAndAQI;
using Microsoft.EntityFrameworkCore;
using Activity = AppLogic.Models.Entities.Activity;

namespace AppLogic
{
    /// <summary>
    /// Represents the application's database context for managing health and wellness data.
    /// </summary>
    public class LoggAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DayCard> DayCards { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<AirQualityData> AirQualityData { get; set; }
        public DbSet<CaffeineDrink> CaffeineDrinks { get; set; }
        public DbSet<WellnessCheckIn> WellnessCheckIns { get; set; }
        public DbSet<Sleep> Sleep { get; set; }
        public DbSet<Exercise> Exercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .HasOne(d => d.WeatherData)
                .WithOne(w => w.DayCard)
                .HasForeignKey<WeatherData>(w => w.DayCardId)
                .OnDelete(DeleteBehavior.SetNull); // WeatherData can be worth saving even if daycard is deleted, so sett null instead of on delete cascade

            modelBuilder.Entity<DayCard>()
                .HasOne(d => d.AirQualityData)
                .WithOne(a => a.DayCard)
                .HasForeignKey<AirQualityData>(a => a.DayCardId)
                .OnDelete(DeleteBehavior.SetNull); // AirQualityData can be worth saving even if daycard is deleted, so sett null instead of on delete cascade


            modelBuilder.Entity<DayCard>()
                .HasOne(dc => dc.User)
                .WithMany(u => u.DayCards)
                .HasForeignKey(dc => dc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sleep>()
                .HasOne(s => s.DayCard)
                .WithOne(dc => dc.Sleep)
                .HasForeignKey<Sleep>(s => s.DayCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WellnessCheckIn>()
                .HasOne(wc => wc.DayCard)
                .WithMany(dc => dc.WellnessCheckIns)
                .HasForeignKey(wc => wc.DayCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasOne(a => a.DayCard)
                .WithMany(dc => dc.Exercises)
                .HasForeignKey(a => a.DayCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CaffeineDrink>()
                .HasOne(cd => cd.DayCard)
                .WithMany(dc => dc.CaffeineDrinks)
                .HasForeignKey(cd => cd.DayCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=HealthLogApp;Trusted_Connection=True;TrustServerCertificate=True;");

        }
    }
}
