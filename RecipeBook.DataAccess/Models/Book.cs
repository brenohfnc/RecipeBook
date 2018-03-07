using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.DataAccess.Models
{
    [Table("Book")]
    public class Book : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<RecipeBook> RecipeBooks { get; set; } = new List<RecipeBook>();
    }
}
