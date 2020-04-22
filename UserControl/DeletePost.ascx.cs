using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Web;

public partial class UserControl_DeletePost : System.Web.UI.UserControl
{
    DA_GroupUserStatus objGrpstatusDA = new DA_GroupUserStatus();
    DO_GroupUserStatus objGrpstatusDO = new DO_GroupUserStatus();

    DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
    DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            ViewState["orgid"] = Convert.ToInt32(Request.QueryString["orgid"]);
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]) || !string.IsNullOrEmpty(Request.QueryString["OrgPostId"]))
            {
                int PostId = Convert.ToInt32(Request.QueryString["PostId"]);
                divDeletePopup.Style.Add("display", "block");
                divMessSucces.Style.Add("display", "none");
            }
        }
        else
        {
            divDeletePopup.Style.Add("display", "none");
            divMessSucces.Style.Add("display", "none");
        }
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (Request.QueryString["PostId"] != null)
        {
            objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(Request.QueryString["PostId"]);
            objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.Delete);

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(Request.QueryString["PostId"]);
            objLog.strAction = "Group Wall Post";
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 2;   // Group Wall Post 
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

            divDeletePopup.Style.Add("display", "none");
            divMessSucces.Style.Add("display", "block");
            Response.Redirect("Group-Home.aspx?GrpId=" + objGrpstatusDO.intGroupId);
        }
        else if (Request.QueryString["OrgPostId"] != null)
        {
            objstatusDO.intStatusUpdateId = Convert.ToInt32(Request.QueryString["OrgPostId"]);
            objstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objstatusDA.AddEditDel_Scrl_OrgStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_OrgStatusUpdateTbl.Delete);

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(Request.QueryString["OrgPostId"]);
            objLog.strAction = "Organisation Group Wall Post";
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 3;   // Organisation Group Wall Post 
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

            divDeletePopup.Style.Add("display", "none");
            divMessSucces.Style.Add("display", "block");
            Response.Redirect("OrgGroupHomDetails.aspx?GrpId=" + objstatusDO.intGroupId + "&orgid=" + ViewState["orgid"]);
        }

        //objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Delete);
        //BindPostUpdate();
        //lblMessage.Text = "Post removed successfully.";
        //lblMessage.ForeColor = System.Drawing.Color.Green;
        //divCancelPopup.Style.Add("display", "none");
    }
}