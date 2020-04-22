using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Configuration;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Data.SqlClient;

public partial class BlogsComments : System.Web.UI.Page
{
    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DO_BlogLikeShare objBlogLikeShare = new DO_BlogLikeShare();
    DA_BlogLikeShare objBlogLikeShareDB = new DA_BlogLikeShare();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();
    DataTable dt = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl myObject;
        myObject = (HtmlContainerControl)Master.FindControl("blogWrite");
        myObject.Visible = true;
        txtInviteMembers.selectPlaceholder = "Enter member names here";
        divSuccess.Style.Add("display", "none");
        divDeletesucessCmnts.Style.Add("display", "none");
        if (!IsPostBack)
        {
            divDeletesucessCmnts.Style.Add("display", "none");
            divDeletesucess.Style.Add("display", "none");
            divSuccess.Style.Add("display", "none");
            ViewState["intBlogId"] = Convert.ToInt32(Request.QueryString["intBlogId"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            else
            {
                Response.Redirect("~/Landing.aspx", true);
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Blog";

            if (Request.QueryString["Blogtype"] != null && Request.QueryString["Blogtype"].ToString() == "R")
            {
                lnkBacklink.Text = "Recent Blogs";
            }

            //if (Request.QueryString["DeleteComment"] != null && Request.QueryString["DeleteComment"] != "")
            //{
            //    divSuccess.Style.Add("display", "block");
            //    lblSuccess.Text = "Successfully deleted.";
            //    Master.BodyTag.Attributes.Add("class", "remove-scroller");
            //}

            CalculateViewBlog();
            BindBlogList();
            BindComments();
            HideShowSubject();
            getInviteeName();
        }
    }

    protected void CalculateViewBlog()
    {
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        if (Convert.ToString(Request.QueryString["hdnAddedBy"]) != Convert.ToString(ViewState["UserID"]))
            objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.AddConsumedBlog);
    }

    private void BindBlogList()
    {
        DataTable dtSub = new DataTable();

        if (Convert.ToString(Request.QueryString["PostBlogID"]) != null && Convert.ToString(Request.QueryString["PostBlogID"]) != "")
        {
            int SrchPostBlogsId = Convert.ToInt32(Request.QueryString["PostBlogID"]);
            objblogdo.intBlogId = SrchPostBlogsId;
            objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        }

        else
        {
            objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        }
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsDetails);
        if (dtSub.Rows.Count > 0)
        {
            int CommentCount = Convert.ToInt32(dtSub.Rows[0]["CommentCount"]);
            if (CommentCount > 0)
            {
                totalComments.InnerText = Convert.ToString(CommentCount) + ((CommentCount == 1) ? " Comment" : " Comments");
            }
            else
            {
                totalComments.Visible = false;
            }
            lstAllBlogs.DataSource = dtSub;
            lstAllBlogs.DataBind();
        }
        else
        {
            lstAllBlogs.DataSource = null;
            lstAllBlogs.DataBind();
            totalComments.Visible = false;
        }
    }

    private void BindComments()
    {
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTableComment(objblogdo, DA_NewBlogs.BlogCommnet.GetByBlog);
        if (dtSub.Rows.Count > 0)
        {
            
            RepPopPost.DataSource = dtSub;
            RepPopPost.DataBind();
        }
        else
        {
            RepPopPost.DataSource = null;
            RepPopPost.DataBind();
            
        }
    }

    protected void lstAllBlogs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        PopUpShare.Style.Add("display", "none");
        HiddenField hdnBlogLikeUserId = e.Item.FindControl("hdnBlogLikeUserId") as HiddenField;
        LinkButton btnBlogLike = e.Item.FindControl("btnBlogLike") as LinkButton;
        Label Label1 = e.Item.FindControl("Label1") as Label;

        objBlogLikeShare.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objBlogLikeShare.strIpAddress = ip;
        objBlogLikeShare.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        divSuccess.Style.Add("display", "none");

        if (e.CommandName == "LikeBlog")
        {
            divDeletesucess.Style.Add("display", "none");
            PopUpShare.Style.Add("display", "none");
            objBlogLikeShare.strRepLiShStatus = "LI";
            objBlogLikeShareDB.AddEditDel_BlogHeadingLikeShareTbl(objBlogLikeShare, DA_BlogLikeShare.BlogHeadingShareLikeFlag.BlogHeadingLike);
            PopUpShare.Style.Add("display", "none");
            BindBlogList();
        }

        if (e.CommandName == "ShareBlog")
        {
            divDeletesucess.Style.Add("display", "none");
            clearsharepopup();
            string path = Request.Url.AbsoluteUri;
            txtLink.Text = path;
            PopUpShare.Style.Add("display", "block");
            BindBlogList();
        }
        else if (e.CommandName == "Edit Blog")
        {
            Response.Redirect("write-blog.aspx?BlogId=" + ViewState["intBlogId"]);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
        }
        else if (e.CommandName == "Delete Blog")
        {
            ViewState["blognames"] = Label1.Text;
            PopUpShare.Style.Add("display", "none");
            divDeletesucess.Style.Add("display", "block");
        }
    }

    protected void lstAllBlogs_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtLike = new DataTable();
        HiddenField hdnBlogLikeUserId = e.Item.FindControl("hdnBlogLikeUserId") as HiddenField;
        LinkButton btnBlogLike = e.Item.FindControl("btnBlogLike") as LinkButton;
        Label lblTotalBloglike = e.Item.FindControl("lblTotalBloglike") as Label;
        ListView lstSubjCategory = (ListView)e.Item.FindControl("lstSubjCategory");
        HiddenField hdnintAddedBy = e.Item.FindControl("hdnintAddedBy") as HiddenField;
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkdelete = (LinkButton)e.Item.FindControl("lnkdelete");
        Label Label1 = (Label)e.Item.FindControl("Label1");
        HtmlGenericControl dvEditDeletePostDots = (HtmlGenericControl)e.Item.FindControl("dvEditDeletePostDots");

        HtmlGenericControl SubList = (HtmlGenericControl)e.Item.FindControl("SubList");
        ViewState["Blogname"] = Label1.Text;
        

        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetSubjectListById);
        if (dtSub.Rows.Count > 0)
        {
            lstSubjCategory.DataSource = dtSub;
            lstSubjCategory.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
            SubList.Visible = false;
        }

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(hdnintAddedBy.Value))
        {
            lnkdelete.Visible = true;
            lnkEdit.Visible = true;
            dvEditDeletePostDots.Visible = true;
        }

        if (hdnBlogLikeUserId.Value == ViewState["UserID"].ToString())
        {
            btnBlogLike.CssClass = "active-toogle on-flag";
        }

        lblTotalBloglike.ToolTip = "View Likes";

        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtLike = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetLikeUsers);
        if (dtLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtLike.Rows.Count; i++)
            {
                if (lblTotalBloglike.ToolTip != "View Likes")
                    lblTotalBloglike.ToolTip += Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lblTotalBloglike.ToolTip = Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
            
        }
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (ViewState["blognames"] != null)
        {
            objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
            objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.DeleteBlog);

            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["intBlogId"]);
            objLog.strAction = "Blog";
            objLog.strActionName = ViewState["blognames"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 24;   // Blog Delete
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("removeBlog?");
                    url.Append("blogId=");
                    url.Append(Convert.ToInt32(ViewState["intBlogId"]));
                    url.Append("&userId=");
                    url.Append(Convert.ToInt32(ViewState["UserID"]));

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Deleting Blog";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }

            //Response.Redirect("AllBlogs.aspx");
            Response.Redirect("~/AllBlogs.aspx?DeleteBlog=true");
        }
        else
        {

            objblogdo.intCommentId = Convert.ToInt32(ViewState["hdnCommentID"]);
            objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
            objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            objblogda.AddEditDel_NewBlogComment(objblogdo, DA_NewBlogs.BlogCommnet.Delete);
            BindBlogList();
            BindComments();
            divDeletesucess.Style.Add("display", "none");

            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["hdnCommentID"]);
            objLog.strAction = "Blog Comment";
            objLog.strActionName = ViewState["blogCommentnames"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 24;   // Blog Delete
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            //Response.Redirect("~/BlogsComments.aspx?DeleteComment=true");
        }

    }

    protected void clear()
    {
        ViewState["Edit"] = null;
        CKDescription.InnerText = "";
        BindBlogList();
        BindComments();
    }

    protected void clearsharepopup()
    {
        txtInviteMembers.RefreshList();
        getInviteeName();
        txtBody.InnerText = "";
        txtLink.Text = "";
        lblMess.Text = "";
        hdnDepartmentIds.Value = "";
    }

    protected void lnkSubmitBlog_Click(object sender, EventArgs e)
    {
        Response.Redirect("write-blog.aspx");
    }

    protected void lnkPostComent_Click(object sender, EventArgs e)
    {
        DataTable dtBlog = new DataTable();
        DataTable dtSub = new DataTable();
        string CommentersName = null;
        string BlogersMail = null;
        string BlogersName = null;
        string BlogersDesc = null;
        
        divDeletesucess.Style.Add("display", "none");
        PopUpShare.Style.Add("display", "none");
        lblMessage.Text = "";
        if (CKDescription.InnerText.Trim() != "")
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objblogdo.strIPAddress = ip;
            if (ViewState["Edit"] == null)
            {

                objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                objblogdo.strComment = CKDescription.InnerHtml.Trim();
                objblogda.AddEditDel_NewBlogComment(objblogdo, DA_NewBlogs.BlogCommnet.Add);
                clear();
                lblSuccess.Text = "Comment posted successfully.";
                divSuccess.Style.Add("display", "block");
                totalComments.Visible = true;
                objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                dtSub = objblogda.GetDataTableComment(objblogdo, DA_NewBlogs.BlogCommnet.GetByBlog);
                objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                DataTable dtblog = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetBlogsEdit);
                BlogersMail = Convert.ToString(dtblog.Rows[0]["BlogersMail"]);
                CommentersName = Convert.ToString(dtSub.Rows[0]["strAddedBy"]);
                BlogersName = Convert.ToString(dtblog.Rows[0]["strAddedBy"]);
                BlogersDesc = Convert.ToString(dtblog.Rows[0]["strBlogHeading"]);
                if (Convert.ToInt32(dtSub.Rows[0]["intAddedBy"]) != Convert.ToInt32(dtblog.Rows[0]["intAddedBy"]))
                {
                    SendMail(CommentersName, BlogersMail, BlogersName, BlogersDesc);
                }
            }
            else
            {
                
                objblogdo.intCommentId = Convert.ToInt32(ViewState["Edit"]);
                objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                objblogdo.strComment = CKDescription.InnerHtml.Trim();
                objblogda.AddEditDel_NewBlogComment(objblogdo, DA_NewBlogs.BlogCommnet.Update);
                clear();
                lblSuccess.Text = "Comment updated successfully.";
                divSuccess.Style.Add("display", "block");
                totalComments.Visible = true;
            }
        }
        else
        {
            lblMessage.Text = "Please enter comment.";
        }
    }

    private void SendMail(string CommenterName, string BlogersMail, string BlogersName, string BlogersDesc)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            string body = null;

            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = BlogersMail;
            string MailURLFinal = MailURL + "/BlogsComments.aspx?intBlogId=" + Convert.ToString(ViewState["intBlogId"]);

            dtRecord.Clear();
            objdoreg.UserName = MailTo;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", BlogersName);
            body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + CommenterName +
                                                "</span> has commented on your blog titled " +
                                                "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + BlogersDesc + "</span>" +
                        "<br><br><a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>View Comment</a>");

            string subject = CommenterName + " has commented on your blog";
            EmailSender.SendEmail(MailTo, subject, body);

            FCMNotification.Send(CommenterName + " has commented on your blog", CommenterName + " has commented on your blog titled " + BlogersDesc,
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailURLFinal);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void RepPopPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        PopUpShare.Style.Add("display", "none");
        divDeletesucess.Style.Add("display", "none");
        HiddenField hdnBlogCommentUser = (HiddenField)e.Item.FindControl("hdnBlogCommentUser");
        HiddenField hdnCommentID = (HiddenField)e.Item.FindControl("hdnCommentID");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        Label lblCommentAll = e.Item.FindControl("lblCommentAll") as Label;
        ViewState["hdnCommentID"] = hdnCommentID.Value;
        objBlogLikeShare.intCommentID = Convert.ToInt32(hdnCommentID.Value);
        objBlogLikeShare.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objBlogLikeShare.strIpAddress = ip;
        objBlogLikeShare.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        divSuccess.Style.Add("display", "none");
        if (e.CommandName == "View profile")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnintAddedBy.Value);
        }
        else
            if (e.CommandName == "LikeForum")
            {
                PopUpShare.Style.Add("display", "none");
                objBlogLikeShare.strRepLiShStatus = "LI";
                objBlogLikeShareDB.AddEditDel_BlogCommentLikeShare(objBlogLikeShare, DA_BlogLikeShare.BlogShareLikeFlag.BlogLike);
                PopUpShare.Style.Add("display", "none");
                BindComments();
            }
            else
                if (e.CommandName == "ShareForum")
                {
                    ViewState["intCommentID"] = hdnCommentID.Value;
                    Label lblComment = (Label)e.Item.FindControl("lblComment");
                    string DocTitle = lblComment.Text;
                    lblTitle.Text = lblComment.Text;
                    PopUpShare.Style.Add("display", "block");
                }
                else
                    if (e.CommandName == "ViewMore")
                    {
                        Label lblComment = (Label)e.Item.FindControl("lblComment");
                        LinkButton lnkViewSubj = (LinkButton)e.Item.FindControl("lnkViewSubj");
                        if (lnkViewSubj.Text == "view more")
                        {
                            lblComment.Visible = false;
                            lblCommentAll.Visible = true;
                            lnkViewSubj.Text = "Close";
                            PopUpShare.Style.Add("display", "none");
                        }
                        else
                        {
                            lblComment.Visible = true;
                            lblCommentAll.Visible = false;
                            lnkViewSubj.Text = "view more";
                            PopUpShare.Style.Add("display", "none");
                        }
                    }
                    else
                        if (e.CommandName == "Edit Blog")
                        {
                            ViewState["Edit"] = hdnCommentID.Value;
                            if (ip == null)
                                ip = Request.ServerVariables["REMOTE_ADDR"];
                            objblogdo.strIPAddress = ip;
                            objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                            objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                            objblogdo.intCommentId = Convert.ToInt32(hdnCommentID.Value);
                            DataTable dtblog = objblogda.GetDataTableComment(objblogdo, DA_NewBlogs.BlogCommnet.GetCommentById);
                            if (dtblog.Rows.Count > 0)
                            {
                                CKDescription.InnerText = Convert.ToString(dtblog.Rows[0]["strComment"]);
                                CKDescription.Focus();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
                            }

                        }
                        else
                            if (e.CommandName == "Delete Blog")
                            {
                                ViewState["blogCommentnames"] = lblCommentAll.Text;
                                divDeletesucessCmnts.Style.Add("display", "block");
                            }

    }

    protected void RepPopPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dtLike = new DataTable();
        DataTable dtSub = new DataTable();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnBlogCommentUser = (HiddenField)e.Item.FindControl("hdnBlogCommentUser");
            HiddenField hdnCommentID = (HiddenField)e.Item.FindControl("hdnCommentID");
            HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("img1");
            HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
            LinkButton lnkViewSubj = (LinkButton)e.Item.FindControl("lnkViewSubj");
            lnkViewSubj.Visible = false;
            Label lblComment = (Label)e.Item.FindControl("lblCommentAll");
            Label lblPostedOn = (Label)e.Item.FindControl("lblPostedOn");
            HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
            LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
            LinkButton lnkdelete = (LinkButton)e.Item.FindControl("lnkdelete");
            HtmlGenericControl dvEditDeleteComment = (HtmlGenericControl)e.Item.FindControl("dvEditDeleteComment");

            if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(hdnintAddedBy.Value))
            {
                lnkdelete.Visible = true;
                lnkEdit.Visible = true;
                dvEditDeleteComment.Visible = true;
            }

            if (lblPostedOn.Text == DateTime.Today.ToString("dd MMM yyyy"))
            {
                lblPostedOn.Text = "Today";
            }
            else if (lblPostedOn.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
            {
                lblPostedOn.Text = "Yesterday";
            }

            if (lblComment.Text.Length > 60)
            {
                lnkViewSubj.Visible = true;
            }

            objblogdo.intCommentId = Convert.ToInt32(hdnCommentID.Value);
            if (imgprofile.Src == "")
            {
                imgprofile.Src = "../images/profile-photo.png";
            }
            else
            {
                string imgPathPhysical = Server.MapPath("~/" + hdnimgprofile.Value);
                if (File.Exists(imgPathPhysical))
                {
                }
                else
                {
                    imgprofile.Src = "images/profile-photo.png";
                }
            }
        }
    }

    protected void lnkViewSubj_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        List<KeyValuePair<string, string>> members = txtInviteMembers.GetSelectedValues();
        if (members.Any())
        {
            objBlogLikeShare.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
            string membersValues = String.Join(",", members.Select(s => s.Value.ToString()).ToArray());
            objBlogLikeShare.strInviteeShare = membersValues;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objBlogLikeShare.strIpAddress = ip;
            objBlogLikeShare.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objBlogLikeShare.strRepLiShStatus = "SH";
            if (txtBody.InnerText.Trim().Replace("'", "''") != "Message")
            {
                objBlogLikeShare.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
            }
            else
            {
                objBlogLikeShare.strMessage = "";
            }
            objBlogLikeShare.strLink = txtLink.Text;
            objBlogLikeShare.strBlogTitle = ViewState["Blogname"].ToString();

            objBlogLikeShareDB.AddEditDel_BlogHeadingLikeShareTbl(objBlogLikeShare, DA_BlogLikeShare.BlogHeadingShareLikeFlag.BlogHeadingShare);
            hdnDepartmentIds.Value = "";
            PopUpShare.Style.Add("display", "none");
            BindBlogList();
            lblSuccess.Text = "Blog shared successfully.";
            divSuccess.Style.Add("display", "block");
            txtInviteMembers.RefreshList();
            txtBody.InnerText = "";
            txtLink.Text = "";
            hdnDepartmentIds.Value = "";
            getInviteeName();
        }
        else
        {
            lblMess.Text = "Please select member";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void getInviteeName()
    {
        DO_Networks objdonetwork = new DO_Networks();
        DA_Networks objdanetwork = new DA_Networks();
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

    protected void btnpopCancel_Click(object sender, EventArgs e)
    {
        clear();
        PopUpShare.Style.Add("display", "none");
        divSuccess.Style.Add("display", "none");
        ViewState["ID"] = "";
        txtInviteMembers.RefreshList();
        txtBody.InnerText = "";
        txtLink.Text = "";
        getInviteeName();
    }

    #region Search Context

    protected void SubjectSearchTempDataTable()
    {
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

    private void BindSearchSubjectList()
    {
        DataTable dtSub = new DataTable();

        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSerchSubjCategory.DataSource = dtSub;
            lstSerchSubjCategory.DataBind();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.DataBind();
        }
    }

    protected void lstSerchSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();

        string ID = "";
        SubLi.Attributes.Add("class", "unselectBlogLi");
        if (ViewState["SubjectSearchCategory"] != null)
        {
            if (Convert.ToString(ViewState["SubjectSearchCategory"]) != "")
            {
                string[] totalSubjects = ViewState["SubjectSearchCategory"].ToString().Split(',');
                var dictionary = new Dictionary<int, int>();

                if (totalSubjects.Count() > 0)
                {
                    Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                                  .ToDictionary(g => g.Key,
                                                                g => g.Count());

                    foreach (var v in counts)
                    {
                        if (v.Value == 1)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 3)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 5)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 7)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 9)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }

                        if (hdnSubCatId.Value == ID.ToString())
                        {
                            SubLi.Attributes.Add("class", "selectBlogLi");
                            lnkCatName.Attributes.Add("class", "selectBlogcat");

                        }
                        ID = "";
                    }
                }
            }
        }

    }

    protected void HideShowSubject()
    {
        BindSearchSubjectList();
        if (lnkViewSubj1.Text == "View all")
        {
            BindTopSubjectList();
        }
        else
        {
            BindSearchSubjectList();
        }
    }

    private void BindTopSubjectList()
    {
        DataTable dtTopSub = new DataTable();
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetTopRecords);
        if (dtTopSub.Rows.Count > 0)
        {
            lstSerchSubjCategory.DataSource = dtTopSub;
            lstSerchSubjCategory.DataBind();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.DataBind();
        }
    }

    protected void lnkViewSubj1_Click(object sender, EventArgs e)
    {
        ViewState["SubjectSearchCategory"] = hdnSubject.Value;
        divDeletesucess.Style.Add("display", "none");
        ViewState["Update"] = "Update";
        if (lnkViewSubj1.Text == "View all")
        {
            lnkViewSubj1.Text = "Close";
        }
        else
        {
            lnkViewSubj1.Text = "View all";
        }
        HideShowSubject();
        divSuccess.Style.Add("display", "none");
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BindSubjectList();
    }

    private void BindSubjectList()
    {
        DataTable dtSub = new DataTable();
        String BlogId = "", ID = string.Empty;

        if (txtblogsearch.Text != "" && txtblogsearch.Text != "Search")
        {
            objblogdo.strSearch = txtblogsearch.Text.Trim().Replace("'", "''");

            if (hdnSubject.Value != "")
            {
                string[] totalSubjects = hdnSubject.Value.Split(',');
                var dictionary = new Dictionary<int, int>();

                if (totalSubjects.Count() > 0)
                {
                    Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
                                                  .ToDictionary(g => g.Key,
                                                                g => g.Count());

                    foreach (var v in counts)
                    {
                        if (v.Value == 1)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 3)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 5)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 7)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }
                        else if (v.Value == 9)
                        {
                            if (string.IsNullOrEmpty(ID))
                                ID = Convert.ToString(v.Key);
                            else
                                ID += "," + Convert.ToString(v.Key);
                        }

                    }
                }
            }

            objblogdo.ID = ID;
            objblogdo.CurrentPage = Convert.ToInt32(1);
            objblogdo.CurrentPageSize = Convert.ToInt32(10);
            dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetSearchResult);

            for (int i = 0; i < dtSub.Rows.Count; i++)
            {
                BlogId = Convert.ToString(dtSub.Rows[i]["intBlogId"]);
                Session["intBlogId"] += "," + BlogId;
            }
            Response.Redirect("AllBlogs.aspx");
        }
    }

    protected void lnkBacklink_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (lnkBacklink.Text == "Recent Blogs")
        {
            Response.Redirect("AllBlogs.aspx?Blogtype=R");
        }
        else
        {
            Response.Redirect("AllBlogs.aspx?Blogtype=A");
        }


    }

}