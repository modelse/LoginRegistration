using System.Collections.Generic;
using loginReg.Factories;
using loginReg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace loginReg.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserFactory _userFactory;
        
        public UsersController(UserFactory user)
        {
            _userFactory=user;
        }
        // GET: /Home/
         [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors= new List<string>();
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid) {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                model.password = Hasher.HashPassword(model, model.password);
                _userFactory.Add(model);
                User CurrentUser = _userFactory.GetLatestUser();
                HttpContext.Session.SetInt32("CurrUserId", CurrentUser.UserId);
                return RedirectToAction("Success");
            }
            ViewBag.errors=ModelState.Values;
            return View("Index");
        }

        [HttpPost]
        [Route("back")]
        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success(){
            return View("success");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            if(email != null)
            {
                User CheckUser = _userFactory.GetuserByEmail(email);
                if(CheckUser != null)
                {
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(CheckUser, CheckUser.password, password))
                    {
                        HttpContext.Session.SetInt32("CurrUserId", CheckUser.UserId);
                        return RedirectToAction("Success");
                    }
                }
            }
            ViewBag.Errors = new List<string>{
                "Invalid Name or Password"
            };
            return View("Index");
        }
    }
}
