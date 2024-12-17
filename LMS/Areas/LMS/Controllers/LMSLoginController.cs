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

            return View();
        }

        public ActionResult SubmitForm(LoginModel omodel)
        {
            if(omodel.username is null || omodel.username =="" )
            {
                ViewBag.ValidateMessage = "Please enter User Name.";
                return View("Login");
            }
            else if (omodel.password is null || omodel.password == "")
            {
                ViewBag.ValidateMessage = "Please enter Password.";
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