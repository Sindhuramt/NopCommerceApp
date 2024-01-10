using Libraries.Data;
using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
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

        public TblProductMaster GetProductById(int productId)
        {
            return _context.TblProductMaster.Find(productId);
        }

        public void AddToCart(long userId, int productId,decimal Price)
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

        public void BuyNow(int userId, int productId)
        {
            // Logic for buying a product immediately (e.g., creating an order)
            // You may need to handle transactions and other details here.
        }
    }

}
