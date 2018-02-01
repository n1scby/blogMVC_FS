using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogMVC_FS.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class BlogManagerController : Controller
    {
        private readonly IBlogRepository _blogRepo;

        public BlogManagerController(IBlogRepository blogRepo)
        {
            _blogRepo = blogRepo;
        }

        // GET: BlogManager
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_blogRepo.GetBlogList());
        }

        [AllowAnonymous]
        // GET: BlogManager/Details/5
        public ActionResult Details(int id)
        {
            return View(_blogRepo.GetBlogById(id));
        }

        // GET: BlogManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog newBlog, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _blogRepo.Add(newBlog);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
              //logging goes here
            }
            return View(newBlog);
        }

        // GET: BlogManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_blogRepo.GetBlogById(id));
        }

        // POST: BlogManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog updatedBlog, int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                // Add logic to verify entry - validateBlog()
                _blogRepo.Edit(updatedBlog);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(updatedBlog);
            }
        }

        // GET: BlogManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_blogRepo.GetBlogById(id));
        }

        // POST: BlogManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Blog deleteBlog, int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _blogRepo.Delete(deleteBlog);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(deleteBlog);
            }
        }
    }
}