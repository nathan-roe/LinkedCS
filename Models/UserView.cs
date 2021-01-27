using System;
using System.Collections.Generic;
namespace LinkedCS.Models
{
    public class UserView
    {
        public int UserViewId {get;set;}
        public int ViewerId {get;set;}
        public User Viewer {get;set;}
        public int UserViewedId {get;set;}
        public User UserViewed {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}