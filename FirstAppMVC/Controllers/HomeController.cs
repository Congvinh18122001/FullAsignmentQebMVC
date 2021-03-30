using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstAppMVC.Models;
using Microsoft.AspNetCore.Http;
using FirstAppMVC.Services;
namespace FirstAppMVC.Controllers
{
    public class HomeController : Controller
    {


        private List<User> _users ;
        private readonly ITestService _service;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ITestService service)
        {
            _logger = logger;
             _service = service;
            _users = _service.GetUsers();  
            
        }

        public IActionResult Index()
        {

            ViewBag.Username = null;
            if (HttpContext.Session.GetString("Username")!=null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");
            }
            
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         public IActionResult LoginForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user == null)
            {
                return RedirectToAction("LoginForm"); 
            }

            if (CheckLogin(user) == null)
            {
                 return RedirectToAction("LoginForm"); 
            }

            AddUserToSession(CheckLogin(user));

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("RoleID");
            
            return RedirectToAction("Index");
        }

        void AddUserToSession(User user){
            HttpContext.Session.SetString("Username",user.Username);
            HttpContext.Session.SetString("RoleID",user.RoleID.ToString());
        }  

        User CheckLogin(User user){
            foreach (var item in _users)
            {
                if (user.Username == item.Username && user.Password== item.Password)
                {
                    return item;
                }
            }
            return null;
        }
    
    }
}
