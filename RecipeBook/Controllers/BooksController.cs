using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeBook.Web.Controllers
{
    using DataAccess.Models;
    using DataAccess.Repositories;

    public class BooksController : Controller
    {
        private readonly BookRepository _repository;

        public BooksController(BookRepository repository)
        {
            _repository = repository;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")]Book book)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID")]Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Route("/Books/{id}/Recipes")]
        public async Task<IActionResult> Recipes(int ID)
        {
            var book = await _repository.GetBookWithRecipesAsync(ID);
            var recipes = book.RecipeBooks.Select(r => r.Recipe);

            ViewBag.BookID = ID;
            ViewBag.BookName = book.Name;

            return View(recipes);
        }

        [HttpGet]
        [Route("/Books/{id}/AddRecipe")]
        public async Task<IActionResult> AddRecipe(int ID)
        {
            var recipesNotInBook = await _repository.GetRecipesNotInBook(ID);

            ViewBag.BookID = ID;

            return View(recipesNotInBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Books/{BookID}/AddRecipe")]
        public async Task<IActionResult> AddRecipe(int BookID, int RecipeID)
        {
            var book = await _repository.GetAsync(BookID);
            book.RecipeBooks.Add(new RecipeBook() { BookID = BookID, RecipeID = RecipeID });

            await _repository.UpdateAsync(book);

            return RedirectToAction(nameof(Recipes), new { ID = BookID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecipe(int BookID, int RecipeID)
        {
            var book = await _repository.GetBookWithRecipesAsync(BookID);
            var recipe = book.RecipeBooks.FirstOrDefault(r => r.BookID == BookID && r.RecipeID == RecipeID);

            book.RecipeBooks.Remove(recipe);

            await _repository.UpdateAsync(book);

            return RedirectToAction(nameof(Recipes), new { ID = BookID });
        }

        private async Task<bool> BookExists(int id)
        {
            return (await _repository.GetAsync()).Any(e => e.ID == id);
        }
    }
}
