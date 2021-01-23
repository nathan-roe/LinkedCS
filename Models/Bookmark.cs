namespace LinkedCS.Models
{
    public class Bookmark
    {
        public int BookmarkId {get;set;}
        public int UserWhoBookmarkedId {get;set;}
        public User UserWhoBookmarked {get;set;}
        public int PostBookmarkedId {get;set;}
        public Post PostBookmarked {get;set;}
    }
}