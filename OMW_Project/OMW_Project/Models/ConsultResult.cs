using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class ConsultResult
    {
        public ConsultResult()
        {
            ConsultResultId = Guid.NewGuid().ToString();
        }
        [Key]
        public string ConsultResultId { get; set; }
        [ForeignKey("Consulting")]
        public string ConsultingId { get; set; }
        public Consulting Consulting { get; set; }
        public DateTime ContactTime { get; set; }
        public string ContactMedia { get; set; }
        public string ContactResult { get; set; }
        public string Suggestion { get; set; }
        public int TreamentDuration { get; set; }
        public DateTime NextContactTime { get; set; }
        public ICollection<ProductSuggest> ProductSuggests { get; set; }
    }
}