using System.ComponentModel.DataAnnotations;

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
    }
}
