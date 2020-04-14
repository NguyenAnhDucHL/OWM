using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
        }
        [Key]
        public string OrderId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public double TotalCost { get; set; }
        public bool Status { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public User Doctor { get; set; }
        [ForeignKey("WebManager")]
        public string WebManagerId { get; set; }
        public User WebManager { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}