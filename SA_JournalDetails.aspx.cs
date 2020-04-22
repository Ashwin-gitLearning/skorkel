using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class SA_JournalDetails : System.Web.UI.Page
{
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
    string FilterText = "";

    DataTable dtArt;

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

            if (Request.QueryString["JrnlId"] != null)
            {
                objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
                GetJournalDetails();
                BindArticles();
            }
        }
    }
    protected void checkedActive_CheckedChanged(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        objDOSAJor.ActiveStatus = checkedActive.Checked;
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.UpdateStatus);
        GetJournalDetails();
        BindArticles();

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
                    ArticleList articleList = new ArticleList();
                    articleList.articles = new List<Article>();


                    Article art;
                    
                    for (int i=0; i < dtArt.Rows.Count; i++)
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
                    url.Append(MailURL+ "/Articles/articles.json");

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

        ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlrt", "showSuccessPopup('Success','Status has been updated successfully.')", true);
    }
    private void GetJournalDetails()
    {
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetJournal);
        if (dt.Rows.Count > 0)
        {
            txtJournalName.InnerText = dt.Rows[0]["JournalTitle"].ToString();
            hdnJournlaActive.Value= dt.Rows[0]["status"].ToString().ToLower();
            checkedActive.Checked = Convert.ToBoolean(hdnJournlaActive.Value);
        }

    }
    private void BindArticles()
    {
        objDOSAJor.CurrentPage = 1;
        objDOSAJor.CurrentPageSize = 100;
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetArticles);
        lstArticles.DataSource = dtArt;
        lstArticles.DataBind();
    }

    protected void lstArticles_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
            HiddenField hdnArticleId = e.Item.FindControl("hdnArticleId") as HiddenField;
            hdnArticleGloId.Value = hdnArticleId.Value;
            objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.RemoveArt);
        }
        else if (e.CommandName == "Download")
        {
            HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
            LinkButton lnkArtTItle = e.Item.FindControl("lnkArtTItle") as LinkButton;
            string strURL = "~\\Articles\\" + hdnFilePath.Value;
            string filename = lnkArtTItle.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }
    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        RemoveArticle();
        BindArticles();
        Clear();
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void RemoveArticle()
    {
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        objDOSAJor.ArticleID = Convert.ToInt32(hdnArticleGloId.Value);
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.RemoveArt);
    }
    private void Clear()
    {
        hdnArticleGloId.Value = "";
    }

    protected void lstArticles_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lnkRemove = e.Item.FindControl("lnkRemove") as LinkButton;
        //HtmlGenericControl divEdit = e.Item.FindControl("divEdit") as HtmlGenericControl;
        //divEdit.Visible = !Convert.ToBoolean(hdnJournlaActive.Value);
        lnkRemove.Visible = !Convert.ToBoolean(hdnJournlaActive.Value);
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