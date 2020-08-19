using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using IITtechcom.Common;
using IITtechcom.Models.DAO;
using IITtechcom.Models.Entities;

namespace IITtechcom.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            // kiểm tra cookies admin còn tồn tại -> Trang admin
            if (Request.Cookies["AdminIIT"] != null && Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() > DateTime.Now)
            {
                return RedirectToAction("Index", "User", "Admin");
            }

            // kiểm tra cookies editor còn tồn tại -> Trang bài viết
            if (Request.Cookies["AdminIIT"] != null && Request.Cookies["AdminIIT"].Values["expires"].AsDateTime() > DateTime.Now)
            {
                return RedirectToAction("Index", "User", "Admin");
            }

            // chưa có cookies -> Trang login
            return View();
        }

        [HttpPost]
        public ActionResult Login(User us)
        {
            var dao = new UserDAO();

            var result = dao.Login(us.UserName, Encryptor.MD5Hash(us.Password));

            //you authorisation logic here
            if (result)
            {
                ////create the authentication ticket
                //var authTicket = new FormsAuthenticationTicket(
                //    1,
                //    us.UserName,  // user name
                //    DateTime.Now,
                //    DateTime.Now.AddMinutes(2),  // expiry
                //    remember,  // true to remember
                //    "Admin" // roles 

                //);

                var admin = dao.GetUser(us.UserName,Encryptor.MD5Hash(us.Password)).IsAdmin;
                var isAdmin = admin != null && (bool)admin;

                if (isAdmin)
                {
                    var cookieText = Encoding.UTF8.GetBytes(us.UserName);
                    var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));

                    //--- Create cookie object and pass name of the cookie and value to be stored.
                    HttpCookie cookieObject = new HttpCookie("AdminIIT", encryptedValue);

                    //---- Set expiry time of cookie.
                    cookieObject.Expires = DateTime.Now.AddDays(30);

                    //---- Add values
                    cookieObject.Values["admin"] = "true";
                    cookieObject.Values["expires"] = DateTime.Now.AddDays(30).ToString();

                    //---- Add cookie to cookie collection.
                    Response.Cookies.Add(cookieObject);

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    var cookieText = Encoding.UTF8.GetBytes(us.UserName);
                    var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));

                    //--- Create cookie object and pass name of the cookie and value to be stored.
                    HttpCookie cookieObject = new HttpCookie("AdminIIT", encryptedValue);

                    //---- Set expiry time of cookie.
                    cookieObject.Expires = DateTime.Now.AddDays(30);

                    //---- Add values
                    cookieObject.Values["admin"] = "false";
                    cookieObject.Values["expires"] = DateTime.Now.AddDays(30).ToString();

                    //---- Add cookie to cookie collection.
                    Response.Cookies.Add(cookieObject);

                    return RedirectToAction("Index", "Article");
                }

            }

            ViewBag.MessageError = "Tài khoản hoặc mật khẩu không chính xác!";
            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies["AdminIIT"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Login");
        }
    }
}