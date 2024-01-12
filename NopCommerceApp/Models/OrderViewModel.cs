using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class OrderViewModel
    {
        public string TransactionNumber { get; set; }
        public DateTime purchasedDate { get; set; } 
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime deliveryDate{ get; set; }
        public long CartMasterId { get; set; }

        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}