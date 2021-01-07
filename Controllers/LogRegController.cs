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
using System.Drawing;
namespace LinkedCS.Controllers
{
    public class LogRegController : Controller
    {
        private MyContext _context;
        public LogRegController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("login")]
        public IActionResult LoginPage() 
        {
            System.Console.WriteLine("You're on the login page");
            return View("LoginPage");
        }
        [HttpGet("")]
        public IActionResult RegPage()
        {
            System.Console.WriteLine("You're on the registration page");
            return View("RegPage");
        }
        [HttpPost("handle_login")]
        public IActionResult HandleLogin(LogUser retUser)
        {
            System.Console.WriteLine("Entered HandleLogin");
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users
                    .FirstOrDefault(u => u.Email == retUser.Email);
                if(userInDb == null)
                {
                    System.Console.WriteLine("User not in db");
                    ModelState.AddModelError("Email", "Please enter a valid email");
                    return View("LoginPage");
                }
                var hasher = new PasswordHasher<LogUser>();
                var result = hasher.VerifyHashedPassword(retUser, userInDb.Password, retUser.Password);
                if(result == 0)
                {
                    System.Console.WriteLine("Stopped at result == 0");
                    ModelState.AddModelError("LoginError", "There was an error logging you in, please try again.");
                    return View("LoginPage");
                }
                System.Console.WriteLine("Success");
                HttpContext.Session.SetInt32("uuid", userInDb.UserId);
                return RedirectToAction("HomePage", "Home");
            }
            System.Console.WriteLine("ModelState isn't valid");
            return View("LoginPage");
        }
        [HttpPost("handle_register")]
        public IActionResult HandleRegister(User newUser)
        {
            System.Console.WriteLine("Entered HandleRegister");
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.Email == newUser.Email))
                {
                    System.Console.WriteLine("Email already in use");
                    ModelState.AddModelError("EmailInUse", "This email address is already in use. Already have an account?");
                    return View("RegPage");
                }
                System.Console.WriteLine("Success");
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("uuid", newUser.UserId);
                return RedirectToAction("HomePage", "Home");
            }
            System.Console.WriteLine("ModelState not valid");
            return View("RegPage");
        }
    }
}