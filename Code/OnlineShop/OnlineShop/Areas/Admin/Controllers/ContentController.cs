using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreateBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Edit(Content model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var session = (UserLogin)Session[CommonConstants.USER_SESSION];
        //        model.CreateBy = session.UserName;
        //        var culture = Session[CommonConstants.CurrentCulture];
        //        model.Language = culture.ToString();
        //        new ContentDao().Edit(model);
        //        return RedirectToAction("Index");
        //    }
        //    SetViewBag(model.CategoryID);
        //    return View();
        //}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", content.CategoryID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,Description," +
            "Images,CategoryID,Detail,Warranty,CreateDate," +
            "CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount,Tags,Language")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", content.CategoryID);
            return View(content);
        }


        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
           
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);

            return RedirectToAction("Index");
        }
    }
}