﻿using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Interfaces
{
    public interface ICartRepository
    {
        List<TblCartItem> GetTblCartsbyId(long UserId);
        string getProductNamebyId(long? productId);
        void RemoveItems(long productId);
        TblCartMaster CreateCartMaster(long userId, decimal? total, decimal discount, decimal? totalDiscountedPrice);
        void UpdateCartMaster(TblCartMaster cartMaster);
        TblCartMaster GetCartMaster(long userId);
        void SaveOrderToDatabase(TblOrderMaster order);
    }
}
