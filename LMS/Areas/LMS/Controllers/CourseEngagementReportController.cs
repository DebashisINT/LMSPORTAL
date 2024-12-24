using DevExpress.Web.Mvc;
using DevExpress.Web;
using LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using UtilityLayer;
using DataAccessLayer;

namespace LMS.Areas.LMS.Controllers
{
    public class CourseEngagementReportController : Controller
    {
        // GET: LMS/CourseEngagementReport
        CourseEngagementModel obj = new CourseEngagementModel();
        LMSReportsModel objLMSReports = new LMSReportsModel();
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CourseEngagementReport/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;
            return View();
        }

        public PartialViewResult _PartialCourseEngagementListing(LMSReportTopicListModel obj)
        {
            string Is_PageLoad = string.Empty;
            if (obj.is_pageload != "1")
            {
                Is_PageLoad = "0";
            }


            return PartialView(GetReport(Is_PageLoad));
        }


        public IEnumerable GetReport(string is_pageload)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["LMSuserid"]);

            if (is_pageload != "0")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_COURSEENGAGEMENTLISTs
                        where Convert.ToString(d.USERID) == Userid
                        //orderby d.SEQ
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_COURSEENGAGEMENTLISTs
                        where d.SEQ == 0
                        select d;
                return q;
            }
        }

        public ActionResult ExporSummaryList(int type,string is_pageload)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetGridViewSettings(), GetReport(is_pageload));                
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Topic Engagement Insights";
            settings.CallbackRouteValues = new { Action = "_PartialCourseEngagementListing", Controller = "CourseEngagementReport" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Topic Engagement Insights";

            settings.Columns.Add(x =>
            {
                x.FieldName = "COURSERank";
                x.Caption = "Rank";
                x.VisibleIndex = 1;
                x.ColumnType = MVCxGridViewColumnType.TextBox;               
                x.Width = System.Web.UI.WebControls.Unit.Percentage(5);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CourseName";
                x.Caption = "Topic Name";
                x.VisibleIndex = 2;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
               
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "DEPARTMENT";
                x.Caption = "Department";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;                
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TotalEnrollments";
                x.Caption = "Total Enrollments";
                x.VisibleIndex = 4;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                //x.Width = 180;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CompletionRate";
                x.Caption = "Completion Rate (%)";
                x.VisibleIndex = 5;
                x.ColumnType = MVCxGridViewColumnType.TextBox;                
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "AverageTimeSpent";
                x.Caption = "Average Time Spent (hrs)";
                x.VisibleIndex = 6;
                x.ColumnType = MVCxGridViewColumnType.TextBox;               
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult GetTopicList()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                Get_Topic dataobj = new Get_Topic();
                List<Get_Topic> productdata = new List<Get_Topic>();
                List<Get_Topic> modelbranch = new List<Get_Topic>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = objLMSReports.GETDROPDOWNVALUE("GETTOPIC");
                modelbranch = APIHelperMethods.ToModelList<Get_Topic>(ComponentTable);
                return PartialView("~/Areas/LMS/Views/CourseEngagementReport/_TopicLookUpPartial.cshtml", modelbranch);
                
            }
            catch
            {
                return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });
            }
        }


        public ActionResult GetDepartment()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
            try
            {
                GetDepartmentList dataobj = new GetDepartmentList();
              
                List<GetDepartmentList> modelbranch = new List<GetDepartmentList>();
                DataTable ComponentTable = new DataTable();
                ComponentTable = GETDROPDOWNVALUE("GetDepartment");
                modelbranch = APIHelperMethods.ToModelList<GetDepartmentList>(ComponentTable);
                return PartialView("~/Areas/LMS/Views/CourseEngagementReport/_DepartmentLookupPartial.cshtml", modelbranch);
            }
            catch
            {
                return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });
            }
        }
        public DataTable GETDROPDOWNVALUE(string Action)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_COURSEENGAGEMENT"))
                {
                    proc.AddVarcharPara("@ACTION", 100, Action);                    
                    dt = proc.GetTable();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc = null;
            }
        }

        public class GetDepartmentList
        {
            public Int32 COST_ID { get; set; }
            public string COST_DESCRIPTION { get; set; }
           
        }

        public class Get_Topic
        {
            public Int64 TOPICID { get; set; }
            public string TOPICNAME { get; set; }
            
        }

        public JsonResult CreateLINQTable(string Topic_Id, string Department_Id, string _Top, string _Bottom,string is_pageload)
        {
            string output = "";
            CreateTable(Topic_Id, Department_Id, _Top, _Bottom, is_pageload);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public void CreateTable(string Topic_Id, string Department_Id, string _Top, string _Bottom, string is_pageload)
        {
            string Userid = Convert.ToString(Session["LMSuserid"]);
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("PRC_LMS_COURSEENGAGEMENT", sqlcon);
            sqlcmd.Parameters.Add("@USER_ID", Userid);
            sqlcmd.Parameters.Add("@ISPAGELOAD", is_pageload);
            sqlcmd.Parameters.Add("@Action", "GetDetails");

            sqlcmd.Parameters.Add("@Topic_Id", Topic_Id);
            sqlcmd.Parameters.Add("@Department_Id", Department_Id);
            sqlcmd.Parameters.Add("@Top", _Top);
            sqlcmd.Parameters.Add("@Bottom", _Bottom);

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();

        }

        public ActionResult PartialTotalEnrollmentsListing(CourseEngagementModel model)
        {
            try
            {
                GetUserTopicList dataobj = new GetUserTopicList();
                List<GetUserTopicList> productdata = new List<GetUserTopicList>();
                DataTable dt = new DataTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_COURSEENGAGEMENT", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GetTotalEnrollmentsList");
                sqlcmd.Parameters.Add("@TOPICID", model.TOPICID);
                sqlcmd.Parameters.Add("@COST_ID", model.COST_ID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable objData = dt;
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetUserTopicList();
                        dataobj.TOPICID = Convert.ToInt64(row["TOPICID"]);
                        dataobj.TOPICNAME = Convert.ToString(row["TOPICNAME"]);
                        dataobj.EMPNAME = Convert.ToString(row["EMPNAME"]);

                        dataobj.user_id = Convert.ToInt64(row["user_id"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.CONTENT_ID = Convert.ToInt64(row["CONTENT_ID"]);

                        dataobj.CONTENTTITLE = Convert.ToString(row["CONTENTTITLE"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.COST_ID = Convert.ToInt32(row["COST_ID"]);
                        dataobj.cost_description = Convert.ToString(row["cost_description"]);                      

                        if (Convert.ToString(row["CREATEDON"]) == "1/1/1900 12:00:00 AM")
                        {

                        }
                        else
                        {
                            dataobj.CREATEDON = Convert.ToDateTime(row["CREATEDON"]);
                        }
                        productdata.Add(dataobj);
                    }

                }
                TempData["ExportTotalEnrollmentsListing"] = productdata;
                TempData.Keep();
                return PartialView("PartialTotalEnrollmentsListing", productdata);
            }
            catch
            {
                return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });
            }
        }

        public ActionResult PartialCompletionRateListing(CourseEngagementModel model)
        {
            try
            {
                GetUserTopicList dataobj = new GetUserTopicList();
                List<GetUserTopicList> productdata = new List<GetUserTopicList>();
                DataTable dt = new DataTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_COURSEENGAGEMENT", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GetCompletionRateList");
                sqlcmd.Parameters.Add("@TOPICID", model.TOPICID);
                sqlcmd.Parameters.Add("@COST_ID", model.COST_ID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable objData = dt;
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetUserTopicList();
                        dataobj.TOPICID = Convert.ToInt64(row["TOPICID"]);
                        dataobj.TOPICNAME = Convert.ToString(row["TOPICNAME"]);
                        dataobj.EMPNAME = Convert.ToString(row["EMPNAME"]);

                        dataobj.user_id = Convert.ToInt64(row["user_id"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.CONTENT_ID = Convert.ToInt64(row["CONTENT_ID"]);

                        dataobj.CONTENTTITLE = Convert.ToString(row["CONTENTTITLE"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.COST_ID = Convert.ToInt32(row["COST_ID"]);
                        dataobj.cost_description = Convert.ToString(row["cost_description"]);
                        dataobj.CREATEDON = Convert.ToDateTime(row["CREATEDON"]);
                        if(Convert.ToString(row["CONTENTLASTVIEW"])== "1/1/1900 12:00:00 AM")
                        {

                        }
                        else
                        {
                            dataobj.CONTENTLASTVIEW = Convert.ToDateTime(row["CONTENTLASTVIEW"]);
                        }                      
                        
                        productdata.Add(dataobj);

                    }
                }
                TempData["ExportCompletionRateListing"] = productdata;
                TempData.Keep();
                return PartialView("PartialCompletionRateListing", productdata);
            }
            catch
            {
                return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });
            }
        }

        public ActionResult PartialAverageTimeSpentListing(CourseEngagementModel model)
        {
            try
            {
                GetUserTopicList dataobj = new GetUserTopicList();
                List<GetUserTopicList> productdata = new List<GetUserTopicList>();
                DataTable dt = new DataTable();

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_COURSEENGAGEMENT", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GetAverageTimeSpentList");
                sqlcmd.Parameters.Add("@TOPICID", model.TOPICID);
                sqlcmd.Parameters.Add("@COST_ID", model.COST_ID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable objData = dt;
                    foreach (DataRow row in objData.Rows)
                    {
                        dataobj = new GetUserTopicList();
                        dataobj.TOPICID = Convert.ToInt64(row["TOPICID"]);
                        dataobj.TOPICNAME = Convert.ToString(row["TOPICNAME"]);
                        dataobj.EMPNAME = Convert.ToString(row["EMPNAME"]);

                        dataobj.user_id = Convert.ToInt64(row["user_id"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.CONTENT_ID = Convert.ToInt64(row["CONTENT_ID"]);

                        dataobj.CONTENTTITLE = Convert.ToString(row["CONTENTTITLE"]);
                        dataobj.user_name = Convert.ToString(row["user_name"]);
                        dataobj.COST_ID = Convert.ToInt32(row["COST_ID"]);
                        dataobj.cost_description = Convert.ToString(row["cost_description"]);
                        dataobj.CREATEDON = Convert.ToDateTime(row["CREATEDON"]);
                        dataobj.TimeSpent = Convert.ToDecimal(row["TimeSpent"]);

                        productdata.Add(dataobj);

                    }
                }
                TempData["ExportAverageTimeSpentListing"] = productdata;
                TempData.Keep();
                return PartialView("PartialAverageTimeSpentListing", productdata);
            }
            catch
            {
                return RedirectToAction("Login", "LMSLogin", new { area = "LMS" });
            }
        }


        
        public ActionResult ExportTotalEnrollmentsList(int type)
        {
            if (TempData["ExportTotalEnrollmentsListing"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToXlsx(GetGridViewTotalEnrollmentsListSettings(), TempData["ExportTotalEnrollmentsListing"]);
                    default:
                        break;
                }
            }
            return null;
        }
        private GridViewSettings GetGridViewTotalEnrollmentsListSettings()
        {


            var settings = new GridViewSettings();
            settings.Name = "gridTotalEnrollmentsList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeeList";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee name";
                x.VisibleIndex = 1;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "cost_description";
                x.Caption = "Department";
                x.VisibleIndex = 2;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });            
            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDON";
                x.Caption = "Assign Date";
                x.VisibleIndex = 5;               
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

            });
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult ExportCompletionRateList(int type)
        {
            if (TempData["ExportCompletionRateListing"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToXlsx(GetGridViewCompletionRateSettings(), TempData["ExportCompletionRateListing"]);
                    default:
                        break;
                }
            }

            return null;
        }
        private GridViewSettings GetGridViewCompletionRateSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridCompletionRateList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeeList";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee name";
                x.VisibleIndex = 1;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "cost_description";
                x.Caption = "Department";
                x.VisibleIndex = 2;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });            
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTLASTVIEW";
                x.Caption = "Completion Date";
                x.VisibleIndex = 5;              
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
                x.ColumnType = MVCxGridViewColumnType.DateEdit;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                (x.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";

            });
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
        public ActionResult ExportAverageTimeSpentList(int type)
        {
            if (TempData["ExportAverageTimeSpentListing"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToXlsx(GetGridViewAverageTimeSettings(), TempData["ExportAverageTimeSpentListing"]);
                    default:
                        break;
                }
            }

            return null;
        }
        private GridViewSettings GetGridViewAverageTimeSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridAverageTimeList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeeList";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee name";
                x.VisibleIndex = 1;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "cost_description";
                x.Caption = "Department";
                x.VisibleIndex = 2;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTENTTITLE";
                x.Caption = "Content";
                x.VisibleIndex = 3;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "TimeSpent";
                x.Caption = "Time Spent (Hrs)";
                x.VisibleIndex = 4;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}