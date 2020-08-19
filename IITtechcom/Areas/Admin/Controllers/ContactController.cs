using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using IITtechcom.Models.DAO;

namespace IITtechcom.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
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

            var dao = new ContactDAO();

            var model = dao.ListPaging(page, pageSize, searchString);

            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new ContactDAO();

            dao.DeleteContact(id);

            return RedirectToAction("Index", "Contact", "Admin");
        }
    }
}