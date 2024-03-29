﻿using Libraries.Data;
using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly NopCommerceContext _context;

        public CartRepository(NopCommerceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<TblCartItem> GetTblCartsbyId(long userId)
        {
            return _context.TblCartItem
                .Where(c => c.UserId == userId)
                .ToList();
        }
        public string getProductNamebyId(long? productId)
        {
            return _context.TblProductMaster.Where(c => c.Id == productId).FirstOrDefault().ProductName;
        }

        public void RemoveItems(long Id)
        {
            var itemsToRemove = _context.TblCartItem
                .Where(c => c.Id == Id)
                .ToList();

            _context.TblCartItem.RemoveRange(itemsToRemove);
            _context.SaveChanges();
        }
        public TblCartMaster CreateCartMaster(long userId, decimal? total, decimal discount, decimal? totalDiscountedPrice)
        {
            TblCartMaster cartMaster = new TblCartMaster
            {
                UserId = userId,
                Price = total,
                Discount = discount,
                TotalPrice = totalDiscountedPrice,
                IsProcessed = false
            };

            _context.TblCartMaster.Add(cartMaster);
            _context.SaveChanges();

            return cartMaster;
        }
        public void UpdateCartMaster(TblCartMaster cartMaster)
        {
            _context.TblCartMaster.Update(cartMaster);  

            // Save changes to the database
            _context.SaveChanges();
        }
        public TblCartMaster GetCartMaster(long userId)
        {
            return _context.TblCartMaster.FirstOrDefault(c => c.UserId == userId);
        }
        public void SaveOrderToDatabase(TblOrderMaster order)
        {
            // Map OrderViewModel properties to TblOrderMaster properties
            var orderMaster = new TblOrderMaster
            {
                TransactionId = order.TransactionId,
                CartMasterId = order.CartMasterId,
                Price = order.Price,
                DiscountPrice = order.DiscountPrice,
                TotalPrice = order.TotalPrice,
                // Set other properties accordingly
            };

            // Save the order to your database using your data context
            _context.TblOrderMaster.Add(orderMaster);
            _context.SaveChanges();
        }
    }
}
