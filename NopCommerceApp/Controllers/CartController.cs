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

        [HttpPost]
        //public ActionResult BuyNow(decimal? discountInput)
        //{
        //    long userId = GetLoggedInUserId();

        //    // Retrieve the existing cart master from the repository
        //    TblCartMaster cartMaster = _cartRepository.GetCartMaster(userId);

        //    // Check if the cart master exists; if not, you may handle this case based on your business logic
        //    if (cartMaster == null)
        //    {
        //        // Handle the case where the cart master doesn't exist
        //        return RedirectToAction("Index");
        //    }

        //    // Calculate the total price of items in the cart
        //    decimal? total = _cartRepository.GetTotalCartPrice(userId);

        //    // Apply the discount input if provided, otherwise, use the default discount logic
        //    decimal discount = CalculateDiscountLogic(total, discountInput);

        //    // Calculate the total discounted price
        //    decimal? totalDiscountedPrice = total - discount;

        //    // Update the properties of the existing cart master
        //    cartMaster.TotalPrice = total;
        //    cartMaster.Discount = discount;
        //    cartMaster.TotalDiscountedPrice = totalDiscountedPrice;

        //    // Update the existing cart master in the repository
        //    _cartRepository.UpdateCartMaster(cartMaster);

        //    // Retrieve the cart items associated with the cart master
        //    var cartItems = _cartRepository.GetTblCartsbyId(userId);
        //    var listCartItems = MapToCartItemViewModels(cartItems);

        //    // Create a BuyNowViewModel and populate it with the cart items and updated cart master
        //    var viewModel = new BuyNowViewModel
        //    {
        //        CartItems = listCartItems,
        //        CartMaster = cartMaster,
        //        TotalPrice = total,
        //        Discount = discount
        //    };

        //    // Pass the BuyNowViewModel to the view
        //    return View("BuyNow", viewModel);
        //}
        public ActionResult ProceedToPayment()
        {
            // Retrieve necessary data for the payment process
            long userId = GetLoggedInUserId();
            var cartItems = _cartRepository.GetTblCartsbyId(userId);
            var listCartItems = MapToCartItemViewModels(cartItems);

            // Retrieve the cart master details
            TblCartMaster cartMaster = _cartRepository.GetCartMaster(userId);

            // Create a PaymentViewModel to pass necessary data to the payment view
            var paymentViewModel = new PaymentViewModel
            {
                CartItems = listCartItems,
                CartMaster = cartMaster
            };

            // You can perform additional processing or validation here if needed

            // Pass the PaymentViewModel to the payment view
            return View(paymentViewModel);
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
        private List<CartItemViewModel> MapToCartItemViewModels(List<TblCartItem> cartItems)
        {
            List<CartItemViewModel> result = new List<CartItemViewModel>();

            foreach (var cartItem in cartItems)
            {
                CartItemViewModel cartItemViewModel = new CartItemViewModel
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = _cartRepository.getProductNamebyId(cartItem.ProductId),
                    ProductPrice = cartItem.Price
                };

                result.Add(cartItemViewModel);
            }

            return result;
        }
        private decimal CalculateDiscountLogic(decimal? total, decimal? discountInput )
        {
            // If the user provides a discountInput, use it; otherwise, apply the default logic
            if (discountInput.HasValue)
            {
                return discountInput.Value;
            }

            // Replace this with your actual default discount calculation logic
            // This is just a placeholder, you should implement your own logic here
            decimal defaultDiscountPercentage = 10; // Example: 10% default discount
            decimal defaultDiscount = (total ?? 0) * (defaultDiscountPercentage / 100);

            return defaultDiscount;
        }

    }

}