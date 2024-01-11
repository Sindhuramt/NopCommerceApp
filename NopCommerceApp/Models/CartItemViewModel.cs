using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class CartItemViewModel
    {
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }

    }
}