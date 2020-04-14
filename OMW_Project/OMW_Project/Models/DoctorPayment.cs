using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class DoctorPayment
    {

        public DoctorPayment()
        {
            DoctorPaymentId = Guid.NewGuid().ToString();
        }
        [Key]
        public string DoctorPaymentId { get; set; }

        public DateTime ConsolidateTime { get; set; }

        public decimal TotalPaid { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public string DoctorId { get; set; }
        public User Doctor { get; set; }
    }
}