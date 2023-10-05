using Book.Rev.Data;
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
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment webHostEnvironment;
        private new List<string> _allowExtenstions = new List<string> { ".jpg", ".jpeg", ".png", ".ico", ".icon", ".gif", ".svg" };

        // GET: CategoryController1cs

        public CategoryController(ApplicationDbContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
            webHostEnvironment = hostEnvironment;

        }
        public ActionResult Index()
        {
            var cats = _context.Categories.ToList();
            return View(cats);
        }

        // GET: CategoryController1cs/Details/5
        public ActionResult Details(int id)
        {
            var cat = _context.Categories.Where(a => a.Id == id).FirstOrDefault();
            return View(cat);
        }

        // GET: CategoryController1cs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController1cs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Category cat)
        {
            try
            {
                _context.Add<Models.Category>(cat);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Category Added Successfully");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController1cs/Edit/5
        public ActionResult Edit(int x)
        {
            var cat = _context.Categories.Where(a => a.Id == x).FirstOrDefault();
            return View(cat);
        }

        // POST: CategoryController1cs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Category cat)
        {
            try
            {
                _context.Update<Models.Category>(cat);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Category Updated Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController1cs/Delete/5
        public ActionResult Delete(int id)
        {
            var cat = _context.Categories.Where(a => a.Id == id).FirstOrDefault();
            return View(cat);
        }

        // POST: CategoryController1cs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Models.Category cat)
        {
            try
            {
                _context.Remove<Models.Category>(cat);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Category Deletd Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
