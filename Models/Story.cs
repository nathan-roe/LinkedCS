using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LinkedCS.Models
{
    public class Story
    {
        [Key]
        public int StoryId {get;set;}
        public string Content {get;set;}
        public int UserId {get;set;}
        [InverseProperty("UserStories")]
        public User StoryCreator {get;set;}
        
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}