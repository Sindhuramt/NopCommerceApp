using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<TblProductMaster> GetProducts();
        TblProductMaster GetProductById(int productId);
        void AddToCart(long userId, int productId,decimal price);
        void BuyNow(int userId, int productId);
    }
}
