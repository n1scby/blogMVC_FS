using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blogMVC_FS.Models;
using ApplicationCore.Interfaces;

namespace blogMVC_FS.Controllers
{
  

    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepo;

        public HomeController(IBlogRepository blogRepo)
        {
            _blogRepo = blogRepo;
        }
    
  
    
        public IActionResult Index()
        {
            return View(_blogRepo.GetBlogList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult BlogPost(int id)
        {
            return View(_blogRepo.GetBlogById(id));
        }

        public IActionResult Profile(int id)
        {

            return View(_blogRepo.GetBlogById(id));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
