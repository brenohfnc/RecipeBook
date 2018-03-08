using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace RecipeBook.DataAccess.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(RecipeBookContext context) : base(context)
        { }

        public async Task<Book> GetBookWithRecipesAsync(int ID)
        {
            var booksWithRecipes = await Context.Set<Book>().Include(b => b.RecipeBooks).FirstOrDefaultAsync(book => book.ID == ID);

            foreach (var recipeBook in booksWithRecipes.RecipeBooks)
                recipeBook.Recipe = await Context.Set<Recipe>().FirstOrDefaultAsync(r => r.ID == recipeBook.RecipeID);

            return booksWithRecipes;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesNotInBook(int ID)
        {
            var allRecipes = await Context.Set<Recipe>().Include(b => b.RecipeBooks).AsNoTracking().ToListAsync();
            var recipesNotInBook = new List<Recipe>();

            foreach (var recipe in allRecipes)
                if (!recipe.RecipeBooks.Any()|| recipe.RecipeBooks.Any(rb => rb.BookID != ID))
                    recipesNotInBook.Add(recipe);

            return recipesNotInBook;
        }
    }
}
