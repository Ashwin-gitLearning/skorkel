using DA_SKORKEL;
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
using System.Web.UI;
//using System.DateTimeOffset;
using System.Web.UI.WebControls;

public partial class SA_Add_News : System.Web.UI.Page
{
    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    DataTable dtNews = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

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
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {
                ViewState["PostId"] = Request.QueryString["PostId"];
                LoadEditNews();
            }

            txtTitle.Focus();
        }
    }

    protected void LoadEditNews()
    {
        dtNews.Clear();
        objDONewsListing.ID = Convert.ToInt32(ViewState["PostId"]);
        dtNews = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleNewsRecord);
        if (dtNews.Rows.Count > 0)
        {
            txtTitle.Text = Convert.ToString(dtNews.Rows[0]["Title"]);
            CKDescription.InnerText = Convert.ToString(dtNews.Rows[0]["Content"]);
            //txtSourceField.Text = Convert.ToString(dtNews.Rows[0]["Type"]);
            if (Convert.ToString(dtNews.Rows[0]["Type"]) == "Skorkel")
            {
                txtSourceField.Text = "";
            }
            else
            {
                txtSourceField.Text = Convert.ToString(dtNews.Rows[0]["Type"]);
            }
            txtSourceLink.Text = Convert.ToString(dtNews.Rows[0]["Source_link"]);
        }
        else
        {
            txtTitle.Text = "";
            CKDescription.InnerText = string.Empty;
            txtSourceField.Text = "";
            txtSourceLink.Text = "";
        }
    }

    protected void lnkPublish_Click(object sender, EventArgs e)
    {
        DataTable dtNews = new DataTable();
        DataTable dtEditNews = new DataTable();
        int intNewsOutId = 0;
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtSourceField.Text != "" && txtSourceLink.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),"ShowAddUserSuccess0", "showSuccessPopup('Alert','If Source title provided, then Source link cannot be empty.');", true);
        }
        else if (txtSourceField.Text == "" && txtSourceLink.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),"ShowAddUserSuccess1", "showSuccessPopup('Alert','If Source link provided, then Source title cannot be empty.');", true);
        }
        else
        {
            if (ViewState["PostId"] == null)
            {
                objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtTitle.Text.Replace("'", "''").Trim()));

                string description = CKDescription.Value;
                description = "<div class='rte-body'>" + description + "</div>";
                //.InnerText.Trim();    //.Replace(",", "''").Replace("<p>","<br>").Replace("</p>", "")
                //string noHTML = Regex.Replace(description, @"<[^>]+>|&nbsp;", "").Trim();
                //objDONewsListing.Content = Regex.Replace(noHTML, @"\s{2,}", " ");
                objDONewsListing.Content = description;
                if (txtSourceField.Text != "")
                {
                    objDONewsListing.Type = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
                }
                else
                {
                    objDONewsListing.Type = "Skorkel";
                }
                if (txtSourceLink.Text != "")
                {
                    objDONewsListing.Source_link = Validations.validateHtmlInput(Convert.ToString(txtSourceLink.Text.Replace("'", "''").Trim()));
                }
                else
                {
                    objDONewsListing.Source_link = "";
                }
                intNewsOutId = objDANewsListing.AddEditDel_NewsListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.AddNews);
                objDONewsListing.ID = intNewsOutId;
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
                        url.Append(intNewsOutId);
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
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO,DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }

                clear();
                Response.Redirect("SA_NewsListing.aspx");
            }
            else
            {
                objDONewsListing.ID = Convert.ToInt32(ViewState["PostId"]);
                objDONewsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtTitle.Text.Replace("'", "''").Trim()));
                string description = CKDescription.Value;//.InnerText.Trim().Replace(",","''").Replace("<p>", "<br>").Replace("</p>", "");
                //string noHTML = Regex.Replace(description, @"<[^>]+>|&nbsp;", "").Trim();
                //objDONewsListing.Content = Regex.Replace(noHTML, @"\s{2,}", " ");
                objDONewsListing.Content = description;
                if (txtSourceField.Text != "")
                {
                    objDONewsListing.Type = Validations.validateHtmlInput(Convert.ToString(txtSourceField.Text.Replace("'", "''").Trim()));
                }
                else
                {
                    objDONewsListing.Type = "Skorkel";
                }
                if (txtSourceLink.Text != "")
                {
                    objDONewsListing.Source_link = Validations.validateHtmlInput(Convert.ToString(txtSourceLink.Text.Replace("'", "''").Trim()));
                }
                else
                {
                    objDONewsListing.Source_link = "";
                }
                objDANewsListing.Edit_NewsListing(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.UpdateNews);
                objDONewsListing.ID = Convert.ToInt32(ViewState["PostId"]);
                dtEditNews.Clear();
                dtEditNews = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleNewsRecord);
               
                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        string detail = null;
                        string noHTML = null;
                        string detail_noHTML = null;

                        detail = Convert.ToString(dtEditNews.Rows[0]["Content"]);
                        noHTML = Regex.Replace(detail, @"<[^>]+>|&nbsp;", "").Trim();
                        detail_noHTML = Regex.Replace(noHTML, @"\s{2,}", " ");

                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("updateNews?");
                        url.Append("newsId=");
                        url.Append(Convert.ToInt32(ViewState["PostId"]));
                        url.Append("&userId=");
                        url.Append(Convert.ToInt32(ViewState["UserID"]));
                        url.Append("&title=");
                        url.Append(dtEditNews.Rows[0]["Title"]);
                        url.Append("&description=");
                        url.Append(detail_noHTML);

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url.ToString();
                        objAPILogDO.strAPIType = "Update News";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch { }
                }

                clear();
                Response.Redirect("SA_NewsListing.aspx");
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void clear()
    {
        //hdnsubject.Value = "";
        //SubjectTempDataTable();
        txtTitle.Text = "";
        CKDescription.InnerText = string.Empty;
        txtSourceField.Text = "";
        txtSourceLink.Text = "";
        //lblSuMess.Text = "";
        //BindSubjectList();
        //Session["SubmitTime"] = DateTime.Now.ToString();
        ViewState["intBlogId"] = null;
    }
}