using Book.Rev.Data;
using Book.Rev.ViewModels;
using BookRev.ViewModels;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System.Drawing;

namespace BookRev.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
		private readonly IWebHostEnvironment webHostEnvironment;
        private new List<string> _allowExtenstions = new List<string> { ".jpg", ".jpeg", ".png", ".ico", ".icon", ".gif", ".svg" };



        public BooksController(ApplicationDbContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
			webHostEnvironment = hostEnvironment;

		}
		// GET: BooksController
		public ActionResult Index()
        {
            var books = _context.Books.Include(b=>b.Author).Include(b=>b.Publisher).Include(b=>b.Category).OrderByDescending(b=>b.Rate ).ToList();
            return View(books);
            
            
            
        }

        // GET: BooksController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).ToList();
            
            if (id== null)
                return BadRequest();

            var book = await _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).SingleOrDefaultAsync(b=>b.Id==id);
            if(book == null)
                return NotFound();

            return View(book);


            
        }

        // GET: BooksController/Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var viewModel = new BookFormViewModel
            {
                Categories=await _context.Categories.OrderBy(m=>m.Name).ToListAsync(),
                Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync()
            }; 
                return View("BookForm", viewModel);
            
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookFormViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync();
                model.Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync();
                model.Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync();
                return View("BookForm", model);
            }

            var files = Request.Form.Files;

            if (!files.Any())
            {
                model.Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync();
                model.Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync();
                model.Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select book image!");
                return View("BookForm", model);
            }

            var image = files.FirstOrDefault();

            if (!_allowExtenstions.Contains(Path.GetExtension(image.FileName).ToLower()))
            {
                model.Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync();
                model.Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync();
                model.Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Image", "Invalid extention only valid extentions"+String.Join(" , ",_allowExtenstions));
                return View("BookForm", model);
            }
            using var datastream=new MemoryStream();
            var ImageName = UploadedFile(model.Image);
            await image.CopyToAsync(datastream);

            var books = new Models.Book
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                AuthorId = model.AuthorId,
                PublisherId = model.PublisherId,
                Year = model.Year,
                Rate = model.Rate,
                Description = model.Description,
                Image = ImageName
            };

            //var books = new Models.Book
            //{
            //             Title = model.Title,
            //             CategoryId = model.CategoryId,
            //             AuthorId = model.AuthorId,
            //             PublisherId = model.PublisherId,
            //             Year = model.Year,
            //             Rate = model.Rate,
            //             Description = model.Description,
            //             Image=ImageName
            //         };
            _context.Books.Add(books);
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Book Added Successfully");

            return RedirectToAction(nameof(Index));

            
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

          // var imageName = UploadedFile(id);

            var viewModel = new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                Rate = book.Rate,
                Year = book.Year,
                ImageName=book.Image,
                Description = book.Description,
                Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync(),
                Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync()
            };

            return View("BookForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnModel = getModel();
                return View("BookForm", returnModel);
            }
            var book = await _context.Books.FindAsync(model.Id);
            //var ImageName = UploadedFile(model.Image);
            if (book == null)
                return NotFound();

            var files = Request.Form.Files;

            if (files.Any())
            {
                var poster = files.FirstOrDefault();
                using var datastream = new MemoryStream();
                var ImageName = UploadedFile(poster);
                await poster.CopyToAsync(datastream);

               // model.Image=datastream.ToArray();
               
                if (!_allowExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    model.Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync();
                    model.Authors = await _context.Authorities.OrderBy(m => m.Name).ToListAsync();
                    model.Publishers = await _context.Publishers.OrderBy(m => m.Name).ToListAsync();
                    ModelState.AddModelError("Image", "Invalid extention only valid extentions" + String.Join(" , ", _allowExtenstions));
                    return View("BookForm", model);
                }
               
               // book.Image = datastream.ToArray();
                 book.Image=ImageName;
            }

            book.Title=model.Title;
            book.CategoryId = model.CategoryId;
            book.AuthorId = model.AuthorId;
            book.PublisherId = model.PublisherId;
            book.Year = model.Year;
            book.Rate = model.Rate;
            book.Description = model.Description;
           

            _context.Update(book);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Book Updated Successfully");
            return RedirectToAction(nameof(Index));

        }

       
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
                return BadRequest();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _toastNotification.AddSuccessToastMessage("Book Removed Successfully");

            _context.SaveChanges();

            return Ok();



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
