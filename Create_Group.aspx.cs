using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI;
using System.Text;
using System.Net.Mime;

public partial class Create_Group : System.Web.UI.Page
{
    DO_Scrl_UserGroupDetailTbl objDOgroup = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDAgroup = new DA_Scrl_UserGroupDetailTbl();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();


    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        txtInviteMember.selectPlaceholder = "Enter member names here";

        if (!IsPostBack)
        {

            Session.Remove("ImagePath");
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Add Group";
            string ukey = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + ViewState["UserID"];
            ViewState["ukey"] = ukey;
            int LoginId = Convert.ToInt32(Session["ExternalUserId"]);
            int FrndId = 0;
            ViewState["UserID"] = LoginId;
            ViewState["RegId"] = FrndId;
            ViewState["join"] = "A";
            getInviteeName();
            SubjectTempDataTableGroup();
            BindGroupSubjectList();
         //  divEditUserProfile.Style.Add("display", "block");
          //  lnkChangeImage.Visible = true;
            imgGroupUser.Src = "~/images/groupPhoto.jpg";
        }
    }

    

    private void BindGroupSubjectList()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstCreateGroup.DataValueField = "intCategoryId";
            lstCreateGroup.DataTextField = "strCategoryName";
            lstCreateGroup.DataSource = dtSub;
            lstCreateGroup.DataBind();
        }
        else
        {
            lstCreateGroup.DataSource = null;
            lstCreateGroup.DataBind();
        }
    }

    protected void SubjectTempDataTableGroup()
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
        ViewState["SubjectCategorygr"] = dtSubjCat;
    }

    //protected void lstCreateGroup_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
    //    SubLi.Attributes.Add("class", "graycreateGroup");
    //}

    protected void imgAutobtn_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "A";
        if (imgAutojoin.ImageUrl != "images/checked2.png")
        {
            imgAutojoin.ImageUrl = "images/checked2.png";
            imgReqjoin.ImageUrl = "images/unchecked2.png";
        }
    }

    protected void imgReqjoin_Click(object sender, EventArgs e)
    {
        ViewState["join"] = "R";
        if (imgReqjoin.ImageUrl == "images/unchecked2.png")
        {
            imgAutojoin.ImageUrl = "images/unchecked2.png";
            imgReqjoin.ImageUrl = "images/checked2.png";
        }
    }

    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {
        if (hdnAutoreqJoin.Value == "1")
        {
            ViewState["join"] = "A";
        }
        else
        {
            ViewState["join"] = "R";
        }

       // return;
        String GroupeAccess = String.Empty;

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        string PhotoPath = "";

        if (hdnFileData.Value!="")
        {
            string name = hdnPhotoName.Value;
            string ext = System.IO.Path.GetExtension(name);

            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
                lblSuccMess.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                lblSuccMess.CssClass = "RedErrormsg";
                return;
            }

            // int FileLength = FileUpload1.PostedFile.ContentLength;
            // if (FileLength <= 3145728)
            // {
            PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(hdnPhotoName.Value).ToString();
            //   FileUpload1.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
            File.WriteAllBytes(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath), Convert.FromBase64String(hdnFileData.Value.Split(',')[1]));
            ViewState["ImagePath"] = PhotoPath;
            lblSuccMess.Text = "";
            //}
            // else
            //    PhotoPath = ViewState["ImagePath"].ToString();
        }

        if (ViewState["ImagePath"] != null)
        {
            objDOgroup.strLogoPath = PhotoPath.ToString();
        }

        objDOgroup.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        //Defect # 9 HTML Validations
        txtCreateGr.Text = Validations.validateHtmlInput(txtCreateGr.Text);
        objDOgroup.strGroupName = txtCreateGr.Text.Trim();
        ViewState["strGroupName"] = txtCreateGr.Text.Trim(); 
        if (txtPurpose.InnerText.Trim() != "Description")
        {
            //Defect # 9 HTML Validations
            
            objDOgroup.strSummary = Validations.validateHtmlInput(txtPurpose.InnerText.Trim());
           
        }
        else
        {
            objDOgroup.strSummary = "";
        }

        objDOgroup.strUniqueKey = ViewState["ukey"].ToString();
        if (ViewState["join"].ToString() == "A")
        {
            objDOgroup.strAccess = Convert.ToString(ViewState["join"]);
        }
        else
        {
            objDOgroup.strAccess = Convert.ToString(ViewState["join"]);
        }
        objDOgroup.strInvitationMessage = "";


        objDOgroup.strIpAddress = ip;

        objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.Insert);
        ViewState["GroupOutId"] = objDOgroup.GroupOutId;

        objDOgroup.strIpAddress = ip;
        List<KeyValuePair<string, string>> members = txtInviteMember.GetSelectedValues();
      //  string totalMembers = hdnMembId.Value;
        objDOgroup.strUniqueKey = ViewState["ukey"].ToString();
        List<KeyValuePair<string, string>> subJectValues = lstCreateGroup.GetSelectedValues();


        if (subJectValues != null && subJectValues.Count > 0)
        {
            string[] subJectCateValues = subJectValues.Select(s => (string)s.Value).ToArray();
            var dictionary = new Dictionary<int, int>();

            Dictionary<string, int> counts = subJectCateValues.ToArray().GroupBy(x => x)
                                          .ToDictionary(g => g.Key,
                                                        g => g.Count());

            foreach (var v in counts)
            {
                if (v.Value == 1)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);
                }
                else if (v.Value == 3)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 5)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 7)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }
                else if (v.Value == 9)
                {
                    objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objDOgroup.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDOgroup.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InserCotextOrgGroupDocument);

                }

            }
        }

      //  string[] members = totalMembers.Split(',');
        for (int i = 0; i < members.Count; i++)
        {
            if ( Convert.ToString(members[i].Value) != "")
            {
                int IDs = Convert.ToInt32(members[i].Value);
                objDOgroup.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                objDOgroup.inviteMembers = Convert.ToString(IDs);
                objDAgroup.AddEditDel_Scrl_UserGroupDetailTbl(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.InsertGroupInvitation);

                if (ViewState["join"].ToString() == "A")
                {
                    objGrpJoinDO.inGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                    objGrpJoinDO.intInvitedUserId = Convert.ToInt32(ViewState["UserID"]);

                    ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = Request.ServerVariables["REMOTE_ADDR"];
                    objGrpJoinDO.strIpAddress = ip;

                    objGrpJoinDO.intAddedBy = Convert.ToInt32(IDs);
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(IDs);
                    objGrpJoinDO.isAccepted = 1;
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                }

            }
        }

        for (int i = 0; i < members.Count; i++)
        {
            if (Convert.ToString(members[i].Value) != "")
            {
                int IDs = Convert.ToInt32(members[i].Value);
                WallMessageDO.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
                WallMessageDO.intGroupId = Convert.ToInt32(ViewState["GroupOutId"]);
                if (ViewState["join"].ToString() == "A")
                {
                    WallMessageDO.StrRecommendation = "Group Owner added You to Group.";
                    //SendMailGroup(); //IDs, Convert.ToInt32(ViewState["UserID"])
                }
                else
                {
                    WallMessageDO.StrRecommendation = "Group Owner Invite You to Join Group.";
                    //SendMailGroup();
                }
                WallMessageDO.strIpAddress = ip;
                WallMessageDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                WallMessageDO.intInvitedUserId = Convert.ToInt32(IDs);
                WallMessageDO.striInvitedUserId = Convert.ToString(IDs);
                WallMessageDO.strSubject = "Group Invitation";
                //Defect # 9 HTML Validations
                WallMessageDO.strTotalGrpMemberID = Validations.validateHtmlInput(txtCreateGr.Text.Trim());
                WallMessageDA.Scrl_AddEditDelWallMessage(WallMessageDO, DA_WallMessage.WallMessage.Add);


            }
        }
        
        getInviteeName();
        string ID = "lnkCreateGroup";
        hdnTabId.Value = ID;

        if (ISAPIURLACCESSED != "0")
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["GroupOutId"])))
            {
				StringBuilder UserURL = new StringBuilder();
				UserURL.Append(APIURL);
				UserURL.Append("createGroup.action?groupId=");
				UserURL.Append(objDOgroup.inGroupId);
				UserURL.Append("&groupName=");
				UserURL.Append(objDOgroup.strGroupName);
				UserURL.Append("&scope=PRIVATE&groupParent=Null&groupOwner=");
				UserURL.Append(objDOgroup.intRegistrationId);
				UserURL.Append("&groupCreationDate=");
				UserURL.Append(DateTime.Now.ToString());


                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Group";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    string[] ttlMembers = null;
                    ttlMembers = (from item in members select item.Value).ToArray();
                    string totalMembers = String.Join(",", ttlMembers);

					UserURL = new StringBuilder();
					UserURL.Append(APIURL);
					UserURL.Append("addMemberToGroup.action?");
					UserURL.Append("groupId=GRP");
					UserURL.Append(Request.QueryString["GrpId"]);
					UserURL.Append("&members=");
					UserURL.Append(totalMembers);

                    HttpWebRequest myRequest11 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest11.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse11 = myRequest1.GetResponse();

                        StreamReader sr1 = new StreamReader(myResponse11.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result1 = sr1.ReadToEnd();

                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Group Member";
                        objAPILogDO.strResponse = result1;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strIPAddress = objGrpJoinDO.strIpAddress;
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }

                }
                catch { }
            }
        }
        //SendMailGroup();
        clearGroup();
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "s", "showSuccessPopup('Alert','Group created sucessfully.');", true);
        
        divDeletesucess.Style.Add("display", "block");
    }

    protected void getInviteeName()
    {

        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);
        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMember.DataSource = dtDoc;
            txtInviteMember.DataValueField = "intInvitedUserId";
            txtInviteMember.DataTextField = "Name";
            txtInviteMember.DataBind();
            txtInviteMember.selectPlaceholder = "Enter member names here";
        }
    }


    protected void fileuplad_onload(object sender, EventArgs e)
    {

        string PhotoPath = "";
        //if (fileinput.Value=="")
        //{
        //    string name = FileUpload1.FileName;
        //    string ext = System.IO.Path.GetExtension(name);

        //    if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
        //    {
        //        lblSuccMess.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
        //        lblSuccMess.CssClass = "RedErrormsg";
        //        return;
        //    }

        //    int FileLength = FileUpload1.PostedFile.ContentLength;
        //    if (FileLength <= 3145728)
        //    {
        //        PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(FileUpload1.FileName).ToString();
        //        FileUpload1.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
        //        ViewState["ImagePath"] = PhotoPath;
        //        Session["ImagePath"] = PhotoPath;
        //        imgGroupUser.Src = "CroppedPhoto/" + Session["ImagePath"].ToString();
        //        ImgRemovePic.Visible = true;
        //        lblSuccMess.Text = "";
        //    }
        //    else
        //        PhotoPath = ViewState["ImagePath"].ToString();
        //}
        //else
        //{
        //    lblSuccMess.Text = "Please select image to upload.";
        //    lblSuccMess.CssClass = "RedErrormsg";
        //    return;
        //}
    }

    protected void ImgRemovePic_OnClick(object sender, EventArgs e)
    {
        string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Session["ImagePath"].ToString());
        if (File.Exists(imgPathPhysical))
        {
            File.Delete(imgPathPhysical);
            imgGroupUser.Src = "~/images/groupPhoto.jpg";
            ImgRemovePic.Visible = false;
            ViewState["ImagePath"] = null;
            Session["ImagePath"] = null;
        }
    }

    private void SendMailGroup() //int RecieverID, int SenderID
    {
        try
        {
            string body = null;
            DataTable dtRecord = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            string MailTo = Convert.ToString(ViewState["TotalMailId"]);
            string Mailbody = "";
            
            objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            dtRecord = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.GetgroupShareInvitee);

            //NetworkCredential cre = new NetworkCredential(username, Password);
            //SmtpClient clientip = new SmtpClient(mailServer);
            //clientip.Port = Convert.ToInt32(Port);
            //clientip.UseDefaultCredentials = true;
            //if (MailSSL != "0")
            //    clientip.EnableSsl = true;

            //clientip.Credentials = cre;

            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = new System.Net.NetworkCredential(username, Password);

            try
            {

                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                Rmm2.Body = Mailbody.ToString();
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");

                DataTable dtMailId = new DataTable();
                objDOgroup.strUniqueKey = ViewState["ukey"].ToString();
                dtMailId = objDAgroup.GetDataTable(objDOgroup, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetMailId);
                if (dtMailId.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMailId.Rows.Count; i++)
                    {
                        string name = Convert.ToString(dtMailId.Rows[i]["Name"]);
                        string MailId = Convert.ToString(dtMailId.Rows[i]["vchrUserName"]);
                        if (MailId != null)
                        {
                            //MailTo += "," + MailId;
                            MailTo = MailId;
                            //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel group joining invitation</b>" 
                            //    + "<br><br>" + "Dear " + name + " "
                            //    + "<br><br>" + ViewState["LoginName"]
                            //    + " request you to allow joining " + ViewState["strGroupName"] + " group. <br><br>" 
                            //    + "Thanks," + "<br>" 
                            //    + "The Skorkel Team<br><br>"
                            //    + "****This is a system generated Email. Kindly do not reply****", null, "text/html");

                            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                            {
                                body = reader.ReadToEnd();
                            }
                            if (ViewState["join"].ToString() == "A")
                            {
                                string MailUrlFinal = MailURL + "/Group-Profile.aspx?GrpId=" + Convert.ToInt32(ViewState["GroupOutId"]);
                                body = body.Replace("{UserName}", name);
                                body = body.Replace("{Content}", "<b>" + dtRecord.Rows[0]["Name"] + "</b>" +
                                       " added you to the " + ViewState["strGroupName"] + " group. <br>" +
                                       "Please click below to view group details: " +
                                       "<br><a href = '" + MailUrlFinal + "' style='background:#01B7BD;padding:5px 10px;border-radius:15px;color:#fff;text-decoration: none;'>View Details</a>");
                                body = body.Replace("{RedirectURL}", MailURL);

                                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                                Rmm2.To.Clear();
                                Rmm2.To.Add(MailTo);
                                //Rmm2.Subject = "Skorkel Group Joining Request.";
                                Rmm2.Subject = ViewState["strGroupName"] + " Skorkel Group included you, By " + dtRecord.Rows[0]["Name"];
                            }
                            else if (ViewState["join"].ToString() == "R")
                            {
                                string MailUrlFinal = MailURL + "/Group-Profile.aspx?GrpId=" + Convert.ToInt32(ViewState["GroupOutId"]);
                                body = body.Replace("{UserName}", name);
                                body = body.Replace("{Content}", "<b>" + dtRecord.Rows[0]["Name"] + "</b>" +
                                       " added you to the " + ViewState["strGroupName"] + " group. <br>" +
                                       "Please click below to view group details: " +
                                       "<br><a href = '" + MailUrlFinal + "' style='background:#01B7BD;padding:5px 10px;border-radius:15px;color:#fff;text-decoration: none;'>View Details</a>");
                                body = body.Replace("{RedirectURL}", MailURL);

                                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                                Rmm2.To.Clear();
                                Rmm2.To.Add(MailTo);
                                //Rmm2.Subject = "Skorkel Group Joining Request.";
                                Rmm2.Subject = ViewState["strGroupName"] + " Skorkel Group included you, By " + dtRecord.Rows[0]["Name"];
                            }
                            Rmm2.AlternateViews.Add(htmlView);
                            Rmm2.IsBodyHtml = true;
                            clientip.Send(Rmm2);
                            Rmm2.To.Clear();
                            // ViewState["TotalMailId"] = TotalMailId;
                        }
                    }
                }

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

    public void clearGroup()
    {
        ViewState["join"] = "A";
        hdnSubject.Value = "";
        lblSuccMess.Text = "";
        txtInviteMember.RefreshList();
        lstCreateGroup.RefreshList();
        Session.Remove("ImagePath");
        ViewState["SubjectCategorygr"] = null;
        BindGroupSubjectList();
        SubjectTempDataTableGroup();
        txtCreateGr.Text = "";
        txtPurpose.InnerText = "";
        string ukey1 = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + ViewState["UserID"];
        ViewState["ukey"] = ukey1;
        imgGroupUser.Src = "~/images/groupPhoto.jpg";
        ImgRemovePic.Visible = false;
        ViewState["SubjectCategorygr"] = null;
        SubjectTempDataTableGroup();
        hdnMembId.Value = "";
        hdnPhotoName.Value = "";
        //hdnFileData.Value = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "s", "clearCropper();", true);
    }

}