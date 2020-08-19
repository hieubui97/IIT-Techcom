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
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index(int? id)
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

            var dao = new MenuDAO();

            var model = dao.GetMenus(id);

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
            
            SetViewBags(null);

            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new  MenuDAO();
                dao.AddMenu(menu);

                return RedirectToAction("Index", "Menu");
            }

            SetViewBags(null, menu.ID);
            return View(menu);
        }

        public void SetViewBags(int? id, int? selectedId = null)
        {
            var dao = new  MenuDAO();
            var menus = dao.GeList(id);
            ViewBag.ParentID = new  SelectList(menus,"ID","Name", selectedId);
            ViewBag.Location = new SelectList(new []{"Header","Footer"});
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

            // Check cookies editor
            if (Request.Cookies["AdminIIT"].Values["admin"].Equals("false"))
            {
                return RedirectToAction("Index", "Article", "Admin");
            }

            var dao = new MenuDAO();

            dao.DeleteMenu(id);

            return RedirectToAction("Index", "User", "Admin");
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

            var dao = new MenuDAO();
            var menu = dao.GetMenu(id);
            SetViewBags(menu.ID,menu.ParentID);

            return View(menu);
        }

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                dao.UpdateMenu(menu);
                return RedirectToAction("Index", "Menu");
            }

            SetViewBags(menu.ID,menu.ParentID);
            return View(menu);
        }
    }
}