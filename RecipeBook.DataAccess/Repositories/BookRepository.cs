namespace RecipeBook.DataAccess.Repositories
{
    using Models;

    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(RecipeBookContext context) : base(context)
        { }
    }
}
