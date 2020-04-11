using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMW_Project.Models
{
    public class OrderProduct
    {
        [ForeignKey("Product")]
        [Key]
        [Column(Order = 1)]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Order")]
        [Key]
        [Column(Order = 2)]
        public string OrderId { get; set; }
        public Order Order { get; set; }

        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}