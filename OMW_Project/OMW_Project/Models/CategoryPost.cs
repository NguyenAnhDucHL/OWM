using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class CategoryPost
    {
        public CategoryPost()
        {
            CategoryPostId = Guid.NewGuid().ToString();
        }
        [Key]
        public string CategoryPostId { get; set; }

        [Required]
        [Display(Name = "Thể loại bài viết")]
        public string CategoryName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}