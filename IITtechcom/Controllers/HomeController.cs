using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using Common;
using IITtechcom.Models.DAO;
using IITtechcom.Models.Entities;

namespace IITtechcom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dao = new CompanyDAO();

            var model = dao.GetInformation();

            return View(model);
        }

        public ActionResult Blogs(int? id, string searchString, int page = 1, int pageSize = 3)
        {
            var articleDao = new ArticleDAO();

            var model = articleDao.ListPagingHome(id, searchString, page, pageSize);

            var menuDao = new MenuDAO();

            ViewBag.Menus = menuDao.GetMenus(null).OrderBy(x => x.Level);
            ViewBag.Id = id;
            ViewBag.SearchString = searchString;

            return View(model);
        }

        public ActionResult Blog(int? id)
        {
            try
            {
                var dao = new ArticleDAO();

                var model = dao.GetArticle(id);

                var menuDao = new MenuDAO();

                ViewBag.Menus = menuDao.GetMenus(null).OrderBy(x => x.Level);

                return View(model);
            }
            catch (Exception )
            {
                return RedirectToAction("Blogs");
            }

        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ContactCaptcha", "Wrong Captcha!")]
        public ActionResult Contact(ContactCaptcha contactCaptcha)
        {
            Contact contact = new Contact();
            if (ModelState.IsValid)
            {
                contact.Name = contactCaptcha.Name;
                contact.Email = contactCaptcha.Email;
                contact.Address = contactCaptcha.Address;
                contact.Phone = contactCaptcha.Phone;
                contact.Request = contactCaptcha.Request;
                contact.CreateDate = DateTime.Now;

                var db = DataProvider.GetInstance();
                db.Contacts.Add(contact);
                db.SaveChanges();

                ViewBag.Success = "Success!";

                MvcCaptcha.ResetCaptcha("ContactCaptcha");

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/NewContact.html"));

                content = content.Replace("{{Name}}", contact.Name);
                content = content.Replace("{{Email}}", contact.Email);
                content = content.Replace("{{Phone}}", contact.Phone);
                content = content.Replace("{{Address}}", contact.Address);
                content = content.Replace("{{Request}}", contact.Request);
                content = content.Replace("{{CreateDate}}", contact.CreateDate.ToString());

                var mailHelper = new MailHelper();
                mailHelper.SendMail(contact.Email, "Send contact success", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                mailHelper.SendMail(toEmail, "New Contact", content);

                return View();
            }


            return View(contactCaptcha);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}