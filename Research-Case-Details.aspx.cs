using DA_SKORKEL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Research_Case_Details : System.Web.UI.Page
{
    DO_Role ObjRole = new DO_Role();
    DA_Role ObjRoleDB = new DA_Role();
    DA_Case objdacaseDB = new DA_Case();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DA_CaseComment objdacase = new DA_CaseComment();
    DO_Case objdocase = new DO_Case();

    DO_Comment objcomment = new DO_Comment();
    DA_Comment dbcomment = new DA_Comment();

    DO_CaseList objDoCaseList = new DO_CaseList();
    DA_CaseList objCaseListDb = new DA_CaseList();

    DA_ContentTagDef objTagDefDB = new DA_ContentTagDef();
    DO_ContentTagDef objTagDef = new DO_ContentTagDef();

    DO_ContentLink objLink = new DO_ContentLink();
    DA_ContentLink objLinkDb = new DA_ContentLink();

    DO_ContentRating objRating = new DO_ContentRating();
    DA_ContentRating objRatingDb = new DA_ContentRating();

    DA_ContentFact objFactDB = new DA_ContentFact();
    DO_ContentFact objFact = new DO_ContentFact();

    DO_ContentRatio objRatio = new DO_ContentRatio();
    DA_ContentRatio objRatioDB = new DA_ContentRatio();

    DA_ContentSummary objSummaryDb = new DA_ContentSummary();
    DO_ContentSummary objSummary = new DO_ContentSummary();

    DO_Mark objmark = new DO_Mark();
    DA_Mark dbmark = new DA_Mark();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Ratio objDARatio = new DA_Ratio();
    DO_Ratio objDORatio = new DO_Ratio();

    DO_Scrl_UserForumPosting objDOBForumPosting = new DO_Scrl_UserForumPosting();
    DA_Scrl_UserForumPosting objDAForumPosting = new DA_Scrl_UserForumPosting();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_ContentRelevantUser objDORelevntU = new DO_ContentRelevantUser();
    DA_ContentRelevantUser objDARelevntU = new DA_ContentRelevantUser();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();
    Int32 CaseID = 0;
    Int64 CaseId = 0, ContentTypeID = 0;
    string BrowserUsing = "";
    string strTagTypeId = string.Empty, Doc = "";
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string UserTypeId = "S";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }

        if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
            ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());


        int ii = Convert.ToInt32(hdnBack.Value);
        ++ii;
        hdnBack.Value = ii.ToString();

        if (Request.QueryString["CTid"] != null && Request.QueryString["CTid"] != "")
        {
            ContentTypeID = Convert.ToInt32(Request.QueryString["CTid"].Trim());
            ViewState["CTypeID"] = Convert.ToInt32(Request.QueryString["CTid"].Trim());
        }

        if (Request.QueryString["cId"] != null && Request.QueryString["cId"] != "")
        {
            CaseID = Convert.ToInt32(Request.QueryString["cId"]);
            ViewState["ContentID"] = Convert.ToInt32(Request.QueryString["cId"].Trim());
        }

        if (Request.QueryString["intCommentAddedFor"] != null && Request.QueryString[""] != "intCommentAddedFor")
        {
            ViewState["intCommentAddedFor"] = Convert.ToInt32(Request.QueryString["intCommentAddedFor"].Trim());
        }

        PopUpShare.Style.Add("display", "none");
        if (!IsPostBack)
        {

            hdnLoginId.Value = Convert.ToString(ViewState["UserID"]);
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            BrowserUsing = browser.Browser;
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Research";

            intCommentAddedFors.Value = "";
            lblOriginalTxt.Text = "Original Text";
            userSelected.InnerText = "Select User";
            userSelected.Attributes["class"] = "";
            lblHighBy.Visible = false;
            divUserStart.Visible = false;
            divusrPosted.Visible = false;
            ViewState["LoginName"] = Convert.ToString(Session["LoginName"]);
            objDoCaseList.intDocId = CaseID;
            objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            DataTable dtt = objCaseListDb.GetMicroTagDataTable(objDoCaseList, DA_CaseList.MicroTagLikeShare.GetfavrtDoc);
            if (dtt.Rows.Count > 0)
            {
                icnBook.Attributes["class"] = "icon-tags-filed";
                txtBookMark.InnerText = "Bookmarked";
            }
            else
            {
                icnBook.Attributes["class"] = "icon-tags";
                txtBookMark.InnerText = "Bookmark";
            }

            if (ViewState["intCommentAddedFor"] == null)
            {
                ReleventUser();
                GetDocDetails();
                ShowDiv(CaseID, ContentTypeID);
                getInviteeName();
            }
            else
            {
                int jj = Convert.ToInt32(Request.QueryString["hdnHistoryback"]);
                jj++;
                hdnBack.Value = jj.ToString();

                hdnComments.Value = "comment";
                hdnCommentAddedFor.Value = ViewState["intCommentAddedFor"].ToString();
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["intCommentAddedFor"].ToString());
                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetUserName);
                if (dt.Rows.Count > 0)
                {
                    lblOriginalTxt.Text = dt.Rows[0]["NAME"].ToString();
                    userSelected.InnerText = dt.Rows[0]["NAME"].ToString();
                    userSelected.Attributes["class"] = "font-weight-bold";
                    lblHighBy.Visible = true;
                    divUserStart.Visible = true;
                    divusrPosted.Visible = true;
                }

                //  lnkWriteButton.Style.Add("display", "block");
                ReleventUser();
                GetDocDetails();
                ShowDiv(CaseId, ContentTypeID);
            }
            GetOwnSummary();
            GetSummary();

            //Added on 31.03.2020
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            hdnIpAddress.Value = ip;
        }

        getInviteeName();

    }

    #region Doc Relevent User

    protected void ReleventUser()
    {
        objDORelevntU.CaseId = CaseID;
        objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = new DataTable();
        dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetRelevantUserDetails);
        if (dt.Rows.Count > 0)
        {
            noUser.Visible = false;
            if (Session["ExternalUserId"] == null)
            {
                Response.Redirect("Landing.aspx");
            }
            else
            {

                DataRow[] drs = dt.Select("intaddedby=" + ((ViewState["intCommentAddedFor"] != null) ? ViewState["intCommentAddedFor"] : Session["ExternalUserId"]).ToString());
                if (drs.Length > 0)
                {
                    string imgPath = "~/CroppedPhoto/" + drs[0]["vchrPhotoPath"].ToString();
                    string imgPathPhysical = Server.MapPath(imgPath);
                    if (File.Exists(imgPathPhysical))
                    {
                        imgUserPosted.ImageUrl = imgPath;

                    }
                    else
                    {
                        imgUserPosted.ImageUrl = "images/profile-photo.png";
                    }

                    lblTotalViews.Text = drs[0]["TotalView"].ToString() + " " + ((drs[0]["TotalView"].ToString() == "1") ? "View" : "Views");
                    lblTotalLikes.Text = drs[0]["TotalLikes"].ToString() + " " + ((drs[0]["TotalLikes"].ToString() == "1") ? "Like" : "Likes");
                    if ((int)drs[0]["LikedByUser"] > 0) { divLike.Attributes["class"] = "active-toogle on-flag"; } else { divLike.Attributes["class"] = "active-toogle"; }
                }
                DataView dv = dt.DefaultView;
                dv.Sort = "TotalLikes DESC";
                lstUserActivityCase.DataSource = dv;
                lstUserActivityCase.DataBind();
            }
        }
        else
        {
            noUser.Visible = true;
        }
    }

    protected void lstUserActivityCase_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        PopUpGmailShare.Style.Add("display", "none");
        HiddenField hdnintCommentAddedFor = (HiddenField)e.Item.FindControl("hdnintCommentAddedFor");
        HiddenField hdnintContentId = (HiddenField)e.Item.FindControl("hdnintContentId");
        //  Label lnkName = (Label)e.Item.FindControl("lnkName");
        LinkButton lnkTitle = (LinkButton)e.Item.FindControl("lnkTitle");
        ViewState["intCommentAddedFor"] = hdnintCommentAddedFor.Value;
        intCommentAddedFors.Value = hdnintCommentAddedFor.Value;
        if (e.CommandName == "UserName")
        {
            if (hdnintCommentAddedFor.Value != ViewState["UserID"].ToString())
            {
                objDORelevntU.CaseId = Convert.ToInt32(hdnintContentId.Value);
                objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intViewId = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intDocAddedBy = Convert.ToInt32(hdnintCommentAddedFor.Value);

                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.InsertDocViewDetails);

            }
            usercommentLoad(objDORelevntU.CaseId, objDORelevntU.ContentTypeID);
            //   ScriptManager.RegisterStartupScript(this, GetType(), "myFuncton", "ShowSummaryPost();", true);

        }
        if (e.CommandName == "Title")
        {
            if (hdnintCommentAddedFor.Value != ViewState["UserID"].ToString())
            {
                objDORelevntU.CaseId = Convert.ToInt32(hdnintContentId.Value);
                objDORelevntU.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                objDORelevntU.AddedBy = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intViewId = Convert.ToInt32(ViewState["UserID"]);
                objDORelevntU.intDocAddedBy = Convert.ToInt32(hdnintCommentAddedFor.Value);

                DataTable dt = new DataTable();
                dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.InsertDocViewDetails);
            }
            usercommentLoad(objDORelevntU.CaseId, objDORelevntU.ContentTypeID);
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "onlinkclick();", true);
    }

    protected void usercommentLoad(long CaseId1, long ContentTypeID1)
    {
        hdnComments.Value = "comment";
        hdnCommentAddedFor.Value = ViewState["intCommentAddedFor"].ToString();
        objDORelevntU.AddedBy = Convert.ToInt32(ViewState["intCommentAddedFor"].ToString());
        DataTable dt = new DataTable();
        dt = objDARelevntU.GetDataTable(objDORelevntU, DA_ContentRelevantUser.RelevantUser.GetUserName);
        if (dt.Rows.Count > 0)
        {
            lblOriginalTxt.Text = dt.Rows[0]["NAME"].ToString();
            userSelected.InnerText = dt.Rows[0]["NAME"].ToString();
            userSelected.Attributes["class"] = "font-weight-bold";
            lblHighBy.Visible = true;
            divUserStart.Visible = true;
            divusrPosted.Visible = true;
        }

        //     lnkWriteButton.Style.Add("display", "block");
        ReleventUser();
        GetDocDetails();
        ShowDiv(CaseId1, ContentTypeID1);
        // BindComments();
    }

    protected void lstUserActivityCase_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblLiked = (Label)e.Item.FindControl("lblLikes");
        Label lblViewed = (Label)e.Item.FindControl("lblViews");
        DataRowView drv = (DataRowView)e.Item.DataItem;
        string appendView = " Views";
        if (drv["TotalView"].ToString() == "1")
        {
            appendView = " View";
        }
        lblViewed.Text = drv["TotalView"].ToString() + appendView;
        lblLiked.Text = drv["TotalLikes"].ToString() + " " + ((drv["TotalLikes"].ToString() == "1") ? "Like" : "Likes");
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
            HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
            if (imgprofile.Src == "CroppedPhoto/")
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
        }
    }

    #endregion

    #region text to HTML

    private void GetDocDetails()
    {
        objdocase.CaseId = Convert.ToInt32(ViewState["ContentID"]);
        DataTable dt = new DataTable();
        objdocase.CaseId = CaseID;
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetDocsDetails);
        if (dt.Rows.Count > 0)
        {
            DataTable dtJudgeName = new DataTable();
            DataRow row;
            DataColumn JudgeName = new DataColumn();
            JudgeName.DataType = System.Type.GetType("System.String");
            JudgeName.ColumnName = "JudgeName";
            dtJudgeName.Columns.Add(JudgeName);
            lblpartyname.Text = Convert.ToString(dt.Rows[0]["strCaseTitle"]);
            lblcitation.Text = Convert.ToString(dt.Rows[0]["strCitation"]);
            if (lblcitation.Text == "")
            {
                spnCitation.Visible = false;
            }
            lblCaseNo.InnerText = Convert.ToString(dt.Rows[0]["intCaseId"]);
            lblcourt.Text = Convert.ToString(dt.Rows[0]["strJurisdiction"]);
            lblyear.Text = Convert.ToString(dt.Rows[0]["intYear"]);
            lblCitedBy.Text = Convert.ToString(dt.Rows[0]["strCitedBy"]);
            string Jname = Convert.ToString(dt.Rows[0]["strJudgeNames"]);
            if (Jname != "")
            {
                string[] JugName = Jname.Split(';');
                for (int i = 0; i < JugName.Length; i++)
                {
                    if (Convert.ToString(JugName.GetValue(i)) != "")
                    {
                        row = dtJudgeName.NewRow();
                        row["JudgeName"] = Convert.ToString(JugName.GetValue(i));
                        dtJudgeName.Rows.Add(row);
                    }
                }

                lstJudgeName.DataSource = dtJudgeName;
                lstJudgeName.DataBind();
            }
            else
            {
                lstJudgeName.DataSource = null;
                lstJudgeName.DataBind();
            }

            if (dt.Rows[0]["strFilePath"].ToString() != "" && dt.Rows[0]["strFilePath"].ToString() != null)
            {
                lblCaseTitle.Text = dt.Rows[0]["strCaseTitle"].ToString();
                ViewState["hdnDocTitle"] = dt.Rows[0]["strCaseTitle"].ToString();
                try
                {
                    Document document = new Document();
                    //string path = Request.PhysicalApplicationPath + "\\uploaddocument\\" + dt.Rows[0]["strfilepath"].ToString();
                    string path = Request.PhysicalApplicationPath + "\\CaseDocument\\" + dt.Rows[0]["strfilepath"].ToString();
                    string content = File.ReadAllText(path);
                    ViewState["Doc"] = TextToHtml(content);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return;
                }
            }
        }
    }

    public static string TextToHtml(string text)
    {
        text = HttpUtility.HtmlEncode(text);
        StringBuilder b = new StringBuilder(text);
        b = b.Replace("&quot; /&gt;<br>", "<br>");               // &lt;span class=&quot;LegSpan&quot; style=&quot;cursor:hand&quot; onclick=&quot;#&quot;&gt;
        b = b.Replace("&lt;span&gt;", "");
        b = b.Replace("&lt;span class=&quot;LegSpan&quot; style=&quot;cursor:hand&quot; onclick=&quot;#&quot;&gt;", "");
        b = b.Replace("&lt;span style=&quot;font-family: Verdana;&quot;&gt;", "");
        b = b.Replace("&lt;/span&gt;", "");
        b = b.Replace("&lt;", "<");
        b = b.Replace("&gt;", ">");
        b = b.Replace("&amp;nbsp;", "");
        b = b.Replace("&#39;", "'");
        b = b.Replace("&amp;", "&");
        b = b.Replace("&sbquo;", ",");
        b = b.Replace("&hellip;", "…");
        b = b.Replace("&permil;", "‰");
        b = b.Replace("&trade;", "™");
        b = b.Replace("&iexcl;", "¡");
        b = b.Replace("&brvbar;", "¦");
        b = b.Replace("&sup2;", "²");
        b = b.Replace("&sup3;", "³");
        b = b.Replace("&Auml;", "Ä");
        b = b.Replace("&tilde;", "˜");

        int count = CountStringOccurrences(b.ToString(), "</div>");
        b = b.Replace("</div>", "");
        b = b.Replace("</i>", "");
        b = b.Replace("<i>", "");
        string finaltext;
        text = b.ToString();
        if (count > 0)
        {
            int txtL = text.Length;
            for (int i = 0; i < count; i++)
            {
                finaltext = text.Insert(txtL, "</div>");
                text = finaltext;
            }
        }
        //text.IndexOf(" ", 0);
        //text.Replace(
        //string yourstring = "<p>Message Pilcrow</p><p>Testing....</p><br/><p>testing in progress...</p>";
        Regex rgxBegp = new Regex("<p>");
        Regex rgxEndp = new Regex("</p>");
        Regex rgxBegBr = new Regex("<br>");
        Regex rgxEndBr = new Regex("<br/>");
        text = rgxBegp.Replace(text, "14999BR");
        text = rgxEndp.Replace(text, "14999BR");
        text = rgxBegBr.Replace(text, "14999BR");
        text = rgxEndBr.Replace(text, "14999BR");
        //Regex rgxStyle = new Regex("/style=(.*)/g");        
        //text = rgxStyle.Replace(text, string.Empty);
        text = Regex.Replace(text, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        string noHTML = null;
        string detail_noHTML = null;
        noHTML = Regex.Replace(text, @"<[^>]+>|&nbsp;", "").Trim();
        detail_noHTML = Regex.Replace(noHTML, @"\s{2,}", string.Empty);

        StringBuilder bR = new StringBuilder(detail_noHTML);
        bR = bR.Replace("14999BR", "<br/>");
        text = bR.ToString();

        return text;
    }

    public static int CountStringOccurrences(string text, string pattern)
    {
        // Loop through all instances of the string 'text'.
        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(pattern, i)) != -1)
        {
            i += pattern.Length;
            count++;
        }
        return count;
    }

    public void BindList(Int64 CaseId)
    {
        DataTable dt = new DataTable();
        objdocase.CaseId = CaseID;
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetRecord);
        Doc = Convert.ToString(ViewState["Doc"]);
        if (!string.IsNullOrEmpty(Doc))
        {
            objDoCaseList.Caseid = CaseID;
            objDoCaseList.ContentTypeId = 1;
            objDoCaseList.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            divdisp.InnerHtml = Doc.ToString().Replace("&lt;br&gt;", "<br />");

            if (Session["ExternalUserId"] != null)
            {
                String DescByRoleGroup = Doc.ToString().Replace("&lt;br&gt;", "<br />");
                divdisp.Visible = true;
                divGuest.Visible = false;
                divdisp.InnerHtml = DescByRoleGroup;
            }
            else
            {
                Response.Redirect("Landing.aspx");
            }
        }
    }

    #endregion

    protected void SubjectTempDataTable()
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
        ViewState["SubjectCategory"] = dtSubjCat;
    }

    protected void GetUserType()
    {
        if (Convert.ToString(ViewState["FlagUser"]) == "1")
            UserTypeId = "S";
        else if (Convert.ToString(ViewState["FlagUser"]) == "2")
            UserTypeId = "P";
        else if (Convert.ToString(ViewState["FlagUser"]) == "3")
            UserTypeId = "LI";
        else if (Convert.ToString(ViewState["FlagUser"]) == "4")
            UserTypeId = "L";
        else if (Convert.ToString(ViewState["FlagUser"]) == "5")
            UserTypeId = "J";
        else if (Convert.ToString(ViewState["FlagUser"]) == "6")
            UserTypeId = "TP";
        else if (Convert.ToString(ViewState["FlagUser"]) == "7")
            UserTypeId = "LF";
    }

    public void ShowDiv(Int64 ContentID, Int64 ContentTypeID)
    {
        if (ContentTypeID == 1)
        {
            BindList(ContentID);
        }
    }

    #region PostComment

    protected void lnkShowOrgtxt_Click(object sender, EventArgs e)
    {
        PopUpGmailShare.Style.Add("display", "none");
        // DivCommentContent.Style.Add("display", "none");
        intCommentAddedFors.Value = "";
        hdnCommentAddedFor.Value = "";
        lblOriginalTxt.Text = "Original Text";
        userSelected.InnerText = "Select User";
        userSelected.Attributes["class"] = "";
        lblHighBy.Visible = false;
        divUserStart.Visible = false;
        divusrPosted.Visible = false;
        ViewState["LoginName"] = Convert.ToString(Session["LoginName"]);
        objDoCaseList.intDocId = CaseID;
        objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dtt = objCaseListDb.GetMicroTagDataTable(objDoCaseList, DA_CaseList.MicroTagLikeShare.GetfavrtDoc);
        if (dtt.Rows.Count > 0)
        {
            icnBook.Attributes["class"] = "icon-tags-filed";
            txtBookMark.InnerText = "Bookmarked";
        }
        else
        {
            icnBook.Attributes["class"] = "icon-tags";
            txtBookMark.InnerText = "Bookmark";
        }

        ViewState["intCommentAddedFor"] = null;
        intCommentAddedFors.Value = "";
        BtnSaveSummary.Style.Add("display", "block");
        ReleventUser();
        GetDocDetails();
        ShowDiv(Convert.ToInt32(ViewState["ContentID"]), Convert.ToInt32(ViewState["CTypeID"]));
        hdnComments.Value = "";
        getInviteeName();
    }

    protected void lstComment_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
            HiddenField hdnTagId = (HiddenField)e.Item.FindControl("hdnTagId");
            ListView lstCommentChild = (ListView)e.Item.FindControl("lstcommentChild");
            HiddenField hdnLinkUserId = (HiddenField)e.Item.FindControl("hdnLinkUserId");
            LinkButton ImgBtnLike = (LinkButton)e.Item.FindControl("ImgBtnLike");
            HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
            objdocase.CaseId = CaseID;
            objdocase.CommnetId = Convert.ToInt32(hdnTagId.Value);
            objdocase.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
            DataTable dtchild = new DataTable();
            Label lblLikes = (Label)e.Item.FindControl("lblLikes");
            objdocase.CaseId = CaseID;
            objdocase.TagId = Convert.ToInt32(hdnTagId.Value);
            objdocase.ContentTypeID = Convert.ToInt64(ViewState["ContentID"]);
            objdocase.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            if (imgprofile.Src == "CroppedPhoto/")
            {
                imgprofile.Src = "images/profile-photo.png";
            }
            else
            {
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
                if (!File.Exists(imgPathPhysical))
                {
                    imgprofile.Src = "images/profile-photo.png";
                }
            }

            dtchild = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetChildComment);
            if (dtchild.Rows.Count > 0)
            {
                ViewState["Comment"] = "1";
                lstCommentChild.DataSource = dtchild;
                lstCommentChild.DataBind();
                ViewState["CommentListChild"] = dtchild;
            }
            else { }
            Label lblComment = (Label)e.Item.FindControl("lblComment");
            if (lblComment.Text != "")
            {
                lblComment.Text = "''" + lblComment.Text.Trim() + "''";
            }

            HiddenField hdnAddedby = (HiddenField)e.Item.FindControl("hdnAddedby");
            HtmlGenericControl SpanAddCommentBox = (HtmlGenericControl)e.Item.FindControl("AddCommentBox");
            int addedby = Convert.ToInt32(hdnAddedby.Value);
            ObjRole.AddedBy = addedby;
            DataTable dtroleUserType = new DataTable();
            dtroleUserType = ObjRoleDB.GetDataTableTypeRole(ObjRole, DA_Role.Role.GetUserType);
            int CommentUserTypeId = 0;
            try
            {
                CommentUserTypeId = Convert.ToInt32(dtroleUserType.Rows[0]["intUserTypeID"].ToString());
            }
            catch
            {
                CommentUserTypeId = 0;
            }
        }
    }

    protected void lstComment_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (Convert.ToString(ViewState["ChildCount"]) != "1" && Convert.ToString(ViewState["ChildCount"]) == "")
        {
            hdnPostBackCheck.Value = "0";
            Label lblLikes = (Label)e.Item.FindControl("lblLikes");
            Label lblDisLike = (Label)e.Item.FindControl("lblDisLike");
            HiddenField hdnTagId = (HiddenField)e.Item.FindControl("hdnTagId");
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            Label lblCommentEdit = (Label)e.Item.FindControl("lblCommentEdit");
            LinkButton ImgBtnLike = (LinkButton)e.Item.FindControl("ImgBtnLike");
            if (e.CommandName == "Like")
            {
                if (Convert.ToString(ViewState["RatingAddRole"]) != "0" && Convert.ToString(ViewState["Comment"]) == "1")
                {
                    objRating.TagId = Convert.ToInt32(hdnTagId.Value);
                    objRating.TagType = "C";
                    objRating.Rating = 1;//For Like
                    objRating.ContentId = CaseID;
                    objRating.addedby = Convert.ToInt32(ViewState["UserID"]);
                    objRating.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
                    DataTable dtAction = new DataTable();
                    if (ImgBtnLike.Text == "Unlike")
                    {
                        dtAction = objRatingDb.GetDataTable(objRating, DA_ContentRating.ContentRating.UnlikeAdd);
                    }
                    else
                    {
                        dtAction = objRatingDb.GetDataTable(objRating, DA_ContentRating.ContentRating.Add);
                    }
                    if (dtAction.Rows[0]["Action"].ToString() == "1")
                        lblLikes.Text = Convert.ToString(Convert.ToInt32(lblLikes.Text) + 1);
                }
            }

            // BindComments();
        }
        ViewState["ChildCount"] = "";
    }


    protected void GetTotalComment()
    {
        objdocase.CaseId = CaseID;
        objdocase.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        DataTable dt = new DataTable();
        dt = objdacase.GetDataTable(objdocase, DA_CaseComment.CaseComment.GetTotalComment);
    }

    #endregion

    #region Summary

    protected void lnkWriteButton_Onclick(object sender, EventArgs e)
    {
        string s = lblOriginalTxt.Text;
        if (lblOriginalTxt.Text == "Original Text" || lblOriginalTxt.Text == "")
        {
            lblHighBy.Visible = false;
            lblOriginalTxt.Text = "Original Text";
            userSelected.InnerText = "Select User";
            userSelected.Attributes["class"] = "";
            divUserStart.Visible = false;
            divusrPosted.Visible = false;
        }

        if (ViewState["intCommentAddedFor"] != null)
        {
            if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(ViewState["intCommentAddedFor"]))
            {
                BtnSaveSummary.Style.Add("display", "block");
            }
            else
            {
                BtnSaveSummary.Style.Add("display", "block");
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunon", "ShoWAddSum();", true);

    }

    protected void BtnSaveSummary_Click(object sender, EventArgs e)
    {
        try
        {
            objSummary.ContentId = CaseID;
            string Summary = txtSummary.InnerText.Trim();
            objSummary.SummaryText = Summary.Replace("&nbsp;", "").Replace("‘", "\'").Replace("’", "\'");
            ViewState["Description"] = objSummary.SummaryText;
            objSummary.addedby = Convert.ToInt32(ViewState["UserID"]);
            objSummary.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);

            if (Summary.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim() != "")
            {
                objSummaryDb.AddEditDel_Summary(objSummary, DA_ContentSummary.ContentSummary.AddUpdateSummary);
                objSummary.PointId = 39;
                //UpdateRecentActivities();
                StringBuilder UserURL = new StringBuilder();
                if (ISAPIURLACCESSED == "1")
                {
                    UserURL.Append(APIURL);
                    UserURL.Append("createMicroTag.action?microTagId=MT");
                    UserURL.Append(objSummary.SummaryId);
                    UserURL.Append("&docUid=");
                    UserURL.Append(CaseID);
                    UserURL.Append("&addByUid=");
                    UserURL.Append(ViewState["UserID"].ToString());
                    UserURL.Append("&insertDt=");
                    UserURL.Append(DateTime.Now);
                    UserURL.Append("&startPos=" + null);
                    UserURL.Append("&content=");
                    UserURL.Append(objSummary.SummaryText);
                    UserURL.Append("&endPos=" + null);
                    UserURL.Append("&microTagType=" + 8);
                    UserURL.Append("&scope=null&parentUid=null&copyUserId=null");


                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Summary Content";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }
                txtSummary.InnerText = "";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "docppup", "OverlayBody();", true);
        }
        GetOwnSummary();
    }

    protected void BtnCancelSummary_Click(object sender, EventArgs e)
    {
        hdnTabId.Value = "";
    }

    #endregion

    #region RecentActivity

    protected void UpdateRecentActivities()
    {
        try
        {
            objSummary.ContentId = CaseID;
            objSummary.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
            objSummary.addedby = Convert.ToInt32(ViewState["UserID"]);
            objSummary.strDescrption = Convert.ToString(ViewState["Description"]);
            objSummary.intUserTypeId = Convert.ToInt32(ViewState["FlagUser"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objSummary.strIPAddress = ip;
            objSummary.intTagType = Convert.ToInt32(hdnTagtypeId.Value);

            if (Convert.ToInt32(hdnTagtypeId.Value) == 4)
            {
                objSummary.PointId = 35;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 5)
            {
                objSummary.PointId = 36;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 6)
            {
                objSummary.PointId = 37;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 7)
            {
                objSummary.PointId = 38;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 1)
            {
                objSummary.PointId = 32;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 2)
            {
                objSummary.PointId = 33;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 3)
            {
                objSummary.PointId = 34;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 8)
            {
                objSummary.PointId = 39;
            }
            else if (Convert.ToInt32(hdnTagtypeId.Value) == 9)
            {
                objSummary.PointId = 40;
            }
            objSummaryDb.AddEditDel_RecentActivities(objSummary, DA_ContentSummary.RecentActivities.AddRecentActivity);
            hdnTagtypeId.Value = "";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    #endregion

    #region For Mark As Tabs
    protected void lnkIssueTab_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "2";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]); ;
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(2);
            DataTable dtchktitle = new DataTable();
            dtchktitle = objDARatio.GetDataTable(objDORatio, DA_Ratio.Ratio.GetTagTypeId);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
            ReleventUser();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = DateTime.Now.ToString();
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string content = objDORatio.strTagDescription;
            string microTagType = "2";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURLs = new StringBuilder();
                UserURLs.Append(APIURL);
                UserURLs.Append("createMicroTag.action?microTagId=MT");
                UserURLs.Append(microTagId);
                UserURLs.Append("&docUid=");
                UserURLs.Append(docUid);
                UserURLs.Append("&addByUid=");
                UserURLs.Append(addByUid);
                UserURLs.Append("&insertDt=");
                UserURLs.Append(insertDt);
                UserURLs.Append("&startPos =");
                UserURLs.Append(startPos);
                UserURLs.Append("&endPos=");
                UserURLs.Append(endPos);
                UserURLs.Append("&content=");
                UserURLs.Append(content);
                UserURLs.Append("&microTagType=");
                UserURLs.Append(microTagType);
                UserURLs.Append("&scope=null&parentUid=null&copyUserId=null");


                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURLs.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURLs.ToString();
                        objAPILogDO.strAPIType = "Issue Content";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkFactTab_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "1";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(1);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "1";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                UserURL.Append(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Fact";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ReleventUser();
        //DivCommentContent.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);

    }

    protected void lnkImportantPara_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        //  DivCommentContent.Style.Add("display", "none");
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "3";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(3);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "3";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                UserURL.Append(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Importan Paragraph";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        hdnPasteCode.Value = "";
        ReleventUser();
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkPrecedent_Click(object sender, EventArgs e)
    {
        //   DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "4";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(4);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "4";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                User.Equals(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Precedent";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span "); ;
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkDecidendit_Click(object sender, EventArgs e)
    {
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "5";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);

            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(5);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "5";
            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                UserURL.Append(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Radio Decidendi";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkOrbite_Click(object sender, EventArgs e)
    {
        //   DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "6";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(6);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();
        }

        if (dt.Rows.Count > 0)
        {
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "6";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                UserURL.Append(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Orbiter Dictum";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        hdndivvalue.Value = MainDesc.Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        hdnPasteCode.Value = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    protected void lnkHighlight_Click(object sender, EventArgs e)
    {
        //   DivCommentContent.Style.Add("display", "none");
        hdnComments.Value = "comment";
        hdnTabId.Value = "";
        hdnTagtypeId.Value = "7";
        lblOriginalTxt.Text = Convert.ToString(ViewState["LoginName"]);
        userSelected.InnerText = Convert.ToString(ViewState["LoginName"]);
        userSelected.Attributes["class"] = "font-weight-bold";
        lblHighBy.Visible = true;
        divUserStart.Visible = true;
        divusrPosted.Visible = true;
        String MainDesc = hdndivvalue.Value.ToString();
        objDORatio.ContentTypeID = Convert.ToInt64(ViewState["CTypeID"]);
        objDORatio.CaseId = CaseID;
        objDORatio.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (BrowserUsing == "IE")
        {
            String DivHtml = hdnMarkText.Value.Replace("\r\n", "<br>");
            String WidBr = DivHtml.Replace("<br>", "#&#&#").Replace("<em>", " #&em&# ").Replace("</em>", " #&&#em&# ").Replace("<strong>", " #&strong&# ").Replace("</strong>", " #&&#strong&# ");
            String result = Regex.Replace(WidBr, @"<[^>]*>", String.Empty);
            String FinalText = result.Replace("#&#&#", "<br />").Replace(" #&strong&# ", "<strong>").Replace(" #&&#strong&# ", "</strong>").Replace(" #&em&# ", "<em>").Replace(" #&&#em&# ", "</em>");
            objDORatio.strTagDescription = FinalText;
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        else
        {
            objDORatio.strTagDescription = hdnMarkText.Value.Replace("\r\n", "<br>");
            ViewState["Description"] = objDORatio.strTagDescription;
        }
        DataTable dt = new DataTable();
        if (Convert.ToInt32(hdnStartIdx.Value) >= 0 && Convert.ToInt32(hdnEndIdx.Value) >= 0)
        {
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                objDORatio.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objDORatio.intTagType = Convert.ToInt32(7);
            objDORatio.strNewRatioTitle = Convert.ToString(ViewState["strRatioTitle"]);
            objDORatio.StartIndex = Convert.ToInt32(hdnStartIdx.Value);
            objDORatio.EndIndex = Convert.ToInt32(hdnEndIdx.Value);
            dt = objDARatio.GetMarkDataTable(objDORatio, DA_Ratio.Ratio.Add);
            UpdateRecentActivities();

        }

        if (dt.Rows.Count > 0)
        {
            //Add API Log           
            string microTagId = Convert.ToString(dt.Rows[0]["intMarkId"]);
            string docUid = Convert.ToString(objDORatio.CaseId);
            string addByUid = Convert.ToString(ViewState["UserID"]);
            string insertDt = null;
            string startPos = Convert.ToString(objDORatio.StartIndex);
            string endPos = Convert.ToString(objDORatio.EndIndex);
            string microTagType = "7";

            if (ISAPIURLACCESSED == "1")
            {
                StringBuilder UserURL = new StringBuilder();
                UserURL.Append(APIURL);
                UserURL.Append("createMicroTag.action?microTagId=MT");
                UserURL.Append(microTagId);
                UserURL.Append("&docUid=");
                UserURL.Append(docUid);
                UserURL.Append("&addByUid=");
                UserURL.Append(addByUid);
                UserURL.Append("&insertDt=");
                UserURL.Append(insertDt);
                UserURL.Append("&startPos=");
                UserURL.Append(startPos);
                UserURL.Append("&endPos=");
                UserURL.Append(endPos);
                UserURL.Append("&content=");
                UserURL.Append(objDORatio.strTagDescription);
                UserURL.Append("&microTagType=");
                UserURL.Append(microTagType);
                UserURL.Append("&scope=null&parentUid=null&copyUserId=null");

                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Highlight";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }

        hdndivvalue.Value = MainDesc.Replace("Apple-style-span", "h3").Replace("</FONT>", "</span>").Replace("</font>", "</span>").Replace("<font ", "<span ");
        BindList(CaseID);
        ReleventUser();
        ScriptManager.RegisterStartupScript(this, GetType(), "myTags", "onlinkclick();", true);
    }

    #endregion

    protected void lnkFvrtImage_Click(object sender, EventArgs e)
    {
        objDoCaseList.intDocId = Convert.ToInt32(ViewState["ContentID"]);
        objDoCaseList.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        objDoCaseList.strIpAddress = ip;
        objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

        objCaseListDb.AddEditDel_MicroTagLikeShare(objDoCaseList, DA_CaseList.MicroTagLikeShare.InsertDeleteFavrt);
        if (objDoCaseList.intMicroLikeShareId == 0)
        {
            icnBook.Attributes["class"] = "icon-tags";
            txtBookMark.InnerText = "Bookmark";
        }
        else
        {
            icnBook.Attributes["class"] = "icon-tags-filed";
            txtBookMark.InnerText = "Bookmarked";
            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("markBookMark.action ?");
                    url.Append("userId=");
                    url.Append(UserTypeId);
                    url.Append(Convert.ToInt32(ViewState["UserID"]));
                    url.Append("&bookmarkOnUid=");
                    url.Append(objDoCaseList.intMicroLikeShareId);

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());

                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Book mark";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch
                {
                }
            }
        }
    }

    #region document Title

    protected void lnkPopupShare_Click(object sender, EventArgs e)
    {
        if (hdnInvId.Value != null && hdnInvId.Value != "")
        {
            objDoCaseList.intDocId = CaseID;
            objDoCaseList.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDoCaseList.strIpAddress = ip;
            objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objDoCaseList.strRepLiShStatus = "SH";
            string id = hdnInvId.Value;
            objDoCaseList.strInviteeShare = hdnInvId.Value;
            if (txtBody.InnerText.Trim().Replace("'", "''") == "Message")
            {
                objDoCaseList.strMessage = "";
            }
            else
            {
                objDoCaseList.strMessage = txtBody.InnerText.Trim().Replace("'", "''");
            }
            objDoCaseList.strLink = txtLink.Text.Trim().Replace("'", "''");
            objCaseListDb.AddEditDel_MicroTagLikeShare(objDoCaseList, DA_CaseList.MicroTagLikeShare.MicrotagShare);
            PopUpShare.Style.Add("display", "none");
            try
            {
                if (ISAPIURLACCESSED == "1")
                {
                    StringBuilder UserURL = new StringBuilder();
                    UserURL.Append(APIURL);
                    UserURL.Append("shareDocument.action?");
                    UserURL.Append("shareByUid=");
                    UserURL.Append(ViewState["UserID"]);
                    UserURL.Append("&shareToUid=");
                    UserURL.Append(hdnInvId.Value);
                    UserURL.Append("&shareObjUid=");
                    UserURL.Append(ViewState["ContentID"]);

                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        if (ISAPIResponse != "0")
                        {
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = UserURL.ToString();
                            objAPILogDO.strAPIType = "Share Document";
                            objAPILogDO.strResponse = result;
                            if (ip == null)
                                objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch { }
                }
            }
            catch
            {
            }
            SendMail(hdnInvId.Value, lblDocTitle.Text, objDoCaseList.strLink);
            txtInviteMembers.Controls.Clear();
            getInviteeName();
            lblMess.Text = "";
            txtBody.InnerText = "";
            txtLink.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bodyscroll", "showBodyScroller();", true);
        }
        else
        {
            lblMess.Text = "Please select member";
            PopUpShare.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dcpo", "OverlayBody();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sarScripts", "callchosencss();", true);
            return;
        }
    }

    private void SendMail(string RecieverIDs, string linkText, string link)
    {
        try
        {

            DataTable dtUserDetails1 = new DataTable();
            DataTable dtUserDetails2 = new DataTable();
            string[] receiverList = RecieverIDs.Split(',');
            for (int i = 0; i < receiverList.Length; i++)
            {
                string body = null;
                objLogin.intRegistartionID = Convert.ToInt32(receiverList[i]);
                dtUserDetails1.Clear();
                dtUserDetails1 = objLoginDB.GetDataTable(objLogin, DA_SKORKEL.DA_Login.Login_1.UserDetails);
                objLogin.intRegistartionID = Convert.ToInt32(ViewState["UserID"]);
                dtUserDetails2.Clear();
                dtUserDetails2 = objLoginDB.GetDataTable(objLogin, DA_SKORKEL.DA_Login.Login_1.UserDetails);

                string MailTo = Convert.ToString(dtUserDetails1.Rows[0]["vchrUserName"]);

                using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", Convert.ToString(dtUserDetails1.Rows[0]["LoginName"]));
                body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'> " + Convert.ToString(dtUserDetails2.Rows[0]["LoginName"])
                    + "</span>" + " has shared a case with you, titled " +
                "<a style='color:#01b7bd;text-decoration:none' href='" + link + "'>" + linkText + "</a>");

                string subject = Convert.ToString(dtUserDetails2.Rows[0]["LoginName"]) + " has shared " + linkText + " with you";

                EmailSender.SendEmail(MailTo, subject, body);

                FCMNotification.Send(Convert.ToString(dtUserDetails2.Rows[0]["LoginName"]) + " has shared " + linkText + " with you", Convert.ToString(dtUserDetails2.Rows[0]["LoginName"]) + " has shared a case with you, titled " + linkText + ".",
                           Convert.ToString(dtUserDetails1.Rows[0]["intRegistrationId"]), link);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void getInviteeName()
    {
        DataTable dtDoc = new DataTable();
        objdonetwork.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        dtDoc = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.ConnectedUsers);

        if (dtDoc.Rows.Count > 0)
        {
            txtInviteMembers.DataSource = dtDoc;
            txtInviteMembers.DataValueField = "intInvitedUserId";
            txtInviteMembers.DataTextField = "NAME";
            txtInviteMembers.DataBind();
        }

    }
    #endregion

    public string GetContentForPDF(string apiType)
    {
        string _Return = string.Empty;
        StringBuilder UserURL = new StringBuilder(APIURL);
        UserURL.Append("downloadDocument.action?");
        UserURL.Append("downloadByUId=");
        UserURL.Append(ViewState["UserID"].ToString());
        UserURL.Append("&downloadedParentId=");
        UserURL.Append(ViewState["ContentID"]);
        UserURL.Append("&downloadType=");
        UserURL.Append("free");


        Session["divDisps"] = hdnDivContent.Value;

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Myss", "callDivHandler();", true);
        try
        {
            if (ISAPIURLACCESSED == "1")
            {
                try
                {
                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    if (ISAPIResponse != "0")
                    {
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDO.strAPIType = apiType;// "Download Document";
                        objAPILogDO.strResponse = result;
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
        }
        catch
        {
        }

        //string texta = hdnDivContent.Value;
        StringBuilder text = new StringBuilder(hdnDivContent.Value);
        text = text.Replace("&quot;", "").Replace("<br>", "<br />").Replace("<br >", "<br />");
        int countCdiv = CountStringOccurrences(text.ToString(), "</div>");
        int countdiv = CountStringOccurrences(text.ToString(), "<div");
        if (countCdiv == 0 && countdiv == 0)
        {
            int txtL = text.Length;
            text = text.Insert(txtL, "</div>");
            text = text.Insert(0, "<div>");
        }

        if (Convert.ToInt32(hdnMarkMaxCount.Value) > 0)
        {
            for (int i = 0; i <= Convert.ToInt32(hdnMarkMaxCount.Value); i++)
            {
                string spanOpen = "<span" + i;
                string replace = "<span";
                string pattern = spanOpen;
                string result = Regex.Replace(text.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                string spanClose = "</span" + i;
                replace = "</span";
                pattern = spanClose;
                result = Regex.Replace(result.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                text = new StringBuilder(result);

            }
            text = text.Replace("class=\"preced\"", "style=\"background-color: #ffeca0;\"").Replace("class=\"rediod\"", "style=\"background-color: #AECF00;\"").Replace("class=\"issuss\"", "style=\"background-color: #faa69d;\"").Replace("span0", "span").Replace("span1", "span").Replace("span2", "span").Replace("span3", "span").Replace("span4", "span").Replace("span5", "span");
            text = text.Replace("class=\"factss\"", "style=\"background-color: #9dc6fa;\"").Replace("class=\"highls\"", "style=\"background-color: Lime;\"").Replace("class=\"orbits\"", "style=\"background-color: #f8adef;\"").Replace("class=\"ImpPss\"", "style=\"background-color: #b0ffd2;\"");
        }
        _Return = Convert.ToString(text);
        return _Return;
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        createPDF(GetContentForPDF("Download Document"), "download");
    }
    protected void gmailshare_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = docText.InnerText = string.Empty;
            if (string.IsNullOrEmpty(Convert.ToString(Session["LoginName"])))
                Response.Redirect("Landing.aspx", true);

            fileName = createPDF(GetContentForPDF("Share On Mail"), "mail");
            if (!string.IsNullOrEmpty(fileName))
            {
                docText.InnerText = fileName;
            }
            lblEmailValidation.Text = txtEmailId.Text = txtBodyGmail.InnerText = string.Empty;
            PopUpGmailShare.Style.Add("display", "block");
        }
        catch (Exception ex)
        {
            PopUpGmailShare.Style.Add("display", "block");
            ex.Message.ToString();
        }
    }
    protected void whatsappShare_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["LoginName"])))
                Response.Redirect("Landing.aspx", true);

            var caseID = intCommentAddedFors.Value;
            string fileName = createPDF(GetContentForPDF("Share On WhatsApp"), "whatsapp");
            if (!string.IsNullOrEmpty(fileName))
            {
                hdnIsCalledWhatsAppMethod.Value = fileName;
                if (!string.IsNullOrEmpty(caseID))
                    ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "onlinkclick();", true);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "dcpo", "OverlayBody();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "redirectToWhatsApp('" + fileName + "');", true);
            }
            else
            {
                hdnIsCalledWhatsAppMethod.Value = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private string createPDF(string html, string type)
    {
        string returnFileName = string.Empty;
        string directory = string.Empty;
        string fileName = string.Empty;
        fileName = "Casedocument_" + ViewState["ContentID"] + ".pdf";
        directory = Server.MapPath("~\\tmp\\");

        if (type == "whatsapp")
        {
            directory = Server.MapPath("~\\SharingDoc\\");
        }
        //else if (type == "mail" || type == "whatsapp")
        //{
        //    directory = Server.MapPath("~\\SharingDoc\\");

        //    Random generator = new Random();
        //    int r = generator.Next(1, 1000000);
        //    string s = r.ToString().PadLeft(6, '0');

        //    DateTime dt = System.DateTime.Now;
        //    string timestam = Convert.ToString(dt.Day) + Convert.ToString(dt.Month) + Convert.ToString(dt.Year) + s;
        //    fileName = "CaseDocument_" + timestam + ".pdf";

        //    if (!string.IsNullOrEmpty(docText.InnerText))
        //    {
        //        returnFileName = docText.InnerText;
        //        return returnFileName;
        //    }
        //}

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        using (var msOutput = new FileStream(directory + fileName, FileMode.Create))
        {
            using (var document = new Document(PageSize.A4, 30, 30, 30, 30))
            {
                try
                {
                    using (var writer = PdfWriter.GetInstance(document, msOutput))
                    {
                        document.Open();
                        var example_css = @".body{font-family: 'Times New Roman'}";
                        Font contentFont = FontFactory.GetFont("Times New Roman", 16.0f, iTextSharp.text.Font.BOLD);
                        Paragraph para = new Paragraph(lblCaseTitle.Text.Trim(), contentFont);
                        para.Alignment = Element.ALIGN_CENTER;
                        document.Add(para);
                        Paragraph para1 = new Paragraph(" ", contentFont);
                        document.Add(para1);
                        document.HtmlStyleClass = @".IContent{font-family: Times New Roman;text-align: justify;font-size: 22px;}";

                        using (TextReader reader = new StringReader(html))
                        {
                            //Parse the HTML and write it to the document
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);

                            ////get the XMLWorkerHelper Instance
                            //XMLWorkerHelper worker = XMLWorkerHelper.GetInstance();
                            ////convert to PDF
                            //worker.ParseXHtml(writer, document, reader);
                            ////worker.GetDefaultCSS(

                        }
                        document.Close();
                    }
                }

                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                addWatermarkInsidePDF(type, fileName);
                if (type == "download")
                {
                    Response.Redirect("handler/DownloadFile.ashx?path=" + "~\\tmp\\" + fileName + "&filename=" + fileName);
                }
                else if (type == "mail" || type == "whatsapp")
                {
                    returnFileName = fileName;
                }


                //msOutput.Seek(0, SeekOrigin.Begin);

                //using (FileStream fs = new FileStream("test.pdf", FileMode.OpenOrCreate))
                //{
                //    msOutput.CopyTo(fs);
                //    fs.Flush();
                //}
                //Response.Clear();
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.AddHeader("Content-Disposition", "attachment; filename=Casedocument_" + ViewState["ContentID"] + ".pdf");
                //Response.ContentType = "application/octet-stream";
                //Response.Buffer = true;
                //Response.OutputStream.Write(msOutput.GetBuffer(), 0, msOutput.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.End();
                //return msOutput.ToArray();
                return returnFileName;
            }
        }
    }
    private void addWatermarkInsidePDF(string type, string fileName)
    {
        string sourceFilePath = string.Empty;
        sourceFilePath = Server.MapPath("~\\tmp\\") + fileName;
        if (type == "whatsapp")
        {
            sourceFilePath = Server.MapPath("~\\SharingDoc\\") + fileName;
        }
        //else if (type == "mail" || type == "whatsapp")
        //{
        //    sourceFilePath = Server.MapPath("~\\SharingDoc\\") + fileName;
        //}

        byte[] bytes = File.ReadAllBytes(sourceFilePath);
        var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/watermark_img.png"));
        img.ScaleToFit(375, 700);
        img.Alignment = iTextSharp.text.Image.UNDERLYING;
        img.SetAbsolutePosition(100, 200);
        PdfContentByte waterMark;
        using (MemoryStream stream = new MemoryStream())
        {
            PdfReader reader = new PdfReader(bytes);
            using (PdfStamper stamper = new PdfStamper(reader, stream))
            {
                int pages = reader.NumberOfPages;
                for (int i = 1; i <= pages; i++)
                {
                    waterMark = stamper.GetUnderContent(i);
                    waterMark.AddImage(img);
                }
            }
            bytes = stream.ToArray();
        }
        File.WriteAllBytes(sourceFilePath, bytes);
    }
    private void PdfStampInExistingFile(string text, string fileName)
    {
        string sourceFilePath = Server.MapPath("~\\tmp\\") + fileName;
        byte[] bytes = File.ReadAllBytes(sourceFilePath);
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(200, 30, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
        System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
        graphics.Clear(System.Drawing.Color.White);
        graphics.DrawString(text, new System.Drawing.Font("Times New Roman", 16.0f, System.Drawing.FontStyle.Bold), new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.PointF(0.4F, 2.4F));
        bitmap.Save(Server.MapPath("~/SharingDoc/Image.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        bitmap.Dispose();
        var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/SharingDoc/Image.jpg"));
        img.SetAbsolutePosition(200, 400);
        PdfContentByte waterMark;
        using (MemoryStream stream = new MemoryStream())
        {
            PdfReader reader = new PdfReader(bytes);
            using (PdfStamper stamper = new PdfStamper(reader, stream))
            {
                int pages = reader.NumberOfPages;
                for (int i = 1; i <= pages; i++)
                {
                    waterMark = stamper.GetUnderContent(i);
                    waterMark.AddImage(img);
                }
            }
            bytes = stream.ToArray();
        }
        File.Delete(Server.MapPath("~/SharingDoc/Image.jpg"));
        File.WriteAllBytes(sourceFilePath, bytes);
    }
    protected void lnkShare_Click(object sender, EventArgs e)
    {
        if (lblOriginalTxt.Text == "Original Text" || lblOriginalTxt.Text == "")
        {
            lblHighBy.Visible = false;
            divUserStart.Visible = false;
            divusrPosted.Visible = false;
            lblOriginalTxt.Text = "Original Text";
            userSelected.InnerText = "Select User";
            userSelected.Attributes["class"] = "";
        }

        string path = Request.Url.AbsoluteUri;
        txtLink.Text = path;
        lblMess.Text = "";
        hdnTabId.Value = "";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpo", "OverlayBody();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "callchosencss();", true);
        imgGrp1.Src = "~/images/file.svg";
        string DocTitle = Convert.ToString(ViewState["hdnDocTitle"]);
        lblDocTitle.Text = Convert.ToString(ViewState["hdnDocTitle"]);
        PopUpShare.Style.Add("display", "block");
        // ScriptManager.RegisterStartupScript(this, GetType(), "myFncon", "ShowSummaryPost();", true);
    }


    private void GetSummary()
    {
        objSummary.ContentId = CaseID;// Convert.ToInt32(context.Request.QueryString["ContentId"]);
        objSummary.addedby = Convert.ToInt32(ViewState["UserID"]); // Convert.ToInt32(context.Request.QueryString["AddedBy"]);
        DataTable dtSummary = new DataTable();
        dtSummary = objSummaryDb.GetDataTable(objSummary, DA_ContentSummary.ContentSummary.GetAllSum);
        if (dtSummary.Rows.Count > 0)
        {
            lblSumOtherss.Visible = true;
            lstSumOthrs.DataSource = dtSummary;
            lstSumOthrs.DataBind();
            //string datatext = " ";
            //int ContentId = 0;
            //datatext = dtSummary.Rows[0]["strSummaryText"].ToString();
            //ContentId = Convert.ToInt32(dtSummary.Rows[0]["intContentId"]);
            // context.Response.Write(datatext);
        }
        else
        {
            lblSumOtherss.Visible = false;
        }
    }
    protected void lnkLike_Click(object sender, EventArgs e)
    {
        objDoCaseList.intDocId = CaseID;
        //   objDoCaseList.ContentTypeID = Convert.ToInt64(Request.QueryString["CTid"]);
        objDoCaseList.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDoCaseList.intRegistrationId = Convert.ToInt32(intCommentAddedFors.Value);
        objCaseListDb.AddEditDel_MicroTagLikeShare(objDoCaseList, DA_CaseList.MicroTagLikeShare.UpdateLikes);
        ReleventUser();
        ScriptManager.RegisterStartupScript(this, GetType(), "myTa1", "onlinkclick();", true);
    }

    protected void lstSumOthrs_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lnkSumLike = (LinkButton)e.Item.FindControl("lnkSumLike");
        HiddenField hdnSumId = (HiddenField)e.Item.FindControl("hdnSumId");
        DataRowView drv = e.Item.DataItem as DataRowView;
        if (Convert.ToInt32(drv["UserLikes"]) > 0)
        {
            lnkSumLike.CssClass = "active-toogle on-flag";
        }
        hdnSumId.Value = drv["intSummaryId"].ToString();
    }

    protected void lstSumOthrs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Like")
        {
            HiddenField hdnSumId = (HiddenField)e.Item.FindControl("hdnSumId");
            objSummary.SummaryId = Convert.ToInt64(hdnSumId.Value);
            objSummary.addedby = Convert.ToInt64(ViewState["UserID"]);
            objSummaryDb.AddEditDel_Summary(objSummary, DA_ContentSummary.ContentSummary.UpdateSumLike);
        }
        GetSummary();
    }
    private void GetOwnSummary()
    {
        objSummary.ContentId = CaseID;// Convert.ToInt32(context.Request.QueryString["ContentId"]);
                                      // objSummary.SummaryId = Convert.ToInt64(hdnSumId.Value);
        objSummary.addedby = Convert.ToInt64(ViewState["UserID"]);
        DataTable dtSummary = new DataTable();
        dtSummary = objSummaryDb.GetDataTable(objSummary, DA_ContentSummary.ContentSummary.SingleRecord);
        if (dtSummary.Rows.Count > 0)
        {
            divSumByyou.Style.Add("display", "block");
            lnkWriteButton.Visible = false;
            lblOwnSumlike.Visible = true;
            lnkOwnLike.Visible = true;
            DataRow dr = dtSummary.Rows[0];
            hdnSummryID.Value = dr["intSummaryId"].ToString();
            if (Convert.ToInt32(dr["UserLikes"]) > 0)
            {
                lnkOwnLike.CssClass = "active-toogle on-flag";
            }
            else
            {
                lnkOwnLike.CssClass = "active-toogle";
            }
            txtSummary.InnerText = dr["strSummaryText"].ToString();
            sumDetails.InnerText = dr["strSummaryText"].ToString();
            lblOwnSumlike.Text = dr["TotalLikes"].ToString() + (dr["TotalLikes"].ToString() == "1" ? " Like" : " Likes");
        }
        else
        {
            divSumByyou.Style.Add("display", "none");
            lblOwnSumlike.Visible = false;
            lnkOwnLike.Visible = false;
            lnkWriteButton.Visible = true;
        }
    }
    protected void lnkOwnLike_Click(object sender, EventArgs e)
    {

        //    HiddenField hdnSumId = (HiddenField)e.Item.FindControl("hdnSumId");
        objSummary.SummaryId = Convert.ToInt64(hdnSummryID.Value);
        objSummary.addedby = Convert.ToInt64(ViewState["UserID"]);
        objSummaryDb.AddEditDel_Summary(objSummary, DA_ContentSummary.ContentSummary.UpdateSumLike);
        GetOwnSummary();
    }

    protected void lnkShareOnGmail_Click(object sender, EventArgs e)
    {
        string path = string.Empty;
        FileInfo file;
        var caseID = intCommentAddedFors.Value;
        try
        {
            string body = string.Empty;
            string subject = string.Empty;
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailURLFinal = MailURL + "/Landing.aspx";
            if (!string.IsNullOrEmpty(txtEmailId.Text.Trim()) && !string.IsNullOrEmpty(docText.InnerText))
            {

                #region Check Email Id Vallidation
                string toAddress = txtEmailId.Text.Trim();
                Regex rgx = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*(\+[a-z0-9-]+)?@[a-z0-9-]+(\.[a-z0-9-]+)*$");
                List<string> emailArray = new List<string>();
                if (toAddress.IndexOf(",") != -1)
                {
                    emailArray = toAddress.Replace(" ", "").Split(',').ToList();
                }
                else
                {
                    emailArray.Add(toAddress);
                }
                foreach (string email in emailArray)
                {
                    if (rgx.IsMatch(email ?? ""))
                    {
                        //SendEmail(email, subject, body);
                    }
                    else
                    {
                        lblEmailValidation.Text = "'" + email + "' is invalid email id!";
                        PopUpGmailShare.Style.Add("display", "block");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "dcpo", "OverlayBody();", true);
                        return;
                    }
                }
                #endregion
                string caseTitle = lblCaseTitle.Text;
                string userMessage = string.Empty;
                if (!string.IsNullOrEmpty(txtBodyGmail.InnerText.Trim()))
                {
                    userMessage = "<b>Message:</b><br/><p>" + txtBodyGmail.InnerText.Trim() + "</p>";
                }

                using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                {
                    body = reader.ReadToEnd();
                }
                //body = body.Replace("{UserName}", Convert.ToString(Session["LoginName"]));
                body = body.Replace("{UserName}", "Sir/Madam");
                body = body.Replace("{Content}", "<span style='font-weight:bold;font-size:14px;color:#373A36;'> " + Convert.ToString(Convert.ToString(Session["LoginName"]))
                    + "</span>" + " has shared a case with you, titled " + caseTitle
                    + "<br /><br />" + userMessage + "" + "<b>User Details:</b> <br />" +
                                "<b>Name:</b> " + Convert.ToString(Session["LoginName"]) + "<br />" +
                                "<b>Email:</b> " + Convert.ToString(((UserSession.UserInfo)Session["UInfo"]).UserName) + "<br/><br/><br/><br/>" +
                                "Login URL:" + "&nbsp;&nbsp;<a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Login</a>");


                subject = Convert.ToString(Session["LoginName"]) + " has shared " + caseTitle + " with you";

                foreach (string email in emailArray)
                {
                    EmailSender.SendEmailWithAttachment(email, subject, body, Convert.ToString(docText.InnerText));
                }
                if (!string.IsNullOrEmpty(caseID))
                    ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "onlinkclick();", true);
                //path = Server.MapPath("~\\tmp\\") + Convert.ToString(docText.InnerText);
                //file = new FileInfo(path);
                //if (file.Exists)//check file exsit or not
                //{
                //    PopUpGmailShare.Attributes.Remove("style");
                //    lblEmailValidation.Text = txtEmailId.Text = txtBodyGmail.InnerText = docText.InnerText = string.Empty;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "bodyscroll", "showBodyScroller();", true);

                //    file.Delete();
                //}
                PopUpGmailShare.Attributes.Remove("style");
                lblEmailValidation.Text = txtEmailId.Text = txtBodyGmail.InnerText = docText.InnerText = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "bodyscroll", "showBodyScroller();", true);
            }
            else
            {
                lblEmailValidation.Text = "Enter member email id!";
                PopUpGmailShare.Style.Add("display", "block");
                if (!string.IsNullOrEmpty(caseID))
                    ScriptManager.RegisterStartupScript(this, GetType(), "myTags1", "onlinkclick();", true);



                ScriptManager.RegisterStartupScript(this, this.GetType(), "dcpo", "OverlayBody();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //file = new FileInfo(path);
            //if (file.Exists)//check file exsit or not
            //{
            //    file.Delete();
            //}
            PopUpGmailShare.Attributes.Remove("style");
            lblEmailValidation.Text = txtEmailId.Text = txtBodyGmail.InnerText = docText.InnerText = string.Empty;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "bodyscroll", "showBodyScroller();", true);
        }
    }

}