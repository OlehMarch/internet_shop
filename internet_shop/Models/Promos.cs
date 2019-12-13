using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internet_shop.Models
{
    public class Promos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int CategoryId { get; set; } // TODO(friday13): why?
        public int BrandId { get; set; } // TODO(friday13): why?
        public int ProductId { get; set; } // TODO(friday13): why?
        public bool IsEnabled { get; set; }
    }
}