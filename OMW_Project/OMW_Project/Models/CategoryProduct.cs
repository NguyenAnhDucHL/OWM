using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class CategoryProduct
    {
        public CategoryProduct()
        {
            CategoryProductId = Guid.NewGuid().ToString();
        }
        [Key]
        public string CategoryProductId { get; set; }

        [Required]
        [Display(Name = "Thể loại sản phẩm")]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}