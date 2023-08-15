using PhotosService.Models;
using Microsoft.EntityFrameworkCore;

namespace PhotosService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<User> Users{ get; set; }
        public DbSet<Photo> Photos{ get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Photos)
            .WithOne(u=> u.User!)
            .HasForeignKey(u=>u.UserId);

            modelBuilder
            .Entity<Photo>()
            .HasOne(u=>u.User)
            .WithMany(u=>u.Photos)
            .HasForeignKey(u=>u.UserId);
        }
    }
}
