using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class DoctorContribution
    {
        [ForeignKey("Order")]
        [Key]
        [Column(Order = 1)]
        public string OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("DoctorPayment")]
        [Key]
        [Column(Order = 2)]
        public string DoctorPaymentId { get; set; }

        public DoctorPayment DoctorPayment { get; set; }

        public decimal CommissionRate { get; set; }
        public decimal CommissionAmount { get; set; }
    }
}