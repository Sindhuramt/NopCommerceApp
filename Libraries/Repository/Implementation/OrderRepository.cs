using Libraries.Data;
using Libraries.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NopCommerceContext _context;

        public OrderRepository(NopCommerceContext context)
        {
            _context = context;
        }
        public List<TblOrderMaster> GetOrdersByUserId(long userId)
        {
            var orders = (from order in _context.TblOrderMaster
                          join cart in _context.TblCartMaster on order.CartMasterId equals cart.Id
                          where cart.UserId == userId
                          select order)
                         .ToList();

            return orders;
        }

    }
}
