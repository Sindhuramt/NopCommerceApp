using Libraries.Data;
using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libraries.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NopCommerceContext _context;

        public CustomerRepository(NopCommerceContext context)
        {
            _context = context;
        }

        public TblCustomerMaster AuthenticateUser(string email, string password)
        {
            // For simplicity, this example assumes plain text passwords.
            // In a production scenario, you should use secure password hashing.

            var customer = _context.TblCustomerMaster
                .FirstOrDefault(c => c.CustomerEmail == email && c.LoginPassword == password);

            if (customer != null)
            {
                // Store the customer ID in the session to track the authenticated user.
                // Note: This is a simple example, and in a real-world scenario, you might use more secure authentication methods.
                HttpContext.Current.Session["CustomerId"] = customer.Id;
            }

            return customer;
        }
        public TblCustomerMaster RegisterCustomer(TblCustomerMaster customerMaster)
        {
            customerMaster.IsActive = true;
            customerMaster.CreatedOn = DateTime.Now;
            _context.TblCustomerMaster.Add(customerMaster);
            _context.SaveChanges();

            return customerMaster;
        }
        public List<TblSecurityQuestionMaster> GetSecurityQuestions()
        {
            return _context.TblSecurityQuestionMaster.ToList();
        }
        public int CheckEmail(string email)
        {
            return _context.TblCustomerMaster.Count(c => c.CustomerEmail == email);
        }

        public int CheckMobile(string mobile)
        {
            return _context.TblCustomerMaster.Count(c => c.CustomerMobile == mobile);
        }
        public bool RecoverPassword(string mobile, string securityQuestionCode, string securityAnswer, string newPassword)
        {
            var customer = _context.TblCustomerMaster
                .FirstOrDefault(c => c.CustomerMobile == mobile &&
                                      c.SecurityQuestionsCode == securityQuestionCode &&
                                      c.SecurityAnswerCode == securityAnswer);

            if (customer != null)
            {
                // Reset the password
                customer.LoginPassword = newPassword;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
