using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NopCommerceApp.Models
{
    public class ForgotPasswordViewModel
    {
        public string Mobile { get; set; }

        public string SecurityQuestionCode { get; set; }

        public string SecurityAnswer { get; set; }

        public string NewPassword { get; set; }

        public List<TblSecurityQuestionMaster> tbl_security_question_master { get; set; }
    }

}