using System;
using DA_SKORKEL;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;


public partial class UserControl_Message : System.Web.UI.UserControl
{
    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();

    DataTable dt = new DataTable();
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string UserTypeId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
                if (ViewState["InvitedUserIds"] != null && Request.QueryString["GrpId"] != null)
                {
                    ViewState["InvitedUserId"] = ViewState["InvitedUserIds"];

                    ViewState["intGroupId"] = Request.QueryString["GrpId"];
                    BinduserSearch();
                }
                else
                {
                    dvPopup.Style.Add("display", "none");
                }
            }
        }
    }

    protected void BinduserSearch()
    {
        objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["InvitedUserId"]);      
        objGrpJoinDO.strSearch = "";

        dt.Clear();
        dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetPopupGroupMemberDtls);

        if (dt.Rows.Count > 0)
        {
            ViewState["messageByUserId"] = dt.Rows[0]["GroupOwnerRegId"];
            ViewState["messageToUserId"] = dt.Rows[0]["intRegistrationId"];
        }

    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        WallMessageDO.intInvitedUserId = Convert.ToInt32(ViewState["InvitedUserId"]);
        WallMessageDO.striInvitedUserId = Convert.ToString(ViewState["InvitedUserId"]);
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            WallMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }
        else
        {
            return;
        }

        WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        WallMessageDO.StrRecommendation = txtBody.InnerHtml;
        WallMessageDO.strSubject = txtSubject.Text.Trim().Replace("'", "''");
        WallMessageDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
        WallMessageDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (WallMessageDO.strIpAddress == null)
            WallMessageDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);

        lblSuccess.Text = "Message sent successfully.";
        lblSuccess.ForeColor = System.Drawing.Color.Green;
        divSuccessMess.Style.Add("display", "block");

        try
        {
            string UserURL = "";
            if (ISAPIURLACCESSED == "1")
            {
                UserURL = APIURL + "massageToUser.action?" +
                           "messageByUserId=USR" + ViewState["messageByUserId"] +
                           "&messageToUserId=USR" + ViewState["intRegistrationId"] +
                           "&message=" + WallMessageDO.StrRecommendation;

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = UserURL;
                objAPILogDO.strAPIType = "Group Member";
                objAPILogDO.strResponse = result;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
        }
        catch { }

        dvPopup.Style.Add("display", "none");
        Clear();
        //Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
    }

    protected void Clear()
    {
        //lblTo.Text = "";
        //lblFrom.Text = "";
        txtSubject.Text = "";
        txtBody.InnerText = "";
    }
}