using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DA_SKORKEL;
using Ionic.Zip;
using Newtonsoft.Json;

public partial class AllArticles : System.Web.UI.Page
{
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string APIURL = ConfigurationManager.AppSettings["APIURL"];

    string FilterText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "")
            {

                txtArticleSearch.Text = Convert.ToString(Request.QueryString["srch"]);
            }
            hdnTotalItem.Value = "10";
            hdnCurrentPage.Value = "1";
            //  objDOSAJor.JournalID = Convert.ToInt32(hdnJournalId.Value);
            if (txtArticleSearch.Text != "")
            {
                BindArticles();
            } else
            {
                Response.Redirect("Home.aspx");
            }
            ddUploader1.URL = "handler/UploadArticle.ashx";
            ddUploader1.MaxFileSize = "5242880";
            ddUploader1.DocType = "article";
            ddUploader1.ShowDelete = true;

        }
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        docModal1.Style.Add("display", "none");
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (!ddUploader1.UploadFile())
        {
            lblErrorUpload.Text = "Please upload article document.";
            lblErrorUpload.Visible = true;
            return;
        }
        ddUploader1.SetValues(ddUploader1.FileName, ddUploader1.UploadedFileName);
        if (!chkConfirm.Checked)
        {
            lblErrorConf.Text = "Please confirm.";
            lblErrorConf.Visible = true;
            return;
        }
        objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOSAJor.FilePath = ddUploader1.UploadedFileName;
        objDOSAJor.Title = txtTitle.Text.Trim();
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.AddArticle);
        Clear();
        docModal1.Style.Add("display", "none");
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "sshol", "showSuccessPopup('Success','Article added successfuly.');", true);
    }
    private void Clear()
    {
        txtTitle.Text = "";
        lblErrorUpload.Text = "";
        lblErrorUpload.Visible = false;
        lblErrorConf.Text = "";
        lblErrorConf.Visible = false;
        ddUploader1.Reset();
        chkConfirm.Checked = true;

    }
    protected void lnkAddArticle_Click(object sender, EventArgs e)
    {
        docModal1.Style.Add("display", "block");
    }
    
    protected void btnArticleSearch_Click(object sender, EventArgs e)
    {
        //hdnSearch.Value = txtNewsSearch.Text.Trim();
        hdnCurrentPage.Value = "1";
        BindArticles();
    }

    private void BindArticles()
    {

        string srch = txtArticleSearch.Text;
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (ISAPIURLACCESSED != "0")
        {
            StringBuilder url = new StringBuilder();
            url.Append(APIURL);
            url.Append("skorkelAdvanceSearch.action?");
            url.Append("title=");
            url.Append(srch);
            url.Append("&searchtype=");
            url.Append("ARTICLE");
            url.Append("&maxResultPerPage=");
            url.Append(hdnTotalItem.Value.ToString());
            url.Append("&pageNo=");
            //string startNum = ((Convert.ToInt32(hdnTotalItem.Value) * (Convert.ToInt32(hdnCurrentPage.Value) - 1)) + 1).ToString();
            //url.Append(startNum);
            url.Append(hdnCurrentPage.Value);

            using (WebClient wc = new WebClient())
            {
                var content = wc.DownloadString(url.ToString());
                dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                List<string> ArticleIds = new List<string>();
                int totalResults = 0;
                if (jsonResponse.responseJson != null)
                {
                    totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                    for (int i = 0; i < Convert.ToInt32(hdnTotalItem.Value) && i < totalResults && i < jsonResponse.responseJson.searchResultArticleSet_.Count; i++)
                    {
                        ArticleIds.Add(jsonResponse.responseJson.searchResultArticleSet_[i].uid.ToString());
                    }
                }
                var result = string.Join(",", ArticleIds);
                objDOSAJor.ArticleIdList = Convert.ToString(result);

                //objDOSAJor.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
                //objDOSAJor.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
                DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetArticlesUS);
                if (dtArt.Rows.Count > 0)
                {
                    lstArticles.Visible = true;
                    lstArticles.DataSource = dtArt;
                    lstArticles.DataBind();
                    dvPage.Visible = true;
                    BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), totalResults);
                }
                else
                {
                    lstArticles.Visible = false;
                    lstArticles.DataSource = null;
                    lstArticles.DataBind();
                    dvPage.Visible = false;
                }                
            }
        }
    }

    protected void lstArticles_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
    {
        HiddenField hdnArticleId = e.Item.FindControl("hdnArticleId") as HiddenField;
        HiddenField hdnJournalId = e.Item.FindControl("hdnJournalId") as HiddenField;
        LinkButton lnkArtTItle = e.Item.FindControl("lnkArtTItle") as LinkButton;
        if (e.CommandName == "ArticleDetails")
        {
            Response.Redirect("ArticleDetails.aspx?JrnlId=" + hdnJournalId.Value + "&ArtId=" + hdnArticleId.Value);
        }
        else if(e.CommandName == "Download")
        {
            HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
            string strURL = "~\\Articles\\" + hdnFilePath.Value;
            string filename = lnkArtTItle.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }

    protected void lstArticles_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {

        HiddenField hdnArticleId = (HiddenField)e.Item.FindControl("hdnArticleId");
        HiddenField hdnJournalId = (HiddenField)e.Item.FindControl("hdnJournalId");
        HtmlGenericControl spnLike = e.Item.FindControl("spnLike") as HtmlGenericControl;
        objDOSAJor.ArticleID = Convert.ToInt32(hdnArticleId.Value);
        objDOSAJor.JournalID = Convert.ToInt32(hdnJournalId.Value);
        objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        DataTable artLikes = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetLikeUnlike);
        if (artLikes.Rows.Count > 0)
        {
            spnLike.Attributes["class"] = "active-toogle on-flag";
        }
        else
        {
            spnLike.Attributes["class"] = "active-toogle";
        }
    }

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
                dvPage.Visible = true;
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
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                }
                if (totalPage > CurrentPage)
                {
                    lnkNext.Visible = true;
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = false;
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
        //divDeletesucess.Style.Add("display", "none");
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
                BindArticles();
                //}

            }
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        //divDeletesucess.Style.Add("display", "none");

        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            BindArticles();
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        //  divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            BindArticles();
        }
    }

}