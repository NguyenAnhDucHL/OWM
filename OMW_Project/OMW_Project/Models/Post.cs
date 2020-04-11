using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMW_Project.Models
{
    public class Post
    {
        public Post()
        {
            PostId = Guid.NewGuid().ToString();
        }
        [Key]
        public string PostId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [NotMapped]
        public HttpPostedFileBase myfile { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Kiểm duyệt")]
        public bool Status { get; set; }
        [ForeignKey("CategoryPost")]
        public string CategoryPostId { get; set; }
        public CategoryPost CategoryPost { get; set; }
    }
}