using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LinkedCS.Models
{
    public class Comment
    {
        [Key]
        public int CommentId {get;set;}
        public string Content {get;set;}
        public int UserId {get;set;}
        public User UserWhoCommented {get;set;}
        public int PostId {get;set;}
        public Post PostCommentedOn {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}