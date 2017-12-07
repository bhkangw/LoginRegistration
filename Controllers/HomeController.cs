using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/user/register")]
        public IActionResult Register(RegisterUser user)
        {
            if(ModelState.IsValid)
            {
                RegisterUser(user);
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("/user/login")]
        public IActionResult Login(LoginUser user)
        {
            List<Dictionary<string,object>> users = _dbConnector.Query($"SELECT id, password FROM users WHERE email = '{user.LogEmail}'");
            System.Console.WriteLine(users);
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            if(users.Count == 0 || hasher.VerifyHashedPassword(user, (string)users[0]["password"], user.LogPassword) == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
            }
            if(ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("/success")]
        public IActionResult Success()
        {
            return View();
        }

        public void RegisterUser(RegisterUser user)
        {
            PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();
            string hashed = hasher.HashPassword(user, user.Password);

            string query = $@"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{hashed}', NOW(), NOW())";
            _dbConnector.Execute(query);
        }
    }
}
