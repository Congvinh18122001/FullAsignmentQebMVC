using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstAppMVC.Models;
using FirstAppMVC.Filters;
namespace FirstAppMVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMembersRepository _repo;
        public MembersController(IMembersRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            List<Member> members = _repo.GetMembers();
            return View(members);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(_repo.GetMemberById(id.Value));
        }
        [Authorize("Admin")]
        public IActionResult Edit(int? id)
        {
             if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(_repo.GetMemberById(id.Value));
        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            if (member == null)
            {
              return RedirectToAction("Index");
            }
               member=_repo.UpdateMember(member);
            return RedirectToAction("Details",member.ID);
        
        }
        [Authorize("Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
           public IActionResult Create(Member member)
        {
             if (member == null)
            {
              return RedirectToAction("Index");
            }
               member=_repo.AddMember(member);
            return RedirectToAction("Details",member.ID);
        }
        [Authorize("Admin")]
        public IActionResult Delete(int? id)
        {
             if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Member member =_repo.GetMemberById(id.Value);
            if (member != null)
            {
                return View(member);
            }
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repo.DeleteMember(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Search(string searchString)
        {
            return View("Index",_repo.GetMembersByName(searchString));
        }

    }
}
