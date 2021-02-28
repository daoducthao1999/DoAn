using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/Bill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new BillDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            Order gh = db.Orders.SingleOrDefault(x => x.ID == id);
            BillDetailDao dao = new BillDetailDao();
            IQueryable<OrderDetail> listgh = dao.ChiTietDH(id);
            ViewData["DonHang"] = gh;
            return View(listgh);
        }
    }
}