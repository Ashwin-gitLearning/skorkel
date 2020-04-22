using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllNewsListing : System.Web.UI.Page
{
    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DataTable dt = new DataTable();

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
                Response.Redirect("~/Landing.aspx", true);
            }

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            BindNewsDetails();
        }
    }

    protected void BindNewsDetails()
    {
        string srch = null;
        int totalResults, totalResultsPage, pageNo;
        totalResults = 0;
        totalResultsPage = 0;
        pageNo = 0;

        //if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "" && (!Page.IsPostBack))
        //{
        //    hdnSearch.Value = null;
        //    srch = Convert.ToString(Request.QueryString["srch"]);

        //    if (ISAPIURLACCESSED != "0")
        //    {
        //        StringBuilder urlMain = new StringBuilder();
        //        urlMain.Append(APIURL);
        //        urlMain.Append("skorkelAdvanceSearch.action?");
        //        urlMain.Append("title=");
        //        urlMain.Append(srch);
        //        urlMain.Append("&searchtype=");
        //        urlMain.Append("NEWS");
        //        urlMain.Append("&pageNo=1");

        //        using (WebClient wc = new WebClient())
        //        {
        //            var content = wc.DownloadString(urlMain.ToString());
        //            dynamic jsonResponse = JsonConvert.DeserializeObject(content);
        //            List<string> NewsIds = new List<string>();
        //            totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
        //            //totalResultsPage = totalResults / 10;
        //            for (int i = 0; i < totalResults && i < 10; i++)
        //            {
        //                //if (i == 0)
        //                //{
        //                //    pageNo = 1;
        //                //}
        //                //else
        //                //{
        //                //    pageNo = pageNo + 10;
        //                //}
        //                //StringBuilder url = new StringBuilder();
        //                //url.Append(APIURL);
        //                //url.Append("skorkelAdvanceSearch.action?");
        //                //url.Append("title=");
        //                //url.Append(srch);
        //                //url.Append("&searchtype=");
        //                //url.Append("NEWS");
        //                //url.Append("&pageNo=");
        //                //url.Append(pageNo);

        //                //for (int j = 0; j < 10; j++)
        //                //{
        //                    NewsIds.Add(jsonResponse.responseJson.searchResultNewsSets[i].news_id.ToString());
        //                //}
        //            }
        //            var result = string.Join(",", NewsIds);
        //            dt.Clear();
        //            objDONewsListing.NewsIdList = Convert.ToString(result);
        //            dt = objDANewsListing.GetDataTableNews(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListingUS);
        //            if (dt.Rows.Count > 0)
        //            {
        //                lstParentCrdDetails.Visible = true;
        //                lstParentCrdDetails.DataSource = dt;
        //                lstParentCrdDetails.DataBind();
        //                dvPage.Visible = true;
        //                BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(totalResults));
        //            }
        //            else
        //            {
        //                //lblmsg.Visible = true;
        //                lstParentCrdDetails.DataSource = null;
        //                lstParentCrdDetails.DataBind();
        //                lstParentCrdDetails.Visible = false;
        //                dvPage.Visible = false;
        //            }
        //        }
        //    }
        //}
        //else 
        if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "")
        {
            hdnSearch.Value = null;
            srch = Convert.ToString(Request.QueryString["srch"]);
            txtNewsSearch.Text = srch;

            if (hdnCurrentPage.Value == "1" || hdnCurrentPage.Value == null || hdnCurrentPage.Value == "")
            {
                pageNo = 1;
            }
            else
            {
                pageNo = Convert.ToInt32(hdnCurrentPage.Value);
            }

            if (ISAPIURLACCESSED != "0")
            {
                StringBuilder urlMain = new StringBuilder();
                urlMain.Append(APIURL);
                urlMain.Append("skorkelAdvanceSearch.action?");
                urlMain.Append("title=");
                urlMain.Append(srch);
                urlMain.Append("&searchtype=");
                urlMain.Append("NEWS");
                urlMain.Append("&pageNo=");
                urlMain.Append(pageNo);
                urlMain.Append("&maxResultPerPage=");
                urlMain.Append(10);

                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(urlMain.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> NewsIds = new List<string>();
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                    }

                    if (pageNo * 10 < totalResults)
                    {
                        totalResultsPage = 10;
                    }
                    else
                    {
                        totalResultsPage = totalResults - ((pageNo - 1) * 10);
                    }
                    for (int i = 0; i < totalResultsPage && i < 10; i++)
                    {
                        NewsIds.Add(jsonResponse.responseJson.searchResultNewsSets[i].UId.ToString());
                    }
                    var result = string.Join(",", NewsIds);
                    dt.Clear();
                    objDONewsListing.NewsIdList = Convert.ToString(result);
                    dt = objDANewsListing.GetDataTableNews(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListingUS);
                    if (dt.Rows.Count > 0)
                    {
                        lstParentCrdDetails.Visible = true;
                        lstParentCrdDetails.DataSource = dt;
                        lstParentCrdDetails.DataBind();
                        dvPage.Visible = true;
                        BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(totalResults));
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
            }
        }
        else if (Convert.ToString(hdnSearch.Value) != null && Convert.ToString(hdnSearch.Value) != "")
        {
            srch = Convert.ToString(hdnSearch.Value);

            if (hdnCurrentPage.Value == "1" || hdnCurrentPage.Value == null || hdnCurrentPage.Value == "")
            {
                pageNo = 1;
            }
            else
            {
                pageNo = Convert.ToInt32(hdnCurrentPage.Value);
            }

            if (ISAPIURLACCESSED != "0")
            {
                StringBuilder urlMain = new StringBuilder();
                urlMain.Append(APIURL);
                urlMain.Append("skorkelAdvanceSearch.action?");
                urlMain.Append("title=");
                urlMain.Append(srch);
                urlMain.Append("&searchtype=");
                urlMain.Append("NEWS");
                urlMain.Append("&pageNo=");
                urlMain.Append(pageNo);
                urlMain.Append("&maxResultPerPage=");
                urlMain.Append(10);

                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(urlMain.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> NewsIds = new List<string>();
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                    }

                    if (pageNo * 10 < totalResults)
                    {
                        totalResultsPage = 10;
                    }
                    else
                    {
                        totalResultsPage = totalResults - ((pageNo - 1) * 10);
                    }
                    for (int i = 0; i < totalResultsPage && i < 10; i++)
                    {
                        NewsIds.Add(jsonResponse.responseJson.searchResultNewsSets[i].UId.ToString());
                    }
                    var result = string.Join(",", NewsIds);
                    dt.Clear();
                    objDONewsListing.NewsIdList = Convert.ToString(result);
                    dt = objDANewsListing.GetDataTableNews(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListingUS);
                    if (dt.Rows.Count > 0)
                    {
                        lstParentCrdDetails.Visible = true;
                        lstParentCrdDetails.DataSource = dt;
                        lstParentCrdDetails.DataBind();
                        dvPage.Visible = true;
                        BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(totalResults));
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
            }
        }
        else
        {
            //String ID = string.Empty;
            hdnSearch.Value = null;
            ViewState["ViewAll"] = "1";
            objDONewsListing.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objDONewsListing.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            dt.Clear();
            dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListingUser);
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
        //hdnSearch.Value = null;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);

        #region To check data either empty or not and then display message accordingly. :: 25.03.2020
        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(hdnEndPage.Value))
            {
                if (Convert.ToInt64(hdnEndPage.Value) <= 10)
                {
                    dvPage.Visible = false;
                }
            }
            divEmptyResult.Style.Add("display", "none");
            pMessageForEmpty.InnerText = string.Empty;
        }
        else
        {
            divEmptyResult.Style.Add("display", "block");
            pMessageForEmpty.InnerText = "0 Results";
            if (!string.IsNullOrEmpty(srch))
            {
                pMessageForEmpty.InnerText = Convert.ToString(dt.Rows.Count) + " Results for " + srch + "";
            }
            lstParentCrdDetails.DataSource = null;
            lstParentCrdDetails.DataBind();
            lstParentCrdDetails.Visible = false;
            dvPage.Visible = false;
        }
        #endregion
    }

    protected void btnNewsSearch_Click(object sender, EventArgs e)
    {
        hdnSearch.Value = txtNewsSearch.Text.Trim();
        hdnCurrentPage.Value = "1";
        BindNewsDetails();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void lstParentCrdDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        string Content = null;
        string noHTML = null;
        Label lblDtlNews = e.Item.FindControl("lblDtlNews") as Label;
        HiddenField hdnPostID = e.Item.FindControl("hdnPostID") as HiddenField;
        DataTable dt = new DataTable();
        DataTable dtRSSList = new DataTable();
        Label lblSource = e.Item.FindControl("lblSource") as Label;

        int PostId = Convert.ToInt32(hdnPostID.Value);
        dt.Clear();
        objDONewsListing.ID = PostId;
        dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsDtls);
        if (dt.Rows.Count > 0)
        {
            Content = dt.Rows[0]["Content"].ToString();
            noHTML = Regex.Replace(Content, @"<[^>]+>|&nbsp;", "").Trim();
            lblDtlNews.Text = noHTML;
            if (dt.Rows[0]["Type"].ToString() == "RSS")
            {
                int SourceID = Convert.ToInt32(dt.Rows[0]["SourceID"]);
                dtRSSList.Clear();
                objDONewsListing.ID = SourceID;

                dtRSSList = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleRecord);
                if (dtRSSList.Rows.Count > 0)
                {
                    if (dtRSSList.Rows[0]["Title"].ToString() != "" && dtRSSList.Rows[0]["Title"].ToString() != null)
                    {
                        lblSource.Text = dtRSSList.Rows[0]["Title"].ToString();
                        //lblSource.Attributes.Add("href", dt.Rows[0]["Source_Link"].ToString());
                        //lblSource.Attributes.Add("target", "_blank");
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
            }
            else if (dt.Rows[0]["Type"].ToString() != "RSS")
            {
                //lblSource.Text = dt.Rows[0]["Type"].ToString();
                if (dt.Rows[0]["Source_Link"].ToString() != "" && dt.Rows[0]["Source_Link"].ToString() != null)
                {
                    lblSource.Text = dt.Rows[0]["Type"].ToString();
                    //lblSource.Attributes.Add("href", dt.Rows[0]["Source_Link"].ToString());
                    //lblSource.Attributes.Add("target", "_blank");
                }
                else
                {
                    lblSource.Text = dt.Rows[0]["Type"].ToString();
                }
            }
            else
            {
                lblSource.Text = "";
            }
        }
        else
        {
            lblDtlNews.Text = "";
        }
    }
    protected void lstParentCrdDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostID = (HiddenField)e.Item.FindControl("hdnPostID");
        LinkButton lblHeading = (LinkButton)e.Item.FindControl("lblHeading");
        //objDONewsListing.ID = Convert.ToInt32(hdnPostID.Value);
        if (e.CommandName == "NewsDetails")
        {
            Response.Redirect("News-Details.aspx?PostId=" + hdnPostID.Value);
        }
    }
    #region Paging For All

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
        //divDeletesucessNews.Style.Add("display", "none");
        //divDeletesucessRss.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindNewsDetails();
            }
            else
            {
                BindNewsDetails();
            }
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        //divDeletesucessNews.Style.Add("display", "none");
        //divDeletesucessRss.Style.Add("display", "none");
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindNewsDetails();
            }
            else
            {
                BindNewsDetails();
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
        //divDeletesucessNews.Style.Add("display", "none");
        //divDeletesucessRss.Style.Add("display", "none");
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
                    BindNewsDetails();
                }
                else
                {
                    BindNewsDetails();
                }
            }
        }
    }

    #endregion
}