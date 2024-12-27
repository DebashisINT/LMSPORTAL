//*********************************************************************************************************************
// 1.0      17/02/2023      2.0.39      Sanchita        A setting required for 'User Account' Master module in FSM Portal
//                                                      Refer: 25669
//*********************************************************************************************************************
using System;
using System.Web;
using System.Web.UI;
using BusinessLogicLayer;
using System.Configuration;
using System.Web.Services;
using DataAccessLayer;
using System.Data;
using ClsDropDownlistNameSpace;
using DevExpress.Web;
using System.Collections.Generic;
using UtilityLayer;
using System.Web.UI.WebControls;
using BusinessLogicLayer.SalesERP;
using static ERP.OMS.Management.Master.management_master_Employee;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_UserAccountList : ERP.OMS.ViewState_class.VSPage
    {
        public string pageAccess = "";       

        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new EntityLayer.CommonELS.UserRightsForPage();
        clsDropDownList oclsDropDownList = new clsDropDownList();

        public bool ActivateEmployeeBranchHierarchy { get; set; }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Code  Added and Commented By Priti on 20122016 to use Covert.Tostring() instead of Tostring()
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                //string sPath = HttpContext.Current.Request.Url.ToString();
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/UserAccountList.aspx");

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_InsertUpdateUser");
            proc.AddPara("@ACTION", "ShowSettingsActivateEmployeeBranchHierarchy");
            dt = proc.GetTable();
            if (dt.Rows.Count > 0)
            {
                string mastersettings = Convert.ToString(dt.Rows[0]["Value"]);
                if (mastersettings == "0")
                {
                    ActivateEmployeeBranchHierarchy = false;
                }
                else
                {
                    ActivateEmployeeBranchHierarchy = true;
                }
            }

            if (!IsPostBack)
            {
                userGrid.SettingsCookies.CookiesID = "BreeezeErpGridCookiesroot_useruserGrid";
                this.Page.ClientScript.RegisterStartupScript(GetType(), "setCookieOnStorage", "<script>addCookiesKeyOnStorage('BreeezeErpGridCookiesroot_useruserGrid');</script>");                               
                Session["exportval"] = null;               
            }
            userGrid.DataSource = BindUserList();
            userGrid.DataBind();
        }
        public DataTable BindUserList()
        {
            string strFromDOJ = String.Empty, strToDOJ = String.Empty;

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_LMS_UserAccountData");
            proc.AddIntegerPara("@User_id", Convert.ToInt32(HttpContext.Current.Session["LMSuserid"]));
            dt = proc.GetTable();
            return dt;
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));           
            if (Filter != 0)
            {
                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }
        }
        public void bindexport(int Filter)
        {         
            string filename = "Users";
            exporter.FileName = filename;

            exporter.PageHeader.Left = "Users";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";
            switch (Filter)
            {
                case 1:
                    exporter.WritePdfToResponse();
                    break;
                case 2:
                    exporter.WriteXlsToResponse();
                    break;
                case 3:
                    exporter.WriteRtfToResponse();
                    break;
                case 4:
                    exporter.WriteCsvToResponse();
                    break;
            }
        }
        protected void userGrid_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpHeight"] = "1";
        }

        [WebMethod]
        public static String GetEmployeeID(string EMPID)
        {
            String Return = null;
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            string query = "Select cnt_ucc from tbl_master_contact where cnt_internalId='" + EMPID + "' ";

            DataTable dt = objEngine.GetDataTable(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                Return = dt.Rows[0]["cnt_ucc"].ToString();
            }

            return Return;
        }

        [WebMethod]
        public static String GetEmployeeIDUpdate(string EMPID, String newEmpID)
        {
            String sreturn = "";
            DataTable dtfromtosumervisor = SalesPersontracking.UpdateEmployeeID(EMPID, newEmpID, Convert.ToString(HttpContext.Current.Session["LMSuserid"]));

            if (dtfromtosumervisor != null && dtfromtosumervisor.Rows.Count > 0)
            {
                sreturn = dtfromtosumervisor.Rows[0]["msg"].ToString();
            }

            return sreturn;
        }

        [WebMethod]
        public static List<StateList> GetStateList(string EMPID)
        {
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            string query = "Select  state as StateName ,id as StateID from tbl_master_state  order by state";
            List<StateList> omodel = new List<StateList>();
            // DataTable dt = objEngine.GetDataTable(query);
            DataTable dt = new DataTable();

            //  DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("Proc_User_StateMAP");

            proc.AddPara("@EMPID", EMPID);

            dt = proc.GetTable();

            if (dt != null && dt.Rows.Count > 0)
            {

                omodel = UtilityLayer.APIHelperMethods.ToModelList<StateList>(dt);

            }

            return omodel;

        }

        [WebMethod]
        public static bool GetStateListSubmit(string EMPID, List<string> Statelist)
        {
            string StateId = "";
            int i = 1;

            if (Statelist != null && Statelist.Count > 0)
            {
                foreach (string item in Statelist)
                {
                    if (item == "0")
                    {
                        StateId = "0";
                        break;
                    }
                    else
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }
                }

            }

            DataTable dtfromtosumervisor = SalesPersontracking.SubmitEmployeeState(EMPID, StateId, Convert.ToString(HttpContext.Current.Session["LMSuserid"]));

            return true;
        }

        [WebMethod]
        public static List<BranchList> GetBranchList(string EMPID)
        {
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            List<BranchList> omodel = new List<BranchList>();
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_User_BranchMAP");
            proc.AddPara("@EMPID", EMPID);
            dt = proc.GetTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                omodel = UtilityLayer.APIHelperMethods.ToModelList<BranchList>(dt);
            }
            return omodel;
        }

        [WebMethod]
        public static bool GetBranchListSubmit(string EMPID, List<string> Branchlist)
        {
            Employee_BL objEmploye = new Employee_BL();
            string BranchId = "";
            int i = 1;

            if (Branchlist != null && Branchlist.Count > 0)
            {
                foreach (string item in Branchlist)
                {
                    if (item == "0")
                    {
                        BranchId = "0";
                        break;
                    }
                    else
                    {
                        if (i > 1)
                            BranchId = BranchId + "," + item;
                        else
                            BranchId = item;
                        i++;
                    }
                }

            }

            DataTable dtfromtosumervisor = objEmploye.SubmitEmployeeBranch(EMPID, BranchId, Convert.ToString(HttpContext.Current.Session["LMSuserid"]));

            return true;
        }

        public class BranchList
        {
            public long branch_id { get; set; }
            public String branch_description { get; set; }
            public bool IsChecked { get; set; }
            public string status { get; set; }
        }
        //End of Mantis Issue 25001
        public class StateList
        {

            public int StateID { get; set; }
            public string StateName { get; set; }
            public string status { get; set; }

            public bool IsChecked { get; set; }
        }

        [WebMethod]
        public static string DeleteUser(string user_id)
        {
            string retval = "";

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_InsertUpdateUser");
            proc.AddPara("@user_id", user_id);
            proc.AddPara("@ACTION", "DELETEUSER");
            dt = proc.GetTable();

            if(dt.Rows.Count>0)
            {
                retval = Convert.ToString( dt.Rows[0][0]);
            }

            return retval;
        }
            
            
    }
}