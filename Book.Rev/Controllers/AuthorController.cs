using Book.Rev.Data;
using Book.Rev.ViewModels;
using BookRev.Models;
using BookRev.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Runtime.Intrinsics.Arm;

namespace BookRev.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment webHostEnvironment;
        private new List<string> _allowExtenstions = new List<string> { ".jpg", ".jpeg", ".png", ".ico", ".icon", ".gif", ".svg" };


        public AuthorController(ApplicationDbContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
            webHostEnvironment = hostEnvironment;

        }

        // GET: AuthorController

        
        
        public ActionResult Index()
        {
            var auths = _context.Authorities.ToList();
            return View(auths);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var auth = _context.Authorities.Where(a => a.Id == id).FirstOrDefault();
            return View(auth);
            
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Author auth)
        {
            try
            {
                _context.Add<Models.Author>(auth);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Author Added Successfully");
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }



       // GET: AuthorController/Edit/5
        public ActionResult Edit(int x)
        {
            var auth = _context.Authorities.Where(a => a.Id == x).FirstOrDefault();
            return View(auth);
        }


       // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Author auth)
        {
            try
            {
                _context.Update<Models.Author>(auth);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Author Updated Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

            // GET: AuthorController/Delete/5
            public ActionResult Delete(int id)
        {
            var auth = _context.Authorities.Where(a => a.Id == id).FirstOrDefault();
            return View(auth);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Models.Author auth)
        {
            try
            {
                _context.Remove<Models.Author>(auth);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Author Removed Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
