using Book.Rev.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRev.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController1cs

        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryController1cs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController1cs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController1cs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CategoryController1cs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController1cs/Edit/5
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

        // GET: CategoryController1cs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController1cs/Delete/5
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
