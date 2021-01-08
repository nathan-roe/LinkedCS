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
            User thisUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("uuid"));
            return View("HomePage", thisUser);
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
    }
}
