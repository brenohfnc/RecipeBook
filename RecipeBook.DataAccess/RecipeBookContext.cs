using Microsoft.EntityFrameworkCore;

namespace RecipeBook.DataAccess
{
    using Models;

    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext(DbContextOptions<RecipeBookContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeBook>()
                .HasKey(recipeBook => new {recipeBook.BookID, recipeBook.RecipeID });

            modelBuilder.Entity<RecipeBook>()
                .HasOne(b => b.Book)
                    .WithMany(rb => rb.RecipeBooks)
                        .HasForeignKey(rb => rb.BookID);

            modelBuilder.Entity<RecipeBook>()
                .HasOne(b => b.Recipe)
                    .WithMany(rb => rb.RecipeBooks)
                        .HasForeignKey(rb => rb.RecipeID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
