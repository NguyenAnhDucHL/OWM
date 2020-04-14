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

        [Required(ErrorMessage = "Thể loại sản phẩm không được để trống")]
        [MinLength(3, ErrorMessage = "Thể loại sản phẩm phải có ít nhất 3 ký tự")]
        [Display(Name = "Thể loại sản phẩm")]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}