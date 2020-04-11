using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OMW_Project.Areas.Identity.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "Quyền hạn không được để trống")]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        [Required]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public bool Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string CMT { get; set; }
        public DateTime DateRange { get; set; }

        public string Workplace { get; set; }

        public string Experience { get; set; }
    }
}