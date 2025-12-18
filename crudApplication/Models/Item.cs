using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace crudApplication.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        [AllowNull,MaxLength(100)]
        public string ImageFileName { get; set; }="";

        [NotMapped]
        public IFormFile? Image { get; set; }
       
    }
}
