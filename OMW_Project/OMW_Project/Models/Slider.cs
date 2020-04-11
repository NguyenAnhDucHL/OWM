using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class Slider
    {
        [Key]
        public string SliderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public string Image { get; set; }
        public string link { get; set; }
    }
}