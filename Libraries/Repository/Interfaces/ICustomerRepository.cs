using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        TblCustomerMaster AuthenticateUser(string email, string password);
        TblCustomerMaster RegisterCustomer(TblCustomerMaster customerMaster);
        List<TblSecurityQuestionMaster> GetSecurityQuestions();
        int CheckEmail(string email);
        int CheckMobile(string mobile);
        bool RecoverPassword(string mobile, string securityQuestionCode, string securityAnswer, string newPassword);
        long GetUserIdByEmail(string email);
        bool CheckSecurityQuestion(string mobile, string securityQuestionCode, string securityAnswer);
    }
}
