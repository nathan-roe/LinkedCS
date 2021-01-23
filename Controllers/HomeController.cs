using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LinkedCS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LinkedCS.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        
        [HttpGet("main")]
        public IActionResult HomePage()
        {
            if(HttpContext.Session.GetInt32("uuid") == null)
            {
                return RedirectToAction("LoginPage", "LogReg");
            }
            ViewBag.partial = "HomePartials/PhotosPopup";
            ViewBag.thisUser = _context.Users
                .Include(u => u.Bookmarks)
                .Include(u => u.LikedPosts)
                .ThenInclude(lp => lp.PostLiked)
                .Include(u => u.UsersFollowed)
                .ThenInclude(uf => uf.UserFollowed)
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            ViewBag.AllUsers = _context.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.UsersWhoLiked)
                .Include(u => u.Followers)
                .ThenInclude(uc => uc.Follower)
                .ToList();

            List<int> conSpaceArr = new List<int>();
            foreach(User u in ViewBag.AllUsers)
            {
                conSpaceArr.Add(FindConnectionSpace(ViewBag.thisUser, u));
            }
            ViewBag.conSpaceArr = conSpaceArr;
            return View("HomePage");
        }
        [HttpGet("edit")]
        public IActionResult EditPage() 
        {
            return View("EditPage");
        }
        [HttpPost("checkedIn")]
        public IActionResult LoggedIn()
        {
            User thisUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            thisUser.HasLogged = true;
            _context.SaveChanges();
            return RedirectToAction("HomePage");
        }
        [HttpGet("changeToPhotos")]
        public IActionResult changeToPhotos()
        {
            if(HttpContext.Session.GetString("profPhoto") != null &&  HttpContext.Session.GetString("profBack") != null)
            {
                ViewBag.profBack = HttpContext.Session.GetString("profBack");
                ViewBag.profPhoto = HttpContext.Session.GetString("profPhoto");
            }
            User model = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            return PartialView("HomePartials/PhotosPopup", model);
        }   
        [HttpGet("changeToBio")]
        public IActionResult changeToBio()
        {
            if(HttpContext.Session.GetString("bio") != null)
            {
                ViewBag.bioText = HttpContext.Session.GetString("bio");
            }
            User model = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            return PartialView("HomePartials/BioPopup", model);
        }         
        [HttpGet("changeToFriends")]
        public IActionResult changeToFriends()
        {
            User model = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            return PartialView("HomePartials/FriendsPopup", model);
        }   
        [HttpPost("updatePopup")]  
        public IActionResult updatePopup(int count, string bio, string[] photos) 
        {
            if(count == 0)
            {
                System.Console.WriteLine(photos[0]);
                System.Console.WriteLine(photos[1]);
                HttpContext.Session.SetString("profPhoto", photos[0]);
                HttpContext.Session.SetString("profBack", photos[1]);
            }
            if(count == 1)
            {
                System.Console.WriteLine(bio);
                HttpContext.Session.SetString("bio", bio);
            }
            if(count == 2)
            {
                System.Console.WriteLine("You just finished the popup");
            }
            
            return RedirectToAction("HomePage");
        }   
        [HttpPost("processPopup")]
        public IActionResult processPopup() 
        {
            User thisUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            thisUser.HasLogged = true;
            thisUser.Photo = HttpContext.Session.GetString("profPhoto");
            thisUser.Background = HttpContext.Session.GetString("profBack");
            thisUser.Summary = HttpContext.Session.GetString("bio");
            _context.SaveChanges();
            return RedirectToAction("HomePage");
        }
        [HttpPost("addConnection")]
        public IActionResult AddConnection(UserConnection uc)
        {
            System.Console.WriteLine($"The association is {uc}");
            int userId = (int)HttpContext.Session.GetInt32("uuid");
            System.Console.WriteLine($"User id is {userId}");
            uc.FollowerId = userId;
            _context.Add(uc);
            _context.SaveChanges();
            System.Console.WriteLine("Successfully added connection");
            return RedirectToAction("HomePage");
        }
        [HttpPost("removeConnection")]
        public IActionResult RemoveConnection(UserConnection u)
        {
            System.Console.WriteLine($"{u.UserFollowedId}");
            int userId = (int)HttpContext.Session.GetInt32("uuid");
            UserConnection uc = _context.UserConnections
                .FirstOrDefault(uc => uc.FollowerId == userId && uc.UserFollowedId == u.UserFollowedId);
            _context.UserConnections.Remove(uc);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
        public int FindConnectionSpace(User user, User potCon, int count = 0, User prev=null)
        {
            // System.Console.WriteLine($"The current user is {user.FirstName}");
            if(count == 3)
            {
                return count;
            }
            // System.Console.WriteLine(user.UsersFollowed);
            if(user.UsersFollowed != null)
            {
                foreach(var u in user.UsersFollowed)
                {
                    if(u.UserFollowed == potCon)
                    {
                        return count;
                    }
                }
                // System.Console.WriteLine("______");
                foreach(var u in user.UsersFollowed)
                {
                    // System.Console.WriteLine(u.UserFollowed.FirstName);
                    if(u.UserFollowed != prev)
                    {
                        // System.Console.WriteLine(u.UserFollowed.FirstName);
                        return FindConnectionSpace(u.UserFollowed, potCon, count+1, user);
                    }
                }
            }
            // System.Console.WriteLine("!________");
            return -1;
        }

        [HttpGet("/discover")]
        public IActionResult Discover()
        {
            ViewBag.thisUser = _context.Users
                .Include(u => u.UsersFollowed)
                .ThenInclude(uf => uf.UserFollowed)
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            ViewBag.AllUsers = _context.Users.ToList();
            return View("Discover");
        }
        [HttpPost("searchPeople")]
        public IEnumerable<User> SearchPeople(string inputVal)
        {
            List<User> theseUsers = _context.Users
                .Where(u => (u.FirstName + ' ' + u.LastName).Contains(inputVal))
                .ToList();
            
            return theseUsers;
        }
        [HttpPost("showPerson")]
        public IActionResult ShowPerson(User user)
        {
            ViewBag.User = user;
            return View("HomePartials/SearchedProfile");
        }  
        [HttpGet("addPostView")]
        public IActionResult AddPostView()
        {
            return View("HomePartials/AddPost");
        }
        [HttpPost("addPost")]
        public IActionResult AddPost(Post FromForm)
        {
            if(ModelState.IsValid)
            {
                FromForm.UserId = (int)HttpContext.Session.GetInt32("uuid");
                _context.Add(FromForm);
                _context.SaveChanges();
            }
            return RedirectToAction("HomePage");
        }
        [HttpGet("addCommentView")]
        public IActionResult AddCommentView(int postId)
        {
            System.Console.WriteLine(postId);
            ViewBag.thisPost = _context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.UserWhoCommented)
                .FirstOrDefault(p => p.PostId == postId);

            return View("HomePartials/AddComment");
        }
        [HttpPost("addComment")]
        public IActionResult AddComment(Comment FromForm)
        {
            if(ModelState.IsValid)
            {
                FromForm.UserId = (int)HttpContext.Session.GetInt32("uuid");
                _context.Add(FromForm);
                _context.SaveChanges();
                System.Console.WriteLine("Success");
            }
            return RedirectToAction("HomePage");
        }
        [HttpPost("addLike")]
        public IActionResult AddLike(LikedPost lp)
        {
            System.Console.WriteLine(lp.PostLikedId);
            int userId = (int)HttpContext.Session.GetInt32("uuid");
            lp.UserWhoLikedId = userId;
            _context.Add(lp);
            _context.SaveChanges();
            System.Console.WriteLine("Successfully added LikedPost");
            return RedirectToAction("HomePage");
        }
        // [HttpPost("removeLike")]
        // public IActionResult RemoveLike()
        [HttpPost("addBookmark")]
        public IActionResult AddBookmark(Bookmark bm)
        {
            System.Console.WriteLine(bm.PostBookmarkedId);
            System.Console.WriteLine($"The association is {bm}");
            int userId = (int)HttpContext.Session.GetInt32("uuid");
            System.Console.WriteLine($"User id is {userId}");
            bm.UserWhoBookmarkedId = userId;
            _context.Add(bm);
            _context.SaveChanges();
            System.Console.WriteLine("Successfully added bookmark");
            return RedirectToAction("HomePage");
        }

        [HttpGet("profile/{userId}")]
        public IActionResult ProfilePage(int userId)
        {
            if(HttpContext.Session.GetInt32("uuid") == null)
            {
                return RedirectToAction("HomePage");
            }
            System.Console.WriteLine(userId);
            User thisUser = _context.Users
                .Include(u => u.Viewers)
                .ThenInclude(v => v.Viewer)
                .FirstOrDefault(u => u.UserId == userId);
            User loggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            bool boolVal = false;
            foreach(UserView uv in thisUser.Viewers)
            {
                System.Console.WriteLine($"{uv.Viewer.FirstName} viewed {uv.UserViewed.FirstName}");
                if(uv.ViewerId == loggedUser.UserId)
                {
                    boolVal = true;
                }
            }
            if(!boolVal)
            {
                UserView u = new UserView();
                u.UserViewedId = thisUser.UserId;
                return AddViewer(u);
            }
            else{
                return View("ProfilePage", thisUser);
            }
        }
        [HttpPost("addViewer")]
        public IActionResult AddViewer(UserView uv)
        {
            System.Console.WriteLine("This person has not viewed this profile yet!");

            System.Console.WriteLine($"The association is {uv}");
            int userId = (int)HttpContext.Session.GetInt32("uuid");
            System.Console.WriteLine($"User id is {userId}");
            uv.ViewerId = userId;
            _context.Add(uv);
            _context.SaveChanges();
            System.Console.WriteLine("Successfully added connection");

            return RedirectToAction("ProfilePage", uv.UserViewedId);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
