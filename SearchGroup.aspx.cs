﻿using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Linq;
using System.Web.UI;
using System.Net.Mime;

public partial class SearchGroup : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    string RequestType = string.Empty;

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_UserPostUpdateTbl objpost = new DO_Scrl_UserPostUpdateTbl();
    DA_Scrl_UserPostUpdateTbl objpostDB = new DA_Scrl_UserPostUpdateTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserFollowGroup objfollowDO = new DO_Scrl_UserFollowGroup();
    DA_Scrl_UserFollowGroup objfollowDA = new DA_Scrl_UserFollowGroup();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        if (!IsPostBack)
        {
            //divFollUnfollPopup.Style.Add("display", "none");
            //divConnDisPopup.Style.Add("display", "none");

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "12";

            if (Request.QueryString["Id"] != "" && Request.QueryString["Id"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Request.QueryString["Id"]);
            }

            if (Request.QueryString["ViewAll"] == "1")
            {
                ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
            BindCity();
            SubjectSearchTempDataTable();
        }
    }

    #region Search

    protected void BindSingleData()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objpost.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objpost.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            objpost.strSearch = "";
            if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                objpost.strCityType = hdnTempUserId.Value;

            dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetMyGroupListkk);

            if (dt.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                lstGroup.DataSource = dt;
                lstGroup.DataBind();
                dvPage.Visible = true;
                BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
            }
            else
            {
                lblMessage.Visible = true;
                lstGroup.DataSource = null;
                lstGroup.DataBind();
                dvPage.Visible = false;
            }
        }
    }

    protected void BinduserSearch()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objpost.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objpost.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            //objpost.strSearch = "";

            if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "" && (!Page.IsPostBack))
            {
                string srch = Convert.ToString(Request.QueryString["srch"]);
                txtGrpSearch.Text = srch;
                hdnSearch.Value = srch;
            }


            objpost.strSearch = txtGrpSearch.Text;
            
            if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                objpost.strCityType = hdnTempUserId.Value;

            dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetGroupSearchDetails);

            if (dt.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                lstGroup.DataSource = dt;
                lstGroup.DataBind();

                dvPage.Visible = true;
                BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));

            }
            else
            {
                lblMessage.Visible = true;
                lstGroup.DataSource = null;
                lstGroup.DataBind();
                dvPage.Visible = false;
            }
            hdnSearch.Value = null;
            //txtPplSearch.Text = "";
        }
    }

    protected void BindCity()
    {
        dt.Clear();
        dt = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);

        lstSerchSubjCategory.selectPlaceholder = "Select contexts to filter";
        if (dt.Rows.Count > 0)
        {
            lstcity.DataSource = dt;
            lstcity.DataBind();

            lstSerchSubjCategory.DataSource = dt;
            lstSerchSubjCategory.DataTextField = "strCategoryName";
            lstSerchSubjCategory.DataValueField = "intCategoryId";

            //lstSerchSubjCategory.DataBind();
            lstSerchSubjCategory.RefreshList();
        } else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.RefreshList();
        }
    }

    protected void lstGroup_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        LinkButton btnjoinreq = dataItem.FindControl("btnjoinreq") as LinkButton;
        LinkButton btnDelete = dataItem.FindControl("btnDelete") as LinkButton;
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblGroupMember = (Label)e.Item.FindControl("lblGroupMember");
        HtmlControl Grid = (HtmlControl)e.Item.FindControl("Grid");
        DataRowView drv = (DataRowView)e.Item.DataItem;
        if (ViewState["check"] != null)
        {
            if (ViewState["hdnId"] != null)
            {
                if (ViewState["hdnId"].ToString() == hdnId.Value)
                {
                    Grid.Visible = false;
                }
            }
        }
        ViewState["hdnId"] = hdnId.Value;

        if (string.IsNullOrEmpty(drv["GroupMembers"].ToString()))
        {
            lblGroupMember.Text = "1 member";
        }
        else
        {
            int i = Convert.ToInt32(drv["GroupMembers"].ToString());
            lblGroupMember.Text = (i + 1).ToString() + " members";
        }
        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/groupPhoto.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/" + imgprofile.Src);
            if (!File.Exists(imgPathPhysical))
            {
                imgprofile.Src = "images/groupPhoto.jpg";
            }
        }
        if (hdnRegistrationId.Value == Convert.ToString(Session["ExternalUserId"]))
        {
            btnjoinreq.Visible = false;
            btnDelete.Visible = true;
            return;
        }
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objgrp.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objgrp.inGroupId = Convert.ToInt32(hdnId.Value);
            DataSet ds = new DataSet();
            ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                {
                    btnjoinreq.Text = "Join";
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                {
                    btnjoinreq.Text = "Leave";
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                {
                    btnjoinreq.Text = "Join";
                }
            }
            else
            {
                btnjoinreq.Text = "Join";
            }
        }
    }

    protected void lstGroup_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        LinkButton lblPostlink = (LinkButton)e.Item.FindControl("lblPostlink");
        LinkButton btnjoinreq = (LinkButton)e.Item.FindControl("btnjoinreq");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        Label lblUserName = (Label)e.Item.FindControl("lblUserName");
        hdnfullname.Value = lblUserName.Text;
        hdnEmailId.Value = lblEmailId.Text;
        ViewState["hdnId"] = Convert.ToString(hdnId.Value);
        ViewState["btnjoinreq"] = btnjoinreq.Text;
        ViewState["chkRegId"] = hdnRegistrationId.Value;
        divSuccess.Style.Add("display", "none");
        if (e.CommandName == "Details")
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + hdnId.Value);
        }

        if (e.CommandName == "JoinGroup")
        {
            divFollUnfollPopup.Style.Add("display", "none");

            if (btnjoinreq.Text == "Join")
            {
                lblConnDisconn.Text = "Do you want to join ?";
            }
            else
            {
                lblConnDisconn.Text = "Do you want to leave ?";
            }
            divConnDisPopup.Style.Add("display", "block");

        }

        else if (e.CommandName == "DeleteGroup")
        {

            objgrp.inGroupId = Convert.ToInt32(hdnId.Value);
            objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Delete);
            BinduserSearch();
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(hdnId.Value);
            objLog.strAction = "Group";
            objLog.strActionName = lblPostlink.Text;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 26;   // Group Delete
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        objgrp.inGroupId = Convert.ToInt32(hdnGrpId.Value);
        objgrpDB.AddEditDel_Scrl_UserGroupDetailTbl(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Delete);
        BinduserSearch();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(hdnGrpId.Value);
        objLog.strAction = "Group";
        objLog.strActionName = hdnGrpName.Value;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 26;   // Group Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "block");
        int InvitedUserId = Convert.ToInt32(ViewState["RegId"]);
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objgrp.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objgrp.inGroupId = Convert.ToInt32(ViewState["hdnId"]);
            DataSet ds = new DataSet();
            ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);
            string ownermailId = ds.Tables[0].Rows[0]["vchrUserName"].ToString();
            string ownerNmae = ds.Tables[0].Rows[0]["Name"].ToString();
            ViewState["strGroupName"] = ds.Tables[0].Rows[0]["strGroupName"].ToString();
            ViewState["GetGroupDetailsByGroupId"] = ds;
            if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
            {

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }

                    else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                    {
                        objGrpJoinDO.isAccepted = 2;
                    }

                    else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }
                }
                else
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }
                }
            }

            else if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "R")
            {

                objGrpJoinDO.inGroupId = Convert.ToInt32(ds.Tables[0].Rows[0]["inGroupId"]);
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(ds.Tables[0].Rows[0]["intRegistrationId"]);
                objGrpJoinDO.intInvitedUserId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.UpdateGroupMembers);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                    {
                        objGrpJoinDO.isAccepted = 1;
                        lblSuccess.Text = "You have already send the group joining request.";
                        //lblSuccess.ForeColor = System.Drawing.Color.Red;
                        divConnDisPopup.Style.Add("display", "none");
                        divSuccess.Style.Add("display", "block");
                        return;
                    }

                    else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                    {
                        objGrpJoinDO.isAccepted = 2;
                        ViewState["isAceptedMembers"] = 1;
                    }

                    else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                    {
                        objGrpJoinDO.isAccepted = 0;
                        ViewState["isAceptedMembers"] = 2;
                    }
                }
            }

            objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["hdnId"]);
            objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ds.Tables[0].Rows[0]["intRegistrationId"]);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objGrpJoinDO.strIpAddress = ip;
            objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());

            objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);

            if (Convert.ToString(ViewState["btnjoinreq"]) == "Join")
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                {
                    SendGroupMail(ownermailId, ownerNmae);
                    divSuccess.Style.Add("display", "block");
                    lblSuccess.Text = "Successfully joined the Group and a mail has been sent to the group owner.";
                    //lblSuccess.ForeColor = System.Drawing.Color.Green;

                    if (Request.QueryString["ViewAll"] == "1")
                    {
                        ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                        BindSingleData();
                    }
                    else
                    {
                        BinduserSearch();
                    }
                }
                else
                {
                    SendGroupMail(ownermailId, ownerNmae);
                    if (Request.QueryString["ViewAll"] == "1")
                    {
                        ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                        BindSingleData();
                    }
                    else
                    {
                        BinduserSearch();
                    }

                    divSuccess.Style.Add("display", "block");
                    lblSuccess.Text = "Group joining request and request mail has been send to the group owner.";
                    //lblSuccess.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                RequestType = "Send UnJoin request";
                if (Request.QueryString["ViewAll"] == "1")
                {
                    ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                    BindSingleData();
                }
                else
                {
                    BinduserSearch();
                }
                divSuccess.Style.Add("display", "block");
                lblSuccess.Text = "Group left successfully.";
                //lblSuccess.ForeColor = System.Drawing.Color.Green;
            }
        }
        divConnDisPopup.Style.Add("display", "none");
    }
   
    protected void btnGroupSearch_Click(object sender, EventArgs e)
    {
        hdnSearch.Value = txtGrpSearch.Text.Trim();
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "12";
        BinduserSearch();
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objfollowDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }
        else
        {
            return;
        }

        if (Convert.ToString(Session["ExternalUserId"]) != Convert.ToString(ViewState["chkRegId"]))
        {
            objfollowDO.intGroupId = Convert.ToInt32(ViewState["hdnId"]);
            objfollowDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objfollowDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objfollowDO.strIpAddress == null)
                objfollowDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];

            DataTable dtfollow = new DataTable();
            dtfollow = objfollowDA.GetDataTable(objfollowDO, DA_Scrl_UserFollowGroup.Scrl_UserFollowGroup.SingleRecord);

            int followstatus = 0;
            if (dtfollow.Rows.Count > 0)
            {
                objfollowDO.intFollowId = Convert.ToInt32(dtfollow.Rows[0]["intFollowId"]);
                followstatus = Convert.ToInt32(dtfollow.Rows[0]["intFollowStatus"]);
                objfollowDA.Scrl_AddEditDelFollowGroup(objfollowDO, DA_Scrl_UserFollowGroup.Scrl_UserFollowGroup.Update);

                if (followstatus == 1)
                {
                    divSuccess.Style.Add("display", "block");
                    lblSuccess.Text = "Unfollow successfully.";
                    //lblSuccess.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    divSuccess.Style.Add("display", "block");
                    lblSuccess.Text = "Follow successfully.";
                   // lblSuccess.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                objfollowDA.Scrl_AddEditDelFollowGroup(objfollowDO, DA_Scrl_UserFollowGroup.Scrl_UserFollowGroup.Add);
                divSuccess.Style.Add("display", "block");
                lblSuccess.Text = "Follow successfully.";
               // lblSuccess.ForeColor = System.Drawing.Color.Green;
            }

            if (Request.QueryString["ViewAll"] == "1")
            {
                ViewState["ViewAll"] = Request.QueryString["ViewAll"];
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('You could not follow your own group.');</script>");
        }
        divFollUnfollPopup.Style.Add("display", "none");
    }

    #endregion

    #region Send Mails
    private void SendMail()
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];

            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = hdnEmailId.Value;
            string Mailbody = "";
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;

            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Joining Request</b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have a Skorkel joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel Invitation";
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();

            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void SendMail(string RequestType)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];

            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];

            string MailTo = hdnEmailId.Value;
            string Mailbody = "";
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (RequestType == "Send Recommendation")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation </b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have a Skorkel recommendation request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Recommendation";
                }
                else if (RequestType == "Ask Recommendation")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask For Recommendation </b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have been asked for a Skorkel recommendation request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Recommendation";
                }
                else if (RequestType == "Send Request")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Joining Request</b>" + "<br><br>" + " " + hdnfullname.Value + " " + "You have a Skorkel joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Invitation";
                }
                else if (RequestType == "Send Leave request")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel's Leave</b>" + "<br><br>" + " " + hdnfullname.Value + " " + "Your group is Leaved by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel Leave Invitation";
                }
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();

            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void SendGroupMail(string mailid, string name)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;
            DataSet ds1 = new DataSet();
            ds1 = (DataSet)ViewState["GetGroupDetailsByGroupId"];

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            
            string MailTo = mailid;
            string Mailbody = "";

            dtRecord.Clear();
            objdoreg.UserName = mailid;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);

            NetworkCredential cre = new NetworkCredential(username, Password);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            clientip.Credentials = cre;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");

                if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "A")
                {
                    //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining</b>"
                    //    + "<br><br>" + "Dear " + name + "<br><br> "
                    //    + "Your " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group has been joined by " +
                    //     Session["LoginName"] + ".<br><br>"
                    //     + "Regards," + "<br>"
                    //     + "Skorkel Team"
                    //     + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    string MailUrlFinal = MailURL + "/groups-members.aspx?GrpId=" + ds1.Tables[0].Rows[0]["inGroupId"];
                    body = body.Replace("{UserName}", name);
                    body = body.Replace("{Content}", "Your <b>" + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + "</b> group has been joined by <b>" +
                         Session["LoginName"] + "</b>.<br>" +
                         "Please click below to view group details: " +
                          "<br><br><a href = '" + MailUrlFinal + "' style='background:#01B7BD;padding:5px 10px;border-radius:15px;color:#fff;text-decoration: none;'>View Details</a>");
                    body = body.Replace("{RedirectURL}", MailURL);

                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    //Rmm2.Subject = "Skorkel group joining.";
                    Rmm2.Subject = "Skorkel group joining " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"];

                    FCMNotification.Send("Skorkel group joining " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"], "Your " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group has been joined by " + Session["LoginName"] + ".",
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailUrlFinal);
                }
                else if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "R")
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    string MailUrlFinal = MailURL + "/groups-members.aspx?GrpId=" + ds1.Tables[0].Rows[0]["inGroupId"];
                    body = body.Replace("{UserName}", name);
                    body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + Session["LoginName"] + "</span> wants to join  <span style='font-weight:bold;font-size:14px;color:#373A36;'>"  + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"])+ "</span>" +
                            "<br><br><a href='" + MailUrlFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Accept</a>");
                    body = body.Replace("{RedirectURL}", MailURL);

                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Joining Request</b>" + "<br><br>" + "Dear " + name + "<br> <br>"
                    //    + Session["LoginName"] + " request you to allow joining "
                    //    + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group.<br><br>"
                    //    + "Regards," + "<br>" + "Skorkel Team"
                    //    + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Request to Join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"];

                    //string MailUrlFinal = MailURL + "/Profile2.aspx?RegId=" + Convert.ToString(ViewState["UserID"]);

                    FCMNotification.Send("Request to Join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"], Session["LoginName"] + " wants to join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]),
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailUrlFinal);
                }
                else if (RequestType == "Send UnJoin request")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group unjoining invitation</b>" + "<br><br>" + "Dear " + name + "<br> " + "Your group has been unjoined by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel group unjoining notification";
                }



                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();
            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region Checkbox details

    protected void Location_CheckedChange(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
        divFollUnfollPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        ViewState["check"] = "check";
        int chkStatus = 0;
        hdnTempUserId.Value = "";
        hdnCurrentPage.Value = "1";
        for (int i = 0; i < lstcity.Items.Count; i++)
        {
            // Find the checkbox to determine if it's checked.
            ListViewDataItem items = lstcity.Items[i];
            CheckBox chkBox = (CheckBox)items.FindControl("ChkCity");
            HiddenField hdnCityId = items.FindControl("hdnCityId") as HiddenField;
            if (chkBox.Checked == true)
            {
                chkStatus = 1;
                if (string.IsNullOrEmpty(hdnTempUserId.Value))
                    hdnTempUserId.Value = Convert.ToString(hdnCityId.Value);
                else
                    hdnTempUserId.Value += "," + Convert.ToString(hdnCityId.Value);
            }
        }

        if (chkStatus == 0)
            hdnTempUserId.Value = "";

        if (Request.QueryString["ViewAll"] == "1")
        {
            ViewState["ViewAll"] = Request.QueryString["ViewAll"];
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Mumbai_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Pune_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Delhi_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void Bangalore_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void CorporateLaw_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void CriminalLaw_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void FamilyLaw_CheckedChange(object sender, EventArgs e)
    {
        ViewState["check"] = "check";
        CheckedPeople();
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }


    protected void CheckedPeople()
    {
        divConnDisPopup.Style.Add("display", "none");
        divFollUnfollPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
    }

    // protected void imgReset_Click(object sender, ImageClickEventArgs e)
    protected void imgReset_Click(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
        divFollUnfollPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        BindCity();
        hdnTempUserId.Value = "";
        ViewState["SubjectSearchCategory"] = null;
        ViewState["SearchMId"] = null;
        SubjectSearchTempDataTable();
        ChkCorporateLaw.Checked = false;
        ChkCriminalLaw.Checked = false;
        ChkFamilyLaw.Checked = false;
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "12";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    #endregion

    #region Paging For All

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {

        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;

            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);

            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;    // ((40 /10) - 1) * 10

            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;


            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = EndPage.ToString();

            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPage.Value) == totalPage)
                {
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }

            if (totalPage == 1)
            {
                lnkPrevious.Visible = false;
                lnkFirst.Visible = false;

                lnkNext.Visible = false;
                lnkLast.Visible = false;

                rptDvPage.DataSource = null;
                rptDvPage.DataBind();
            }
            else
            {
                DataTable dtPage = new DataTable();
                DataColumn PageNo = new DataColumn();
                PageNo.DataType = System.Type.GetType("System.String");
                PageNo.ColumnName = "intPageNo";
                dtPage.Columns.Add(PageNo);

                for (Int64 i = StartPage; i <= EndPage; i++)
                {
                    dtPage.Rows.Add(i.ToString());
                    hdnLastPage.Value = i.ToString();
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > 1)
                {
                    lnkFirst.Visible = true;
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                    lnkFirst.Visible = true;

                }
                if (CurrentPage == totalPage)
                {
                    lnkNext.Visible = false;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                    lnkLast.Visible = true;
                }
                else
                {
                    lnkNext.Visible = true;
                    lnkLast.Visible = true;
                }
            }
        }

    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }
        else if (Convert.ToInt32(hdnCurrentPage.Value) < Convert.ToInt32(hdnNextPage.Value))
        {
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");

        hdnCurrentPage.Value = "1";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");

        hdnCurrentPage.Value = hdnLastPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");

        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindSingleData();
            }
            else
            {
                BinduserSearch();
            }
        }
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PageLink")
        {
            divFollUnfollPopup.Style.Add("display", "none");
            divConnDisPopup.Style.Add("display", "none");
            divSuccess.Style.Add("display", "none");

            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            HiddenField hdnItemCount = (HiddenField)e.Item.FindControl("hdnItemCount");

            if (lnkPageLink != null)
            {
                divSuccess.Style.Add("display", "none");
                divFollUnfollPopup.Style.Add("display", "none");
                divConnDisPopup.Style.Add("display", "none");
                hdnCurrentPage.Value = lnkPageLink.Text;

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }

                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    BindSingleData();
                }
                else
                {
                    BinduserSearch();
                }
            }
        }
    }

    protected void rptDvPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            HiddenField hdnItemCount = (HiddenField)e.Item.FindControl("hdnItemCount");

            if (lnkPageLink != null)
            {
                if (hdnCurrentPage.Value == lnkPageLink.Text)
                {
                    lnkPageLink.Enabled = false;
                    if (ViewState["lnkPageLink"] != null)
                    {
                        if (lnkPageLink.Text == "1")
                        {
                            ViewState["lnkPageLink"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLink.Enabled = true;
                }
                if (hdnCurrentPage.Value == "1")
                {
                    ViewState["lnkPageLink"] = "PageCount";
                }
            }
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        divFollUnfollPopup.Style.Add("display", "none");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
    }

    #endregion

    protected void SubjectSearchTempDataTable()
    {
        //Subjects
        DataTable dtSubjCat = new DataTable();

        DataColumn SubjId = new DataColumn();
        SubjId.DataType = System.Type.GetType("System.String");
        SubjId.ColumnName = "intCategoryId";
        dtSubjCat.Columns.Add(SubjId);

        DataColumn SubjCat = new DataColumn();
        SubjCat.DataType = System.Type.GetType("System.String");
        SubjCat.ColumnName = "strCategoryName";
        dtSubjCat.Columns.Add(SubjCat);

        DataRow rwSubj = dtSubjCat.NewRow();
        ViewState["SubjectSearchCategory"] = dtSubjCat;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "12";

        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> subJectValues = lstSerchSubjCategory.GetSelectedValues();
        string[] subJectCateValues = subJectValues.Select(s => s.Value).ToArray();

        hdnTempUserId.Value = "";

        if (subJectCateValues.Count() > 0)
        {
            hdnTempUserId.Value = String.Join(",", subJectCateValues);
        }

        if (Request.QueryString["ViewAll"] == "1")
        {
            ViewState["ViewAll"] = Request.QueryString["ViewAll"];
            BindSingleData();
        }
        else
        {
            BinduserSearch();
        }
    }
}