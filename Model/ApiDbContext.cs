using CanadianCitiesAPI.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace CanadianCitiesAPI.Model
{
    /* Created a dbContext because i intentionally want to
     * interact with the DB for this project
     */
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // defines the structure/configuration for our local .Net objects
            modelBuilder.Entity<Province>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Province)
                .HasForeignKey(c => c.ProvinceId);
            
            modelBuilder.Seed();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
    }
}
