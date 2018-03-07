using System.ComponentModel.DataAnnotations;

namespace RecipeBook.DataAccess.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
