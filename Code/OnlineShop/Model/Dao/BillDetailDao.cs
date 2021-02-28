using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BillDetailDao
    {
        OnlineShopDbContext db = null;
        public BillDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public IQueryable<OrderDetail> ChiTietDH(int mahd)
        {
            var res = (from sp in db.OrderDetails where sp.OrderID == mahd select sp);
            return res;
        }
    }
}
