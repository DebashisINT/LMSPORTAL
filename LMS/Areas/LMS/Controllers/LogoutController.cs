using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.LMS.Controllers
{
    public class LogoutController : Controller
    {
        // GET: LMS/Logout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            //return Redirect("/OMS/Signoff.aspx");

            Session.Clear();

            Session.Abandon();

            return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });

            //return RedirectToAction("Login", "LMS/LMSLogin");

        }

        public ActionResult ChangePassword()
        {
            return Redirect("/OMS/Management/ToolsUtilities/frmchangepassword.aspx");
           
        }
    }
}