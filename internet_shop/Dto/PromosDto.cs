using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Dto
{
    public class PromosDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UniversalId { get; set; }
        public int Value { get; set; }
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
