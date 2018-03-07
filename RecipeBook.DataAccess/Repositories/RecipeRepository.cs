namespace RecipeBook.DataAccess.Repositories
{
    using Models;

    public class RecipeRepository : GenericRepository<Recipe>
    {
        public RecipeRepository(RecipeBookContext context) : base(context)
        { }
    }
}
