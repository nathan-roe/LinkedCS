using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace LinkedCS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Confirm { get; set; }
        
        public string Photo {get;set;}

        public string Background {get;set;} = "grey";
        public bool HasLogged {get;set;} = false;
        public string Summary {get;set;} = "";

        [InverseProperty("UserFollowed")]
        public List<UserConnection> Followers { get; set; }

        [InverseProperty("Follower")]
        public List<UserConnection> UsersFollowed { get; set; }


        [InverseProperty("Viewer")]
        public List<UserView> Viewers {get;set;}
        [InverseProperty("UserViewed")]
        public List<UserView> ViewedUsers {get;set;}


        [InverseProperty("UserWhoPosted")]
        public List<Post> Posts {get;set;}
        public List<Comment> UserComments {get;set;}
        public List<LikedPost> LikedPosts {get;set;}
        public List<Bookmark> Bookmarks {get;set;}
        
        public Preference UserPreference {get;set;}

        [InverseProperty("StoryCreator")]
        public List<Story> UserStories {get;set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}