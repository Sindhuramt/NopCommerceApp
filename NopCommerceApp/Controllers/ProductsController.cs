using Libraries.Data;
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
            List<TblProductMaster> products = new List<TblProductMaster>();
            products = _productRepository.GetProducts();
            foreach(var product in products)
            {
                product.ProductImage = _productRepository.GetImageUrl(product.Id);
                if (_productRepository.IsProductInCart(userId, product.Id))
                {
                    ViewBag.Flag = 1;
                }
            }
            ViewBag.UserId = userId;
            return View(products);
        }

        [HttpPost]
        public ActionResult AddToCart(long productId, decimal price , string productName)
        {
            long userId = GetLoggedInUserId();

            _productRepository.AddToCart(userId, productId  , price);

            TempData["AddedProduct"] = productName;

            // Return a success status
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult BuyNow(long productId)
        {
          
            long userId = GetLoggedInUserId(); 

            _productRepository.BuyNow(userId, productId);

            
            return RedirectToAction("Index", "Orders");
        }
        private long GetLoggedInUserId()
        {
            return Session["UserId"] as long? ?? 0;
            
        }
        
    }

}
    
