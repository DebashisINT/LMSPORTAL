using BusinessLogicLayer;
using DevExpress.Map.Kml;
using DevExpress.Xpo.Logger;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSLoginController : Controller
    {
        // GET: LMS/LMSLogin
        LoginModel model = new LoginModel();
        DBEngine oDBEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);


        public ActionResult Login()
        {
            ViewBag.ApplicationVersion = oDBEngine.GetApplicationVersion();
            ViewBag.ValidateMessage = "";

            if (Session["userid"] != null)
            {

                if (Request.Cookies["ERPACTIVEURL"] != null && Convert.ToString(Request.Cookies["ERPACTIVEURL"].Value) == "1")
                {
                    Response.Redirect("/OMS/MultiTabError.aspx", true);
                }
                 
                Session["DeveloperRedirect"] = "Yes";

                HttpCookie ERPACTIVEURL = new HttpCookie("ERPACTIVEURL");
                ERPACTIVEURL.Value = "1";
                Response.Cookies.Add(ERPACTIVEURL);

            }

            return View();
        }

        public ActionResult SubmitForm(LoginModel omodel)
        {
            ViewBag.ValidateMessage = "";

            if ((omodel.username is null || omodel.username == "") && (omodel.password is null || omodel.password == ""))
            {
                ViewBag.ValidateMessage = "Please enter User Name and Password";
                return View("Login");
            }
            else if (omodel.username is null || omodel.username =="" )
            {
                ViewBag.ValidateMessage = "Please enter User Name";
                return View("Login");
            }
            else if (omodel.password is null || omodel.password == "")
            {
                ViewBag.ValidateMessage = "Please enter Password";
                return View("Login");
            }
            
            else
            {
                Encryption epasswrd = new Encryption();
                string Encryptpass = epasswrd.Encrypt(omodel.password.Trim());

                string Validuser;
                Validuser = oDBEngine.AuthenticateUser(omodel.username, Encryptpass).ToString();
                if (Validuser == "Y")
                {
                    HttpCookie cookie = new HttpCookie("sKey");
                    cookie.Value = omodel.username;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);

                    HttpCookie ERPACTIVEURL = new HttpCookie("ERPACTIVEURL");
                    ERPACTIVEURL.Value = "1";
                    Response.Cookies.Add(ERPACTIVEURL);


                    return RedirectToAction("LMSDashboard", "DashboardMenu");
                }

                else
                {
                    ViewBag.ValidateMessage = Validuser;
                    return View("Login");
                }
            }
            
        }
    }
}