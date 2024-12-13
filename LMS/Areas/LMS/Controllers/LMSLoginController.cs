using BusinessLogicLayer;
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
            return View("Login");
        }

        public ActionResult SubmitForm(LoginModel omodel)
        {

            Encryption epasswrd = new Encryption();
            string Encryptpass = epasswrd.Encrypt(omodel.password.Trim());

            string Validuser;
            Validuser = oDBEngine.AuthenticateUser(omodel.username, Encryptpass).ToString();
            if (Validuser == "Y")
            {
                return RedirectToAction("FSMDashboard", "DashboardMenu");
            }

            else
            {
                return View();
            }
        }
    }
}