using OMW_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMW_Project.ViewModels
{
    public class OrdersViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public List<OrderProduct> ListProducts { get; set; }
    }
}