using Libraries.Data;
using Libraries.Repository.Interfaces;
using NopCommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NopCommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public ActionResult Index()
        {
            var listCartItems = new List<CartItemViewModel>();
            long userId = GetLoggedInUserId();
            var cartItems = _cartRepository.GetTblCartsbyId(userId);
            foreach(var cart in cartItems)
            {
                CartItemViewModel cartItem = new CartItemViewModel();
                cartItem.ProductId = cart.ProductId;
                cartItem.ProductName = _cartRepository.getProductNamebyId(cart.ProductId);
                cartItem.ProductPrice = cart.Price;
                listCartItems.Add(cartItem);
            }
            return View(listCartItems);
        }
        public ActionResult BuyNow()
        {
            long userId = GetLoggedInUserId();
            List<TblCartItem> cartItems = _cartRepository.GetTblCartsbyId(userId);

            // Calculate total and apply any discount logic here
            decimal? total = cartItems.Sum(item => item.Price);
            decimal discount = 0; // Implement your discount logic here
            decimal? totalDiscountedPrice = total - discount;

            // Create a cart master record
            TblCartMaster cartMaster = _cartRepository.CreateCartMaster(userId, total, discount, totalDiscountedPrice);

            // Pass the cart master ID to the CartMaster view
            return View("CartMaster", cartMaster);
        }

        [HttpPost]
        public ActionResult ClearCart(long productId)
        {
            long userId = GetLoggedInUserId();
            _cartRepository.RemoveItems(productId);
            return View();
        }
        private long GetLoggedInUserId()
        {
            return Session["UserId"] as long? ?? 0;

        }
    }

}