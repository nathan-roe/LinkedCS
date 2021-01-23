using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LinkedCS.Models
{
    public class Post
    {
        [Key]
        public int PostId {get;set;}
        public string Desc {get;set;}
        public string Image {get;set;}
        public string Video {get;set;}
        public string Article {get;set;}
        public int UserId {get;set;}
        [InverseProperty("Posts")]
        public User UserWhoPosted {get;set;}
        public List<Comment> Comments {get;set;}
        public List<LikedPost> UsersWhoLiked {get;set;}
        public List<Bookmark> UsersWhoBookmarked {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}