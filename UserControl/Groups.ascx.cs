using System;
using System.Data;
using DA_SKORKEL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Net.Mime;

public partial class UserControl_Groups : System.Web.UI.UserControl
{
    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_GroupShare objGroupShareDA = new DA_GroupShare();
    DO_GroupShare objGroupShareDO = new DO_GroupShare();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();
    DataTable dt = new DataTable();
    string RequestType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divConnDisPopup.Style.Add("display", "none");
            divGroupMessPopup.Style.Add("display", "none");

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
                Session["intGroupId"] = Request.QueryString["GrpId"];
                GetGroupDetails();
                GetGroupMembers();
            }


            PopUpShare.Style.Add("display", "none");
            GetInviteeName();
            GetTotalJoin();
        }
    }

    protected void GetTotalJoin()
    {
        string TotalGrpJoinMemID = "";
        objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetTotalGrpJoinId);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int MemId = Convert.ToInt32(dt.Rows[i]["intRegistrationId"]);
                TotalGrpJoinMemID = TotalGrpJoinMemID + "," + MemId;
                ViewState["TotalGrpJoinMemID"] = TotalGrpJoinMemID;
            }
        }
    }

    protected void GetGroupDetails()
    {
        objgrp.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        Session["GroupOwnerId"] = Convert.ToInt32(ViewState["intGroupId"]);

        DataSet ds = new DataSet();
        ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["GetGroupDetailsByGroupId"] = ds;

            if (ds.Tables[0].Rows[0]["strLogoPath"].ToString() != "" && ds.Tables[0].Rows[0]["strLogoPath"].ToString() != string.Empty)
            {
                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(ds.Tables[0].Rows[0]["strLogoPath"]));
                    if (File.Exists(imgPathPhysical))
                    {
                        imgGrp.Src = "~\\CroppedPhoto\\" + Convert.ToString(ds.Tables[0].Rows[0]["strLogoPath"]);
                    }
                    else
                    {
                        imgGrp.Src = "~/images/photo1.png";
                    }
            }
            else
                imgGrp.Src = "~/images/photo1.png";
            ViewState["GrpOwnerID"] = Convert.ToString(ds.Tables[0].Rows[0]["intRegistrationId"]);
            lblOwner.Text = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);

            lblGroupName.Text = Convert.ToString(ds.Tables[0].Rows[0]["strGroupName"]);

            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["dtAddedOn"])))
            {
                lblCreatedOn.Text = Convert.ToString(ds.Tables[0].Rows[0]["dtAddedOn"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["strSummary"])))
            {
                lblGroupSummary.Text = Convert.ToString(ds.Tables[0].Rows[0]["strSummary"]);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["strAccess"])))
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                    lblGroupType.Text = "Auto Join";

                else if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "R")
                    lblGroupType.Text = "Request To Join";
            }
        }

        if (Convert.ToString(ds.Tables[0].Rows[0]["intRegistrationId"]) == Convert.ToString(Session["ExternalUserId"]))
        {
            lnkJoin.Visible = false;
            lnkMessage.Visible = false;
            lnkEdit.Visible= true;
            lnkAddMember.Visible = true;
            spandropdownMenuLink.Visible = true;
            return;
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
            {
                lnkJoin.InnerText = "Join";
                lnkJoin.Style.Remove("display");
                lnkShare.Visible = false;

            }
            else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
            {
                lnkJoin.InnerText = "Leave";
                
                spandropdownMenuLink.Visible = true;
                lnkLeave.Visible = true;
                lnkJoin.Style.Add("display", "none");
            }
            else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
            {
                lnkJoin.InnerText = "Join";
                lnkJoin.Style.Remove("display");
                lnkShare.Visible = false;
            }
        }
        else
        {
            lnkJoin.InnerText = "Join";
            lnkJoin.Style.Remove("display");
            lnkShare.Visible = false;
        }
    }

    protected void GetGroupMembers()
    {
        objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objGrpJoinDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objGrpJoinDO.strSearch = "";
        objGrpJoinDO.PageSize = 100;
        objGrpJoinDO.Currentpage = 1;
        dt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GroupMembersCount);

        if (dt.Rows.Count > 0)
        {
            lblMembers.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblMembers.Text = "0";
        }
    }

    protected void lnkJoin_Click(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");

        if (lnkJoin.InnerText == "Join")
        {
            lblConnDisconn.Text = "Do you want to Join ?";
        }
        else
        {
            lblConnDisconn.Text = "Do you want to Leave ?";
        }
        divConnDisPopup.Style.Add("display", "block");
    }

    protected void lnkMessage_Click(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
        divGroupMessPopup.Style.Add("display", "block");
        divSuccess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");

    }

    private void SendGroupMail(string mailid, string name)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            string MailTo = mailid;
            string Mailbody = "";

            dtRecord.Clear();
            objdoreg.UserName = mailid;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);

            NetworkCredential cre = new NetworkCredential(username, Password);
            DataSet ds1 = new DataSet();
            ds1 = (DataSet)ViewState["GetGroupDetailsByGroupId"];

            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            clientip.Credentials = cre;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            string MailUrlFinal = MailURL + "/groups-members.aspx?GrpId=" + ds1.Tables[0].Rows[0]["inGroupId"];
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
                    //    + "Your " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group has been joined by " + Session["LoginName"] + "<br><br>" + "Regards," + "<br>" 
                    //    + "Skorkel Team" + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{UserName}", name);
                    //body = body.Replace("{Content}", ViewState["LoginName"] +
                    //               " added you to the " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group. <br>");
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

                    body = body.Replace("{UserName}", name);
                    body = body.Replace("{Content} ", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + Session["LoginName"] + "</span> wants to join  <span style='font-weight:bold;font-size:14px;color:#373A36;'>" + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + "</span>" +
                            "<br><br><a href='" + MailUrlFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Accept</a>");
                    body = body.Replace("{RedirectURL}", MailURL);

                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "You have a Skorkel group joining request posted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html"); Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]);
                    //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Joining Request</b>" + "<br><br>" + "Dear " 
                    //    + name + " <br><br>"
                    //    + Session["LoginName"] + " request you to allow joining " 
                    //    + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " group.<br><br>"
                    //    + "Regards," + "<br>" + "Skorkel Team"
                    //    + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                    //Member_Name request you to allow joining 'Group_name' group
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    //Rmm2.Subject = "Skorkel Group Joining Request.";
                    Rmm2.Subject = "Request to Join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"];

                    FCMNotification.Send("Request to Join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]) + " - " + Session["LoginName"], Session["LoginName"] + " wants to join " + Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]),
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailUrlFinal);
                }
                else if (RequestType == "Send UnJoin request")
                {
                    htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group unjoining invitation</b>" + "<br><br>" + " " + name + " " + "Your group has been unjoined by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = "Skorkel group unjoining notification";
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

    protected void lnkShare_Click(object sender, EventArgs e)
    {
        clear();
        string path = Request.Url.AbsoluteUri;

        PopUpShare.Style.Add("display", "block");
        divConnDisPopup.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        divGroupMessPopup.Style.Add("display", "none");

        objgrp.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);

        DataSet ds = new DataSet();
        ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["GetGroupDetailsByGroupId"] = ds;
            if (ds.Tables[0].Rows[0]["strLogoPath"].ToString() != "" && ds.Tables[0].Rows[0]["strLogoPath"].ToString() != string.Empty)
            {
                try
                {
                    string imgPathPhysicals = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(ds.Tables[0].Rows[0]["strLogoPath"]));
                    if (File.Exists(imgPathPhysicals))
                    {
                        imgGrp1.Src = "~\\CroppedPhoto\\" + ds.Tables[0].Rows[0]["strLogoPath"].ToString();
                    }
                    else
                    {
                        imgGrp1.Src = "~/images/photo1.png";
                    }
                }
                catch
                {
                    imgGrp1.Src = "~/images/photo1.png";
                }
            }
            else
                imgGrp1.Src = "~/images/photo1.png";

            txtLink.Text = path;
            lblGroupName1.Text = Convert.ToString(ds.Tables[0].Rows[0]["strGroupName"]);


            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["strSummary"])))
            {
                lblGroupSummary1.Text = Convert.ToString(ds.Tables[0].Rows[0]["strSummary"]);
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "name", "ShareCancel();", true);
    }

    protected void GetInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);

        if (dtDoc.Rows.Count > 0)
        {
            txtInvitee.DataSource = dtDoc;
            txtInvitee.DataValueField = "intInvitedUserId";
            txtInvitee.DataTextField = "Name";
            txtInvitee.DataBind();
        }
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
      
        if (hdnDepartmentIds.Value != null && hdnDepartmentIds.Value != "")
        {
            objGroupShareDO.strInvitee = hdnDepartmentIds.Value;

            if (txtBody.InnerText.Trim().Replace("'", "''") != "")
            {
                objGroupShareDO.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
            }
            else
            {
                objGroupShareDO.strMessage = "";
            }

            objGroupShareDO.strLink = txtLink.Text.Trim();
            objGroupShareDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objGroupShareDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objGroupShareDO.strIPAddress = ip;
            objGroupShareDA.AddEditDel_GroupShare(objGroupShareDO, DA_GroupShare.ShareGroup.Add);
            clear();
            PopUpShare.Style.Add("display", "none");
            lblSuccess.Text = "Group shared successfully.";
            lblSuccess.CssClass = "GreenErrormsg";
            divSuccess.Style.Add("display", "block");

        }
        else
        {
            lblMess.Text = "Please select member";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "name", "ShareCancel();", true);
    }

    protected void lnkCancels_Click(object sender, EventArgs e)
    {
        PopUpShare.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "name", "ShareCancel();", true);
    }

    protected void clear()
    {
        hdnDepartmentIds.Value = "";
        txtInvitee.Controls.Clear();
        txtBody.InnerText = "";
        txtLink.Text = "";
        lblMess.Text = "";
        GetInviteeName();
    }

    protected void btnpopCancel_Click(object sender, EventArgs e)
    {
        clear();
        PopUpShare.Style.Add("display", "none");
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = new DataSet();
            ds1 = (DataSet)ViewState["GetGroupDetailsByGroupId"];
            ViewState["strGroupName"] = Convert.ToString(ds1.Tables[0].Rows[0]["strGroupName"]);
            if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "A")
            {
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "0")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }

                    else if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "1")
                    {
                        objGrpJoinDO.isAccepted = 2;
                    }

                    else if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "2")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }
                }
                else
                {
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "A")
                    {
                        objGrpJoinDO.isAccepted = 1;
                    }
                }
            }

            else if (Convert.ToString(ds1.Tables[0].Rows[0]["strAccess"]) == "R")
            {

                objGrpJoinDO.inGroupId = Convert.ToInt32(ds1.Tables[0].Rows[0]["inGroupId"]);
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(ds1.Tables[0].Rows[0]["intRegistrationId"]);
                objGrpJoinDO.intInvitedUserId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.UpdateGroupMembersReq);
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "0")
                    {
                        objGrpJoinDO.isAccepted = 1;

                        lblSuccess.Text = "You have already send the group joining request.";
                        lblSuccess.ForeColor = System.Drawing.Color.Red;
                        divSuccess.Style.Add("display", "block");

                        //lblMessage.Text = "You have already send the group joining request.";
                        //lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    else if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "1")
                    {
                        objGrpJoinDO.isAccepted = 2;
                    }

                    else if (Convert.ToString(ds1.Tables[1].Rows[0]["IsAccepted"]) == "2")
                    {
                        objGrpJoinDO.isAccepted = 0;
                    }
                }
            }

            objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ds1.Tables[0].Rows[0]["intRegistrationId"]);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objGrpJoinDO.strIpAddress = ip;

            objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());

            objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);


            if (lnkJoin.InnerText == "Join")
            {
                SendGroupMail(Convert.ToString(ds1.Tables[0].Rows[0]["vchrUserName"]), Convert.ToString(ds1.Tables[0].Rows[0]["Name"]));
                if (Convert.ToString(lblGroupType.Text) == "Auto Join")
                {
                    lblSuccess.Text = "Successfully joined the Group and a mail has been sent to the group owner.";
                    lblSuccess.ForeColor = System.Drawing.Color.Green;
                    divSuccess.Style.Add("display", "block");
                }
                else
                {
                    lblSuccess.Text = "Group joining request and request mail has been sent to the group owner.";
                    lblSuccess.ForeColor = System.Drawing.Color.Green;
                    divSuccess.Style.Add("display", "block");
                }
            }
            else
            {
                lblSuccess.Text = "Group left successfully.";
                lblSuccess.ForeColor = System.Drawing.Color.Green;
                divSuccess.Style.Add("display", "block");

                RequestType = "Send UnJoin request";
            }
            GetGroupDetails();
            GetGroupMembers();
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"] + "");
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void brokerSearchButton_Click(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "name", "ShareCancel();", true);
    }

    protected void lnkJoinUnjoinCancel_Click(object sender, EventArgs e)
    {
        divConnDisPopup.Style.Add("display", "none");
    }

    protected void lnkSendMess_Click(object sender, EventArgs e)
    {
        objgrp.intAddedBy = Convert.ToInt32(Convert.ToString(ViewState["UserID"]));
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        Session["GroupOwnerId"] = Convert.ToInt32(ViewState["intGroupId"]);

        DataSet ds = new DataSet();
        ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
        WallMessageDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        Session["GroupOwnerId"] = Convert.ToInt32(ViewState["intGroupId"]);
        WallMessageDO.StrRecommendation = txtBodyMessage.InnerText.Trim();
        WallMessageDO.strSubject = txtSubject.Text.Trim().Replace("'", "''").Replace("\n", "<br>");
        WallMessageDO.strIpAddress = ip;
        WallMessageDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        WallMessageDO.intInvitedUserId = Convert.ToInt32(ViewState["InvitedUserId"]);
        WallMessageDO.striInvitedUserId = Convert.ToString(ViewState["GrpOwnerID"]);
        WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);
        txtBodyMessage.InnerText = "";
        txtSubject.Text = "";
        divGroupMessPopup.Style.Add("display", "none");
        lblSuccess.Text = "Message sent successfully.";
        lblSuccess.ForeColor = System.Drawing.Color.Green;
        divSuccess.Style.Add("display", "block");
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]+"&EditId=E");
    }

    protected void lnkAddMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"] + "&EditId=E&NavigationId=A");
    }
}