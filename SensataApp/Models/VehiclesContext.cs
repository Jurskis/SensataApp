using Microsoft.EntityFrameworkCore;

namespace SensataApp.Models
{
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions<VehiclesContext> options) : base(options) {}

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleInput> VehicleInputs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Inputs)
                .WithOne(vi => vi.Vehicle);

            modelBuilder.Entity<VehicleInput>()
                .Property(vi => vi.Created)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
