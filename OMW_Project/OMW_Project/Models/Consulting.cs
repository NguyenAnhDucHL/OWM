using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class Consulting
    {
        public Consulting()
        {
            ConsultingId = Guid.NewGuid().ToString();
        }
        [Key]
        public string ConsultingId { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public User Patient { get; set; }
        public string PatientIssue { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        public string DoctorId { get; set; }
        public User Doctor { get; set; }
        [ForeignKey("WebManager")]
        public string WebManagerId { get; set; }
        public User WebManager { get; set; }
        [Required]
        public DateTime StartConsulting { get; set; }
        public bool Status { get; set; }
    }
}