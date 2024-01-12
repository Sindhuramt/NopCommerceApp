using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class BuyNowViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public TblCartMaster CartMaster { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal Discount { get; set; }
    }

    //   IPv4 Address. . . . . . . . . . . : 192.168.1.138(Preferred)
}