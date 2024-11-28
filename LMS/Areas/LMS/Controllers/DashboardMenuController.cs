using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
//using LMS.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using System.Runtime.InteropServices.ComTypes;
//using DocumentFormat.OpenXml.EMMA;
//using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;


namespace LMS.Areas.LMS.Controllers
{
    public partial class DashboardMenuController : Controller
    {
        Dashboard dashbrd = new Dashboard();
        DataTable dtdashboard = new DataTable();
        DBDashboardSettings dashboardsetting = new DBDashboardSettings();

        public ActionResult Dashboard()
        {
            try
            {
                DashboardModelC model = new DashboardModelC();
                string userid = Session["userid"].ToString();

                string todaydate = DateTime.Now.ToString("yyyy-MM-dd");
                DataTable dtdashboard = dashbrd.GetFtsDashboardyList(todaydate, userid);

                model.employeecount = "0";
                model.employeeatwork = "0";
                model.employeeonleave = "0";
                model.employeenotlogin = "0";
                model.Userid = userid;

                BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
                DataTable dt = objEngine.GetDataTable("FTSDASHBOARD_REPORT", " ACTION,EMPCNT,AT_WORK,ON_LEAVE,NOT_LOGIN ", " USERID='" + userid + "' AND RPTTYPE='Summary' AND ACTION in ('EMP','AT_WORK','NOT_LOGIN','ON_LEAVE')");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string strString = Convert.ToString(row["ACTION"]);
                        if (strString == "EMP") model.employeecount = Convert.ToString(row["EMPCNT"]);
                        else if (strString == "AT_WORK") model.employeeatwork = Convert.ToString(row["AT_WORK"]);
                        else if (strString == "ON_LEAVE") model.employeeonleave = Convert.ToString(row["ON_LEAVE"]);
                        else if (strString == "NOT_LOGIN") model.employeenotlogin = Convert.ToString(row["NOT_LOGIN"]);


                    }
                }

                return View(model);





            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult FSMDashboard()
        {
            TempData["LMSDashboardGridView"] = null;
            if (Session["userid"] != null)
            {
                FSMDashBoardFilter obj = new FSMDashBoardFilter();
                try
                {
                    string userid = Session["userid"].ToString();
                    DataSet dsdashboard = dashboardsetting.GetDashboardSettingMappedListByID(Convert.ToInt32(userid));
                    DataTable dt = dsdashboard.Tables[0];
                    List<DashboardSettingMapped> list = new List<DashboardSettingMapped>();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DashboardSettingMapped objmap = null;
                            foreach (DataRow row in dt.Rows)
                            {
                                objmap = new DashboardSettingMapped();
                                objmap.DashboardSettingMappedID = Convert.ToInt32(row["DashboardSettingMappedID"]);
                                objmap.FKDashboardSettingID = Convert.ToInt32(row["FKDashboardSettingID"]);
                                objmap.FKuser_id = Convert.ToInt32(row["FKuser_id"]);
                                objmap.FKDashboardDetailsID = Convert.ToInt32(row["FKDashboardDetailsID"]);
                                objmap.DetailsName = Convert.ToString(row["DetailsName"]);
                                list.Add(objmap);
                            }
                        }
                    }
                    obj.DashboardSettingMappedList = list;
                }
                catch { }

                //Mantis Issue 24729
                Session["PageloadChk"] = "1";
                //End of Mantis Issue 24729
                return View(obj);

            }
            else
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }


        }

        public ActionResult Dashboard1()
        {

            return View();
        }
        // Mantis Issue 24729
        //public JsonResult GetDashboardData(string stateid)
        //public JsonResult GetDashboardData(string stateid, string branchid)
        //// End of Mantis Issue 24729
        //{


        //    Dashboard dashboarddataobj = new Dashboard();
        //    FSMDashboard Dashboarddata = new FSMDashboard();
        //    try
        //    {
        //        // Mantis Issue 24729
        //        //DataSet objData = dashboarddataobj.CreateLINQforDashBoard(stateid);
        //        DataSet objData = dashboarddataobj.CreateLINQforDashBoard(stateid, branchid);
        //        // End of Mantis Issue 24729
        //        //DataTable objData = dashboarddataobj.GetDashboardAttendanceData();

        //        int NOT_LOGIN = 0;
        //        int AT_WORK = 0;
        //        int ON_LEAVE = 0;
        //        int Total = 0;

        //        string TOTALSHOP = "0";
        //        string REVISIT = "0";
        //        string NEWSHOPVISIT = "0";

        //        string AVGSHOPVISIT = "0";
        //        string AVGDURATION = "00:00";
        //        string TODAYSALES = "0.00";
        //        string AVGSALES = "0.00";
        //        string TOTALSALES = "0.00";

        //        foreach (DataRow item in objData.Tables[0].Rows)
        //        {
        //            if (Convert.ToString(item["ACTION"]) == "NOT_LOGIN")
        //            {
        //                NOT_LOGIN = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "AT_WORK")
        //            {
        //                AT_WORK = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "ON_LEAVE")
        //            {
        //                ON_LEAVE = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "EMP")
        //            {
        //                Total = Convert.ToInt32(item["Count"]);

        //            }

        //        }
        //        Dashboarddata.lblAtWork = AT_WORK;
        //        Dashboarddata.lblOnLeave = ON_LEAVE;
        //        Dashboarddata.lblNotLoggedIn = NOT_LOGIN;
        //        Dashboarddata.lblTotal = Total;

        //        Dashboarddata.NewVisit = Convert.ToInt32(NEWSHOPVISIT);
        //        Dashboarddata.ReVisit = Convert.ToInt32(REVISIT);
        //        Dashboarddata.TotalVisit = Convert.ToInt32(TOTALSHOP);
        //        Dashboarddata.AvgPerDay = Convert.ToDecimal(AVGSHOPVISIT);
        //        Dashboarddata.AvgDurationPerShop = AVGDURATION;
        //        Dashboarddata.AVGSALES = AVGSALES;
        //        Dashboarddata.TODAYSALES = TODAYSALES;
        //        Dashboarddata.TOTALSALES = TOTALSALES;
        //    }
        //    catch
        //    {
        //    }
        //    return Json(Dashboarddata);
        //}
        //// Team Visit
        //public JsonResult GetDashboardDataVisit(string stateid, string branchid)
        //{
        //    Dashboard dashboarddataobj = new Dashboard();
        //    FSMDashboard Dashboarddata = new FSMDashboard();
        //    try
        //    {
        //        DataSet objData = dashboarddataobj.CreateLINQforDashBoardTeamVisit(stateid, branchid);

        //        int NOT_LOGIN = 0;
        //        int AT_WORK = 0;
        //        int ON_LEAVE = 0;
        //        int Total = 0;

        //        string TOTALSHOP = "0";
        //        string REVISIT = "0";
        //        string NEWSHOPVISIT = "0";

        //        string AVGSHOPVISIT = "0";
        //        string AVGDURATION = "00:00";
        //        string TODAYSALES = "0.00";
        //        string AVGSALES = "0.00";
        //        string TOTALSALES = "0.00";

        //        foreach (DataRow item in objData.Tables[0].Rows)
        //        {
        //            if (Convert.ToString(item["ACTION"]) == "NOT_LOGIN")
        //            {
        //                NOT_LOGIN = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "AT_WORK")
        //            {
        //                AT_WORK = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "ON_LEAVE")
        //            {
        //                ON_LEAVE = Convert.ToInt32(item["Count"]);
        //            }
        //            if (Convert.ToString(item["ACTION"]) == "EMP")
        //            {
        //                Total = Convert.ToInt32(item["Count"]);

        //            }

        //        }

        //        Dashboarddata.lblAtWork = AT_WORK;
        //        Dashboarddata.lblOnLeave = ON_LEAVE;
        //        Dashboarddata.lblNotLoggedIn = NOT_LOGIN;
        //        Dashboarddata.lblTotal = Total;

        //        Dashboarddata.NewVisit = Convert.ToInt32(NEWSHOPVISIT);
        //        Dashboarddata.ReVisit = Convert.ToInt32(REVISIT);
        //        Dashboarddata.TotalVisit = Convert.ToInt32(TOTALSHOP);
        //        Dashboarddata.AvgPerDay = Convert.ToDecimal(AVGSHOPVISIT);
        //        Dashboarddata.AvgDurationPerShop = AVGDURATION;
        //        Dashboarddata.AVGSALES = AVGSALES;
        //        Dashboarddata.TODAYSALES = TODAYSALES;
        //        Dashboarddata.TOTALSALES = TOTALSALES;
        //    }
        //    catch
        //    {
        //    }
        //    return Json(Dashboarddata);
        //}

        //public PartialViewResult DashboardGridView(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    SalesSummary_Report objgps = new SalesSummary_Report();
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();
        //    string query = "";
        //    string orderby = "";

        //    orderby = " order by EMPNAME";

        //    if (dd.Type == "Attendance")
        //    {
        //        string ColumnName = "*";

        //        // Mantis Issue 25434 [ column BRANCH [Branch] added ]
        //        if (dd.FilterName == "AT_WORK")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],ISNULL(CONTACTNO,'') [Mobile No.],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID";

        //            orderby = " order by SHOPS_VISITED DESC";
        //        }
        //        else if (dd.FilterName == "NOT_LOGIN")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],ISNULL(STATE,'') [State],REPORTTO [Supervisor],CONTACTNO [Contact No.]";

        //        }
        //        else if (dd.FilterName == "ON_LEAVE")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],LEAVEDATE [Applied Leave Date]";
        //        }
        //        else if (dd.FilterName == "EMP")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],BRANCH [Branch],DEPARTMENT [Department],REPORTTO [Supervisor],CONTACTNO [Contact No.]";
        //        }

        //        //string StateId = dd.statefilterid == "0" ? "" : dd.statefilterid;

        //        // Mantis Issue 24765
        //        //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
        //        DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId == "0" ? "" : dd.BranchId);
        //        // End of Mantis Issue 24765

        //        query = "Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

        //        //if (dd.statefilterid != null)
        //        //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";
        //        //else
        //        //    query += " AND ISNULL(STATE,'')=''";

        //        //if (Convert.ToString(dd.designation) != "" && dd.designation != null)
        //        //    query += " AND DESIGNATION='" + Convert.ToString(dd.designation) + "'";

        //        //if (Convert.ToString(dd.statefilterid) != "" && dd.statefilterid != null)
        //        //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";

        //        query += orderby;

        //        dbDashboardData = objdb.GetDataTable(query);
        //    }
        //    // Rev Tanmoy store query in tempdata
        //    // TempData["DashboardGridView"] = dbDashboardData;
        //    TempData["DashboardGridView"] = query;
        //    //End Rev 
        //    // DataTable MyInstrumentsList = null;
        //    return PartialView(dbDashboardData);
        //}

        //public PartialViewResult DashboardGridViewFV(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    SalesSummary_Report objgps = new SalesSummary_Report();
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();
        //    string query = "";
        //    string orderby = "";

        //    orderby = " order by EMPNAME";

        //    if (dd.Type == "Attendance")
        //    {
        //        string ColumnName = "*";


        //        if (dd.FilterName == "AT_WORK")
        //        {
        //            // Rev 3.0
        //            //ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID";
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(ITCORDER_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],EMPCODE EMPID ";
        //            // End of Rev 3.0

        //            orderby = " order by SHOPS_VISITED DESC";
        //        }
        //        else if (dd.FilterName == "NOT_LOGIN")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],ISNULL(STATE,'') [State],REPORTTO [Supervisor],REPORTTOUID [Supervisor ID],CONTACTNO [Login ID]";

        //        }
        //        else if (dd.FilterName == "ON_LEAVE")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],LEAVEDATE [Applied Leave Date]";
        //        }
        //        else if (dd.FilterName == "EMP")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCH [Branch],REPORTTO [Supervisor],REPORTTOUID [Supervisor ID],CONTACTNO [Login ID]";
        //        }

        //        //string StateId = dd.statefilterid == "0" ? "" : dd.statefilterid;

        //        // Mantis Issue 24765
        //        //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
        //        DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId == "0" ? "" : dd.BranchId);
        //        // End of Mantis Issue 24765

        //        query = "Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

        //        //if (dd.statefilterid != null)
        //        //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";
        //        //else
        //        //    query += " AND ISNULL(STATE,'')=''";

        //        //if (Convert.ToString(dd.designation) != "" && dd.designation != null)
        //        //    query += " AND DESIGNATION='" + Convert.ToString(dd.designation) + "'";

        //        //if (Convert.ToString(dd.statefilterid) != "" && dd.statefilterid != null)
        //        //    query += " AND STATE='" + Convert.ToString(dd.statefilterid) + "'";

        //        query += orderby;

        //        dbDashboardData = objdb.GetDataTable(query);
        //    }
        //    // Rev Tanmoy store query in tempdata
        //    // TempData["DashboardGridView"] = dbDashboardData;
        //    TempData["DashboardGridViewFV"] = query;
        //    //End Rev 
        //    // DataTable MyInstrumentsList = null;
        //    return PartialView(dbDashboardData);
        //}
        //// Team Visit
        //public PartialViewResult DashboardGridViewTeam(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    SalesSummary_Report objgps = new SalesSummary_Report();
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();
        //    string query = "";
        //    string orderby = "";

        //    orderby = " order by EMPNAME";

        //    if (dd.Type == "Attendance")
        //    {
        //        string ColumnName = "*";


        //        if (dd.FilterName == "AT_WORK")
        //        {
        //            // Rev 3.0
        //            //ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],CHANNEL [Channel],CIRCLE [Circle],SECTION [Section], EMPCODE EMPID";
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(CONTACTNO,'') [Login ID],LOGGEDIN [First in time],CASE WHEN LOGEDOUT='--' THEN '' ELSE LOGEDOUT END [Last logout time],CURRENT_STATUS [Current Status],ISNULL(GPS_INACTIVE_DURATION,'00:00') +' (HH:MM)' [GPS Inactivity],ISNULL(SHOPS_VISITED,0) [Shops Visited],ISNULL(TOTAL_ORDER_BOOKED_VALUE,'0.00') [Sales Value],ISNULL(ITCORDER_VALUE,'0.00') [Order Value],ISNULL(TOTAL_COLLECTION,'0.00') [Collection Amt.],CHANNEL [Channel],CIRCLE [Circle],SECTION [Section], EMPCODE EMPID";
        //            // End of Rev 3.0

        //            orderby = " order by SHOPS_VISITED DESC";
        //        }
        //        else if (dd.FilterName == "NOT_LOGIN")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],ISNULL(STATE,'') [State],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID], CONTACTNO [Login ID]";

        //        }
        //        else if (dd.FilterName == "ON_LEAVE")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],LEAVEDATE [Applied Leave Date]";
        //        }
        //        else if (dd.FilterName == "EMP")
        //        {
        //            ColumnName = "EMPNAME [Employee],DESIGNATION [Designation],EMPID [Employee ID],BRANCHDESC [Branch],REPORTTO [Supervisor], REPORTTOUID [Supervisor ID],CONTACTNO [Login ID], CHANNEL [Channel],CIRCLE [Circle],SECTION [Section]";
        //        }

        //        // mantis issue 25567 
        //        //DataTable dt = objgps._GetSalesSummaryReportTeam(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, dd.BranchIdTV, "", "", dd.FilterName);
        //        DataTable dt = objgps._GetSalesSummaryReportTeam(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, dd.BranchIdTV == "0" ? "" : dd.BranchIdTV, "", "", dd.FilterName);
        //        // End of mantis issue 25567

        //        query = "Select " + ColumnName + " from FTSTEAMVISITDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'";

        //        query += orderby;

        //        dbDashboardData = objdb.GetDataTable(query);
        //    }
        //    // Rev Tanmoy store query in tempdata
        //    // TempData["DashboardGridView"] = dbDashboardData;
        //    TempData["DashboardGridViewTeam"] = query;
        //    //End Rev 
        //    // DataTable MyInstrumentsList = null;
        //    return PartialView(dbDashboardData);
        //}


        //public PartialViewResult DashboardGridViewDetailsTeamVisit(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    DataTable dbDashboardData = new DataTable();
        //    dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
        //    TempData["DashboardGridViewDetailsTeamVisit"] = dbDashboardData;
        //    return PartialView(dbDashboardData);
        //}
        ////Team visit End

        //public PartialViewResult DashboardSummaryGridView(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    SalesSummary_Report objgps = new SalesSummary_Report();
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();

        //    if (dd.Type == "Attendance")
        //    {
        //        string ColumnName = "*";

        //        ColumnName = "STATE [State],DESIGNATION [Designation],COUNT(*) Count";


        //        if (dd.FilterName != "AT_WORK")
        //        {
        //            // Mantis Issue 24765
        //            //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
        //            DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId);
        //            // End o f Mantis Issue 24765

        //            if (dd.StateId != null && dd.StateId != "0")
        //            {
        //                dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "' GROUP BY DESIGNATION,STATE order by STATE,COUNT(*) DESC");
        //            }
        //            else
        //            {
        //                dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'  GROUP BY DESIGNATION,STATE order by STATE");
        //            }
        //        }
        //        else
        //        {
        //            DataTable dt = dashbrd.GetAtWorkSummary(dd.StateId);
        //            dbDashboardData = objdb.GetDataTable("Select STATE [State],DESIGNATION [Designation],EMPCNT [Count] from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'");
        //        }

        //        ViewBag.Type = "Attendance";

        //    }
        //    else if (dd.Type == "Tracking")
        //    {
        //        dbDashboardData = dashbrd.GetLiveVisits(dd.StateId);
        //        ViewBag.Type = "Tracking";
        //    }
        //    if (dbDashboardData.Rows.Count > 0)
        //    {
        //        TempData["ExportDashboardSummaryGridList"] = dbDashboardData;
        //        TempData.Keep();
        //        ViewBag.ExportDashboardSummaryGridListCount = dbDashboardData.Rows.Count;
        //    }
        //    else
        //    {

        //        ViewBag.ExportDashboardSummaryGridListCount = "0";
        //    }

        //    // DataTable MyInstrumentsList = null;
        //    return PartialView(dbDashboardData);
        //}

        //public PartialViewResult DashboardSummaryGridViewFV(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    SalesSummary_Report objgps = new SalesSummary_Report();
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();

        //    if (dd.Type == "Attendance")
        //    {
        //        string ColumnName = "*";

        //        ColumnName = "STATE [State],DESIGNATION [Designation],COUNT(*) Count";


        //        if (dd.FilterName != "AT_WORK")
        //        {
        //            // Mantis Issue 24765
        //            //DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName);
        //            DataTable dt = objgps._GetSalesSummaryReport(DateTime.Now.ToString("yyyy-MM-dd"), Convert.ToString(Session["userid"]), dd.StateId == "0" ? "" : dd.StateId, "", "", dd.FilterName, dd.BranchId);
        //            // End o f Mantis Issue 24765

        //            if (dd.StateId != null && dd.StateId != "0")
        //            {
        //                dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "' GROUP BY DESIGNATION,STATE order by STATE,COUNT(*) DESC");
        //            }
        //            else
        //            {
        //                dbDashboardData = objdb.GetDataTable("Select " + ColumnName + " from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'  GROUP BY DESIGNATION,STATE order by STATE");
        //            }
        //        }
        //        else
        //        {
        //            DataTable dt = dashbrd.GetAtWorkSummary(dd.StateId);
        //            dbDashboardData = objdb.GetDataTable("Select STATE [State],DESIGNATION [Designation],EMPCNT [Count] from FTSDASHBOARD_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(dd.FilterName) + "'");
        //        }

        //        ViewBag.Type = "Attendance";

        //    }
        //    else if (dd.Type == "Tracking")
        //    {
        //        dbDashboardData = dashbrd.GetLiveVisits(dd.StateId);
        //        ViewBag.Type = "Tracking";
        //    }
        //    if (dbDashboardData.Rows.Count > 0)
        //    {
        //        TempData["ExportDashboardSummaryGridListFV"] = dbDashboardData;
        //        TempData.Keep();
        //        ViewBag.ExportDashboardSummaryGridListCount = dbDashboardData.Rows.Count;
        //    }
        //    else
        //    {

        //        ViewBag.ExportDashboardSummaryGridListCount = "0";
        //    }

        //    // DataTable MyInstrumentsList = null;
        //    return PartialView(dbDashboardData);
        //}
        //public PartialViewResult DashboardGridViewDetails(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    DataTable dbDashboardData = new DataTable();
        //    dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
        //    TempData["DashboardGridViewDetails"] = dbDashboardData;
        //    return PartialView(dbDashboardData);
        //}

        //public PartialViewResult DashboardGridViewDetailsFV(FSMDashBoardFilter dd)
        //{
        //    ViewBag.WindowSize = dd.WindowSize;
        //    DataTable dbDashboardData = new DataTable();
        //    dbDashboardData = dashbrd.GetSalesManVisitDetails(dd.EMPCODE);
        //    TempData["DashboardGridViewDetailsFV"] = dbDashboardData;
        //    return PartialView(dbDashboardData);
        //}
        //public PartialViewResult DashboardGridViewSalesmanDetail(string ID, string action, string rptype, string empid, string stateid, string designid)
        //{
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();
        //    try
        //    {
        //        dbDashboardData = dashbrd.GetDashboardGridData(action, rptype, empid, stateid, designid);
        //        ViewBag.Type = ID;
        //        if (ID == "1")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT CAST(DEG_ID as varchar(50)) DEGID,DESIGNATION [Designation],STATE [State],VISITCNT [Visit Count],EMPCNT [Employee Count], (VISITCNT / EMPCNT) [Avg Count] FROm FTSDASHBOARDGRIDDETAILS_REPORT WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITCNT DESC");
        //        }
        //        else if (ID == "2")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,EMPNAME [Employee Name],STATE [State],DESIGNATION [Designation],SHOPASSIGN [Assigned Shops],DISTANCE_COVERED [KM Travelled],SHOPS_VISITED [Today's Visit],LAST7DAYVISIT [Visit-Last 7 Days],PENDINGVISIT7DAYS [Pending-Last 7 Days] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' order by SHOPS_VISITED DESC");
        //        }
        //        else if (ID == "3")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,SHOP_NAME [Shop Name],SHOP_TYPE [Shop Type],SHOPLOCATION [Location],SHOPCONTACT [Mobile No.],VISITED_TIME [Visit Time],SPENT_DURATION [Duration Spent],VISIT_TYPE [Visit Type] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITED_TIME");
        //        }
        //    }
        //    catch { }
        //    TempData["DashboardGridViewSalesmanDetail"] = dbDashboardData;
        //    TempData["DashboardGridViewSalesmanDetailType"] = ViewBag.Type;
        //    TempData.Keep();
        //    return PartialView(dbDashboardData);
        //}
        //public PartialViewResult DashboardGridViewSalesmanDetailFV(string ID, string action, string rptype, string empid, string stateid, string designid)
        //{
        //    DBEngine objdb = new DBEngine();
        //    DataTable dbDashboardData = new DataTable();
        //    try
        //    {
        //        dbDashboardData = dashbrd.GetDashboardGridData(action, rptype, empid, stateid, designid);
        //        ViewBag.Type = ID;
        //        if (ID == "1")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT CAST(DEG_ID as varchar(50)) DEGID,DESIGNATION [Designation],STATE [State],VISITCNT [Visit Count],EMPCNT [Employee Count], (VISITCNT / EMPCNT) [Avg Count] FROm FTSDASHBOARDGRIDDETAILS_REPORT WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITCNT DESC");
        //        }
        //        else if (ID == "2")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,EMPNAME [Employee Name],STATE [State],DESIGNATION [Designation],SHOPASSIGN [Assigned Shops],DISTANCE_COVERED [KM Travelled],SHOPS_VISITED [Today's Visit],LAST7DAYVISIT [Visit-Last 7 Days],PENDINGVISIT7DAYS [Pending-Last 7 Days] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' order by SHOPS_VISITED DESC");
        //        }
        //        else if (ID == "3")
        //        {
        //            dbDashboardData = objdb.GetDataTable("SELECT EMPCODE EMPID,SHOP_NAME [Shop Name],SHOP_TYPE [Shop Type],SHOPLOCATION [Location],SHOPCONTACT [Mobile No.],VISITED_TIME [Visit Time],SPENT_DURATION [Duration Spent],VISIT_TYPE [Visit Type] FROM FTSDASHBOARDGRIDDETAILS_REPORT  WHERE USERID='" + Convert.ToString(Session["userid"]) + "' and ACTION='" + Convert.ToString(action) + "' and RPTTYPE='" + rptype + "' ORDER BY VISITED_TIME");
        //        }
        //    }
        //    catch { }
        //    TempData["DashboardGridViewSalesmanDetail"] = dbDashboardData;
        //    TempData["DashboardGridViewSalesmanDetailType"] = ViewBag.Type;
        //    TempData.Keep();
        //    return PartialView(dbDashboardData);
        //}

        //public ActionResult DashboardStateComboboxFV()
        //{

        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    //string userid = "0";
        //    List<StateData> statedate = new List<StateData>();
        //    List<StateData> statedateobj = new List<StateData>();
        //    try
        //    {
        //        StateData obj = null;
        //        statedate = dashboard.GetStateList(Convert.ToInt32(userid));
        //        foreach (var item in statedate)
        //        {
        //            obj = new StateData();
        //            obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
        //            obj.name = item.name;
        //            statedateobj.Add(obj);
        //        }
        //    }
        //    catch { }
        //    ViewBag.StateListCount = statedate.Count;
        //    // Rev 1.0
        //    TempData["statedate"] = statedate;
        //    TempData["statedateobj"] = statedateobj;
        //    // End of Rev 1.0

        //    return PartialView(statedateobj);
        //}
        //public ActionResult DashboardStateCombobox()
        //{

        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    //string userid = "0";
        //    List<StateData> statedate = new List<StateData>();
        //    List<StateData> statedateobj = new List<StateData>();

        //    // Rev 1.0
        //    if (TempData["statedateobj"] == null)
        //    {
        //        // End of Rev 1.0
        //        try
        //        {
        //            StateData obj = null;
        //            statedate = dashboard.GetStateList(Convert.ToInt32(userid));
        //            foreach (var item in statedate)
        //            {
        //                obj = new StateData();
        //                obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
        //                obj.name = item.name;
        //                statedateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.StateListCount = statedate.Count;
        //        // Rev 1.0
        //    }
        //    else
        //    {
        //        statedate = (List<StateData>)TempData["statedate"];
        //        statedateobj = (List<StateData>)TempData["statedateobj"];

        //        ViewBag.StateListCount = statedate.Count;
        //    }
        //    // End of Rev 1.0

        //    return PartialView(statedateobj);
        //}
        //public ActionResult DashboardBranchComboboxFV(string stateid)
        //{

        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    int chkState = 0;
        //    if (stateid == null)
        //    {
        //        chkState = 1;
        //    }

        //    List<BranchData> branchdate = new List<BranchData>();
        //    List<BranchData> branchdateobj = new List<BranchData>();

        //    string stateIds = dashboard.StateId;
        //    try
        //    {
        //        BranchData obj = null;
        //        if (stateid == null)
        //        {
        //            stateid = "";
        //        }

        //        branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
        //        foreach (var item in branchdate)
        //        {
        //            obj = new BranchData();
        //            obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
        //            obj.name = item.name;
        //            branchdateobj.Add(obj);
        //        }
        //    }
        //    catch { }
        //    ViewBag.BranchListCount = branchdate.Count;
        //    // Rev 1.0
        //    TempData["branchdate"] = branchdate;
        //    TempData["branchdateobj"] = branchdateobj;
        //    // End of Rev 1.0

        //    if (chkState == 1)
        //    {
        //        return PartialView("DashboardBranchComboboxFV", branchdateobj);
        //    }
        //    else
        //    {
        //        Session["PageloadChk"] = "0";
        //        Session["BranchList"] = branchdateobj;
        //        return Json(branchdate, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //// Mantis Issue 24729
        //public ActionResult DashboardBranchCombobox(string stateid)
        //{
        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    int chkState = 0;
        //    if (stateid == null)
        //    {
        //        chkState = 1;
        //    }
        //    List<BranchData> branchdate = new List<BranchData>();
        //    List<BranchData> branchdateobj = new List<BranchData>();

        //    // Rev 1.0
        //    if (TempData["branchdateobj"] == null)
        //    {
        //        // End of Rev 1.0
        //        string stateIds = dashboard.StateId;
        //        try
        //        {
        //            BranchData obj = null;
        //            if (stateid == null)
        //            {
        //                stateid = "";
        //            }

        //            branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);

        //            foreach (var item in branchdate)
        //            {
        //                obj = new BranchData();
        //                obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
        //                obj.name = item.name;
        //                branchdateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.BranchListCount = branchdate.Count;
        //        // Rev 1.0
        //    }
        //    else
        //    {
        //        branchdate = (List<BranchData>)TempData["branchdate"];
        //        branchdateobj = (List<BranchData>)TempData["branchdateobj"];

        //        ViewBag.BranchListCount = branchdate.Count;
        //    }
        //    // End of Rev 1.0

        //    if (chkState == 1)
        //    {
        //        return PartialView("DashboardBranchCombobox", branchdateobj);
        //    }
        //    else
        //    {
        //        Session["PageloadChk"] = "0";
        //        Session["BranchList"] = branchdateobj;
        //        return Json(branchdate, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //// End of Mantis Issue 24729
        //// bRANCH TV
        //public ActionResult DashboardBranchComboboxTV(string stateid)
        //{

        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    int chkState = 0;
        //    if (stateid == null)
        //    {
        //        chkState = 1;
        //    }

        //    List<BranchData> branchdate = new List<BranchData>();
        //    List<BranchData> branchdateobj = new List<BranchData>();

        //    // Rev 1.0
        //    if (TempData["branchdateobj"] == null)
        //    {
        //        // End of Rev 1.0
        //        string stateIds = dashboard.StateId;
        //        try
        //        {
        //            BranchData obj = null;
        //            if (stateid == null)
        //            {
        //                stateid = "";
        //            }

        //            // Rev 1.0
        //            //branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
        //            if (TempData["branchdate"] != null)
        //            {
        //                branchdate = (List<BranchData>)TempData["branchdate"];
        //            }
        //            else
        //            {
        //                branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
        //            }
        //            // End of Rev 1.0

        //            foreach (var item in branchdate)
        //            {
        //                obj = new BranchData();
        //                obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
        //                obj.name = item.name;
        //                branchdateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.BranchListCount = branchdate.Count;
        //        // Rev 1.0
        //    }
        //    else
        //    {
        //        branchdate = (List<BranchData>)TempData["branchdate"];
        //        branchdateobj = (List<BranchData>)TempData["branchdateobj"];

        //        ViewBag.BranchListCount = branchdate.Count;
        //    }
        //    // End of Rev 1.0

        //    if (chkState == 1)
        //    {
        //        return PartialView("DashboardBranchComboboxTV", branchdateobj);
        //    }
        //    else
        //    {
        //        Session["PageloadChk"] = "0";
        //        Session["BranchList"] = branchdateobj;
        //        return Json(branchdate, JsonRequestBehavior.AllowGet);
        //    }
        //}


        ////
        //public ActionResult DashboardStateComboboxTV()
        //{
        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    //string userid = "0";
        //    List<StateData> statedate = new List<StateData>();
        //    List<StateData> statedateobj = new List<StateData>();

        //    // Rev 1.0
        //    if (TempData["statedateobj"] == null)
        //    {
        //        // End of Rev 1.0
        //        try
        //        {
        //            StateData obj = null;
        //            statedate = dashboard.GetStateList(Convert.ToInt32(userid));
        //            foreach (var item in statedate)
        //            {
        //                obj = new StateData();
        //                obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
        //                obj.name = item.name;
        //                statedateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.StateListCount = statedate.Count;

        //        // Rev 1.0
        //    }
        //    else
        //    {
        //        statedate = (List<StateData>)TempData["statedate"];
        //        statedateobj = (List<StateData>)TempData["statedateobj"];

        //        ViewBag.StateListCount = statedate.Count;
        //    }
        //    // End of Rev 1.0
        //    return PartialView(statedateobj);
        //}
        //public ActionResult DashboardAttendance(List<DashboardSettingMapped> list)
        //{

        //    return PartialView(list);
        //}
        //public ActionResult DashboardAttendanceFV(List<DashboardSettingMapped> list)
        //{

        //    return PartialView(list);
        //}

        //public ActionResult DashboardAttendanceTV(List<DashboardSettingMapped> list)
        //{

        //    return PartialView(list);
        //}
        //public ActionResult leaveListView(List<DashboardSettingMapped> list)
        //{

        //    return PartialView(list);
        //}

        //// Rev Sanchita
        //public ActionResult DashboardStateComboboxOrder()
        //{
        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    //string userid = "0";
        //    List<StateData> statedate = new List<StateData>();
        //    List<StateData> statedateobj = new List<StateData>();

        //    if (TempData["statedateobj"] == null)
        //    {
        //        try
        //        {
        //            StateData obj = null;
        //            statedate = dashboard.GetStateList(Convert.ToInt32(userid));
        //            foreach (var item in statedate)
        //            {
        //                obj = new StateData();
        //                obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
        //                obj.name = item.name;
        //                statedateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.StateListCount = statedate.Count;

        //    }
        //    else
        //    {
        //        statedate = (List<StateData>)TempData["statedate"];
        //        statedateobj = (List<StateData>)TempData["statedateobj"];

        //        ViewBag.StateListCount = statedate.Count;
        //    }
        //    return PartialView(statedateobj);
        //}

        //public ActionResult DashboardBranchComboboxOrder(string stateid)
        //{

        //    FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
        //    string userid = Session["userid"].ToString();
        //    int chkState = 0;
        //    if (stateid == null)
        //    {
        //        chkState = 1;
        //    }

        //    List<BranchData> branchdate = new List<BranchData>();
        //    List<BranchData> branchdateobj = new List<BranchData>();

        //    if (TempData["branchdateobj"] == null)
        //    {
        //        string stateIds = dashboard.StateId;
        //        try
        //        {
        //            BranchData obj = null;
        //            if (stateid == null)
        //            {
        //                stateid = "";
        //            }

        //            if (TempData["branchdate"] != null)
        //            {
        //                branchdate = (List<BranchData>)TempData["branchdate"];
        //            }
        //            else
        //            {
        //                branchdate = dashboard.GetBranchList(Convert.ToInt32(userid), stateid);
        //            }

        //            foreach (var item in branchdate)
        //            {
        //                obj = new BranchData();
        //                obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
        //                obj.name = item.name;
        //                branchdateobj.Add(obj);
        //            }
        //        }
        //        catch { }
        //        ViewBag.BranchListCount = branchdate.Count;

        //    }
        //    else
        //    {
        //        branchdate = (List<BranchData>)TempData["branchdate"];
        //        branchdateobj = (List<BranchData>)TempData["branchdateobj"];

        //        ViewBag.BranchListCount = branchdate.Count;
        //    }

        //    if (chkState == 1)
        //    {
        //        return PartialView("DashboardBranchComboboxOrder", branchdateobj);
        //    }
        //    else
        //    {
        //        Session["PageloadChk"] = "0";
        //        Session["BranchList"] = branchdateobj;
        //        return Json(branchdate, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //// End of Rev Sanchita

        public ActionResult CheckDashboardDataExists()
        {
            ViewData["ExportDashboardSummaryGridList"] = TempData["ExportDashboardSummaryGridList"];
            TempData.Keep();
            if (ViewData["ExportDashboardSummaryGridList"] != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("failure");
            }

        }
        

        ////REV 4.0
        public ActionResult DashboardLMS(List<DashboardSettingMapped> list)
        {

            return PartialView(list);
        }

        public JsonResult GETLMSCOUNTDATA(string stateid, string branchid)
        {
            Dashboard dashboarddataobj = new Dashboard();
            FSMDashboard Dashboarddata = new FSMDashboard();
            try
            {
                DataSet objData = dashboarddataobj.LINQFORLMSDASHBOARD(stateid, branchid);

                int TotalLearnersCNT = 0;
                int AssignedTopicsCNT = 0;
                int YettoStartCNT = 0;
                int InProgressCNT = 0;
                int CompletedCNT = 0;
                decimal AverageProgressCNT = 0;


                foreach (DataRow item in objData.Tables[0].Rows)
                {
                    TotalLearnersCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[1].Rows)
                {
                    AssignedTopicsCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[2].Rows)
                {
                    YettoStartCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[3].Rows)
                {
                    InProgressCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[4].Rows)
                {
                    CompletedCNT = Convert.ToInt32(item["CNT"]);
                }
                foreach (DataRow item in objData.Tables[5].Rows)
                {
                    AverageProgressCNT = Convert.ToDecimal(item["AverageProgress"]);
                }

                Dashboarddata.TotalLearners = TotalLearnersCNT;
                Dashboarddata.AssignedTopics = AssignedTopicsCNT;
                Dashboarddata.YettoStart = YettoStartCNT;
                Dashboarddata.InProgress = InProgressCNT;
                Dashboarddata.Completed = CompletedCNT;
                Dashboarddata.AverageProgress = AverageProgressCNT;

            }
            catch
            {
            }
            return Json(Dashboarddata);
        }

        public PartialViewResult DashBoardGVLMS(FSMDashBoardFilter dd)
        {
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("prc_LMSDASHBOARDDATA", sqlcon);
            sqlcmd.Parameters.Add("@ACTION", dd.ActionType);
            sqlcmd.Parameters.Add("@USERID", Convert.ToString(Session["userid"]));
            sqlcmd.Parameters.Add("@STATEID", dd.STATEIDS);
            sqlcmd.Parameters.Add("@BRANCHID", dd.BRANCHIDS);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            TempData["LMSDashboardGridView"] = dt;
            TempData.Keep();
            return PartialView(dt);
        }

        public ActionResult LMSExportDashboardGridView(int type, String Name)
        {
            //Rev Tanmoy get data through execute query
            DataTable dbDashboardData = new DataTable();
            DBEngine objdb = new DBEngine();

            if (TempData["LMSDashboardGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    // return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["DashboardGridView"]), dbDashboardData);Replace ViewData To datatable
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridViewLMS(TempData["LMSDashboardGridView"], Name), TempData["LMSDashboardGridView"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridViewLMS(object datatable, String Name)
        {
            var settings = new GridViewSettings();
            //settings.Name = "DashboardGridView";
            settings.Name = Name;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            // settings.SettingsExport.FileName = "DashboardGridView";
            settings.SettingsExport.FileName = Name;
            //String ID = Convert.ToString(TempData["LMSDashboardGridView"]);
            //TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                //if (datacolumn.ColumnName != "EMPID")
                //{
                settings.Columns.Add(column =>
                {
                    column.Caption = datacolumn.ColumnName;
                    column.FieldName = datacolumn.ColumnName;
                    //if (datacolumn.DataType.FullName == "System.Decimal" || datacolumn.DataType.FullName == "System.Int32" || datacolumn.DataType.FullName == "System.Int64")
                    //{
                    //    if (datacolumn.ColumnName != "Shops Visited")
                    //    {
                    //        column.PropertiesEdit.DisplayFormatString = "0.00";
                    //    }
                    //}
                });
                //}

            }

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult DashboardBranchComboboxLMS(string stateid)
        {
            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            int chkState = 0;
            if (stateid == null)
            {
                chkState = 1;
            }
            List<BranchData> branchdate = new List<BranchData>();
            List<BranchData> branchdateobj = new List<BranchData>();
            if (TempData["branchdateobjLMS"] == null)
            {
                string stateIds = dashboard.StateId;
                try
                {
                    BranchData obj = null;
                    if (stateid == null)
                    {
                        stateid = "";
                    }

                    branchdate = dashboard.GetLMSBranchList(Convert.ToInt32(userid), stateid);

                    foreach (var item in branchdate)
                    {
                        obj = new BranchData();
                        obj.BranchID = !String.IsNullOrEmpty(Convert.ToString(item.BranchID)) ? Convert.ToInt32(item.BranchID) : 0;
                        obj.name = item.name;
                        branchdateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.BranchListCount = branchdate.Count;
            }
            else
            {
                branchdate = (List<BranchData>)TempData["branchdate"];
                branchdateobj = (List<BranchData>)TempData["branchdateobjLMS"];
                ViewBag.BranchListCount = branchdate.Count;
            }

            if (chkState == 1)
            {
                return PartialView("DashboardBranchComboboxLMS", branchdateobj);
            }
            else
            {
                Session["PageloadChk"] = "0";
                Session["BranchList"] = branchdateobj;
                return Json(branchdate, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DashboardStateComboboxLMS()
        {

            FSMDashBoardFilter dashboard = new FSMDashBoardFilter();
            string userid = Session["userid"].ToString();
            //string userid = "0";
            List<StateData> statedate = new List<StateData>();
            List<StateData> statedateobj = new List<StateData>();

            // Rev 1.0
            if (TempData["statedateobj"] == null)
            {
                // End of Rev 1.0
                try
                {
                    StateData obj = null;
                    statedate = dashboard.GetStateList(Convert.ToInt32(userid));
                    foreach (var item in statedate)
                    {
                        obj = new StateData();
                        obj.StateID = !String.IsNullOrEmpty(item.id) ? Convert.ToInt32(item.id) : 0;
                        obj.name = item.name;
                        statedateobj.Add(obj);
                    }
                }
                catch { }
                ViewBag.StateListCount = statedate.Count;
                // Rev 1.0
            }
            else
            {
                statedate = (List<StateData>)TempData["statedate"];
                statedateobj = (List<StateData>)TempData["statedateobj"];

                ViewBag.StateListCount = statedate.Count;
            }
            // End of Rev 1.0

            return PartialView(statedateobj);
        }

        //REV 4.0 END
    }
}