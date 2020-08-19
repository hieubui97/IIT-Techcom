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
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
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

            var dao = new UserDAO();

            var model = dao.ListPaging(page, pageSize, searchString);

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

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var result = dao.AddUser(user);

                if (result)
                {
                    return RedirectToAction("Index", "User", "Admin");
                }
                else
                {
                    ViewBag.Error = "User name has already exsist!";
                    return View(user);
                }
            }

            return View(user);
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

            var dao = new UserDAO();

            var model = dao.GetUserById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var result = dao.UpdateUser(user);

                if (result)
                {
                    return RedirectToAction("Index", "User", "Admin");
                }
                else
                {
                    ViewBag.Error = "User name has already exsist!";
                    return View(user);
                }

            }

            return View(user);
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

            var dao = new UserDAO();
            var model = dao.GetUserById(id);

            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDAO();

            dao.DeleteUser(id);

            return RedirectToAction("Index", "User", "Admin");
        }
    }
}
