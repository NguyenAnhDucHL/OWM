using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class ProductSuggest
    {
        [ForeignKey("ConsultResult")]
        [Key]
        [Column(Order = 1)]
        public string ConsultResultId { get; set; }
        public ConsultResult ConsultResult { get; set; }

        [ForeignKey("Product")]
        [Key]
        [Column(Order = 2)]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public string Dosage { get; set; }
    }
}