using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Code,MetaTitle," +
            "Description,Images,MoreImages,Price,PromotionPrice,IncludeVAT," +
            "Quantity,CategoryID,Detail,Warranty,CreateDate,CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Code,MetaTitle," +
            "Description,Images,MoreImages,Price,PromotionPrice,IncludeVAT," +
            "Quantity,CategoryID,Detail,Warranty,CreateDate,CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }



        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);

            return RedirectToAction("Index");
        }
    }
}