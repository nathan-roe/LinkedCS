namespace LinkedCS.Models
{
    public class LikedPost
    {
        public int LikedPostId {get;set;}
        public int UserWhoLikedId {get;set;}
        public User UserWhoLiked {get;set;}
        public int PostLikedId {get;set;}
        public Post PostLiked {get;set;}
    }
}