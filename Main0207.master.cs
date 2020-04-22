using DA_SKORKEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main1 : System.Web.UI.MasterPage
{
    DO_Scrl_UserSendMessage objMessageDO = new DO_Scrl_UserSendMessage();
    DA_Scrl_UserSendMessage objMessageDA = new DA_Scrl_UserSendMessage();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            //    form1.Action = QueryStringModule.Encrypt(Request.Url.Query.ToString().Replace("?", ""));
            //    ViewState["orgid"] = Convert.ToInt32(Request.QueryString["orgid"]);
            //    if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            //    {
            //        ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            //    }
            //    else
            //    {
            //        Response.Redirect("Landing.aspx", true);
            //    }
            //lblUserName.InnerText = Convert.ToString(Session["LoginName
            
            BindMessages();
        }
    }

    protected void BindMessages()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            lblInboxCount.Text = "";
            objMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtmessage = objMessageDA.GetDataTable(objMessageDO, DA_Scrl_UserSendMessage.Scrl_UserSendMessage.AllRecords);
            if (dtmessage.Rows.Count > 0)
            {
                ViewState["MaxCount"] = dtmessage.Rows[0]["Maxcount"].ToString();
                lblInboxCount.Text = dtmessage.Rows[0]["Maxcount"].ToString();
            }
            else
            {
                lblInboxCount.Text = "0";

            }
        }
        else
        {
            lblInboxCount.Text = "0";
        }
    }

    protected void lstNotification_ItemCommand(object sender, EventArgs e)
    {

    }
    protected void lstNotification_ItemDataBound(object sender, EventArgs e)
    {

    }
    protected void lstNotification_ItemDeleting(object sender, EventArgs e)
    {

    }

    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        objLogin.intRegistartionID = Convert.ToInt32(Session["ExternalUserId"]);
        if (objLogin.intRegistartionID != 0)
         objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Logout);

        Session.Abandon();
        Response.Redirect("Landing.aspx");
    }
}
