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

        public void RemoveItems(long productID)
        {
            var itemsToRemove = _context.TblCartItem
                .Where(c => c.ProductId == productID)
                .ToList();

            _context.TblCartItem.RemoveRange(itemsToRemove);
            _context.SaveChanges();
        }
    }
}
