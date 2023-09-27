using Book.Rev.Data;
using BookRev.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace BookRev.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;


		public BooksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
			webHostEnvironment = hostEnvironment;

		}
		// GET: BooksController
		public ActionResult Index()
        {
            var books = _context.Books.Include(b=>b.Author).Include(b=>b.Publisher).Include(b=>b.Category).ToList();
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

            var viewModel = getModel();

            return View(viewModel);
            
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookFormViewModel model)
        {
            var BookModel = getModel();
            if (!ModelState.IsValid)
            {
                return View(BookModel);
            }

            var files = Request.Form.Files;

            if (!files.Any())
            {
               
                ModelState.AddModelError("Poster", "Please Select book image!");
                return View(BookModel);
            }

            var image = files.FirstOrDefault();
            var allowExtenstions = new List<string> { ".jpg", ".jpeg", ".png", ".ico", ".svg" };

            if (!allowExtenstions.Contains(Path.GetExtension(image.FileName).ToLower()))
            {
                ModelState.AddModelError("Image", "Invalid extention only valid extentions"+String.Join(" , ",allowExtenstions));
                return View(BookModel);
            }
            var ImageName = UploadedFile(model.Image);

			var books = new Models.Book
			{
                Title = model.Title,
                CategoryId = model.CategoryId,
                AuthorId = model.AuthorId,
                PublisherId = model.PublisherId,
                Year = model.Year,
                Rate = model.Rate,
                Description = model.Description,
                Image=ImageName
            };
            _context.Books.Add(books);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

            
        }
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

        private BookFormViewModel getModel() {
            var model = new BookFormViewModel() {
				Categories = _context.Categories.OrderBy(m => m.Name).ToList(),
				Authors = _context.Authorities.OrderBy(m => m.Name).ToList(),
				Publishers = _context.Publishers.OrderBy(m => m.Name).ToList()
			};
        return model;
        }

		private string UploadedFile(IFormFile file)
		{
			string uniqueFileName = null;

			if (file != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath,"images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" +file.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}
	}
}
