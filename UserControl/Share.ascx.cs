using System;
using System.Data;
using DA_SKORKEL;

public partial class UserControl_Share : System.Web.UI.UserControl
{
    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DA_GroupUserStatus objGrpstatusDA = new DA_GroupUserStatus();
    DO_GroupUserStatus objGrpstatusDO = new DO_GroupUserStatus();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserStatusUpdateTbl objstatusDO = new DO_Scrl_UserStatusUpdateTbl();
    DA_Scrl_UserStatusUpdateTbl objstatusDA = new DA_Scrl_UserStatusUpdateTbl();


    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
                //BindExperience();
            }
            GrpPopUpShare.Style.Add("display", "none");
            GetShareInviteeName();
            if (Request.QueryString["ShareId"] != null)
            {
                int ShareId = Convert.ToInt32(Request.QueryString["ShareId"]);
                GrpPopUpShare.Style.Add("display", "block");
            }
            if (Request.QueryString["StatusId"] != null)
            {
                int StatusId = Convert.ToInt32(Request.QueryString["StatusId"]);
                GrpPopUpShare.Style.Add("display", "block");
            }
            if (Request.QueryString["OrgStatusId"] != null)
            {
                int StatusId = Convert.ToInt32(Request.QueryString["OrgStatusId"]);
                GrpPopUpShare.Style.Add("display", "block");
            }
        }
        else
        {
            GrpPopUpShare.Style.Add("display", "none");
        }
    }

    protected void GetShareInviteeName()
    {
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dt = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);

        //objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);//Convert.ToInt32(ViewState["intGroupId"]);
        //dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetTotalGrpJoinId);
        if (dt.Rows.Count > 0)
        {
            txtInviteMembers.DataValueField = "intInvitedUserId";
            txtInviteMembers.DataTextField = "Name";
            txtInviteMembers.DataSource = dt;
            txtInviteMembers.DataBind();
        }
        string path = Request.Url.AbsoluteUri;
        txtLink.Text = path;
    }

    //protected void GetShareInviteeName()
    //{
    //    objGrpJoinDO.inGroupId =Convert.ToInt32(Request.QueryString["GrpId"]);//Convert.ToInt32(ViewState["intGroupId"]);
    //    dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetTotalGrpJoinId);
    //    if (dt.Rows.Count > 0)
    //    {          
    //        txtInviteMembers.DataValueField = "intAddedBy";
    //        txtInviteMembers.DataTextField = "Name";
    //        txtInviteMembers.DataSource = dt;
    //        txtInviteMembers.DataBind();
    //    }                
    //}

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ShareId"] != null)
        {
            if (hdnInvId.Value != null && hdnInvId.Value != "")
            {
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objDOBForumPosting.strIpAddress = ip;
                objDOBForumPosting.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                objDOBForumPosting.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
                objDOBForumPosting.intForumPostingId = Convert.ToInt32(Request.QueryString["ForumPostingId"]);
                objDOBForumPosting.intSharedPostingId = Convert.ToInt32(Request.QueryString["SharedPostingId"]); //Convert.ToInt32(ViewState["intSharedPostingId"]);
                objDOBForumPosting.strRepLiShStatus = "SH";
                string id = hdnInvId.Value;
                objDOBForumPosting.strInvitee = hdnInvId.Value;
                objDOBForumPosting.inviteMembers = hdnInvId.Value;
                if (txtBody.InnerText.Trim() != "Message")
                    objDOBForumPosting.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
                if (txtLink.Text.Trim() != "Paste link")
                    objDOBForumPosting.strLink = txtLink.Text.Trim().Replace("'", "''");

                objDAForumPosting.AddEditDel_Scrl_UserForumPostingTbl(objDOBForumPosting, DA_Scrl_UserForumPosting.Scrl_UseForumPosting.InsertShare);
                GrpPopUpShare.Style.Add("display", "none");
                if(Convert.ToInt32(Request.QueryString["GrpId"])==0)
                {
                    Response.Redirect("OrgForumDetails.aspx?GrpId=" + Request.QueryString["GrpId"] + "&ForumId=" + Request.QueryString["ForumId"] + "&orgid=" + Request.QueryString["orgid"]);

                }else
                {
                Response.Redirect("forum-landing-page.aspx?GrpId=" + Request.QueryString["GrpId"]);
                }
            }
            else
            {
                lblMess.Text = "Please select member";
                GrpPopUpShare.Style.Add("display","block");

            }
        }
        else if (Request.QueryString["StatusId"] != null)
        {
            if (hdnInvId.Value != null && hdnInvId.Value != "")
            {
                objGrpstatusDO.strInvitee = hdnInvId.Value;
                if (txtBody.InnerText.Trim() != "Message")
                    objGrpstatusDO.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
                if (txtLink.Text.Trim() != "Paste link")
                    objGrpstatusDO.strLink = txtLink.Text.Trim().Replace("'", "''");
                objGrpstatusDO.intStatusUpdateId = Convert.ToInt32(Request.QueryString["StatusId"]);
                objGrpstatusDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objGrpstatusDO.strIpAddress = ip;
                objGrpstatusDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
                objGrpstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objGrpstatusDO, DA_GroupUserStatus.GropUserStatusUpdate.Share);
                clear();
                GrpPopUpShare.Style.Add("display", "none");
                Session["SubmitTime"] = DateTime.Now.ToString();
            }
            else
            {
                lblMess.Text = "Please select member";
                GrpPopUpShare.Style.Add("display", "block");
            }
        }
        else if (Request.QueryString["OrgStatusId"] != null)
        {
            if (hdnInvId.Value != null && hdnInvId.Value != "")
            {
                objstatusDO.strInvitee = hdnInvId.Value;
                if (txtBody.InnerText.Trim() != "Message")
                    objstatusDO.strMessage = txtBody.InnerText.Trim().Replace("''", "''");
                if (txtLink.Text.Trim() != "Paste link")
                    objstatusDO.strLink = txtLink.Text.Trim().Replace("''", "''");
                objstatusDO.intStatusUpdateId = Convert.ToInt32(Request.QueryString["OrgStatusId"]);
                objstatusDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objstatusDO.strIpAddress = ip;
                objstatusDA.AddEditDel_Scrl_OrgStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_OrgStatusUpdateTbl.Share);
                clear();
                GrpPopUpShare.Style.Add("display", "none");
            }
            else
            {
                lblMess.Text = "Please select member";
                GrpPopUpShare.Style.Add("display", "block");
            }

        }
        // }
       // ehClick.Invoke(this, new EventArgs());
    }

    protected void clear()
    {
        txtInviteMembers.Controls.Clear();
        txtBody.InnerText = "";
        txtLink.Text = "";
        lblMess.Text = "";
        GetShareInviteeName();
    }

    protected void btnpopCancel_Click(object sender, EventArgs e)
    {
        GrpPopUpShare.Style.Add("display", "none");

    }
}