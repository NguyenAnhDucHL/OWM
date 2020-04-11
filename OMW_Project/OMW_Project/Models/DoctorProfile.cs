using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class DoctorProfile
    {
        [ForeignKey("User")]
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "Số CMTND/CCCD")]
        [StringLength(12)]
        public string CMT { get; set; }

        [Display(Name = "Ngày cấp")]
        public DateTime DateRange { get; set; }
        [Display(Name = "Nơi làm việc")]
        public string Workplace { get; set; }
        [Display(Name = "Kinh nghiệm")]
        public string Experience { get; set; }

    }
}