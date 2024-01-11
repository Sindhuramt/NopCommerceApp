using Libraries.Data;
using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly NopCommerceContext _context;

        public ProductRepository(NopCommerceContext context)
        {
            _context = context;
        }

        public List<TblProductMaster> GetProducts()
        {
            return _context.TblProductMaster.ToList();
        }

        public TblProductMaster GetProductById(long productId)
        {
            return _context.TblProductMaster.Find(productId);
        }

        public void AddToCart(long userId, long productId,decimal Price)
        {
            var cartItem = new TblCartItem
            {
                UserId = userId,
                ProductId = productId,
                Price = Price
            };

            _context.TblCartItem.Add(cartItem);
            _context.SaveChanges();
        }

        public void BuyNow(long userId, long productId)
        {
     
            var cartItem = _context.TblCartItem
                .SingleOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
               
                _context.TblCartItem.Remove(cartItem);
                _context.SaveChanges();
            }
            
        }
        public string GetImageUrl(long productId)
        {
            var product = _context.TblProductMaster.Find(productId);
            return product?.ProductImage;
        }
        
    }


    }


