﻿using Libraries.Data;
using Libraries.Repository.Interfaces;
using NopCommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

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
                    long userId = customer.Id;
                    // Authentication successful
                    return RedirectToAction("Index", "Products",  new { userId }); // Redirect to the home page after login
                }
                else
                {
                    // Invalid credentials, add a model error
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
                var resetPasswordResult = _customerRepository.RecoverPassword(model.Mobile, model.SecurityQuestionCode, model.SecurityAnswer, model.NewPassword);

                if (resetPasswordResult)
                {
                    // Password reset successful, redirect to login page or another page
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "Invalid mobile number, security question, or security answer");
            }

            ModelState.AddModelError("Mobile", "Mobile Number not exist, Register first ");
            // If password reset fails, return the forgot password view with validation errors
            var securityQuestions = _customerRepository.GetSecurityQuestions();
            var forgotPasswordViewModel = new ForgotPasswordViewModel
            {
                tbl_security_question_master = securityQuestions
            };
            return View(forgotPasswordViewModel);
        }


    }


}