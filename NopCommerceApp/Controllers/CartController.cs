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
                cartItem.Id = cart.Id;
                cartItem.ProductId = cart.ProductId;
                cartItem.ProductName = _cartRepository.getProductNamebyId(cart.ProductId);
                cartItem.ProductPrice = cart.Price;
                listCartItems.Add(cartItem);
            }
            return View(listCartItems);
        }
        public ActionResult BuyNow()
        {
            var listCartItems = new List<CartItemViewModel>();
            long userId = GetLoggedInUserId();
            var cartItems = _cartRepository.GetTblCartsbyId(userId);
            foreach (var cart in cartItems)
            {
                CartItemViewModel cartItem = new CartItemViewModel();
                cartItem.Id = cart.Id;
                cartItem.ProductId = cart.ProductId;
                cartItem.ProductName = _cartRepository.getProductNamebyId(cart.ProductId);
                cartItem.ProductPrice = cart.Price;
                listCartItems.Add(cartItem);
            }
            // Calculate the total price of items in the cart
            decimal? total = cartItems.Sum(item => item.Price);

            // For demonstration, applying a discount (you can replace this with your actual discount logic)
            decimal discount = 0;

            // Calculate the total discounted price
            decimal? totalDiscountedPrice = total - discount;

            // Create a TblCartMaster object with the calculated values
            TblCartMaster cartMaster = _cartRepository.CreateCartMaster(userId, total, discount, totalDiscountedPrice);

            // Create a BuyNowViewModel and populate it with the cart items and cart master
            var viewModel = new BuyNowViewModel
            {
                CartItems = listCartItems,
                CartMaster = cartMaster,
                TotalPrice = total,
                Discount = discount
            };

            // Pass the BuyNowViewModel to the view
            return View(viewModel);
        }



        public ActionResult ClearCart(long Id)
        {
            long userId = GetLoggedInUserId();
            _cartRepository.RemoveItems(Id);
            return RedirectToAction("Index", new { userId });
        }

        private long GetLoggedInUserId()
        {
            return Session["UserId"] as long? ?? 0;

        }
    }

}