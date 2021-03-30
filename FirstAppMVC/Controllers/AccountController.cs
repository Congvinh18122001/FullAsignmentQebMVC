using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstAppMVC.Models;
namespace FirstAppMVC.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IAccountRepository _repo;
        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
         public IActionResult Register(User user)
        {

            user = _repo.AddUser(user);
            return View("Details",user);
        }

        public IActionResult Details(int ? id)
        {
            if (id.HasValue)
            {
                 User user = _repo.GetUserByID(id.Value);
                 if (user != null)
                 {
                     return View(user);
                 }
            }
          return RedirectToAction("Index","Home");
        }
    }
}