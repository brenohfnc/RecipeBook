using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.DataAccess.Models
{
    public class RecipeBook : BaseEntity
    {
        [ForeignKey("FK_Book")]
        public int BookID { get; set; }

        public Book Book { get; set; }

        [ForeignKey("FK_Recipe")]
        public int RecipeID { get; set; }

        public Recipe Recipe { get; set; }
    }
}
