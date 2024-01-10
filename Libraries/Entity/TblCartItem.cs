using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class TblCartItem
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Price { get; set; }
        public long? CartmasterId { get; set; }
        public long? UserId { get; set; }
    }
}
