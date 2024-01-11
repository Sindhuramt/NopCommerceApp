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
        TblProductMaster GetProductById(long productId);
        void AddToCart(long userId, long productId,decimal price);
        void BuyNow(long userId, long productId);
        string GetImageUrl(long productId);
        bool IsProductInCart(long userId, long productId);

    }
}
