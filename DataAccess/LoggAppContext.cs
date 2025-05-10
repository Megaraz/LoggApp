using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Intake;
using BusinessLogic.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LoggAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DayCard> DayCards { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<CaffeineDrink> CaffeineDrinks { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<SupplementIngredient> SupplementIngredients { get; set; }
        public DbSet<BusinessLogic.Models.Activity.Activity> Activities { get; set; }
        public DbSet<Exercise> Exercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bas-tabell för Activity
            modelBuilder.Entity<BusinessLogic.Models.Activity.Activity>().ToTable("Activities");

            // Egen tabell för Exercise
            modelBuilder.Entity<Exercise>().ToTable("Exercises");

            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.OwnsOne(w => w.Temperature);
                entity.OwnsOne(w => w.Pressure);
                entity.OwnsOne(w => w.Humidity);
                entity.OwnsOne(w => w.Precipitation);
                entity.OwnsOne(w => w.CloudCover);
            });

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=HealthLogg;Trusted_Connection=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:rlack.database.windows.net,1433;Initial Catalog=HealthLogg;Persist Security Info=False;User ID=rlack;Password=Tellus46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        }
    }
}
