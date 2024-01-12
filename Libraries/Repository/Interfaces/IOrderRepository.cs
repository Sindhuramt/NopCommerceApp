using Libraries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.Interfaces
{
    public interface IOrderRepository
    {
        List<TblOrderMaster> GetOrdersByUserId(long userId);

    }
}
