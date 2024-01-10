using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class TblOrderMaster
    {
        public long Id { get; set; }
        public long? CartMasterId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
