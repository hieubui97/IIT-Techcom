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
    public class CompanyIn4Controller : Controller
    {
        // GET: Admin/CompanyIn4
        public ActionResult Index()
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }

            var dao = new CompanyDAO();

            var model = dao.GetInformation();

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

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }


            var dao = new  CompanyDAO();
            var inf = dao.GetInformation();
            if (inf != null)
            {
                return RedirectToAction("Index", "CompanyIn4");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Information in4)
        {
            if (ModelState.IsValid)
            {
                var dao = new CompanyDAO();

                dao.AddCompanyIn4(in4);

                return RedirectToAction("Index", "CompanyIn4", "Admin");
            }

            return View(in4);
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            //validate

            //xử lý upload
            file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
            return "/Content/img/" + file.FileName;
        }

        public ActionResult Edit(int id)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }

            var dao = new CompanyDAO();

            var model = dao.GetInformation();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Information in4)
        {
            if (ModelState.IsValid)
            {
                var dao = new CompanyDAO();
                var result = dao.UpdateIn4(in4);

                if (result)
                {
                    return RedirectToAction("Index", "CompanyIn4");
                }

                return View(in4);
            }
            return View(in4);
        }

        public ActionResult Details(int id)
        {
            // Check cookies admin
            if (Request.Cookies["AdminIIT"] == null ||
                Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() < DateTime.Now)
            {
                return RedirectToAction("Login", "Account", "Admin");
            }

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }

            var dao = new CompanyDAO();

            var model = dao.GetInformation();

            return View(model);
        }
    }
}