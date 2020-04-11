using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMW_Project.Models
{
    public class Product
    {
        public Product()
        {
            ProductId = Guid.NewGuid().ToString();
        }
        [Key]
        public string ProductId { get; set; }

        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Nhà thuốc")]
        public string Drugstore { get; set; }
        [AllowHtml]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Giá gốc")]
        public int OriginPrice { get; set; }
        [NotMapped]
        public HttpPostedFileBase myfile { get; set; }
        [Display(Name = "Giá bán")]
        public int SalePrice { get; set; }
        [ForeignKey("CategoryProduct")]
      
        public string CategoryId { get; set; }
        
        public CategoryProduct CategoryProduct { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}