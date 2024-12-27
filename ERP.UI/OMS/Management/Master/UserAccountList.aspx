<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                17/02/2023        2.0.39           Sanchita            A setting required for 'Login Configuration' Master module in FSM Portal
                                                                          Refer: 25669  
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Users" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_UserAccountList" CodeBehind="UserAccountList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #divDashboardHeaderList .panel:last-child {
            margin-bottom: 0;
        }
        ul.inline-list {
            padding-left: 0;
            margin-bottom: 0;
        }
        .inline-list > li {
            display: inline-block;
            list-style-type: none;
            margin-right: 20px;
            margin-bottom: 8px;
        }
        .inline-list > li > input {
                -webkit-transform: translateY(3px);
                -moz-transform: translateY(3px);
                transform: translateY(3px);
                margin-right: 4px;
            }

        .panel-title > a {
            font-size: 13px !important;
            display: inline-block;
            padding-left: 10px;
        }

        #list2 option,
        #list1 option {
            padding: 5px 3px;
        }

        .padTbl > tbody > tr > td {
            padding-right: 20px;
            vertical-align: central;
        }

            .padTbl > tbody > tr > td > label {
                margin-bottom: 0px !important;
            }

        #divDashboardHeaderList .panel-title {
            position: relative;
        }

            #divDashboardHeaderList .panel-title > a:focus {
                text-decoration: none;
            }

            #divDashboardHeaderList .panel-title > a:after {
                content: '\f056';
                font-family: FontAwesome;
                font-size: 18px;
                position: absolute;
                right: 10px;
                top: 6px;
            }

            #divDashboardHeaderList .panel-title > a + input[type="checkbox"] {
                -webkit-transform: translateY(2px);
                -moz-transform: translateY(2px);
                transform: translateY(2px);
            }

            #divDashboardHeaderList .panel-title > a.collapsed:after {
                content: '\f055';
            }

        .errorField {
            position: absolute;
            right: -17px;
            top: 3px;
        }

        #multiselect_to option, #multiselect option {
            padding: 5px 3px;
        }

        .min3 {
            min-height: 150px;
        }

        .pad28 {
            padding-top: 26px;
        }
    </style>

    <style>
        .transfer-demo {
            width: 640px;
            height: 351px;
        }

            .transfer-demo .transfer-double-header {
                display: none;
            }

        .transfer-double-selected-list-main .transfer-double-selected-list-ul .transfer-double-selected-list-li .checkbox-group {
            width: 85%;
        }

        .red {
            color: red;
        }

        input:focus, textarea:focus, select:focus {
            outline: none;
        }

        .transfer-double-content-param {
            border-bottom: 1px solid #4236f5;
            background: #4236f5;
            color: #e8e8e8;
        }

        /*Rev 1.0*/

        body , .dxtcLite_PlasticBlue
        {
            font-family: 'Poppins', sans-serif !important;
        }

    #BranchGridLookup {
        min-height: 34px;
        border-radius: 5px;
    }

    .dxeButtonEditButton_PlasticBlue {
        background: #094e8c !important;
        border-radius: 4px !important;
        padding: 0 4px !important;
    }

    .dxeButtonDisabled_PlasticBlue {
        background: #ababab !important;
    }

    .chosen-container-single .chosen-single div {
        background: #094e8c;
        color: #fff;
        border-radius: 4px;
        height: 30px;
        top: 1px;
        right: 1px;
        /*position:relative;*/
    }

        .chosen-container-single .chosen-single div b {
            display: none;
        }

        .chosen-container-single .chosen-single div::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 2px;
            right: 3px;
            font-size: 13px;
            transform: rotate(269deg);
            font-weight: 500;
        }

    .chosen-container-active.chosen-with-drop .chosen-single div {
        background: #094e8c;
        color: #fff;
    }

        .chosen-container-active.chosen-with-drop .chosen-single div::after {
            transform: rotate(90deg);
            right: 7px;
        }

    .calendar-icon {
        position: absolute;
        bottom: 9px;
        right: 5px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear {
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    .h-branch-select {
        position: relative;
    }

        .h-branch-select::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 33px;
            right: 13px;
            font-size: 18px;
            transform: rotate(269deg);
            font-weight: 500;
            background: #094e8c;
            color: #fff;
            height: 18px;
            display: block;
            width: 28px;
            /* padding: 10px 0; */
            border-radius: 4px;
            text-align: center;
            line-height: 19px;
            z-index: 0;
        }

        select:not(.btn):focus
        {
            border-color: #094e8c;
        }

        select:not(.btn):focus-visible
        {
            box-shadow: none;
            outline: none;
            
        }

    .multiselect.dropdown-toggle {
        text-align: left;
    }

    .multiselect.dropdown-toggle, #ddlMonth, #ddlYear {
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    select:not(.btn) {
        padding-right: 30px;
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    #ddlShowReport:focus-visible {
        box-shadow: none;
        outline: none;
        border: 1px solid #164f93;
    }

    #ddlShowReport:focus {
        border: 1px solid #164f93;
    }

    .whclass.selectH:focus-visible {
        outline: none;
    }

    .whclass.selectH:focus {
        border: 1px solid #164f93;
    }

    .dxeButtonEdit_PlasticBlue {
        border: 1px Solid #ccc;
    }

    .chosen-container-single .chosen-single {
        border: 1px solid #ccc;
        background: #fff;
        box-shadow: none;
    }

    .daterangepicker td.active, .daterangepicker td.active:hover {
        background-color: #175396;
    }

    label {
        font-weight: 500;
    }

    .dxgvHeader_PlasticBlue {
        background: #164f94;
    }

    .dxgvSelectedRow_PlasticBlue td.dxgv {
        color: #fff;
    }

    .dxeCalendarHeader_PlasticBlue {
        background: #185598;
    }

    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue,
    .dxbButton_PlasticBlue,
    .dxeCalendar_PlasticBlue,
    .dxeEditArea_PlasticBlue,
    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue{
        font-family: 'Poppins', sans-serif !important;
    }

    .dxgvEditFormDisplayRow_PlasticBlue td.dxgv, .dxgvDataRow_PlasticBlue td.dxgv, .dxgvDataRowAlt_PlasticBlue td.dxgv, .dxgvSelectedRow_PlasticBlue td.dxgv, .dxgvFocusedRow_PlasticBlue td.dxgv {
        font-weight: 500;
    }

    .btnPadding .btn {
        padding: 7px 14px !important;
        border-radius: 4px;
    }

    .btnPadding {
        padding-top: 24px !important;
    }

    .dxeButtonEdit_PlasticBlue {
        border-radius: 5px;
        height: 34px;
    }

    #dtFrom, #dtTo {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #tblshoplist_wrapper .dataTables_scrollHeadInner table tr th {
        background: #165092;
        vertical-align: middle;
        font-weight: 500;
    }

    /*#refreshgrid {
        background: #e5e5e5;
        padding: 0 10px;
        margin-top: 15px;
        border-radius: 8px;
    }*/

    .styled-checkbox {
        position: absolute;
        opacity: 0;
        z-index: 1;
    }

        .styled-checkbox + label {
            position: relative;
            /*cursor: pointer;*/
            padding: 0;
            margin-bottom: 0 !important;
        }

            .styled-checkbox + label:before {
                content: "";
                margin-right: 6px;
                display: inline-block;
                vertical-align: text-top;
                width: 16px;
                height: 16px;
                /*background: #d7d7d7;*/
                margin-top: 2px;
                border-radius: 2px;
                border: 1px solid #c5c5c5;
            }

        .styled-checkbox:hover + label:before {
            background: #094e8c;
        }


        .styled-checkbox:checked + label:before {
            background: #094e8c;
        }

        .styled-checkbox:disabled + label {
            color: #b8b8b8;
            cursor: auto;
        }

            .styled-checkbox:disabled + label:before {
                box-shadow: none;
                background: #ddd;
            }

        .styled-checkbox:checked + label:after {
            content: "";
            position: absolute;
            left: 3px;
            top: 9px;
            background: white;
            width: 2px;
            height: 2px;
            box-shadow: 2px 0 0 white, 4px 0 0 white, 4px -2px 0 white, 4px -4px 0 white, 4px -6px 0 white, 4px -8px 0 white;
            transform: rotate(45deg);
        }

    #dtstate {
        padding-right: 8px;
    }

    .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .modal-content {
        border: none;
        border-radius: 10px;
    }

    .modal-header .modal-title {
        font-size: 14px;
    }

    .close {
        font-weight: 400;
        font-size: 25px;
        color: #fff;
        text-shadow: none;
        opacity: .5;
    }

    #EmployeeTable {
        margin-top: 10px;
    }

        #EmployeeTable table tr th {
            padding: 5px 10px;
        }

    .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
    }

        .dynamicPopupTbl > tbody > tr > td,
        #EmployeeTable table tr th {
            font-family: 'Poppins', sans-serif !important;
            font-size: 12px;
        }

    .w150 {
        width: 160px;
    }

    .eqpadtbl > tbody > tr > td:not(:last-child) {
        padding-right: 20px;
    }

    #dtFrom_B-1, #dtTo_B-1 , #cmbDOJ_B-1, #cmbLeaveEff_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img , #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img {
            display: none;
        }

    #dtFrom_I, #dtTo_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        /*z-index: 1;*/
    }

    .pad-md-18 {
        padding-top: 24px;
    }

    .open .dropdown-toggle.btn-default {
        background: transparent !important;
    }

    .input-group-btn .multiselect-clear-filter {
        height: 32px;
        border-radius: 0 4px 4px 0;
    }

    .btn .caret {
        display: none;
    }

    .iminentSpan button.multiselect.dropdown-toggle {
        height: 34px;
    }

    .col-lg-2 {
        padding-left: 8px;
        padding-right: 8px;
    }

    .dxeCalendarSelected_PlasticBlue {
        color: White;
        background-color: #185598;
    }

    .dxeTextBox_PlasticBlue
    {
            height: 34px;
            border-radius: 4px;
    }

    #cmbDOJ_DDD_PW-1
    {
        z-index: 9999 !important;
    }

    #cmbDOJ, #cmbLeaveEff
    {
        position: relative;
    z-index: 1;
    background: transparent;
    }

    .btn-sm, .btn-xs
    {
        padding: 7px 10px !important;
    }

    .dxpcLite_PlasticBlue .dxpc-headerText
    {
            color: #fff;
    }

    #drdExport
    {
        max-height: 32px;
        padding: 5px 10px !important;
    }

    @media only screen and (max-width: 768px) 
    {
        .overflow-x-auto {
            overflow-x: auto !important;
                width: 300px;
        }

        .backBox
        {
            overflow: hidden !important;
        }

        .breadCumb > span
        {
            padding: 9px 15px;
        }
    }

    /*Rev end 1.0*/

    .listStyle > li {
        list-style-type: none;
        padding: 5px;
    }

    .listStyle {
        /*height: 450px;*/
        overflow-y: auto;
    }

        .listStyle > li > input[type="checkbox"] {
            -webkit-transform: translateY(3px);
            -moz-transform: translateY(3px);
            transform: translateY(3px);
        }

    #divModalBody li a:hover:not(.header) {
        background-color: none;
    }
    .modal-backdrop{
        z-index:auto !important;
    }

    </style>

    <style>
        #myInputBranchMap {
            background-image: url('/css/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #divModalBodyBranchMap {
            /* Remove default list styling */
            list-style-type: none;
            padding: 0;
            margin: 0;
            margin-bottom: 8px;
        }

            #divModalBodyBranchMap li {
                padding: 5px 10px;
            }

                #divModalBodyBranchMap li a {
                    margin-top: -1px; /* Prevent double borders */
                    padding: 0 12px; /* Add some padding */
                    text-decoration: none; /* Remove default text underline */
                    font-size: 14px; /* Increase the font-size */
                    color: black; /* Add a black text color */
                    display: inline-block; /* Make it into a block element to fill the whole list */
                    cursor: pointer;
                }
                .tblView>tbody>tr>td{
                    padding-right:5px;
                    padding-bottom:10px
                }
                .tblView>tbody>tr>td>label{
                    display:block
                }
    </style>

     <style>
       .branch-list-modal .modal-header
       {
         background: #074270;
             padding: 10px 15px;
       }
       .branch-list-modal .modal-header h4
       {
         color: #fff;
         line-height: 1;
         font-size: 16px;
       }
       .branch-list-modal .modal-header button span
       {
         color: #fff;
             font-size: 28px;
     font-weight: 300;
     line-height: 21px;
       }

       .branch-list-modal .modal-content
       {
         border-radius: 15px;
             border: none;
             box-shadow: 1px 1px 15px #11111145;
       }
       .branch-list-modal .modal-header
       {
         border-top-left-radius: 15px;
     border-top-right-radius: 15px;
       }

       .branch-list-modal .modal-body .input-group
       {
           width: 100%;
       }

       .branch-list-modal .modal-body .input-group-text
       {
         background-color: transparent;
         border: none;
             color: #a5a5a5;
       }
       .branch-list-modal .modal-body .input-group-prepend
       {
         position: absolute;
         z-index: 1;
         min-height: 33px;
         line-height: 33px;
         width: 30px;
         text-align: center;
       }

       .branch-list-modal .modal-body .form-control
       {
             padding-left: 40px;
             background: transparent !important;
             border-radius: 5px !important;
             border-color: #eaeaea;
             transition: all .4s;
             color: #111;
       }

       .branch-list-modal .modal-body .form-control:focus
       {
         box-shadow: none;
         border-color: #0a4f85;
       }

       .custom-checkbox-single {
           display: block;
           position: relative;
           padding-left: 34px;
           margin-bottom: 15px;
           cursor: pointer;
           font-size: 16px;
           -webkit-user-select: none;
           -moz-user-select: none;
           -ms-user-select: none;
           user-select: none;
           line-height: 22px;
               font-weight: 500;
         }

         /* Hide the browser's default checkbox */
         .custom-checkbox-single input {
           position: absolute;
           opacity: 0;
           cursor: pointer;
           height: 0;
           width: 0;
         }

         /* Create a custom checkbox */
         .checkmark {
           position: absolute;
           top: 0;
           left: 0;
           height: 21px;
           width: 21px;
           background-color: #fff;
     border-radius: 4px;
     border: 1px solid #6a6a6a;
         }

         /* On mouse-over, add a grey background color */
         .custom-checkbox-single:hover input ~ .checkmark {
           background-color: #1541a4;
         }

         /* When the checkbox is checked, add a blue background */
         .custom-checkbox-single input:checked ~ .checkmark {
           background-color: #1541a4;
         }

         /* Create the checkmark/indicator (hidden when not checked) */
         .checkmark:after {
           content: "";
           position: absolute;
           display: none;
         }

         /* Show the checkmark when checked */
         .custom-checkbox-single input:checked ~ .checkmark:after {
           display: block;
         }

         /* Style the checkmark/indicator */
         .custom-checkbox-single .checkmark:after {
               left: 6px;
             top: 1px;
             width: 7px;
             height: 12px;
             border: solid white;
             border-width: 0 3px 3px 0;
             -webkit-transform: rotate(45deg);
             -ms-transform: rotate(45deg);
             transform: rotate(45deg);
         }

         /*Rev 3.0*/
         .fullMulti .multiselect-native-select, .fullMulti .multiselect-native-select .btn-group {
             width: 100%;
         }

             .fullMulti .multiselect-native-select .multiselect {
                 width: 100%;
                 text-align: left;
                 border-radius: 4px !important;
             }

                 .fullMulti .multiselect-native-select .multiselect .caret {
                     float: right;
                     margin: 9px 5px;
                 }

         .hideScndTd > table > tbody > tr > td:last-child {
             display: none;
         }

         .multiselect.dropdown-toggle {
         text-align: left;
         }

         .multiselect.dropdown-toggle, #ddlMonth, #ddlYear {
             -webkit-appearance: none;
             position: relative;
             z-index: 1;
             background-color: transparent;
         }

         .dynamicPopupTbl {
         font-family: 'Poppins', sans-serif !important;
         }

             .dynamicPopupTbl > tbody > tr > td,
             #EmployeeTable table tr th {
                 font-family: 'Poppins', sans-serif !important;
                 font-size: 12px;
             }

             /*Rev 10.0*/
             #EmployeefromsuperTable table tr th {
                 font-family: 'Poppins', sans-serif !important;
                 font-size: 12px;
             }
             #EmployeetosupervisorTable table tr th {
                 font-family: 'Poppins', sans-serif !important;
                 font-size: 12px;
             }
             /*End of Rev 10.0*/
         /*End of Rev 3.0*/
     </style>

    <link href="/assests/pluggins/Transfer/icon_font/css/icon_font.css" rel="stylesheet" />
    
    <link href="/assests/css/custom/PMSStyles.css" rel="stylesheet" />
    <script src="/Scripts/SearchPopup.js"></script>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="/assests/css/custom/SearchPopup.css" rel="stylesheet" />    
    <script src="/Scripts/SearchMultiPopup.js?v1.0"></script>  
    <link href="/assests/pluggins/Transfer/css/jquery.transfer.css" rel="stylesheet" />
    <script src="/assests/pluggins/Transfer/jquery.transfer.js"></script>   

    <script language="javascript" type="text/javascript">

        function AddUserDetails() {           
            var url = 'UserAccountAdd.aspx?id=Add';
            location.href = url;
        } 
        FieldName = 'Headermain1_cmbSegment';
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function callback() {
            grid.PerformCallback('All');
            // grid.PerformCallback();
        }
        function EndCall(obj) {
        }
        function OnEditButtonClick(keyValue) {
            var url = 'UserAccountAdd.aspx?id=' + keyValue;
            window.location.href = url;
        }
    </script>  

    <script>
        function OnCopyInfoClick(keyValue) {
            if (keyValue != '') {
                var url = 'UserAccountAdd.aspx?id=' + keyValue + '&Mode=Copy';
                window.location.href = url;
            }
        }
        function EMPIDBind(empID) {
            $("#hdnEMPCode").val(empID);
            var str
            str = { EMPID: empID }

            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetEmployeeID",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    //cGrdEmployee.PerformCallback("Show~~~");
                    //jAlert('Supervisor Changed Successfully.');
                    //cActivationPopupsupervisor.Hide();

                    $("#lblOLDEmpIDname").html(responseFromServer.d);
                    $("#myEmpIDModal").modal('show');
                    $("#txtEMPId").focus();
                }
            });
        }

        function UpdateEmpId() {
            var empID = $("#hdnEMPCode").val();
            var newEmpID = $("#txtEMPId").val();
            if (newEmpID == "") {
                jAlert("Please enter new employee id.");
                $("#txtEMPId").focus();
                return
            }
            var str1
            //  alert(a);

            str1 = { EMPID: empID, newEmpID: newEmpID }
            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetEmployeeIDUpdate",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    if (responseFromServer.d == "UPDATED") {
                        $("#myEmpIDModal").modal('hide');
                        jAlert('Employee id update successfully.');
                        $("#txtEMPId").val('');
                        $("#hdnEMPCode").val('');
                    }
                    else if (responseFromServer.d == "ALREADY EXISTS") {
                        jAlert('Employee id already exists.');
                        $("#txtEMPId").focus();
                    }
                    else {
                        jAlert('Please try again later.');
                        $("#txtEMPId").focus();
                    }
                }
            });
        }

        function StateBind(empID) {
            $("#hdnEMPID").val(empID);
            var str
            str = { EMPID: empID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetStateList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    if (responseFromServer.d.length == 0) {
                        jAlert('You must create a user, and map this employee. After mapping, you can map the State. So, these employees will appear on Dashboard and in reports for the selected state.');

                    }
                    else if (responseFromServer.d[0].status != 'Success') {

                        jAlert('You must create a user, and map this employee. After mapping, you can map the State. So, these employees will appear on Dashboard and in reports for the selected state.');

                    }
                    else {

                        for (i = 0; i < responseFromServer.d.length; i++) {

                            if (responseFromServer.d[i].StateID == "0") {

                                if (responseFromServer.d[i].IsChecked == true) {

                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheckall' onclick=CheckAll(" + responseFromServer.d[i].StateID + ") value=" + responseFromServer.d[i].StateID + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + " >" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                                else {
                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheckall' onclick=CheckAll(" + responseFromServer.d[i].StateID + ")  value=" + responseFromServer.d[i].StateID + "   /><a href='#'><label id='lblstatename' class='lblstate'  for=" + responseFromServer.d[i].StateID + ">" + responseFromServer.d[i].StateName + "</label></a></li>";


                                }
                            }
                            else {

                                if (responseFromServer.d[i].IsChecked == true) {

                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StateID + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + " >" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                                else {
                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + " class='statecheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StateID + "   /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + ">" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                            }
                        }
                        $("#divModalBody").html(html);
                        $("#myModal").modal('show');
                    }

                }
            });

        }


        var statelist = []
        function STATEPUSHPOP() {
            var empID = $("#hdnEMPID").val();
            // debugger;
            //$('input:checkbox.statecheck').each(function () {

            //    var ischecked = $(this).is(':checked');
            //    if (ischecked == true) {
            //        alert(ischecked);
            //    }
            //});

            let a = [];

            $(".statecheckall:checked").each(function () {
                a.push(this.value);
            });

            $(".statecheck:checked").each(function () {
                a.push(this.value);
            });
            var str1
            //  alert(a);

            str1 = { EMPID: empID, Statelist: a }
            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetStateListSubmit",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    $("#myModal").modal('hide');
                    jAlert('State assigned successfully');
                }
            });
        }

        function fn_BranchMap(empID) {
            $("#hdnEMPID").val(empID);
            var str
            str = { EMPID: empID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetBranchList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    for (i = 0; i < responseFromServer.d.length; i++) {
                        //if (responseFromServer.d[i].IsChecked == true) {
                        //    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + " checked  /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].branch_id + " >" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        //}
                        //else {
                        //    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + " class='BranchMapcheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + "   /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].branch_id + ">" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        //}
                        if (responseFromServer.d[i].IsChecked == true) {
                            html += "<label class='custom-checkbox-single'>" + responseFromServer.d[i].branch_description + "<input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + " checked  > <span class='checkmark'></span></label>";
                        }
                        else {
                            html += "<label class='custom-checkbox-single'>" + responseFromServer.d[i].branch_description + "<input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + "  > <span class='checkmark'></span></label>";
                        }
                    }
                    $("#divModalBodyBranchMap").html(html);
                    $("#myModalBranchMap").modal('show');
                }
            });
        }

        var Branchlist = []
        function BranchPushPop() {
            var empID = $("#hdnEMPID").val();
            let a = [];

            $(".BranchMapcheckall:checked").each(function () {
                a.push(this.value);
            });

            $(".BranchMapcheck:checked").each(function () {
                a.push(this.value);
            });
            var str1
            //  alert(a);

            str1 = { EMPID: empID, Branchlist: a }
            $.ajax({
                type: "POST",
                url: "UserAccountList.aspx/GetBranchListSubmit",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    $("#myModalBranchMap").modal('hide');
                    jAlert('Branch assigned successfully');
                }
            });
        }

        function fn_DeleteEmp(keyValue) {

            if (keyValue != "378") {  // "EMV0000001"
                jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                    if (r == true) {
                        var str1
                        
                        str1 = { user_id: keyValue }

                        $.ajax({
                            type: "POST",
                            url: "UserAccountList.aspx/DeleteUser",
                            data: JSON.stringify(str1),
                            contentType: "application/json; charset=utf-8",
                            datatype: "json",
                            success: function (responseFromServer) {
                                // alert(responseFromServer.d)
                                if (responseFromServer.d == '1') {
                                    jAlert('Record deleted successfully');

                                    grid.Refresh();

                                }
                                else if (responseFromServer.d == '-1') {
                                    jAlert('Cannot Delete.This user has been assigned to Topic.')
                                }
                                else if (responseFromServer.d == '-10') {
                                    jAlert('Error in Delete.')
                                }
                                else {
                                    jAlert('Please Try Again Later.')
                                }
                            }
                        });
                    }
                    else {
                        return false;
                    }
                });
            } else {
                jAlert("Sorry, you can not delete the Admin.");
            }


        }

       

    </script>
</asp:Content>
<%--Rev work start .Refer: 25046 27.07.2022 New Listing page create for new Login Configuration Page--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
        <span>Login Configuration</span>
    </div>

    <div class="container mt-4">
        <div class="backBox p-3">
            <table class="TableMain100">              
                <tr>
                    <td>
                        <table width="100%" class="mb-3">
                            <tr>
                                <td style="text-align: left; vertical-align: top">
                                    <table>
                                        <tr>
                                            <td id="ShowFilter">
                                                <%if (rights.CanAdd)
                                                  { %>
                                                <a href="javascript:void(0);" onclick="AddUserDetails()" class="btn btn-success"><span>Add New</span> </a>
                                                <% } %>
                                                <% if (rights.CanExport)
                                                   { %>
                                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                                    <asp:ListItem Value="2">XLS</asp:ListItem>
                                                    <asp:ListItem Value="3">RTF</asp:ListItem>
                                                    <asp:ListItem Value="4">CSV</asp:ListItem>
                                                </asp:DropDownList>
                                                <% } %>                                                
                                            </td>                                           
                                        </tr>
                                    </table>
                                </td>
                                <td></td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="overflow-x-auto">
                            <%--Rev 2.0: grid column width increase--%>
                            <dxe:ASPxGridView ID="userGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="USER_ID" Width="100%" OnCustomJSProperties="userGrid_CustomJSProperties" SettingsBehavior-AllowFocusedRow="true"
                            Settings-HorizontalScrollBarMode="Auto" >
                            <Columns>
                                <%--<dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="UID"
                                    Caption="UID" Width="0%" SortOrder="Descending">
                                    <EditFormSettings></EditFormSettings>
                                </dxe:GridViewDataTextColumn>--%>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="USER_ID"
                                    Caption="User ID" Width="20%" >
                                    <EditFormSettings></EditFormSettings>
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="1" FieldName="USER_NAME"
                                    Caption="User Name" Width="20%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                            <RequiredField ErrorText="Please Enter user Name" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Caption="User Name:" Visible="True" />
                                </dxe:GridViewDataTextColumn>
                                
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="2" FieldName="BRANCHNAME"
                                    Caption="Branch" Width="20%" >
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>
                                <%--Rev 2.0 [ caption changed from Report To to WD ID--%>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="3" FieldName="REPORTTO"
                                    Caption="Report To" Width="20%">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>  
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="DEPARTMENTNAME"
                                    Caption="Department" Width="20%">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>  
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" FieldName="deg_designation"
                                    Caption="Designation" Width="20%">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="6" FieldName="GROUPNAME"
                                    Caption="Group" Width="10%">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>  
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="7" FieldName="user_inactive"
                                    Caption="Active" Width="10%">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   

                                <dxe:GridViewDataTextColumn VisibleIndex="8" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="20%">
                                
                                    <DataItemTemplate>
                                         <% if (rights.CanEdit)
                                            { %>
                                        <a href="javascript:void(0);" onclick="OnEditButtonClick('<%# Container.KeyValue %>')" title="More Info" class="pad">
                                            <img src="../../../assests/images/Edit.png" />
                                        </a>
                                          <% } %>

                                        <% if (rights.CanAdd)
                                        { %>
                                            <a href="javascript:void(0);" onclick="OnCopyInfoClick('<%# Container.KeyValue %>')" class="pad" title="Copy">
                                                <img src="../../../assests/images/copy2.png" /></a>
                                            </a>

                                            <a href="javascript:void(0);" onclick="EMPIDBind('<%#Eval("ContactID") %>')" title="Update Employee ID" class="pad" style="text-decoration: none;">
                                                <img src="../../../assests/images/update-id.png" />
                                            </a>

                                            <a href="javascript:void(0);" onclick="StateBind('<%#Eval("ContactID") %>')" title="State Mapping" class="pad" style="text-decoration: none;">
                                                <img src="../../../assests/images/state-mapping.png" />
                                            </a>

                                            
                                         <% } %>

                                        <% if (!ActivateEmployeeBranchHierarchy){ %>
                                            <a href="javascript:void(0);" onclick="fn_BranchMap('<%#Eval("cnt_id") %>')" class="pad" title="Branch Mapping">
                                                <img src="../../../assests/images/branch-mapping.png"  />
                                            </a>
                                        <% }  %>
                                            

                                         <% if (rights.CanDelete)
                                           { %>
                                         <a href="javascript:void(0);" onclick="fn_DeleteEmp('<%#Eval("UID") %>')" title="Active/Inactive User">
                                            <img src="../../../assests/images/Delete.png" /></a>
                                         <% } %>
                                    </DataItemTemplate>
                                    <HeaderTemplate>Actions</HeaderTemplate>
                                    <EditFormSettings Visible="False"></EditFormSettings>
                                </dxe:GridViewDataTextColumn>
                                                       
                            </Columns>                          
                            <SettingsSearchPanel Visible="True" />
                            <Settings ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="True" ShowFilterRowMenu="true" />                           
                            <SettingsBehavior ConfirmDelete="True" />
                            <ClientSideEvents EndCallback="function(s, e) { EndCall(s.cpHeight); }" />
                        </dxe:ASPxGridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>        
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>

    <div id="myEmpIDModal" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 450px;">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Update Employee ID</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-5">
                            <label id="lbloldEmpID" class="lblstate">OLD Employee ID :</label>
                        </div>
                        <div class="col-sm-7">
                            <label id="lblOLDEmpIDname" class="lblstate">9563218466</label>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-sm-5">
                            <label id="lblEmpIDname" class="lblstate">New Employee ID :</label>
                        </div>
                        <div class="col-sm-7">
                            <input type="text" id="txtEMPId" class="form-control" />
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="modal-footer" style="padding: 8px 26px 5px;">
                    <input type="button" id="btnEMPidSubmit" title="Update" value="Update" class="btn btn-primary" onclick="UpdateEmpId()" />
               
                     <input type="hidden" id="hdnEMPCode" class="btn btn-primary" />
                </div>
            </div>
        </div>

    </div>

    <div id="myModal" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 450px;">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">State List</h4>
                </div>
                <div class="modal-body">
                    <div>

                        <input type="text" id="myInput" onkeyup="myFunction()" placeholder="Search for States.">

                        <ul id="divModalBody" class="listStyle">
                            <%--<input type="checkbox" id="idstate" class="statecheck" /><label id="lblstatename" class="lblstate"></label>--%>
                        </ul>
                    </div>
                    <input type="button" id="btnsatesubmit" title="SUBMIT" value="SUBMIT" class="btn btn-primary" onclick="STATEPUSHPOP()" />
                    <input type="hidden" id="hdnstatelist" class="btn btn-primary" />
                    <input type="hidden" id="hdnEMPID" class="btn btn-primary" />
                </div>
            </div>

        </div>
    </div>

    <div id="myModalBranchMap" class="modal fade branch-list-modal" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 520px;">

            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearData();"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Branch List</h4>
                </div>
                <div class="modal-body">
                    <div>

                        <%--<input type="text" id="myInputBranchMap" onkeyup="myFunctionBranchMap()" placeholder="Search for Branch.">--%>

                        <div class="input-group flex-nowrap">
                          <div class="input-group-prepend">
                            <span class="input-group-text" id="addon-wrapping"><i class="fa fa-search"></i></span>
                          </div>
                          <input type="text" id="myInputBranchMap" onkeyup="myFunctionBranchMap()" class="form-control" placeholder="Search for Branch" aria-describedby="addon-wrapping">
                        </div>

                        <div id="divModalBodyBranchMap" class="listStyle">
                            <%--<input type="checkbox" id="idstate" class="statecheck" /><label id="lblstatename" class="lblstate"></label>--%>
                        </div>
                    </div>
                    <input type="button" id="btnBranchMapsubmit" title="SUBMIT" value="SUBMIT" class="btn btn-primary" onclick="BranchPushPop()" />
                    <%--<input type="hidden" id="hdnstatelist" class="btn btn-primary" />
                    <input type="hidden" id="hdnEMPID" class="btn btn-primary" />--%>
                </div>
               <%-- <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="ClearData();">Close</button>
            </div>--%>
            </div>

        </div>
    </div>
</asp:Content>