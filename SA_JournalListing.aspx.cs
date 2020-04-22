using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class SA_JournalListing : System.Web.UI.Page
{
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
    string FilterText = "";

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string MailURL = ConfigurationManager.AppSettings["MailURL"];

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

            hdnTotalItem.Value = "10";
            LoadYears();
            txtFilter.InnerText = " All Journals";
            LoadAllJournal();
        }

    }
    protected void LoadYears()
    {
        int year = DateTime.Now.Year;
        int start = year - 40;
        int end = year + 20;
        txtFromYear.Items.Add(new ListItem("Year", "Year"));
        for (int i = start; i <= end; i++)
        {
            string value = i.ToString();
            ListItem li = new ListItem(value, value);
            txtFromYear.Items.Add(li);
        }

    }
    protected void LoadAllJournal()
    {
        objDOSAJor.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSAJor.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.AllJournals);
        if (dt.Rows.Count > 0)
        {
            lstJournals.DataSource = dt;
            lstJournals.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {

            lstJournals.DataSource = null;
            lstJournals.DataBind();
        }

    }
    protected void LoadFilteredJournal()
    {
        objDOSAJor.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSAJor.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objDOSAJor.ActiveStatus = hdnIsFiltered.Value == "active";
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.FilteredJournals);
        if (dt.Rows.Count > 0)
        {
            lstJournals.DataSource = dt;
            lstJournals.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            lstJournals.DataSource = null;
            lstJournals.DataBind();
        }

    }


    protected void checkedActive_CheckedChanged(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        CheckBox cb = (CheckBox)sender;
        ListViewItem lvi = (ListViewItem)cb.Parent;
        HiddenField hdnJournalId = (HiddenField)lvi.FindControl("hdnJournalId");
        objDOSAJor.JournalID = Convert.ToInt32(hdnJournalId.Value);
        objDOSAJor.ActiveStatus = cb.Checked;
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.UpdateStatus);
        ReLoadJournals();

        if (objDOSAJor.ActiveStatus == false)
        {
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("removeJournal?");
                    url.Append("journalId=");
                    url.Append(objDOSAJor.JournalID);
                    url.Append("&userId=");
                    url.Append(Convert.ToInt32(ViewState["UserID"]));

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Deleting Journals";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }
        } else
        {
            if (ISAPIURLACCESSED != "0")
            {
                try
                {

                    objDOSAJor.CurrentPage = 1;
                    objDOSAJor.CurrentPageSize = 100;
                    //objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
                    DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetArticles);

                    ArticleList articleList = new ArticleList();
                    articleList.articles = new List<Article>();


                    Article art;

                    for (int i = 0; i < dtArt.Rows.Count; i++)
                    {
                        art = new Article();
                        art.articleId = dtArt.Rows[i]["ArticleId"].ToString();
                        art.title = dtArt.Rows[i]["ArtTitle"].ToString();
                        art.url = MailURL + "/Articles/" + dtArt.Rows[i]["FilePath"].ToString();
                        articleList.articles.Add(art);
                    }

                    string json_result = JsonConvert.SerializeObject(articleList);

                    string filePath = "~/Articles/";
                    string pathPhysical = Server.MapPath(filePath + "articles.json");
                    if (File.Exists(pathPhysical))
                    {
                        File.Delete(pathPhysical);
                    }

                    using (var tw = new StreamWriter(pathPhysical, true))
                    {
                        tw.WriteLine(json_result.ToString());
                        tw.Close();
                    }

                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("addJournal?");
                    url.Append("journalId=");
                    url.Append(objDOSAJor.JournalID);
                    url.Append("&userId=");
                    url.Append(Convert.ToInt32(ViewState["UserID"]));
                    url.Append("&url=");
                    url.Append(MailURL + "/Articles/articles.json");

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "Adding Journals";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
                catch { }
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Status has been updated successfully.')", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearJournal();
        docModal1.Style.Add("display", "none");
    }

    private void ReLoadJournals()
    {
        if (hdnIsFiltered.Value != "false")
        {
            LoadFilteredJournal();
        }
        else
        {
            LoadAllJournal();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveJournal();
        ReLoadJournals();

        ClearJournal();
        docModal1.Style.Add("display", "none");
    }

    private void SaveJournal()
    {
        objDOSAJor.ActiveStatus = false;
        objDOSAJor.Month = Convert.ToInt32(fromMonth.SelectedValue);
        objDOSAJor.Year = Convert.ToInt32(txtFromYear.SelectedValue);
        objDOSAJor.Title = txtTitle.Text;
        if(string.IsNullOrEmpty(hdnJournalIDGlo.Value) || hdnJournalIDGlo.Value == "false")
            objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.AddJournal);
        else
        {
            objDOSAJor.JournalID = Convert.ToInt32(hdnJournalIDGlo.Value);
            objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.EditJournal);
        }
    }
    protected void lnkAddJournal_Click(object sender, EventArgs e)
    {
        docModal1.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "eu12p", "OverlayBody();", true);
    }
    protected void ClearJournal()
    {
        hdnJournalIDGlo.Value = "";
        txtTitle.Text = "";
        txtFromYear.SelectedIndex = -1;
        fromMonth.SelectedIndex = -1;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "eu12p", "RemoveOverlay();", true);
    }
    protected void LnkActiveClick(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        hdnIsFiltered.Value = "active";
        FilterText = "active";
        txtFilter.InnerText = "Active Journals";
        ReLoadJournals();
    }
    protected void LnkInActiveClick(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        hdnIsFiltered.Value = "inactive";
        FilterText = "inactive";
        txtFilter.InnerText = "Inactive Journals";
        ReLoadJournals();
    }
    protected void lstJournals_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnJournalId = e.Item.FindControl("hdnJournalId") as HiddenField;
        if (e.CommandName == "Details")
        {
            Response.Redirect("SA_JournalDetails.aspx?JrnlId=" + hdnJournalId.Value);
        }
        else if (e.CommandName == "EditJournal")
        {
            HiddenField hdnMonth = e.Item.FindControl("hdnMonth") as HiddenField;
            HiddenField hdnYear = e.Item.FindControl("hdnYear") as HiddenField;
            HiddenField hdnTitle = e.Item.FindControl("hdnTitle") as HiddenField;
            txtTitle.Text = hdnTitle.Value;
            txtFromYear.SelectedValue = hdnYear.Value;
            fromMonth.SelectedValue = hdnMonth.Value;
            hdnJournalIDGlo.Value = hdnJournalId.Value;
            docModal1.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "eu12p", "OverlayBody();", true);
        }
    }

    protected void lstJournals_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl lblArtCounts = e.Item.FindControl("lblArtCounts") as HtmlGenericControl;
        DataRowView drv = e.Item.DataItem as DataRowView;
        lblArtCounts.InnerText = drv["ArticleCount"].ToString() + (Convert.ToInt32(drv["ArticleCount"]) == 1 ? " Article" : " Articles");
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
                if (totalPage >= EndPage)
                {
                    lnkNext.Visible = true;
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;
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
                ReLoadJournals();
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
            ReLoadJournals();
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        //  divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            ReLoadJournals();
        }
    }

    class ArticleList
    {
        public List<Article> articles;
    }

    class Article
    {
        public string articleId;
        public string title;
        public string url;
    }
}