using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class CustomerPayment
    {
        [Key]
        public string CustomerPaymentId { get; set; }
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime PayDate { get; set; }
        public string PayType { get; set; }
        public decimal Amount { get; set; }
    }
}