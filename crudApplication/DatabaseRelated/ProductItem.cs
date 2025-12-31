using System.ComponentModel.DataAnnotations;

namespace crudApplication.DatabaseRelated
{
    public class ProductItem
    {
      
        public string Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
    }
}
