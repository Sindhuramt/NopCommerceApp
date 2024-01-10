using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class TblCartMaster
    {
        public long Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsProcessed { get; set; }
        public long? UserId { get; set; }
    }
}
