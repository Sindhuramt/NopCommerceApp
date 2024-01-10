using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NopCommerceApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Index(long userId)
        {
            var products = _productRepository.GetProducts();
            ViewBag.UserId = userId;
            return View(products);
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, long userId , decimal price)
        {

            _productRepository.AddToCart(userId, productId  , price);

            // Optionally, redirect to the cart or another page
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult BuyNow(int productId)
        {
            // Get the user ID from your authentication mechanism
            int userId = 1; // Replace with actual user ID retrieval

            _productRepository.BuyNow(userId, productId);

            // Optionally, redirect to the order confirmation or another page
            return RedirectToAction("Index", "Orders");
        }
    }
}