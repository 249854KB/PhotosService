using PhotosService.Models;
using Microsoft.EntityFrameworkCore;

namespace PhotosService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Dog> Dogs{ get; set; }
        public DbSet<Photo> Photos{ get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Dog>()
            .HasMany(u => u.Photos)
            .WithOne(u=> u.dog!)
            .HasForeignKey(u=>u.DogId);

            modelBuilder
            .Entity<Photo>()
            .HasOne(u=>u.dog)
            .WithMany(u=>u.Photos)
            .HasForeignKey(u=>u.DogId);
        }
    }
}
