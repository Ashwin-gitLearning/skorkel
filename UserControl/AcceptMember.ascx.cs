using System;
using System.Data;
using DA_SKORKEL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class UserControl_AcceptMember : System.Web.UI.UserControl
{

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DataTable dt = new DataTable();
    String EmailId = "", Name = "", GroupName = "";
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            int RegID = Convert.ToInt32(Request.QueryString["AccRegId"]);
            int RejRegId = Convert.ToInt32(Request.QueryString["RejRegId"]);
            if (Request.QueryString["Change"] == "ch")
            {
                divPopupMember.Style.Add("display", "none");

            }
            if (Request.QueryString["AccRegId"] != null)
            {
                txtBody.Visible = false;
                divPopupMember.Style.Add("display", "block");
                lblMessage.Text = "Do you want to accept group joining request?";
                lblTitle.Text = "Connect group";
            }
            else if (Request.QueryString["RejRegId"] != null)
            {
                txtBody.Visible = false;
                lblMessage.Text = "Do you want to reject group joining request?";
                lblTitle.Text = "Reject Request";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["DisConnFrnd"] != null)
            {

                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "DisConnect";
                lblMessage.Text = "You have successfully disconnected.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["ConnFrnd"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "Connect";
                lblMessage.Text = "You have successfully sent request.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["MessId"] != null)
            {
                divPopupMember.Style.Add("display", "block");
                lblMessage.Visible = false;
                txtBody.Visible = true;
                lblTitle.Visible = true;
                lnkPopupOK.Text = "Post";
                lblTitle.Text = "Send Message";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;

            }
            else if (Request.QueryString["GrpAutoJoinId"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "Join";
                lblMessage.Text = "Successfully joined the Group and a mail has been sent to the group owner.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["GrpReqJoinId"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "Join";
                lblMessage.Text = "Group joining request and request mail has been sent to the group owner.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;

            }
            else if (Request.QueryString["GrpUnJoinId"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "Unjoin";
                lblMessage.Text = "Group unjoined successfully and a mail has been send to the group owner.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;

            }
            else if (Request.QueryString["GrpAutoJoin"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "Join";
                lblMessage.Text = "Group joining request and request mail has been send to the group owner.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["GrpUnJoin"] != null)
            {
                txtBody.Visible = false;
                lnkPopupOK.Text = "Ok";
                lblTitle.Text = "UnJoin";
                lblMessage.Text = "Group unjoined successfully and a mail has been send to the group owner.";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;
            }
            else if (Request.QueryString["MessPepId"] != null)
            {
                divPopupMember.Style.Add("display", "block");
                lblMessage.Visible = false;
                txtBody.Visible = true;
                lblTitle.Visible = true;
                lnkPopupOK.Text = "Post";
                lblTitle.Text = "Send Message";
                ddlRoleDetails.Visible = false;
                tdddlRoles.Visible = false;

            }
            else
            {
                divPopupMember.Style.Add("display", "none");
            }
        }
    }

    private void BindRole()
    {

        objDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        if (Request.QueryString["AccRegId"] != null)
        {
            dt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.BindRoleByGrpID);
        }
        else
        {
            dt = objDA.GetOrgDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_OrgGroupDetailTbl.BindRoleByGrpID);
        }
        if (dt.Rows.Count > 0)
        {
            ddlRoleDetails.DataTextField = "strRoleName";
            ddlRoleDetails.DataValueField = "intGrpRoleId";
            ddlRoleDetails.DataSource = dt;
            ddlRoleDetails.DataBind();
            ddlRoleDetails.Items.Insert(0, "Select Role");
        }
        else
        {
            ddlRoleDetails.Items.Insert(0, "Select Role");
        }

    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        int intRequestJoinId = 0;
        if (Request.QueryString["AccRegId"] != null)
        {

            objGrpJoinDO.intRegistrationId = Convert.ToInt32(Request.QueryString["AccRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["JoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 1;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                    try
                    {
                        string UserURL = "";

                        if (ISAPIURLACCESSED == "1")
                        {
                            UserURL = APIURL + "addMemberToGroup.action?" +
                                   "groupId=GRP" + Request.QueryString["GrpId"] +
                                   "&members=USR" + Request.QueryString["AccRegId"];

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                            myRequest1.Method = "GET";
                            if (ISAPIResponse == "1")
                            {
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

                    }
                    catch { }
                }
                else
                {
                    return;
                }
            }

            EmailId = Convert.ToString(dtSearch.Rows[0]["vchrUserName"]);
            Name = Convert.ToString(dtSearch.Rows[0]["Name"]);
            GroupName = Convert.ToString(dtSearch.Rows[0]["strGroupName"]);

            divPopupMember.Style.Add("display", "none");
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccess.Text = "Group joining request accepted successfully and a mail is sent to the member.";
            SendGroupMail(EmailId, Name, GroupName);
        }
        else if (Request.QueryString["RejRegId"] != null)
        {
            objGrpJoinDO.intRegistrationId = Convert.ToInt32(Request.QueryString["RejRegId"]);
            objGrpJoinDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
            objGrpJoinDO.PageSize = 100;
            objGrpJoinDO.Currentpage = 1;
            DataTable dtSearch = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                intRequestJoinId = Convert.ToInt32(dtSearch.Rows[0]["intRequestJoinId"]);

                if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
                {
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRequestJoinId = intRequestJoinId;
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    objGrpJoinDO.isAccepted = 2;
                    if (objGrpJoinDO.strIpAddress == null)
                        objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                    DeleteMemberRole();

                    try
                    {
                        string UserURL = "";

                        if (ISAPIURLACCESSED == "1")
                        {
                            UserURL = APIURL + "removeMemberFromGroup.action?" +
                                     "groupId=GRP" + Request.QueryString["GrpId"] +
                                     "&members=USR" + Request.QueryString["RejRegId"];

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
                            myRequest1.Method = "GET";
                            if (ISAPIResponse == "1")
                            {
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
                    }
                    catch { }
                }
                else
                {
                    return;
                }
            }

            divPopupMember.Style.Add("display", "none");
            divSuccessAcceptMember.Style.Add("display", "block");
            lblSuccess.Text = "Group joining request rejected successfully.";
        }
        else if (Request.QueryString["DisConnFrnd"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["DisConnFrnd"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["ConnFrnd"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["ConnFrnd"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["MessId"] != null)
        {
            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objRecmndDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                return;
            }

            objRecmndDO.StrRecommendation = txtBody.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);
            txtBody.InnerText = "";
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["MessId"]);
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpReqJoinId"] != null || Request.QueryString["GrpAutoJoinId"] != null || Request.QueryString["GrpUnJoinId"] != null)
        {
            Response.Redirect("SearchGroup.aspx");
            divPopupMember.Style.Add("display", "none");
        }
        else if (Request.QueryString["GrpAutoJoin"] != null)
        {
            int GrpAutoJoin = Convert.ToInt32(Request.QueryString["GrpAutoJoin"]);
            divPopupMember.Style.Add("display", "none");
            Response.Redirect("groups-members.aspx?GrpId=" + GrpAutoJoin);
        }
        else if (Request.QueryString["GrpUnJoin"] != null)
        {
            int GrpUnJoin = Convert.ToInt32(Request.QueryString["GrpUnJoin"]);
            divPopupMember.Style.Add("display", "none");
            Response.Redirect("groups-members.aspx?GrpId=" + GrpUnJoin);
        }
        else if (Request.QueryString["MessPepId"] != null)
        {
            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objRecmndDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                return;
            }

            objRecmndDO.StrRecommendation = txtBody.InnerText.Trim().Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);
            txtBody.InnerText = "";
            Response.Redirect("SearchPeople.aspx?ViewAll=" + Request.QueryString["ViewAll"]);
            divPopupMember.Style.Add("display", "none");
        }
    }

    protected void lnkcose_Click(object sender, EventArgs e)
    {
        divPopupMember.Style.Add("display", "none");
        divSuccessAcceptMember.Style.Add("display", "none");
    }

    protected void SaveMemberRole()
    {
        objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDO.strIpAddress = ip;
        objDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        objDO.intRoleId = Convert.ToInt32(ddlRoleDetails.SelectedItem.Value);

        if (Request.QueryString["AccRegId"] != null)
        {
            objDO.inviteMembers = Request.QueryString["AccRegId"];
            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InsertGroupInvitation);
        }
        else
        {
            objDO.inviteMembers = Request.QueryString["AccGrpOrgRegId"];
            objDA.AddEditDel_Scrl_OrgGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_OrgGroupDetailTbl.InsertGroupInvitation);
        }
    }

    protected void btnpopCancel_Click(object sender, EventArgs e)
    {
        divPopupMember.Style.Add("display", "none");
        if (Request.QueryString["MessPepId"] != null)
        {
            Response.Redirect("SearchPeople.aspx?ViewAll=" + Request.QueryString["ViewAll"]);
        }
        if (Request.QueryString["MessId"] != null)
        {
            Response.Redirect("Home.aspx?RegId=" + Request.QueryString["MessId"]);
        }
    }

    protected void lnkSuccessFailure_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["AccRegId"] != null || Request.QueryString["RejRegId"] != null)
        {
            Response.Redirect("groups-members.aspx?GrpId=" + Convert.ToInt32(Request.QueryString["GrpId"]));
        }
    }

    private void SendGroupMail(string mailid, string name, string groupname)
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
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = mailid;
            string Mailbody = "";
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = true;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom);
                Rmm2.Body = Mailbody.ToString();
                //FOR AUTO-JOIN
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" + "<br><br>" + " " + name + " " + "You are successfuly join the" + " " + groupname + " " + "group" + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = "Skorkel group joining invitation";
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

    protected void DeleteMemberRole()
    {
        objDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDO.strIpAddress = ip;
        objDO.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        if (Request.QueryString["RejRegId"] != null)
        {
            objDO.inviteMembers = Request.QueryString["RejRegId"];
            objDA.AddEditDel_Scrl_UserGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.DeleteGrpInvities);
        }
        else
        {
            objDO.inviteMembers = Request.QueryString["RejGrpOrgRegId"];
            objDA.AddEditDel_Scrl_OrgGroupDetailTbl(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_OrgGroupDetailTbl.DeleteGrpInvities);
        }
    }
}