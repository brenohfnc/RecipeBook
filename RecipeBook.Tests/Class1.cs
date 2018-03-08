using RecipeBook.DataAccess.Models;
using Xunit;

namespace RecipeBook.Tests
{
    public class TestesRepositorios
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(2, 2);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(2, 3);
        }

    }
}
