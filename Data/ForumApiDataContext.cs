using ForumApi.Data.Mappings;
using ForumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data
{

    public class ForumApiDataContext : DbContext
    {

        public DbSet<Category>? Categories { get; set; }


        public DbSet<Post>? Posts { get; set; }



        public DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            =>options.UseSqlServer("Server=localhost,1433;Database=ForumApi;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }


    }
}