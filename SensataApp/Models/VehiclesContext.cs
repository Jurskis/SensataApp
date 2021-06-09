using Microsoft.EntityFrameworkCore;

namespace SensataApp.Models
{
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions<VehiclesContext> options) : base(options) {}

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleData> Data { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Data)
                .WithOne(vd => vd.Vehicle);

            modelBuilder.Entity<VehicleData>()
                .Property(vd => vd.Created)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
