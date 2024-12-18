﻿using BusinessLogicLayer.MenuBLS;
using EntityLayer.MenuHelperELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public PartialViewResult _PartialMenu()
        {
            MenuBL menuBL = new MenuBL();
            Session["LMSusergoup"] = "1";
            Session["LMSEntryProfileType"] = "F";
            List<MenuListModel> ModelLsit = menuBL.GetUserMenuListByGroup();
            return PartialView(ModelLsit);
        }

        public ActionResult RedirectToLogin(string rurl)
        {
            return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });

            //string url = "/OMS/Login.aspx";

            //if (!string.IsNullOrWhiteSpace(rurl))
            //{
            //    url += "?rurl=" + rurl;
            //}

            //return Redirect(url);
        }
    }
}