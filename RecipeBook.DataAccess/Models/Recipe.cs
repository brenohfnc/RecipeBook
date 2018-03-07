using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.DataAccess.Models
{
    [Table("Recipe")]
    public class Recipe : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        public ICollection<RecipeBook> RecipeBooks { get; set; } = new List<RecipeBook>();
    }
}
