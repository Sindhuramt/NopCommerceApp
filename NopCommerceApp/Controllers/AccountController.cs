using Libraries.Data;
using Libraries.Repository.Interfaces;
using NopCommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NopCommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public AccountController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerRepository.AuthenticateUser(model.Email, model.Password);

                if (customer != null)
                {
                    var userId = _customerRepository.GetUserIdByEmail(model.Email);
                    Session["UserId"] = userId;

                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    return RedirectToAction("Index", "Products",  new { userId }); 
                }
                else
                {
                  
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If we reach here, something went wrong, redisplay the form
            return View(model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            var securityQuestions = _customerRepository.GetSecurityQuestions();
            var registrationViewModel = new RegistrationViewModel
            {
                Customer = new TblCustomerMaster(),
                tbl_security_question_master = securityQuestions
            };
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            var isEmailExist = _customerRepository.CheckEmail(model.Customer.CustomerEmail);
            var mobileExist = _customerRepository.CheckMobile(model.Customer.CustomerMobile);

            if (isEmailExist == 0 && mobileExist == 0)
            {
                _customerRepository.RegisterCustomer(model.Customer);
                ViewBag.AlertMessage = "Register Successfully";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.AlertMessage = "User already exists. Please check email and mobile number.";
                var securityQuestions = _customerRepository.GetSecurityQuestions();
                var registrationViewModel = new RegistrationViewModel
                {
                    Customer = new TblCustomerMaster(),
                    tbl_security_question_master = securityQuestions
                };
                return View(registrationViewModel);
            }
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var securityQuestions = _customerRepository.GetSecurityQuestions();
            var forgotPasswordViewModel = new ForgotPasswordViewModel
            {
                tbl_security_question_master = securityQuestions
            };

            return View(forgotPasswordViewModel);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            var mobileExist = _customerRepository.CheckMobile(model.Mobile);

            if (mobileExist == 1)
            {
                // Check security question and answer
                var isSecurityAnswerCorrect = _customerRepository.CheckSecurityQuestion(model.Mobile, model.SecurityQuestionCode, model.SecurityAnswer);

                if (isSecurityAnswerCorrect)
                {
                    // Security question and answer are correct, proceed with password recovery
                    var resetPasswordResult = _customerRepository.RecoverPassword(model.Mobile, model.SecurityQuestionCode, model.SecurityAnswer, model.NewPassword);

                    if (resetPasswordResult)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError(string.Empty, "Failed to reset the password. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError("SecurityAnswer", "Incorrect security question or answer.");
                }
            }
            else
            {
                ModelState.AddModelError("Mobile", "Mobile number is not registered. Please register first.");
            }

            var securityQuestions = _customerRepository.GetSecurityQuestions();
            var forgotPasswordViewModel = new ForgotPasswordViewModel
            {
                tbl_security_question_master = securityQuestions
            };

            return View(forgotPasswordViewModel);
        }



    }


}