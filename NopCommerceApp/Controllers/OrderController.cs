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
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly ICartRepository _cartRepository;

        public OrderController(IOrderRepository repository ,ICartRepository cartRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
        }

        public ActionResult ViewOrders()
        {
            // Get the currently logged-in user ID (you may replace this with your authentication logic)
            long userId = GetLoggedInUserId();

            // Fetch orders for the user
            List<TblOrderMaster> orders = _repository.GetOrdersByUserId(userId);

            // Get the cart items for the user
            List<TblCartItem> cartItems = _cartRepository.GetTblCartsbyId(userId);

            // Get the total quantity of items in the cart
            int totalQuantity = cartItems.Count;

            // Assuming you have a view model to represent order details
            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                TransactionNumber = order.TransactionId,
                Quantity = totalQuantity,
                purchasedDate = order.DeliveryDate ?? DateTime.MinValue,
                ProductName = _cartRepository.getProductNamebyId(GetProductName(cartItems, userId)),
                Price = order.Price ?? 0,
                deliveryDate = order.DeliveryDate ?? DateTime.MinValue,
                CartMasterId = order.CartMasterId ?? 0,
                DiscountPrice = order.DiscountPrice ?? 0,
                TotalPrice = order.TotalPrice ?? 0
            }).ToList();

            return View(orderViewModels);
        }

        private long GetProductName(List<TblCartItem> cartItems, long userId)
        {
            // Implement your logic to get product name based on cart items and product ID
            // For example, you might iterate through cart items and match the product ID
            // Adjust this logic based on your actual data structure

            // Sample logic:
            var matchingCartItem = cartItems.FirstOrDefault(cartItem => cartItem.UserId == userId);

            return matchingCartItem.ProductId ?? 0;
        }


        private long GetLoggedInUserId()
        {
            return Session["UserId"] as long? ?? 0;

        }
    }
}