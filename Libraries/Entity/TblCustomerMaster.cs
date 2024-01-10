using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class TblCustomerMaster
    {
        public long Id { get; set; }
        public string CustomerFname { get; set; }
        public string CustomerLname { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string LoginPassword { get; set; }
        public string CustomerAddress { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string SecurityQuestionsCode { get; set; }
        public string SecurityAnswerCode { get; set; }
        public string AccountNo { get; set; }
    }
}
