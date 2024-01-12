using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class PaymentViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public TblCartMaster CartMaster { get; set; }
    }
}