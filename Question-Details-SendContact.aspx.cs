using System;
using System.Data;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net.Mime;

public partial class Question_Details_SendContact : System.Web.UI.Page
{
    string loginUserProfilePhotoSrc = "";

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    //Newly Added
    DA_Scrl_UserQAPostingAnswer objDAQAPostingAnswer = new DA_Scrl_UserQAPostingAnswer();
    DO_Scrl_UserQAPostingAnswer objDOQAPostingAnswer = new DO_Scrl_UserQAPostingAnswer();

    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DataTable dt = new DataTable();
    DataTable dtchild = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl myObject;
        myObject = (HtmlContainerControl)Master.FindControl("PostQuest");
        myObject.Visible = true;
        txtInviteMembers.selectPlaceholder = "Enter member names here";

        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
            divDeletesucessCmnts.Style.Add("display", "none");

            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/Landing.aspx", true);
            }
            GetLoginUserDetails();
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Q&A";
            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"]);
            BindQADetails();
            getInviteeName();
            GetRelatedQuestion();
            GetAllReplies();
            if (!string.IsNullOrEmpty(Request.QueryString["MP"]))
            {
                ViewState["MP"] = "MP";
            }            
        }        
    }

    protected void GetLoginUserDetails()
    {
        DataTable dtDetails = new DataTable();
        objRegistration.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dtDetails = objRegistrationDB.GetDataTable(objRegistration, DA_Registrationdetails.RegistrationDetails.GetApprovedStudentByInstitute);
        if (dtDetails.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dtDetails.Rows[0]["vchrPhotoPath"])))
            {
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString());
                if (File.Exists(imgPathPhysical))
                {
                    loginUserProfilePhotoSrc = "CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString();
                }
                else
                {
                    loginUserProfilePhotoSrc = "images/profile-photo.png";
                }
            }
            else
            {
                loginUserProfilePhotoSrc = "images/profile-photo.png";
            }
        }
        else
        {
            loginUserProfilePhotoSrc = "images/profile-photo.png";
        }
        hdnCommentImageSrcId.Value = loginUserProfilePhotoSrc;
    }

    protected void BindQADetails()
    {
        if (Convert.ToString(Request.QueryString["PostId"]) != null && Convert.ToString(Request.QueryString["PostId"]) != "")
        {
            int SrchPostQuestionId = Convert.ToInt32(Request.QueryString["PostId"]);
            objDOQAPosting.intPostQuestionId = SrchPostQuestionId;
        }
        else
        {
            int PostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
            objDOQAPosting.intPostQuestionId = PostQuestionId;
        }
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSingleQueAnsDetails);
        if (dt.Rows.Count > 0)
        {
            lstParentQADetails.DataSource = dt;
            lstParentQADetails.DataBind();
        }
        else
        {
            lstParentQADetails.DataSource = null;
            lstParentQADetails.DataBind();
        }
    }

    protected void lstParentQADetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl divEditDeleteList = (HtmlGenericControl)e.Item.FindControl("divEditDeleteList");
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        ListView lstSubjCategory = (ListView)e.Item.FindControl("lstSubjCategory");
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs = (Label)e.Item.FindControl("lblAttachDocs");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlGenericControl liSubCategory = (HtmlGenericControl)e.Item.FindControl("liSubCategory");
        Label Label1 = (Label)e.Item.FindControl("Label1");
        ViewState["Labelname"] = Label1.Text;
        ViewState["QAUserId"] = hdnintAddedBy.Value;
        HtmlImage imgUser = (HtmlImage)e.Item.FindControl("imgUser");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkDeleteQues = (LinkButton)e.Item.FindControl("lnkDeleteQues");

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        DataTable dtChildContext = new DataTable();
        DataTable dtt = new DataTable();

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(ViewState["QAUserId"]))
        {
            lnkDeleteQues.Visible = true;
            lnkEdit.Visible = true;
            divEditDeleteList.Visible = true;
        }

        if (hdnPostQuestionID.Value != "" && hdnPostQuestionID.Value != null)
        {
            DataTable dtsub = new DataTable();
            objCategory.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
            dtsub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetCatNameByPostQuestionId);
            if (dtsub.Rows.Count > 0)
            {
                lstSubjCategory.DataSource = dtsub;
                lstSubjCategory.DataBind();
            }
            else
            {
                lstSubjCategory.DataSource = null;
                lstSubjCategory.DataBind();
                liSubCategory.Visible = false;
            }
        }
        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dt.Rows[0]["TotalShare"]);
            lblTotallike.Text = Convert.ToString(TotalLike) + ((TotalLike == 1) ? " Like" : " Likes");
            lblreply.Text = Convert.ToString(TotalReply) + ((TotalReply == 1) ? " Answer" : " Answers") ;
            totalAwnsers.InnerText = Convert.ToString(TotalReply) + ((TotalReply == 1)? " Answer": " Answers");
            totalAwnsers.Style.Add("display", TotalReply == 0 ? "none" : "block");
            lblShare.Text = Convert.ToString(TotalShare) + ((TotalShare == 1) ? " Share" : " Shares");
            if (dt.Rows[0]["LikeUserId"].ToString() == Convert.ToString(ViewState["UserID"]))
            {
                btnLike.CssClass = "active-toogle on-flag";
            }
        }
        else
        {
            lblTotallike.Text = "0 Likes";
            lblreply.Text = "0 Answers";
            totalAwnsers.Style.Add("display", "none");
            lblShare.Text = "0 Shares";
        }

        lblTotallike.ToolTip = "View Likes";
        dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetLikeUser);
        if (dtt.Rows.Count > 0)
        {
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                if (lblTotallike.ToolTip != "View Likes")
                    lblTotallike.ToolTip += Convert.ToString(dtt.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lblTotallike.ToolTip = Convert.ToString(dtt.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
        
    }

    protected void lstParentQADetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HiddenField hdnAuthorId = (HiddenField)e.Item.FindControl("hdnAuthorId");
        LinkButton btnLike = (LinkButton)e.Item.FindControl("btnLike");
        PopUpShare.Style.Add("display", "none");

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        if (e.CommandName == "LikeForum")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.strIPAddress = ip;
            objDOQAPosting.strRepLiShStatus = "LI";
            objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
            objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.LikeQAInsert);
            PopUpShare.Style.Add("display", "none");
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);

            //if (ISAPIURLACCESSED != "0")
            //{
            //    try
            //    {
            //        bool? flag = null;
            //        if (Convert.ToString(dt.Rows[0]["QuesLiStatus"]) != null)
            //        {
            //            if (Convert.ToString(dt.Rows[0]["QuesLiStatus"]) != "LI")
            //            { flag = false; }
            //            else
            //            { flag = true; }
            //        }
            //        else
            //        { flag = false; }

            //        StringBuilder url = new StringBuilder();
            //        url.Append(APIURL);
            //        url.Append("likeQuestion?");
            //        url.Append("quesId=");
            //        url.Append(objDOQAPosting.intPostQuestionId);
            //        url.Append("&userId=");
            //        url.Append(objDOQAPosting.intAddedBy);
            //        url.Append("&flag=");
            //        url.Append(flag);

            //        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
            //        myRequest1.Method = "GET";
            //        WebResponse myResponse1 = myRequest1.GetResponse();

            //        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            //        String result = sr.ReadToEnd();

            //        objAPILogDO.strURL = url.ToString();
            //        objAPILogDO.strAPIType = "Like/Unlike Question";
            //        objAPILogDO.strResponse = result;
            //        objAPILogDO.strIPAddress = ip;
            //        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            //        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            //    }
            //    catch { }
            //}
        }
        if (e.CommandName == "QADetails")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value);
        }
        if (e.CommandName == "ShareForum")
        {
			lblmsg.Text = "";
			clearsharepopup();
            string path = Request.Url.AbsoluteUri;
            txtLink.Text = path;
            PopUpShare.Style.Add("display", "block");
            ViewState["PostQuestionID"] = Convert.ToInt32(hdnPostQuestionID.Value);

        }
        if (e.CommandName == "ReplyPost")
        {
            PopUpShare.Style.Add("display", "none");
        }
        if (e.CommandName == "GoToProfile")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnAuthorId.Value);
        }
        else if (e.CommandName == "Delete Quest")
        {
            ViewState["hdnPostQuestionID"] = hdnPostQuestionID.Value;
            //ViewState["Questiona"] = Label1.Text;
            //divDeletesucessQuest.Style.Add("display", "block");
            //lnkDeleteConfirm.Visible = false;
        }
        else if (e.CommandName == "Edit QA")
        {
            Response.Redirect("post-new-question.aspx?PostQAId=" + hdnPostQuestionID.Value);
        }
        BindQADetails();
    }

    protected void lnkDeleteQuest_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteQA);

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPostQuestionID"]);
        objLog.strAction = "Q&A";
        objLog.strActionName = hdnstrQuestionDescription.Value;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 25;   // Q&A Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("removeQuestion?");
                url.Append("queId=");
                url.Append(Convert.ToInt32(hdnDeletePostQuestionID.Value));
                url.Append("&userId=");
                url.Append(Convert.ToInt32(ViewState["UserID"]));

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url.ToString();
                objAPILogDO.strAPIType = "Deleting Question";
                objAPILogDO.strResponse = result;
                objAPILogDO.strIPAddress = ip;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch { }
        }

        BindQADetails();

        divDeletesucessQuest.Style.Add("display", "none");
        hdnDeletePostQuestionID.Value = "";
        Response.Redirect("~/AllQuestionDetails.aspx?DeleteQuest=true");
        //lnkDeleteConfirm.Visible = true;
    }

    protected void clearsharepopup()
    {
        txtInviteMembers.RefreshList();
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        List<KeyValuePair<string, string>> selectedValues = txtInviteMembers.GetSelectedValues();
        if (selectedValues != null && selectedValues.Count > 0)
        {
            hdnInvId.Value = string.Join(",", selectedValues.Select(s => (string)s.Value).ToArray());
        }
        else
        {
            hdnInvId.Value = "";
        }            
        
        if (hdnInvId.Value != "")
        {
            lblmsg.Text = "";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
            objDOQAPosting.strIPAddress = ip;
            objDOQAPosting.strRepLiShStatus = "SH";
            objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOQAPosting.strInvitee = hdnInvId.Value;

            if (txtBody.InnerText.Trim().Replace("'", "''") != "Message")
            {
                objDOQAPosting.strComment = txtBody.InnerText.Trim().Replace("'", "''");
            }
            else
            {
                objDOQAPosting.strComment = "";
            }

            objDOQAPosting.strSharelink = txtLink.Text;
            objDOQAPosting.strFileName = Convert.ToString(ViewState["Labelname"]);
            objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.InsertShare);
            BindQADetails();
            hdnInvId.Value = "";
            txtInviteMembers.RefreshList();
            txtBody.InnerText = "";
            getInviteeName();
            PopUpShare.Style.Add("display", "none");
			System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "hdScroll", "hideScroll();", true);

		}
		else
        {
            lblmsg.Text = "Please select members.";
            return;
        }
    }

    protected void getInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);
        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMembers.DataSource = dtDoc;
            txtInviteMembers.DataValueField = "intInvitedUserId";
            txtInviteMembers.DataTextField = "Name";
            txtInviteMembers.DataBind();
            txtInviteMembers.selectPlaceholder = "Enter member names here";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PopUpShare.Style.Add("display", "none");
        if (CKEditorControl.InnerText != "")
        {
            DataTable dtQuestion = new DataTable();
            DataTable dtAnsCommentsUID = new DataTable();

            string AnswerersName = null;
            string QuestionersName = null;
            string QuestionDesc = null;
            string QuestionersMail = null;
            string QuestionId = null;


            int intAnswerId = 0;
            int ResultId = 0;
            string docPath = "";
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
            QuestionId = Convert.ToString(Request.QueryString["PostQAId"]);
            objDOQAPosting.strIPAddress = ip;

            if (ViewState["Edit"] == null)
            {                
                objDOQAPosting.strFilePath = docPath;
                objDOQAPosting.strFileName = "";// uploadDoc.FileName;
                objDOQAPosting.strRepLiShStatus = "CM";
                objDOQAPosting.strComment = Validations.validateHtmlInput(CKEditorControl.InnerText.Trim().Replace(",", "''"));
                objDOQAPosting.PrvateMessage = 0;
                objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.InsertComment);
                ResultId = objDOQAPosting.intPostQuestionId;
                intAnswerId = objDOQAPosting.intPostQuestionId;
                objDOQAPostingAnswer.intAnswerId = intAnswerId;
                objDOQAPostingAnswer.strAnsLiStatus = null;
                objDOQAPostingAnswer.intAnsAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDOQAPostingAnswer.strComment = null;
                objDAQAPostingAnswer.AddEditDel_Scrl_UserAddingAnsPost(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.AddAnsListDtl);
                //ResultId = objDAQAPostingAnswer.AddEditDel_Scrl_UserAddingAnsPost(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.AddAnsListDtl);

                //if (ISAPIURLACCESSED != "0")
                //{
                //    try
                //    {
                //        StringBuilder url = new StringBuilder();
                //        url.Append(APIURL);
                //        url.Append("addAnswer.action?");
                //        url.Append("answerId=");
                //        url.Append(objDOQAPosting.intPostQuestionId);
                //        url.Append("&questionId=");
                //        url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
                //        url.Append("&userId=");
                //        url.Append(objDOQAPosting.intAddedBy);
                //        url.Append("&userName=" + null);
                //        url.Append("&insertDt=");
                //        url.Append(DateTime.Now);
                //        url.Append("&content=");
                //        url.Append(objDOQAPosting.strComment);
                //        url.Append("&title=");
                //        url.Append(objDOQAPosting.strComment);

                //        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                //        myRequest1.Method = "GET";
                //        WebResponse myResponse1 = myRequest1.GetResponse();

                //        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                //        String result = sr.ReadToEnd();

                //        objAPILogDO.strURL = url.ToString();
                //        objAPILogDO.strAPIType = "Add Answer";
                //        objAPILogDO.strResponse = result;
                //        objAPILogDO.strIPAddress = ip;
                //        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                //        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                //    }
                //    catch { }
                //}

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("addAnswer?");
                        url.Append("answerId=");
                        url.Append(objDOQAPosting.intPostQuestionId);
                        url.Append("&questionId=");
                        url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
                        url.Append("&content=");
                        url.Append(objDOQAPosting.strComment);
                        //url.Append("&userName=" + null);
                        url.Append("&userId=");
                        url.Append(objDOQAPosting.intAddedBy);                                               
                        url.Append("&title=");
                        url.Append(objDOQAPosting.strComment);

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url.ToString();
                        objAPILogDO.strAPIType = "Add Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                    }
                    catch { }
                }

                objDOQAPosting.intQAReplyLikeShareId = ResultId;
                dtAnsCommentsUID.Clear();
                //dtAnsCommentsUID = objDAQAPostingAnswer.GetDataTableCommentsByID(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.GetUserdtlByCmnyId);
                dtAnsCommentsUID = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetViewMoreDetails);
                if (Convert.ToString(Request.QueryString["PostId"]) != null && Convert.ToString(Request.QueryString["PostId"]) != "" && (!Page.IsPostBack))
                {
                    objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostId"]);
                }
                else
                {
                    objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
                }
                //objDOQAPosting.intPostQuestionId = Convert.ToInt32(dtAnsCommentsUID.Rows[0]["intPostQuestionId"]);
                dtQuestion.Clear();
                dtQuestion = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSingleQueAnsDetails);
                QuestionersName = Convert.ToString(dtQuestion.Rows[0]["AuthorName"]); ;
                QuestionDesc = Convert.ToString(dtQuestion.Rows[0]["strQuestionDescription"]);
                QuestionersMail = Convert.ToString(dtQuestion.Rows[0]["QuestionersMail"]);
                AnswerersName = Convert.ToString(dtAnsCommentsUID.Rows[0]["Name"]);
                if (Convert.ToInt32(dtQuestion.Rows[0]["intAddedBy"]) != Convert.ToInt32(dtAnsCommentsUID.Rows[0]["intAddedBy"]))
                {
                    SendMailOnAnswer(AnswerersName, QuestionersName, QuestionDesc, QuestionersMail, QuestionId);
                }

                CKEditorControl.InnerText = "";
                BindQADetails();
                GetAllReplies();
                ViewState["Edit"] = null;
            }
            else
            {
                string Questionid = null;
                if (Convert.ToString(Request.QueryString["PostId"]) != null && Convert.ToString(Request.QueryString["PostId"]) != "")
                {
                    Questionid = Request.QueryString["PostId"];
                }
                else
                {
                    Questionid = Request.QueryString["PostQAId"];
                }
                objDOQAPosting.strFilePath = docPath;
                objDOQAPosting.strFileName = "";// uploadDoc.FileName;
                objDOQAPosting.strRepLiShStatus = "CM";
                //Bug #9 HTML Inputs
                objDOQAPosting.strComment = Validations.validateHtmlInput(CKEditorControl.InnerText.Trim().Replace(",", "''"));
                objDOQAPosting.PrvateMessage = 0;
                objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(ViewState["Edit"]);
                objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.UpdateComment);

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("updateAnswer?");
                        url.Append("answerId=");
                        url.Append(Convert.ToInt32(ViewState["Edit"]));
                        url.Append("&questionId=");
                        url.Append(Questionid);
                        url.Append("&content=");
                        url.Append(objDOQAPosting.strComment);
                        //url.Append("&userName=" + null);
                        url.Append("&userId=");
                        url.Append(objDOQAPosting.intAddedBy);

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url.ToString();
                        objAPILogDO.strAPIType = "Update Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }

                //          if (ISAPIURLACCESSED != "0")
                //          {
                //              try
                //              {
                //StringBuilder url = new StringBuilder();
                //url.Append(APIURL);
                //url.Append("addAnswer.action?");
                //url.Append("answerId=");
                //url.Append(objDOQAPosting.intPostQuestionId);
                //url.Append("&questionId=");
                //url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
                //url.Append("&userId =");
                //url.Append(objDOQAPosting.intAddedBy);
                //url.Append("&userName =" +null );
                //url.Append(DateTime.Now);
                //url.Append("&content =");
                //url.Append(objDOQAPosting.strComment);
                //url.Append("&title =");
                //url.Append(objDOQAPosting.strComment);

                //                  HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                //                  myRequest1.Method = "GET";
                //                  WebResponse myResponse1 = myRequest1.GetResponse();

                //                  StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                //                  String result = sr.ReadToEnd();

                //                  objAPILogDO.strURL = url.ToString();
                //                  objAPILogDO.strAPIType = "Update Answer";
                //                  objAPILogDO.strResponse = result;
                //                  objAPILogDO.strIPAddress = ip;
                //                  objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                //                  objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                //              }
                //              catch { }
                //          }

                CKEditorControl.InnerText = "";
                BindQADetails();
                GetAllReplies();
                ViewState["Edit"] = null;
            }

        }
    }

    protected void GetRelatedQuestion()
    {
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetRelatedAllQuestion);
        if (dt.Rows.Count > 0)
        {
            lstRelQuestions.DataSource = dt;
            lstRelQuestions.DataBind();
        }
    }

    protected void GetAllReplies()
    {
        if (Convert.ToString(Request.QueryString["PostId"]) != null && Convert.ToString(Request.QueryString["PostId"]) != "")
        {
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostId"]);
        }
        else
        {
            objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
        }
        if (Convert.ToString(ViewState["ViewAll"]) == "ViewAll")
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllDetailsOfReplies);
            ViewState["ViewAll"] = "Close";
        }
        else
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllReplies);
            ViewState["ViewAll"] = "View more";
        }

        if (dt.Rows.Count > 0)
        {
            lstAllReplies.Visible = true;
            lstAllReplies.DataSource = dt;
            lstAllReplies.DataBind();
        }
        else
        {
            lstAllReplies.Visible = false;
            lstAllReplies.DataSource = null;
            lstAllReplies.DataBind();
        }
    }

    protected void GetViewMoreDetails()
    {
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(Request.QueryString["PostQAId"]);
        objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(ViewState["QAReplyLikeShareId "]);
        if (Convert.ToString(ViewState["ViewAll"]) == "ViewAll")
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetViewMoreDetails);
            ViewState["ViewAll"] = "Close";
        }
        else
        {
            dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetAllReplies);
            ViewState["ViewAll"] = "View more";
        }

        if (dt.Rows.Count > 0)
        {
            lstAllReplies.DataSource = dt;
            lstAllReplies.DataBind();
        }
    }

    protected void lstAllReplies_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs = (Label)e.Item.FindControl("lblAttachDocs");
        LinkButton lnkClose = (LinkButton)e.Item.FindControl("lnkClose");
        Label lblReplyComment = (Label)e.Item.FindControl("lblReplyComment");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnintPrivateMessage = (HiddenField)e.Item.FindControl("hdnintPrivateMessage");
        HtmlControl QArep = (HtmlControl)e.Item.FindControl("QArep");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblDate = (Label)e.Item.FindControl("lblDate");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkdelete = (LinkButton)e.Item.FindControl("lnkdelete");
        HtmlControl editDelLink = (HtmlControl)e.Item.FindControl("editDelLink");
        //Newly Added
        LinkButton btnLikeAns = (LinkButton)e.Item.FindControl("btnLikeAns");
        Label lblTotallikeAns = (Label)e.Item.FindControl("lblTotallikeAns");
        HiddenField hdnQAReplyLikeShareId = (HiddenField)e.Item.FindControl("hdnQAReplyLikeShareId");
        Label lblreplyAns = (Label)e.Item.FindControl("lblreplyAns");
        HtmlImage imgUser = (HtmlImage)e.Item.FindControl("imgUser");        
        Label lblUsername = (Label)e.Item.FindControl("lblUsername");     


        if (Convert.ToInt32(ViewState["UserID"]) != Convert.ToInt32(hdnintAddedBy.Value))
        {
            editDelLink.Style.Add("display", "none");
        }

        if (lblDate.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblDate.Text = "Today";
        }
        else if (lblDate.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblDate.Text = "Yesterday";
        }

        if (imgprofile.Src == "/CroppedPhoto/")
        {
            imgprofile.Src = "images/profile-photo.png";
        }
        else
        {

            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.Src = "images/profile-photo.png";
            }

        }

        if (hdnCommentImageSrcId.Value == "" || hdnCommentImageSrcId.Value == null)
        {
            imgUser.Src = "images/profile-photo.png";
        }
        else
        {
            imgUser.Src = hdnCommentImageSrcId.Value;
        }

        if (hdnintPrivateMessage.Value == "1")
        {
            if (Convert.ToString(ViewState["QAUserId"]) != Convert.ToString(ViewState["UserID"]))
            {
                QArep.Visible = false;
            }

            if (hdnintAddedBy.Value == ViewState["UserID"].ToString())
            {
                QArep.Visible = true;
            }
        }

        if (Convert.ToString(ViewState["ViewAll"]) == "Close")
        {
            lnkClose.Style.Add("display", "block");
            lnkClose.Text = "Close";
            if (lblAttachDocs.Text != "" && lblAttachDocs.Text != null)
            {
                pnlAttachFile.Style.Add("display", "block");
            }
            else
            {
                pnlAttachFile.Style.Add("display", "none");
            }
        }
        
        //Newly Added
        objDOQAPostingAnswer.intAnswerId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
        objDOQAPostingAnswer.intAnsAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAQAPostingAnswer.GetDataTable(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.GetTotalAnsLikeById);
        dtchild = objDAQAPostingAnswer.GetDataTableComments(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.CommentsDtlsById);
        
        ListView lstChild = e.Item.FindControl("lstAllComments") as ListView;
        lstChild.DataSource = dtchild;
        lstChild.DataBind();
        
        int TotalLike1 = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
        int TotalComment = Convert.ToInt32(dt.Rows[0]["TotalComment"]);

        if (Convert.ToString(dt.Rows[0]["LikeUserId"]) != "" && Convert.ToString(dt.Rows[0]["LikeUserId"]) != null)
        {
            if (Convert.ToInt32(dt.Rows[0]["LikeUserId"]) == Convert.ToInt32(ViewState["UserID"]))
            {
                if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
                {
                    lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                    btnLikeAns.CssClass = "active-toogle on-flag";
                }
                else
                {
                    lblTotallikeAns.Text = "0 Likes";
                    btnLikeAns.CssClass = "active-toogle";
                }
            }
            else
            {
                if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
                {
                    lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                    btnLikeAns.CssClass = "active-toogle";
                }
                else
                {
                    lblTotallikeAns.Text = "0 Likes";
                    btnLikeAns.CssClass = "active-toogle";
                }
            }
        }
        else
        {
            if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
            {
                lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                btnLikeAns.CssClass = "active-toogle";
            }
            else
            {
                lblTotallikeAns.Text = "0 Likes";
                btnLikeAns.CssClass = "active-toogle";
            }
        }
        if (TotalComment > 0)
        {
            lblreplyAns.Text = Convert.ToString(TotalComment) + ((TotalComment == 1) ? " Comment" : " Comments");
        }
        else
        {
            lblreplyAns.Text = "0 Comments";
        }
        
    }

    protected void lstAllComments_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //DataTable dtCmnts = new DataTable();
        Label lblDescriptionAns = (Label)e.Item.FindControl("lblDescriptionAns");
        if (e.CommandName == "Delete Comments")
        {
            divDeletesucessCmnts.Style.Add("display", "block");
        }
    }

    protected void lstAllReplies_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int ResultId = 0;
        PopUpShare.Style.Add("display", "none");
        DataTable dtAns = new DataTable();

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        LinkButton lnkClose = (LinkButton)e.Item.FindControl("lnkClose");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnQAReplyLikeShareId = (HiddenField)e.Item.FindControl("hdnQAReplyLikeShareId");
        Label lblReplyComment = e.Item.FindControl("lblReplyComment") as Label;
        ViewState["QAReplyLikeShareId "] = hdnQAReplyLikeShareId.Value;
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HiddenField hdnCommentEditId = (HiddenField)e.Item.FindControl("hdnCommentEditId");
        //HiddenField hdnComment = (HiddenField)e.Item.FindControl("hdnComment");

        LinkButton btnLikeAns = (LinkButton)e.Item.FindControl("btnLikeAns");
        Label lblTotallikeAns = (Label)e.Item.FindControl("lblTotallikeAns");
        PopUpShare.Style.Add("display", "none");
        LinkButton btnSaveCmnt = (LinkButton)e.Item.FindControl("btnSaveCmnt");
        TextBox CmntAns = (TextBox)e.Item.FindControl("CmntAns");
        Label lblreplyAns = (Label)e.Item.FindControl("lblreplyAns");

        if (e.CommandName == "LikeAns")
        {
            objDOQAPostingAnswer.strAnsLiStatus = "LI";
            objDOQAPostingAnswer.intAnsAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDOQAPostingAnswer.intAnswerId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
            objDAQAPostingAnswer.AddEditDel_Scrl_UserQueAnswerTbl(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.AnsListDtl);
            PopUpShare.Style.Add("display", "none");

            objDOQAPostingAnswer.intAnswerId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
            objDOQAPostingAnswer.intAnsAddedBy = Convert.ToInt32(ViewState["UserID"]);
            dt = objDAQAPostingAnswer.GetDataTable(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.GetTotalAnsLikeById);

            int TotalLike1 = Convert.ToInt32(dt.Rows[0]["TotalLike"]);

            if (Convert.ToString(dt.Rows[0]["LikeUserId"]) != "" && Convert.ToString(dt.Rows[0]["LikeUserId"]) != null)
            {
                if (Convert.ToInt32(dt.Rows[0]["LikeUserId"]) == Convert.ToInt32(ViewState["UserID"]))
                {
                    if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
                    {
                        lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                        btnLikeAns.CssClass = "active-toogle on-flag";
                    }
                    else
                    {
                        lblTotallikeAns.Text = "0 Likes";
                        btnLikeAns.CssClass = "active-toogle";
                    }
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
                    {
                        lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                        btnLikeAns.CssClass = "active-toogle";
                    }
                    else
                    {
                        lblTotallikeAns.Text = "0 Likes";
                        btnLikeAns.CssClass = "active-toogle";
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(dt.Rows[0]["TotalLike"]) > 0)
                {
                    lblTotallikeAns.Text = Convert.ToString(TotalLike1) + ((TotalLike1 == 1) ? " Like" : " Likes");
                    btnLikeAns.CssClass = "active-toogle";
                }
                else
                {
                    lblTotallikeAns.Text = "0 Likes";
                    btnLikeAns.CssClass = "active-toogle";
                }
            }

            //if (ISAPIURLACCESSED != "0")
            //{
            //    try
            //    {

            //        StringBuilder url = new StringBuilder();
            //        url.Append(APIURL);
            //        url.Append("likeAnswer.action?");
            //        url.Append("quesId=");
            //        url.Append(objDOQAPosting.intPostQuestionId);
            //        url.Append("&questionId=");
            //        url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
            //        url.Append("&userId=");
            //        url.Append(objDOQAPosting.intAddedBy);
            //        url.Append("&userName=" + null);
            //        url.Append("&insertDt=");
            //        url.Append(DateTime.Now);
            //        url.Append("&content=");
            //        url.Append(objDOQAPosting.strComment);
            //        url.Append("&title=");
            //        url.Append(objDOQAPosting.strComment);

            //        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
            //        myRequest1.Method = "GET";
            //        WebResponse myResponse1 = myRequest1.GetResponse();

            //        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            //        String result = sr.ReadToEnd();

            //        objAPILogDO.strURL = url.ToString();
            //        objAPILogDO.strAPIType = "Add Answer";
            //        objAPILogDO.strResponse = result;
            //        objAPILogDO.strIPAddress = ip;
            //        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            //        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            //    }
            //    catch { }
            //}
            
            //if (ISAPIURLACCESSED != "0")
            //{
            //    try
            //    {
            //        bool? flag = null;
            //        if (Convert.ToString(dt.Rows[0]["AnsLiStatus"]) != null)
            //        {
            //            if (Convert.ToString(dt.Rows[0]["AnsLiStatus"]) != "LI")
            //            { flag = false; }
            //            else
            //            { flag = true; }
            //        }
            //        else
            //        { flag = false; }

            //        StringBuilder url = new StringBuilder();
            //        url.Append(APIURL);
            //        url.Append("likeAnswer?");
            //        url.Append("ansId=");
            //        url.Append(objDOQAPostingAnswer.intAnswerId);
            //        url.Append("&userId=");
            //        url.Append(objDOQAPostingAnswer.intAnsAddedBy);
            //        url.Append("&flag=");
            //        url.Append(flag);

            //        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
            //        myRequest1.Method = "GET";
            //        WebResponse myResponse1 = myRequest1.GetResponse();

            //        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
            //        String result = sr.ReadToEnd();

            //        objAPILogDO.strURL = url.ToString();
            //        objAPILogDO.strAPIType = "Like/Unlike Answer";
            //        objAPILogDO.strResponse = result;
            //        objAPILogDO.strIPAddress = ip;
            //        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            //        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            //    }
            //    catch { }
            //}
        }

        if (e.CommandName == "btnSaveCmnt")
        {
            //Response.Redirect("Home.aspx");
            DataTable dtAnsCommentsUID = new DataTable();
            DataTable dtQuestion = new DataTable();
            string CommentersName = null;
            string AnswersName = null;
            string QuestionsName = null;
            string QuestionDesc = null;
            string AnswersMail = null;
            string QuestionsMail = null;
            string QuestionId = Convert.ToString(Request.QueryString["PostQAId"]);
            int postQuestionId;
            objDOQAPostingAnswer.strComment = Validations.validateHtmlInput(CmntAns.Text.Trim().Replace(",", "''"));
            objDOQAPostingAnswer.strAnsLiStatus = "CM";
            objDOQAPostingAnswer.intAnswerId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
            objDOQAPostingAnswer.intAnsAddedBy = Convert.ToInt32(ViewState["UserID"]);

            if (hdnCommentEditId.Value == "")
            {
                ResultId = objDAQAPostingAnswer.AddEditDel_Scrl_UserAddingAnsPost(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.UpdateCommentById);
                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("commentAnswer?");
                        url.Append("answerId=");
                        url.Append(objDOQAPostingAnswer.intAnswerId);
                        url.Append("&questionId=");
                        url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
                        url.Append("&content=");
                        url.Append(objDOQAPostingAnswer.strComment);
                        url.Append("&commentByUid=");
                        url.Append(objDOQAPostingAnswer.intAnsAddedBy);
                        url.Append("&commentId=");
                        url.Append(ResultId);

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url.ToString();
                        objAPILogDO.strAPIType = "Add Comment Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }
                objDOQAPostingAnswer.ID = ResultId;
                dtAnsCommentsUID.Clear();
                dtAnsCommentsUID = objDAQAPostingAnswer.GetDataTableCommentsByID(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.GetUserdtlByCmnyId);
                CommentersName = Convert.ToString(dtAnsCommentsUID.Rows[0]["UserName"]);
                AnswersName = Convert.ToString(dtAnsCommentsUID.Rows[0]["AnsUserName"]);
                postQuestionId = Convert.ToInt32(dtAnsCommentsUID.Rows[0]["QuestionID"]);
                objDOQAPosting.intPostQuestionId = postQuestionId;
                dtQuestion.Clear();
                dtQuestion = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSingleQueAnsDetails);
                QuestionsName = Convert.ToString(dtQuestion.Rows[0]["AuthorName"]); ;
                QuestionDesc = Convert.ToString(dtQuestion.Rows[0]["strQuestionDescription"]); 
                AnswersMail = Convert.ToString(dtAnsCommentsUID.Rows[0]["AnsUserMail"]);
                QuestionsMail = Convert.ToString(dtQuestion.Rows[0]["QuestionersMail"]);
                // Need to check with Rajat
                if (Convert.ToInt32(dtAnsCommentsUID.Rows[0]["UserId"]) != Convert.ToInt32(dtAnsCommentsUID.Rows[0]["AnsUserId"]))
                {
                    SendMailAnswerers(CommentersName, AnswersName, QuestionsName, QuestionDesc, AnswersMail, QuestionId);
                }
                if (Convert.ToInt32(dtAnsCommentsUID.Rows[0]["UserId"]) != Convert.ToInt32(dtQuestion.Rows[0]["intAddedBy"]))
                {
                    SendMailQuestioners(CommentersName, AnswersName, QuestionsName, QuestionDesc, QuestionsMail, QuestionId);
                }
            }
            else
            {
                // TODO Edit

                objDOQAPostingAnswer.ID = Convert.ToInt32(hdnCommentEditId.Value);
                objDAQAPostingAnswer.AddEditDel_Scrl_UserQueAnsUpdateCmntsTbl(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.UpdateCommentBytblId);

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("commentAnswer?");
                        url.Append("answerId=");
                        url.Append(objDOQAPostingAnswer.intAnswerId);
                        url.Append("&questionId=");
                        url.Append(Convert.ToInt32(Request.QueryString["PostQAId"]));
                        url.Append("&content=");
                        url.Append(objDOQAPostingAnswer.strComment);
                        url.Append("&commentByUid=");
                        url.Append(objDOQAPostingAnswer.intAnsAddedBy);
                        url.Append("&commentId=");
                        url.Append(Convert.ToInt32(hdnCommentEditId.Value));

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url.ToString();
                        objAPILogDO.strAPIType = "Update Comment Answer";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }
            }
            CmntAns.Text = "";
            hdnCommentEditId.Value = "";

            dtchild = objDAQAPostingAnswer.GetDataTableComments(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.CommentsDtlsById);
            
            ListView lstChild = e.Item.FindControl("lstAllComments") as ListView;
            lstChild.DataSource = dtchild;
            lstChild.DataBind();

            int TotalComment = lstChild.Items.Count;

            if (TotalComment > 0)
            {
                lblreplyAns.Text = Convert.ToString(TotalComment) + ((TotalComment == 1) ? " Comment" : " Comments");
            }
            else
            {
                lblreplyAns.Text = "0 Comments";
            }
        }

        if (e.CommandName == "View More")
            {
                GetAllReplies();
            }
        else
        if (e.CommandName == "View Close")
            {
                GetAllReplies();
            }
        else
        if (e.CommandName == "Edit Ans")
            {
                ViewState["Edit"] = hdnQAReplyLikeShareId.Value;
                objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(hdnQAReplyLikeShareId.Value);
                dtAns = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetCommentByID);
                if (dtAns.Rows.Count > 0)
                {
                    CKEditorControl.InnerText = Convert.ToString(dtAns.Rows[0]["strComment"]);
                    CKEditorControl.Focus();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#editqa';", true);
                }
            }
        else
        if (e.CommandName == "Delete Ans")
            {
                ViewState["lblReplyComment"] = lblReplyComment.Text;
                divDeletesucess.Style.Add("display", "block");
            }
        else
        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnintAddedBy.Value);
        }
        else
        if (e.CommandName == "Edit Comments")
        {
            hdnCommentEditId.Value = hdnintCommentDelete.Value;
            CmntAns.Text = hdnCommentEdit.Value;
            CmntAns.Focus();
        }
    }

    private void SendMailOnAnswer(string AnswerersName, string QuestionersName, string QuestionDesc, string QuestionersMail, string QuestionId)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = QuestionersMail;
            string MailURLFinal = MailURL + "/Question-Details-SendContact.aspx?PostQAId=" + QuestionId;

            dtRecord.Clear();
            objdoreg.UserName = MailTo;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", QuestionersName);
            body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + AnswerersName +
                                                "</span> has answered your question - " + "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + QuestionDesc + "</span>" +
                        "<br><br><a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>View Response</a>");
        
            string subject = AnswerersName + " has responded to your Question";
            EmailSender.SendEmail(MailTo, subject, body);

            FCMNotification.Send(AnswerersName + " has responded to your Question", AnswerersName + " has answered your question - " + QuestionDesc,
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailURLFinal);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void SendMailQuestioners(string CommenterName, string AnswersName, string QuestionsName, string QuestionDesc, string QuestionsMail, string QuestionId)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = QuestionsMail;
            string MailURLFinal = MailURL + "/Question-Details-SendContact.aspx?PostQAId=" + QuestionId;

            dtRecord.Clear();
            objdoreg.UserName = MailTo;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            //"<a href='" + MailURL + "/SignUp1.aspx?id=" + Session["ExternalUserId"] + "'>" + MailURL + "/SignUp1.aspx" + encript + "</a>"
            body = body.Replace("{UserName}", QuestionsName);
            body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + CommenterName +
                                                "</span> has commented on " +
                                                "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + AnswersName + "'s " +
                                                "</span>answer to your question - <span style='font-weight:bold;font-size:14px;color:#373A36;'> " + QuestionDesc + "</span>" +
                        "<br><br><a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>View Comment</a>"); 
       
        
            string subject = CommenterName + " has commented on an answer to your question";
            EmailSender.SendEmail(MailTo, subject, body);

            FCMNotification.Send(CommenterName + " has commented on an answer to your question", CommenterName + " has commented on " + AnswersName + "’s answer to your question " + QuestionDesc + ".",
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailURLFinal);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void SendMailAnswerers(string CommenterName, string AnswersName, string QuestionsName, string QuestionDesc, string AnswersMail, string QuestionId)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = AnswersMail;
            string MailURLFinal = MailURL + "/Question-Details-SendContact.aspx?PostQAId=" + QuestionId;

            dtRecord.Clear();
            objdoreg.UserName = MailTo;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", AnswersName);
            body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + CommenterName +
                                                "</span> has commented on your answer to " +
                                                "<span><span style='font-weight:bold;font-size:14px;color:#373A36;'>" + QuestionsName + "'s " +
                                                "</span>Question - <span style='font-weight:bold;font-size:14px;color:#373A36;'> " + QuestionDesc + "</span>" +
                        "<br><br><a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>View Comment</a>");    
            string subject = CommenterName + " has commented on your answer";
            EmailSender.SendEmail(MailTo, subject, body);

            FCMNotification.Send(CommenterName + " has commented on your answer", CommenterName + " has commented on your answer to " + QuestionsName + "’s Question " + QuestionDesc + ".",
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailURLFinal);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        objDOQAPosting.intQAReplyLikeShareId = Convert.ToInt32(ViewState["QAReplyLikeShareId "]);
        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteAnsCmnts);
        BindQADetails();
        GetAllReplies();
        //upasss.rel
        divDeletesucess.Style.Add("display", "none");
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        //check this
        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("removeAnswer?");
                url.Append("answerId=");
                url.Append(objDOQAPosting.intQAReplyLikeShareId);
                url.Append("&userId=");
                url.Append(objDOQAPosting.intAddedBy);

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url.ToString();
                objAPILogDO.strAPIType = "Deleting Answer";
                objAPILogDO.strResponse = result;
                objAPILogDO.strIPAddress = ip;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch { }
        }

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["QAReplyLikeShareId"]);
        objLog.strAction = "Q&A Ans";
        objLog.strActionName = Convert.ToString(ViewState["lblReplyComment"]); ;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 25;   // Q&A Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
    }

    protected void lnkDeleteCmntConfirm_Click(object sender, EventArgs e)
    {
        DataTable dtAnsComments = new DataTable();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        //HiddenField hdnCommentEditId = (HiddenField)e.Item.FindControl("hdnCommentEditId");
        //string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        objDOQAPostingAnswer.ID = Convert.ToInt32(hdnintCommentDelete.Value);
        dtAnsComments = objDAQAPostingAnswer.GetDataTableCommentsByID(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.CommentsDtlsBytblId);
        objDOQAPostingAnswer.ID = Convert.ToInt32(hdnintCommentDelete.Value);
        objDAQAPostingAnswer.AddEditDel_Scrl_UserQueAnsDeleteCmntsTbl(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.DeleteAnsCmnts);
        
        //check this
        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                //http://localhost:8080/SkorkelWeb/removeCommentAnswer?answerId=123424&userId=10067&commentId=12547
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("removeCommentAnswer?");
                url.Append("answerId=");
                url.Append(Convert.ToInt32(dtAnsComments.Rows[0]["intAnswerId"]));
                url.Append("&userId=");
                url.Append(Convert.ToInt32(ViewState["UserID"]));
                url.Append("&commentId=");
                url.Append(Convert.ToInt32(hdnintCommentDelete.Value));

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url.ToString();
                objAPILogDO.strAPIType = "Dalating Comment Answer";
                objAPILogDO.strResponse = result;
                objAPILogDO.strIPAddress = ip;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch { }
        }

        divDeletesucessCmnts.Style.Add("display", "none");

        GetAllReplies();
    }

    protected void btnAllQuestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("AllQuestionDetails.aspx");
    }

    protected void lstRelQuestions_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionId = (HiddenField)e.Item.FindControl("hdnPostQuestionId");
        if (e.CommandName == "OpenQ")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionId.Value);
        }
    }

    protected void lnkBack_click(object sender, EventArgs e)
    {
        if (ViewState["MP"] != null)
        {
            Response.Redirect("AllQuestionDetails.aspx?MP=" + Convert.ToString(ViewState["MP"]));
        }
        else
        {
            Response.Redirect("AllQuestionDetails.aspx");
        }
    }


    protected void lstAllReplies_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChild = e.Item.FindControl("lstAllComments") as ListView;
        lstChild.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstAllComments_ItemDataBound);
    }

    protected void lstAllComments_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtAnsComments = new DataTable();
        HtmlControl editDelCmnts = (HtmlControl)e.Item.FindControl("editDelCmnts");
        HiddenField hdnChildtblId = (HiddenField)e.Item.FindControl("hdnChildtblId");
        objDOQAPostingAnswer.ID = Convert.ToInt32(hdnChildtblId.Value);
        dtAnsComments = objDAQAPostingAnswer.GetDataTableCommentsByID(objDOQAPostingAnswer, DA_Scrl_UserQAPostingAnswer.QuetionAns.CommentsDtlsBytblId);        

        if (Convert.ToInt32(ViewState["UserID"]) != Convert.ToInt32(dtAnsComments.Rows[0]["intAnsAddedBy"]))
        {
            editDelCmnts.Style.Add("display", "none");
        }

    }
    public string GetStatusImage(string status)
    {
        if (status != "")
        {            
            string TargetDir = Server.MapPath("").ToString() + "\\CroppedPhoto\\" + Eval("PhotoPath");
            if (File.Exists(TargetDir))
            {
                return "CroppedPhoto/" + Eval("PhotoPath");
            }
            else
            {
                return "images/profile-photo.png";
            }
        }
        else
        {
            return "images/profile-photo.png";
        }
    }
}