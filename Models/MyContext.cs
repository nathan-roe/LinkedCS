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
    }
}