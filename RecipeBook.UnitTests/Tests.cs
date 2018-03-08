using Xunit;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace RecipeBook.UnitTests
{
    using DataAccess;
    using DataAccess.Repositories;

    public class Tests
    {
        private BookRepository _bookRepository;

        public Tests()
        {
            _bookRepository = InMemoryBookRepository();
        }

        [Fact(DisplayName = nameof(CreateBook))]
        public async Task CreateBook()
        {
            await _bookRepository.AddAsync(new DataAccess.Models.Book() { Name = "TestBook" });

            Assert.Single((await _bookRepository.GetAsync()));
        }

        [Fact(DisplayName = nameof(UpdateBook))]
        public async Task UpdateBook()
        {
            var book = new DataAccess.Models.Book() { Name = "TestBook" };
            await _bookRepository.AddAsync(book);

            book.Name = "TestBook2";
            await _bookRepository.UpdateAsync(book);

            Assert.Equal("TestBook2", (await _bookRepository.GetAsync(book.ID)).Name);
        }

        [Fact(DisplayName = nameof(DeleteBook))]
        public async Task DeleteBook()
        {
            await _bookRepository.AddAsync(new DataAccess.Models.Book() { Name = "TestBook" });

            var book = (await _bookRepository.GetAsync()).FirstOrDefault();

            await _bookRepository.DeleteAsync(book.ID);

            Assert.Empty(await _bookRepository.GetAsync());
        }

        private BookRepository InMemoryBookRepository()
        {
            DbContextOptions<RecipeBookContext> options;
            var builder = new DbContextOptionsBuilder<RecipeBookContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            RecipeBookContext recipeBookContext = new RecipeBookContext(options);
            recipeBookContext.Database.EnsureDeleted();
            recipeBookContext.Database.EnsureCreated();

            return new BookRepository(recipeBookContext);
        }
    }
}
