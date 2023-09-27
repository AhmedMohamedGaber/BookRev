using BookRev.Models;
using BookRev.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BookRev.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: BooksController
        public ActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
            
            
            
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create

        [HttpGet]
        public IActionResult Create()
        {

            var viewModel = new BookFormViewModel
            {
                Categories =_context.Categories.OrderBy(m => m.Name).ToList(),
                Authors =_context.Authorities.OrderBy(m => m.Name).ToList(),
                Publishers =_context.Publishers.OrderBy(m => m.Name).ToList()
            };

            return View(viewModel);
            
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.OrderBy(m => m.Name).ToList();
                model.Authors = _context.Authorities.OrderBy(m => m.Name).ToList();
                model.Publishers = _context.Publishers.OrderBy(m => m.Name).ToList();
                return View(model);
            }



            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.OrderBy(m => m.Name).ToList();
                model.Authors = _context.Authorities.OrderBy(m => m.Name).ToList();
                model.Publishers = _context.Publishers.OrderBy(m => m.Name).ToList();
                return View(model);
            }

            var files = Request.Form.Files;

            if (!files.Any())
            {
                model.Categories = _context.Categories.OrderBy(m => m.Name).ToList();
                model.Authors = _context.Authorities.OrderBy(m => m.Name).ToList();
                model.Publishers = _context.Publishers.OrderBy(m => m.Name).ToList();
                ModelState.AddModelError("Poster", "Please Select book image!");
                return View(model);
            }

            var poster = files.FirstOrDefault();
            var allowExtenstions = new List<string>();






            if (!allowExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                model.Categories = _context.Categories.OrderBy(m => m.Name).ToList();
                model.Authors = _context.Authorities.OrderBy(m => m.Name).ToList();
                model.Publishers = _context.Publishers.OrderBy(m => m.Name).ToList();
                ModelState.AddModelError("Poster", "Only .png, .jpg images ..!");
                return View(model);
            }
            using var dataStream = new MemoryStream();
            poster.CopyToAsync(dataStream);


            var books = new Book
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                AuthorId = model.AuthorId,
                PublisherId = model.PublisherId,
                Year = model.Year,
                Rate = model.Rate,
                Description = model.Description,
                Poster = dataStream.ToArray()
            };
            _context.Books.Add(books);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

            //try
            //{
            //    emp.CreatedDate = DateTime.Now;
            //    emp.UpdateDate = DateTime.Now;
            //    emp.IsDeleted = false;
            //    emp.IsVisable = true;

            //    _myDBContext.Add<Employee>(emp);
            //    _myDBContext.SaveChanges();
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
            //return RedirectToAction(nameof(Index));
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
