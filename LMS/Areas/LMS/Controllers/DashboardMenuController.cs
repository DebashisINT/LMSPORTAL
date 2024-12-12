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