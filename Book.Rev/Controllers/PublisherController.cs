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
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PublisherController(ApplicationDbContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
             _context=context;
            _toastNotification = toastNotification;
            webHostEnvironment = hostEnvironment;

        }

        
       
        // GET: PublisherController
        public ActionResult Index()
        {
            var pubs = _context.Publishers.ToList();
            return View(pubs);
        }

        // GET: PublisherController/Details/5
        public ActionResult Details(int id)
        {
            var pub = _context.Publishers.Where(a => a.Id == id).FirstOrDefault();
            return View(pub);
        }

        // GET: PublisherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Publisher pub)
        {
            try
            {
                _context.Add<Models.Publisher>(pub);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Publisher Added Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: PublisherController/Edit/5
        public ActionResult Edit(int x)
        {
            var pub = _context.Publishers.Where(a => a.Id == x).FirstOrDefault();
            return View(pub);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Publisher pub)
        {
            try
            {
                _context.Update<Models.Publisher>(pub);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Publisher Updated Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: PublisherController/Delete/5
        public ActionResult Delete(int id)
        {
            var pub = _context.Publishers.Where(a => a.Id == id).FirstOrDefault();
            return View(pub);
        }

        // POST: PublisherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Models.Publisher pub)
        {
            try
            {
                _context.Remove<Models.Publisher>(pub);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Publisher Removed Successfully");

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
