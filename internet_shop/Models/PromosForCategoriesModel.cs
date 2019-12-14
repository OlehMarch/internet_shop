using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace internet_shop.Models
{
    public class PromosForCategoriesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PromoId { get; set; }
        //public int CategoryId { get; set; }
        public string Name { get; set; }
        public int UniversalId { get; set; }
        public bool Enabled { get; set; }
    }
}
