using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDataLayer
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Specification> Specs { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
