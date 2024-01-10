using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class TblProductMaster
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
    }
}
