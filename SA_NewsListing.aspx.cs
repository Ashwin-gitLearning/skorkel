using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ServiceModel.Syndication;
using System.Configuration;
using DA_SKORKEL;
using System.Net;
using System.IO;

public partial class SA_NewsListing : System.Web.UI.Page
{
    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    DataTable dt = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/SuperAdminLanding.aspx", true);
            }
            
            PnlRss.Visible = false;

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            
            BindNewsDetails();            
        }
    }

    protected void BindNewsDetails()
    {
        //String ID = string.Empty;
        objDONewsListing.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDONewsListing.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);

        dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListing);

        if (dt.Rows.Count > 0)
        {
            lstParentCrdDetails.Visible = true;
            lstParentCrdDetails.DataSource = dt;
            lstParentCrdDetails.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            //lblmsg.Visible = true;
            lstParentCrdDetails.DataSource = null;
            lstParentCrdDetails.DataBind();
            lstParentCrdDetails.Visible = false;
            dvPage.Visible = false;
        }
    }

    protected void BindRssDetails()
    {
        DataTable dtRss = new DataTable();
        objDONewsListing.CurrentPage = Convert.ToInt32(hdnCurrentPageRss.Value);
        objDONewsListing.CurrentPageSize = Convert.ToInt32(hdnTotalItemRss.Value);

        dtRss = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetRssListing);

        if (dtRss.Rows.Count > 0)
        {
            lstRssFeed.Visible = true;
            lstRssFeed.DataSource = dtRss;
            lstRssFeed.DataBind();
            dvPageRss.Visible = true;
            BindRptPagerRss(Convert.ToInt32(hdnTotalItemRss.Value), Convert.ToInt32(hdnCurrentPageRss.Value), Convert.ToInt32(dtRss.Rows[0]["Maxcount"]));
        }
        else
        {
            lstRssFeed.DataSource = null;
            lstRssFeed.DataBind();
            lstRssFeed.Visible = false;
            dvPageRss.Visible = false;
        }
    }

    protected void lstParentCrdDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        string Content = null;
        string noHTML = null;
        DataTable dtlst = new DataTable();
        DataTable dtRSSList = new DataTable();

        Label lblDtlNews = e.Item.FindControl("lblDtlNews") as Label;         
        HtmlGenericControl ToggleBtn = (HtmlGenericControl)e.Item.FindControl("ToggleBtn");
        HtmlGenericControl editDelLinkNews = (HtmlGenericControl)e.Item.FindControl("editDelLinkNews");
        HiddenField hdnPostID = (HiddenField)e.Item.FindControl("hdnPostID");
        CheckBox chkToggle = e.Item.FindControl("chkToggle") as CheckBox;
        Label lblSource = e.Item.FindControl("lblSource") as Label;

        int NewsId = Convert.ToInt32(hdnPostID.Value);
        dtlst.Clear();
        objDONewsListing.ID = NewsId;
        dtlst = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleNewsRecord);
        if (dtlst.Rows.Count > 0)
        {
            Content = dtlst.Rows[0]["Content"].ToString();
            noHTML = Regex.Replace(Content, @"<[^>]+>|&nbsp;", "").Trim();
            lblDtlNews.Text = noHTML;
            if (dtlst.Rows[0]["Type"].ToString() == "RSS")
            {
                int SourceID = Convert.ToInt32(dtlst.Rows[0]["SourceID"]);
                dtRSSList.Clear();
                objDONewsListing.ID = SourceID;

                dtRSSList = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleRecord);
                if (dtRSSList.Rows.Count > 0)
                {
                    if (dtRSSList.Rows[0]["Title"].ToString() != "" && dtRSSList.Rows[0]["Title"].ToString() != null)
                    {
                        lblSource.Text = dtRSSList.Rows[0]["Title"].ToString();
                    }
                    else
                    {
                        lblSource.Text = "";
                    }
                }
                else
                {
                    lblSource.Text = "";
                }
                ToggleBtn.Visible = true;
                editDelLinkNews.Visible = false;
                if (dtlst.Rows[0]["Status"].ToString() == "U")
                {
                    chkToggle.Checked = true;
                }
                else
                {
                    chkToggle.Checked = false;
                }
            }
            else if (dtlst.Rows[0]["Type"].ToString() != "RSS")
            {
                lblSource.Text = dtlst.Rows[0]["Type"].ToString();
                ToggleBtn.Visible = false;
                editDelLinkNews.Visible = true;
            }
            else
            {
                lblSource.Text = "";
                ToggleBtn.Visible = false;
                editDelLinkNews.Visible = true;
            }
        }
        else
        {
            ToggleBtn.Visible = false;
            editDelLinkNews.Visible = false;
        }
    }
    protected void lstParentCrdDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostId = e.Item.FindControl("hdnPostID") as HiddenField;
        CheckBox chkToggle = e.Item.FindControl("chkToggle") as CheckBox;        

        chkToggle.CheckedChanged += new EventHandler(Check_Click);

        if (e.CommandName == "Edit News")
        {
            ViewState["PostId"] = "";
            Response.Redirect("SA_Add-News.aspx?PostId=" + hdnPostId.Value);
        }
        if (e.CommandName == "Delete News")
        {
            //ViewState["hdnPostID"] = hdnPostId.Value;            
            //divDeletesucessNews.Style.Add("display", "block");
        }
        //if (e.CommandName == "NewsDetails")
        //{
        //    Response.Redirect("SA_News-Details.aspx?PostId=" + hdnPostId.Value);
        //}
        //if (chkToggle.Checked == true)
        //{
        //    objDONewsListing.ID = Convert.ToInt32(hdnPostId.Value);
        //    objDANewsListing.Edit_RssUserListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateRSSUser);
        //}
        BindNewsDetails();
    }

    protected void Check_Click(object sender, EventArgs e)
    {
        DataTable dtNews = new DataTable();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        bool? checkTgl=null;
        var chkToggle = (CheckBox)sender;
        //HiddenField hdnPostId = e.Item.FindControl("hdnPostId") as HiddenField;
        var reminderHiddenField = (HiddenField)chkToggle.NamingContainer.FindControl("hdnNewsIdToggle");
        if (chkToggle.Checked == true)
        {
            checkTgl = true;
            objDONewsListing.ID = Convert.ToInt32(reminderHiddenField.Value);            
            objDANewsListing.Edit_RssUserListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateRSSUser, Convert.ToBoolean(checkTgl));

            dtNews.Clear();
            dtNews = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleNewsRecord);

            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    string detail = null;
                    string noHTML = null;
                    string detail_noHTML = null;

                    detail = Convert.ToString(dtNews.Rows[0]["Content"]);
                    noHTML = Regex.Replace(detail, @"<[^>]+>|&nbsp;", "").Trim();
                    detail_noHTML = Regex.Replace(noHTML, @"\s{2,}", " ");

                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("addNews?");
                    url.Append("newsId=");
                    url.Append(objDONewsListing.ID);
                    url.Append("&userId=");
                    url.Append(Convert.ToInt32(ViewState["UserID"]));
                    url.Append("&dateOfSubmission=");
                    url.Append(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    url.Append("&title=");
                    url.Append(dtNews.Rows[0]["Title"]);
                    url.Append("&description=");
                    url.Append(detail_noHTML);
                    url.Append("&source=");
                    url.Append(dtNews.Rows[0]["Source_link"]);
                    url.Append("&newsCategory=");
                    url.Append(dtNews.Rows[0]["Type"]);
                    url.Append("&documentId=" + null);

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Add News";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Status has been updated successfully.')", true);
        }
        else
        {
            checkTgl = false;
            objDONewsListing.ID = Convert.ToInt32(reminderHiddenField.Value);
            objDANewsListing.Edit_RssUserListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateRSSUser, Convert.ToBoolean(checkTgl));

            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("removeNews?");
                    url.Append("newsId=");
                    url.Append(objDONewsListing.ID);
                    url.Append("&userId=");
                    url.Append(Convert.ToInt32(ViewState["UserID"]));

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Deleting News";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Status has been updated successfully.')", true);
        }
    }
    protected void lstRssFeed_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        int PostId = Convert.ToInt32(hdnPostUpdateId.Value);
    }
    protected void lstRssFeed_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;

        if (e.CommandName == "Edit Rss")
        {
            lnkPublish.Text = "Update";
            ViewState["hdnPostUpdateId"] = "";
            dt.Clear();
            objDONewsListing.ID = Convert.ToInt32(hdnPostUpdateId.Value);
            hdnintStatusUpdateId.Value = Convert.ToString(hdnPostUpdateId.Value);

            dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "456", "ShowEditPost(" +
                    "'" + Convert.ToString(hdnPostUpdateId.Value) + "', " +
                    "'" + Convert.ToString(dt.Rows[0]["Title"]) + "', " +
                    "'" + Convert.ToString(dt.Rows[0]["link"]) + "' " + ");", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "456", "ShowEditPost(" +
                //    "'" + Convert.ToString(hdnPostUpdateId.Value) + "', " +
                //    "'" + dt.Rows[0]["Type"].ToString() + "', " +
                //    "'" + Convert.ToString(dt.Rows[0]["Title"]) + "', " +
                //    "'" + Convert.ToString(dt.Rows[0]["Source_link"]) + "' " + ");", true);
            }
        }
    }
    //protected void lstRssFeed_ItemCreated(object sender, ListViewItemEventArgs e)
    //{
    //    //ListView lstChild = e.Item.FindControl("lstChild") as ListView;
    //    //lstChild.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChild_ItemCommand);
    //    //lstChild.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChild_ItemDataBound);
    //}
    protected void lnkPublish_Click(object sender, EventArgs e)
    {
        //lnkPublish.Text = "Publish";
        //clear();
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        dvSourceLinkErrorText.InnerText = "";

        if (!SaveLink())
        {
            dvSourceLinkErrorText.InnerText = "The link you have entered is invalid.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess2", "$('#rssModal').modal('show'); $('#txtSourceLink').val('"+txtSourceLink.Text+"'); $('#txtSourceField').val('" + txtSourceField.Text + "'); ", true);
        }
        clear();
    }

    protected bool SaveLink()
    {
        bool IsValid = IsValidFeedUrl(txtSourceLink.Text.Trim());
        int RSSCount=0;
        DataTable dtRSS = new DataTable();
        dtRSS.Clear();

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (IsValid == true)
        {
            if (string.IsNullOrEmpty(hdnintStatusUpdateId.Value))
            {
                objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
                objDONewsListing.Source_link = txtSourceLink.Text;

                dtRSS = objDANewsListing.GetNewsDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.RSSCount);
                if (dtRSS.Rows.Count > 0 )
                    RSSCount = Convert.ToInt32(dtRSS.Rows[0]["RssCount"]);

                if (RSSCount < 20)
                {
                    int RssValue = objDANewsListing.Add_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.RssNews);

                    if (RssValue == 0)
                    {
                        dvSourceLinkErrorText.Visible = true;
                        dvSourceLinkErrorText.InnerText = "RSS Link already exists.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess2", "$('#rssModal').modal('show'); $('#txtSourceLink').val('" + txtSourceLink.Text + "'); $('#txtSourceField').val('" + txtSourceField.Text + "'); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "$('#rssModal').modal('hide');showSuccessPopup('Success','Record saved successfully.');", true);
                    }
                    //clear();
                    BindRssDetails();
                }
                else
                {
                    //dvSourceLinkErrorText.InnerText = "Maximum limit of RSS Feed reached...";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Alert','Maximum limit of RSS Feed reached...')", true);
                }
            }
            else
            {
                objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
                objDONewsListing.Source_link = txtSourceLink.Text;
                objDONewsListing.ID = Convert.ToInt32(hdnintStatusUpdateId.Value);

                //int RssValue = objDANewsListing.AddEditDel_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.RssNews);
                int RssValue = objDANewsListing.Checking_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.CheckingRSSEdit);

                if (RssValue == 0)
                {
                    dvSourceLinkErrorText.Visible = true;
                    dvSourceLinkErrorText.InnerText = "RSS Link already exists.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess4", "$('#rssModal').modal('show'); $('#txtSourceLink').val('" + txtSourceLink.Text + "'); $('#txtSourceField').val('" + txtSourceField.Text + "'); ", true);
                }
                else
                {
                    objDONewsListing.ID = Convert.ToInt32(hdnintStatusUpdateId.Value);
                    objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
                    string description = txtSourceLink.Text.Trim().Replace(",", "''").Replace("<p>", "<br>").Replace("</p>", "");
                    objDONewsListing.Source_link = description;
                    objDANewsListing.Edit_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateRSSByID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "$('#rssModal').modal('hide');showSuccessPopup('Success','Record updated successfully.');", true);
                    //clear();
                    hdnintStatusUpdateId.Value = "";
                    BindRssDetails();
                }
            }
            return true;
        }
        else
        {
            //clear();
            return false;
        }        
    }

    //protected bool SavePost()
    //{
    //    if (string.IsNullOrEmpty(hdnintStatusUpdateId.Value))
    //    {
    //        var posts = GetFeeds(txtSourceLink.Text);
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("<p style='font-weight:larger'><b>C# Corner Latest Content Fetch From RSS</b></p>");
    //        foreach (var item in posts.Take(10))
    //        {
    //            sb.Append("<b>Title: </b>" + item.tit);
    //            sb.Append("<br />");
    //            sb.Append("<b>Description: </b>" + item.desc);
    //            sb.Append("<br />");
    //            sb.Append("<b>Article Link: </b><a target='_blank' href='" + item.contentlink + "'>" + item.contentlink + "</a>");
    //            sb.Append("<br />");
    //            sb.Append("<b>Published Date: </b>" + item.date);
    //            sb.Append("<br />");
    //            sb.Append("<b>Author: </b>" + item.auth);
    //            sb.Append("<br />");
    //            sb.Append("------------------------------------------------------------------------------------------------------------");
    //            sb.Append("<br />");
    //            string description = item.desc;
    //            objDONewsListing.Source_link = item.contentlink;
    //            objDONewsListing.Content = description;
    //            objDONewsListing.Published_Timestamp = Convert.ToDateTime(item.date);
    //            objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
    //            //string description = txtSourceLink.Text.Trim().Replace(",", "''").Replace("<p>", "<br>").Replace("</p>", "");            

    //            //string noHTML = Regex.Replace(description, @"<[^>]+>|&nbsp;", "").Trim();
    //            //objDONewsListing.Source_link = description;
    //            int RssValue = objDANewsListing.AddEditDel_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.RssNews);
            
    //            if (RssValue == 0)
    //            {
    //                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "AlertMessage", "<script language=\"javascript\" type=\"text/javascript\">;alert('Already exist.');</script>", false);
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess0", "showSuccessPopup('Alert','Record already exists.');", true);
    //            }
    //            else
    //            {
    //                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "AlertMessage", "<script language=\"javascript\" type=\"text/javascript\">;alert('Record saved successfully.');</script>", false);
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "showSuccessPopup('Alert','Record saved successfully.');", true);
    //            }
    //        }
    //        clear();
    //        BindRssDetails();
            
    //        //Response.Redirect(Request.RawUrl, false);
    //        //PnlRss.Visible = true;
    //        //PnlNews.Visible = false;

    //    }
    //    else
    //    {
    //        objDONewsListing.ID = Convert.ToInt32(hdnintStatusUpdateId.Value);
    //        objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
    //        string description = txtSourceLink.Text.Trim().Replace(",", "''").Replace("<p>", "<br>").Replace("</p>", "");
    //        objDONewsListing.Source_link = description;
    //        objDANewsListing.Edit_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateRSSByID);
    //        clear();
    //        BindRssDetails();

    //        //Response.Redirect(Request.RawUrl, false);
    //        //PnlRss.Visible = true;
    //        //PnlNews.Visible = false;
    //    }
        
    //    return true;
    //}
    protected void lnkNews_Click(object sender, EventArgs e)
    {
        PnlRss.Visible = false;
        PnlNews.Visible = true;
        lnkNews.CssClass = "nav-link active show";
        lnkRss.CssClass = "nav-link";
        //dvPage.Visible = true;
        dvPageRss.Visible = false;

        BindNewsDetails();
    }
    protected void lnkRss_Click(object sender, EventArgs e)
    {
        PnlNews.Visible = false;
        PnlRss.Visible = true;
        lnkRss.CssClass = "nav-link active show";
        lnkNews.CssClass = "nav-link";
        hdnCurrentPageRss.Value = "1";
        hdnTotalItemRss.Value = "10";
        dvPage.Visible = false;
        dvPageRss.Visible = true;

        BindRssDetails();
    }
    protected void clear()
    {
        //if (dvSourceLinkErrorText.InnerText == "RSS Link already exists.")
        //{
        //    dvSourceLinkErrorText.InnerText = "RSS Link already exists...";
        //}
        //else
        //{
            //dvSourceLinkErrorText.InnerText = "";
        //}
        txtSourceField.Text = "";
        txtSourceLink.Text = "";
        //dvSourceLinkErrorText.InnerText = "";
    }
    protected void lnkDeleteNewsConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDONewsListing.ID = Convert.ToInt32(hdnintNewsDelete.Value);
        objDANewsListing.DelByID_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.DeleteNewsByID);
        divDeletesucessNews.Style.Add("display", "none");

        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("removeNews?");
                url.Append("newsId=");
                url.Append(objDONewsListing.ID);
                url.Append("&userId=");
                url.Append(Convert.ToInt32(ViewState["UserID"]));              

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url.ToString();
                objAPILogDO.strAPIType = "Deleting News";
                objAPILogDO.strResponse = result;
                objAPILogDO.strIPAddress = ip;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch { }
        }

        BindNewsDetails();
    }
    protected void lnkDeleteRssConfirm_Click(object sender, EventArgs e)
    {
        objDONewsListing.ID = Convert.ToInt32(hdnintRssDelete.Value);
        objDANewsListing.DelByID_RssListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.DeleteRssByID);
        divDeletesucessRss.Style.Add("display", "none");
        BindRssDetails();
    }

    public static bool IsValidFeedUrl(string url)
    {
        bool isValid = true;
        
        try
        {
            //Newly Added protocol
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            XmlReader reader = XmlReader.Create(url);
            Rss20FeedFormatter formatter = new Rss20FeedFormatter();
            formatter.ReadFrom(reader);
            reader.Close();
        }
        catch(Exception ex)
        {
            isValid = false;
        }

        return isValid;
    }

    #region Paging For News

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;
            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);
            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;
            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = totalPage.ToString();

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
                dvPage.Visible = false;
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
                    lnkPrevious.Visible = true;
                    //hdnPreviousPage.Value = (StartPage - 1).ToString();
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                }
                if (totalPage >= EndPage)
                {
                    lnkNext.Visible = true;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;
                }
            }
        }

    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                if (PnlNews.Visible == true)
                {
                    BindNewsDetails();
                }
                else
                {
                    BindRssDetails();
                }
            }
            else
            {
                if (PnlNews.Visible == true)
                {
                    BindNewsDetails();
                }
                else
                {
                    BindRssDetails();
                }
            }
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                if (PnlNews.Visible == true)
                {
                    BindNewsDetails();
                }
                else
                {
                    BindRssDetails();
                }
            }
            else
            {
                if (PnlNews.Visible == true)
                {
                    BindNewsDetails();
                }
                else
                {
                    BindRssDetails();
                }
            }
        }
    }
    protected void rptDvPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
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
    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Style.Add("color", "#141414 !important");
                lnkPageLink.Style.Add("text-decoration", "none !important");

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }
                if (lnkPageLink.Text != "1")
                {
                    //imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    //imgPaging.Style.Add("opacity", "0.2");
                }
                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    if (PnlNews.Visible == true)
                    {
                        BindNewsDetails();
                    }
                    else
                    {
                        BindRssDetails();
                    }
                }
                else
                {
                    if (PnlNews.Visible == true)
                    {
                        BindNewsDetails();
                    }
                    else
                    {
                        BindRssDetails();
                    }
                }
            }
        }
    }

    #endregion

    #region Paging For RSS

    protected void BindRptPagerRss(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;
            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);
            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;
            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPageRss.Value = (CurrentPage + 1).ToString();
            hdnPreviousPageRss.Value = (CurrentPage - 1).ToString();
            hdnEndPageRss.Value = totalPage.ToString();

            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPageRss.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPageRss.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPageRss.Value) == totalPage)
                {
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPageRss.Value = EndPage.ToString();
                }
            }

            if (totalPage == 1)
            {
                dvPageRss.Visible = false;
                rptDvPageRss.DataSource = null;
                rptDvPageRss.DataBind();
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
                    hdnLastPageRss.Value = i.ToString();
                }

                rptDvPageRss.DataSource = dtPage;
                rptDvPageRss.DataBind();


                if (CurrentPage > 1)
                {
                    lnkPreviousRss.Visible = true;
                    //hdnPreviousPage.Value = (StartPage - 1).ToString();
                    hdnPreviousPageRss.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPreviousRss.Visible = false;
                }
                if (totalPage >= EndPage)
                {
                    lnkNextRss.Visible = true;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPageRss.Value = totalPage.ToString();
                }
                else
                {
                    lnkNextRss.Visible = true;
                }
            }
        }

    }
    protected void lnkNextRss_Click(object sender, EventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPageRss.Value) >= Convert.ToInt32(hdnNextPageRss.Value))
        {
            hdnCurrentPageRss.Value = hdnNextPageRss.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindRssDetails();                
            }
            else
            {
                BindRssDetails();
            }
        }
    }
    protected void lnkPreviousRss_Click(object sender, EventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (hdnPreviousPageRss.Value != "0")
        {
            hdnCurrentPageRss.Value = hdnPreviousPageRss.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindRssDetails();
            }
            else
            {
                BindRssDetails();
            }
        }
    }
    protected void rptDvPageRss_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLinkRss = (LinkButton)e.Item.FindControl("lnkPageLinkRss");
            if (lnkPageLinkRss != null)
            {
                if (hdnCurrentPageRss.Value == lnkPageLinkRss.Text)
                {
                    lnkPageLinkRss.Enabled = false;
                    if (ViewState["lnkPageLinkRss"] != null)
                    {
                        if (lnkPageLinkRss.Text == "1")
                        {
                            ViewState["lnkPageLinkRss"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLinkRss.Enabled = true;
                }

                if (hdnCurrentPageRss.Value == "1")
                {
                    ViewState["lnkPageLinkRss"] = "PageCount";
                }

            }
        }
    }
    protected void rptDvPageRss_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucessNews.Style.Add("display", "none");
        divDeletesucessRss.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLinkRss = (LinkButton)e.Item.FindControl("lnkPageLinkRss");
            if (lnkPageLinkRss != null)
            {
                hdnCurrentPageRss.Value = lnkPageLinkRss.Text;
                lnkPageLinkRss.Style.Add("color", "#141414 !important");
                lnkPageLinkRss.Style.Add("text-decoration", "none !important");

                if (lnkPageLinkRss.Text == "")
                {
                    hdnCurrentPageRss.Value = "1";
                }
                if (lnkPageLinkRss.Text != "1")
                {
                    //imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    //imgPaging.Style.Add("opacity", "0.2");
                }
                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    BindRssDetails();                    
                }
                else
                {
                    BindRssDetails();
                }
            }
        }
    }

    #endregion
}