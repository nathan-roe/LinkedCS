namespace LinkedCS.Models
{
    public class UserConnection
    {
        public int UserConnectionId {get;set;}
        public int FollowerId {get;set;}
        public User Follower {get;set;}
        public int UserFollowedId {get;set;}
        public User UserFollowed {get;set;}
    }
}