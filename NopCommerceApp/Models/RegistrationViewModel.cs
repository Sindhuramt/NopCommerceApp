using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class RegistrationViewModel
    {
        public TblCustomerMaster Customer { get; set; }
        public List<TblSecurityQuestionMaster> tbl_security_question_master { get; set; }
    }
}