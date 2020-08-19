using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using IITtechcom.Models.DAO;
using IITtechcom.Models.Entities;

namespace IITtechcom.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Index(int? id, string searchString, int page = 1, int pageSize = 2)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }

            var dao = new ArticleDAO();

            var model = dao.ListPaging(id, searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        public ActionResult Create()
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }


            SetViewBags();
            return View();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            //validate

            //xử lý upload
            file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
            return "/Content/img/" + file.FileName;
        }

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDAO();

                dao.AddArticle(article);

                return RedirectToAction("Index", "Article", "Admin");
            }

            SetViewBags();
            return View(article);
        }

        public ActionResult Edit(int id)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }


            var dao = new ArticleDAO();

            var model = dao.GetArticle(id);

            SetViewBags(model.MenuID);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDAO();

                dao.UpdateArticle(article);

                return RedirectToAction("Index", "Article", "Admin");
            }

            SetViewBags(article.ID);
            return View(article);
        }

        public ActionResult Details(int id)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }


            var dao = new ArticleDAO();
            var model = dao.GetArticle(id);

            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }


            var dao = new ArticleDAO();

            dao.DeleteArticle(id);

            return RedirectToAction("Index", "Article", "Admin");
        }

        public void SetViewBags(int? selectedId = null)
        {
            var dao = new MenuDAO();
            var menus = dao.GeList2();
            ViewBag.MenuID = new SelectList(menus, "ID", "Name", selectedId);
        }
    }
}