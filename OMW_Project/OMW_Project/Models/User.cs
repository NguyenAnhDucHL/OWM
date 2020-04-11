using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMW_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        
        public string FullName { get; set; }

        public string Address { get; set; }
        public bool Sex { get; set; }

        public string Image { get; set; }
        [ForeignKey("DoctorId")]
        public ICollection<Order> DoctorOrders { get; set; }
        [ForeignKey("WebManagerId")]
        public ICollection<Order> WebManagerOrders { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Order> UserOrders { get; set; }
        [ForeignKey("ProductId")]
        public ICollection<Product> Products { get; set; }
        [ForeignKey("SliderId")]
        public ICollection<Slider> Sliders { get; set; }

        [ForeignKey("DoctorPaymentId")]
        public ICollection<DoctorPayment> DoctorPayments { get; set; }

        [ForeignKey("DoctorId")]
        public ICollection<Consulting> DoctorConsultings { get; set; }
        [ForeignKey("WebManagerId")]
        public ICollection<Consulting> WebManagerConsultings { get; set; }
        [ForeignKey("PatientId")]
        public ICollection<Consulting> PatentConsultings { get; set; }
        [ForeignKey("PostId")]
        public ICollection<Post> Posts { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


}