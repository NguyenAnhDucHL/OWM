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

        [Required(ErrorMessage = "Thể loại bài viết không được để trống")]
        [MinLength(3,ErrorMessage = "Thể loại bài viết phải có ít nhất 3 ký tự")]
        [Display(Name = "Thể loại bài viết")]
        public string CategoryName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}