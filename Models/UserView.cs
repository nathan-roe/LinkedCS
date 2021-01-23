namespace LinkedCS.Models
{
    public class UserView
    {
        public int UserViewId {get;set;}
        public int ViewerId {get;set;}
        public User Viewer {get;set;}
        public int UserViewedId {get;set;}
        public User UserViewed {get;set;}
    }
}