using Microsoft.EntityFrameworkCore;
namespace LinkedCS.Models
{ 
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<Post> Posts {get;set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<LikedPost> LikedPosts {get;set;}
        public DbSet<Bookmark> Bookmarks {get;set;}
        public DbSet<UserView> UserViews {get;set;}
        public DbSet<Preference> Preferences {get;set;}
        public DbSet<Story> Stories {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserPreference)
                .WithOne(up => up.UserWithPreference)
                .HasForeignKey<Preference>(p => p.UserForeignKey);
        }
    }
}