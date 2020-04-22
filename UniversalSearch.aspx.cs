using DA_SKORKEL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UniversalSearch : System.Web.UI.Page
{
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DO_Scrl_UserPostUpdateTbl objpost = new DO_Scrl_UserPostUpdateTbl();
    DA_Scrl_UserPostUpdateTbl objpostDB = new DA_Scrl_UserPostUpdateTbl();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();

    DataTable dt = new DataTable();
    DataTable dtResearch = new DataTable();
    DataRow row;

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
            string srch = Convert.ToString(Request.QueryString["srch"]);
            lblSearch.Text = srch;
            BindAllSearches();
        }        
    }

    protected void BindAllSearches()
    {
        BindCases();
        //BindQuestions();
        //BindNews();
        //BindBlogs();
        //BindJournalArticles();
        //BindUsers();
        //BindGroups();
    }

    protected void BindJournalArticles()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
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
                url.Append(50);

                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(url.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> ArticleIds = new List<string>();
                    int totalResults = 0;
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                        for (int i = 0; i < 3 && i < totalResults; i++)
                        {
                            ArticleIds.Add(jsonResponse.responseJson.searchResultArticleSet_[i].uid.ToString());
                        }
                    }

                    var result = string.Join(",", ArticleIds);
                    //result = "17";
                    dt.Clear();
                    objDOSAJor.ArticleIdList = Convert.ToString(result);
                    dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetArticlesUS);
                    if (dt.Rows.Count > 0)
                    {
                        divArticles.Visible = true;
                        lstParentArticlesResults.Visible = true;
                        lstParentArticlesResults.DataSource = dt;
                        lstParentArticlesResults.DataBind();
                    }
                    else
                    {
                        divArticles.Visible = false;
                        lstParentArticlesResults.DataSource = null;
                        lstParentArticlesResults.DataBind();
                        lstParentArticlesResults.Visible = false;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindCases()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);

            if (ISAPIURLACCESSED != "0")
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("skorkelAdvanceSearch.action?");
                url.Append("title=");
                url.Append(srch);
                url.Append("&searchtype=");
                url.Append("FREE+TEXT");
                url.Append("&maxResultPerPage=");
                url.Append(50);

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(url));
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                GetJsonDocument(result);
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void GetJsonDocument(string rslt)
    {
        //string data = Convert.ToString(ViewState["url"]);

        _responseJson1 my = JsonConvert.DeserializeObject<_responseJson1>(rslt);
        StringBuilder sb = new StringBuilder();
        ResearchDataTable();
        if (my.responseJson != null)
        {
            try
            {
                string totalResults = my.responseJson.totalResultCount.ToString();
                //ViewState["searchCount"] = my.responseJson.totalResultCount.ToString();
                //ViewState["CursorID"] = my.responseJson.cursorId;
                var NameList = (from r in my.responseJson.searchResultDocumentSets
                                select new
                                {
                                    appellant = r.appellant,
                                    court = r.court,
                                    bookmark = r.bookmark,
                                    commentCount = r.commentCount,
                                    displayContent = r.displayContent,
                                    docType = r.docType,
                                    downloadCount = r.downloadCount,
                                    judgeTagCount = r.judgeTagCount,
                                    likes = r.likes,
                                    profTagCount = r.profTagCount,
                                    rating = r.rating,
                                    score = r.score,
                                    shareCount = r.shareCount,
                                    studentTagCount = r.studentTagCount,
                                    title = r.title,
                                    uploadBy = r.uploadBy,
                                    uploadByName = r.uploadByName,
                                    uploadDt = r.uploadDt,
                                    weightage = r.weightage,
                                    tagCnt = r.tagCnt,
                                    citation = r.citation,
                                    r.year,
                                    r.docUid,
                                    r.citedBy,
                                    r.subject,
                                    r.judgeNames
                                }).Distinct().ToList();
                foreach (var v in NameList)
                {
                    row = dtResearch.NewRow();
                    row["appellant"] = v.appellant;
                    row["bookmark"] = v.bookmark;
                    row["commentCount"] = v.commentCount;
                    row["court"] = v.court;
                    row["displayContent"] = v.displayContent;
                    row["docType"] = v.docType;
                    row["downloadCount"] = v.downloadCount;
                    row["judgeTagCount"] = v.judgeTagCount;
                    row["likes"] = v.likes;
                    row["profTagCount"] = v.profTagCount;
                    row["rating"] = v.rating;
                    row["score"] = v.score;
                    row["shareCount"] = v.shareCount;
                    row["studentTagCount"] = v.studentTagCount;
                    row["tagCnt"] = v.tagCnt;
                    row["title"] = v.title;
                    row["uploadBy"] = v.uploadBy;
                    row["uploadByName"] = v.uploadByName;
                    row["uploadDt"] = v.uploadDt;
                    row["weightage"] = v.weightage;
                    row["citation"] = v.citation;
                    row["year"] = v.year;
                    row["docUid"] = v.docUid;
                    row["citedBy"] = v.citedBy;
                    row["subject"] = v.subject;
                    row["judgeNames"] = v.judgeNames;
                    dtResearch.Rows.Add(row);
                }

                if (dtResearch.Rows.Count > 0)
                {
                    divCases.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "functionHideCall();", true);
                    //divsearchHeight.Style.Add("height", "auto");

                    //ViewState["SearchResults"] = dtResearch;
                    hdnMaxcount.Value = my.responseJson.totalResultCount.ToString();
                    int pageNum = 1;
                    int pageSize = 3;
                    DataTable dtPage = dtResearch.Rows.Cast<System.Data.DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
                    lstParentCasesResults.DataSource = dtPage;
                    lstParentCasesResults.DataBind();
                    if (dtResearch.Rows.Count > 0)
                    {
                        //pLoadMore.Style.Add("display", "block");
                        //lblNoMoreRslt.Visible = false;
                        lstParentCasesResults.DataSource = dtPage;
                        lstParentCasesResults.DataBind();
                    }
                    else
                    {
                        //pLoadMore.Style.Add("display", "none");
                        //lblNoMoreRslt.Visible = true;
                        //lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
                        lstParentCasesResults.DataSource = null;
                        lstParentCasesResults.DataBind();
                    }
                    dtResearch = new DataTable();
                    //ViewState["NewPageSize"] = null;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "functionHideCall();", true);
                    lstParentCasesResults.DataSource = null;
                    lstParentCasesResults.DataBind();
                    //divsearchHeight.Style.Add("height", "550px");
                    dtResearch = new DataTable();
                    //pLoadMore.Style.Add("display", "none");
                    //lblNoMoreRslt.Visible = true;
                    //lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
                }
            }
            catch
            {
            }
        }
        else if (my.responseJson == null)
        {
            //divsearchHeight.Style.Add("height", "550px");
            // totalResults = "0";
            lstParentCasesResults.DataSource = null;
            lstParentCasesResults.DataBind();
            //pLoadMore.Style.Add("display", "none");
            //lblNoMoreRslt.Visible = true;
            //lblEnterKeyword.Text = "Entered Keyword or Search Attributes";
        }
    }

    protected void ResearchDataTable()
    {
        DataColumn judgeNames = new DataColumn();
        judgeNames.DataType = System.Type.GetType("System.String");
        judgeNames.ColumnName = "judgeNames";
        dtResearch.Columns.Add(judgeNames);

        DataColumn bookmark = new DataColumn();
        bookmark.DataType = System.Type.GetType("System.String");
        bookmark.ColumnName = "bookmark";
        dtResearch.Columns.Add(bookmark);

        DataColumn UserTypeId = new DataColumn();
        UserTypeId.DataType = System.Type.GetType("System.String");
        UserTypeId.ColumnName = "UserTypeId";
        dtResearch.Columns.Add(UserTypeId);

        DataColumn commentCount = new DataColumn();
        commentCount.DataType = System.Type.GetType("System.Int32");
        commentCount.ColumnName = "commentCount";
        dtResearch.Columns.Add(commentCount);

        DataColumn displayContent = new DataColumn();
        displayContent.DataType = System.Type.GetType("System.String");
        displayContent.ColumnName = "displayContent";
        dtResearch.Columns.Add(displayContent);

        DataColumn docType = new DataColumn();
        docType.DataType = System.Type.GetType("System.String");
        docType.ColumnName = "docType";
        dtResearch.Columns.Add(docType);

        DataColumn downloadCount = new DataColumn();
        downloadCount.DataType = System.Type.GetType("System.Int32");
        downloadCount.ColumnName = "downloadCount";
        dtResearch.Columns.Add(downloadCount);

        DataColumn judgeTagCount = new DataColumn();
        judgeTagCount.DataType = System.Type.GetType("System.String");
        judgeTagCount.ColumnName = "judgeTagCount";
        dtResearch.Columns.Add(judgeTagCount);

        DataColumn rating = new DataColumn();
        rating.DataType = System.Type.GetType("System.String");
        rating.ColumnName = "rating";
        dtResearch.Columns.Add(rating);

        DataColumn likes = new DataColumn();
        likes.DataType = System.Type.GetType("System.String");
        likes.ColumnName = "likes";
        dtResearch.Columns.Add(likes);

        DataColumn profTagCount = new DataColumn();
        profTagCount.DataType = System.Type.GetType("System.String");
        profTagCount.ColumnName = "profTagCount";
        dtResearch.Columns.Add(profTagCount);

        DataColumn score = new DataColumn();
        score.DataType = System.Type.GetType("System.String");
        score.ColumnName = "score";
        dtResearch.Columns.Add(score);

        DataColumn shareCount = new DataColumn();
        shareCount.DataType = System.Type.GetType("System.Int32");
        shareCount.ColumnName = "shareCount";
        dtResearch.Columns.Add(shareCount);

        DataColumn studentTagCount = new DataColumn();
        studentTagCount.DataType = System.Type.GetType("System.String");
        studentTagCount.ColumnName = "studentTagCount";
        dtResearch.Columns.Add(studentTagCount);

        DataColumn title = new DataColumn();
        title.DataType = System.Type.GetType("System.String");
        title.ColumnName = "title";
        dtResearch.Columns.Add(title);

        DataColumn uploadBy = new DataColumn();
        uploadBy.DataType = System.Type.GetType("System.String");
        uploadBy.ColumnName = "uploadBy";
        dtResearch.Columns.Add(uploadBy);

        DataColumn uploadDt = new DataColumn();
        uploadDt.DataType = System.Type.GetType("System.String");
        uploadDt.ColumnName = "uploadDt";
        dtResearch.Columns.Add(uploadDt);

        DataColumn weightage = new DataColumn();
        weightage.DataType = System.Type.GetType("System.String");
        weightage.ColumnName = "weightage";
        dtResearch.Columns.Add(weightage);

        DataColumn uploadByName = new DataColumn();
        uploadByName.DataType = System.Type.GetType("System.String");
        uploadByName.ColumnName = "uploadByName";
        dtResearch.Columns.Add(uploadByName);

        DataColumn tagCnt = new DataColumn();
        tagCnt.DataType = System.Type.GetType("System.String");
        tagCnt.ColumnName = "tagCnt";
        dtResearch.Columns.Add(tagCnt);

        DataColumn appellant = new DataColumn();
        appellant.DataType = System.Type.GetType("System.String");
        appellant.ColumnName = "appellant";
        dtResearch.Columns.Add(appellant);

        DataColumn court = new DataColumn();
        court.DataType = System.Type.GetType("System.String");
        court.ColumnName = "court";
        dtResearch.Columns.Add(court);

        DataColumn citation = new DataColumn();
        citation.DataType = System.Type.GetType("System.String");
        citation.ColumnName = "citation";
        dtResearch.Columns.Add(citation);

        DataColumn year = new DataColumn();
        year.DataType = System.Type.GetType("System.String");
        year.ColumnName = "year";
        dtResearch.Columns.Add(year);

        DataColumn docUid = new DataColumn();
        docUid.DataType = System.Type.GetType("System.String");
        docUid.ColumnName = "docUid";
        dtResearch.Columns.Add(docUid);

        DataColumn citedBy = new DataColumn();
        citedBy.DataType = System.Type.GetType("System.String");
        citedBy.ColumnName = "citedBy";
        dtResearch.Columns.Add(citedBy);

        DataColumn subject = new DataColumn();
        subject.DataType = System.Type.GetType("System.String");
        subject.ColumnName = "subject";
        dtResearch.Columns.Add(subject);
    }

    protected void BindQuestions()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            if (ISAPIURLACCESSED != "0")
            {
                //try
                //{
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("skorkelAdvanceSearch.action?");
                url.Append("title=");
                url.Append(srch);
                url.Append("&searchtype=");
                url.Append("QUESTION");
                url.Append("&maxResultPerPage=");
                url.Append(50);

                //HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                //myRequest1.Method = "GET";
                //WebResponse myResponse1 = myRequest1.GetResponse();
                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(url.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> questionIds = new List<string>();

                    int totalResults = 0;
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                        for (int i = 0; i < 3 && i < totalResults; i++)
                        {
                            questionIds.Add(jsonResponse.responseJson.searchResultQASets[i].uid.ToString());
                        }

                    }
                    var result = string.Join(",", questionIds);

                    dt.Clear();
                    objDOQAPosting.QuestionIdList = Convert.ToString(result);
                    dt = objDAQAPosting.GetDataTableQuestion(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetQuestionListing);
                    if (dt.Rows.Count > 0)
                    {
                        divQuestions.Visible = true;
                        lstParentResults.Visible = true;
                        lstParentResults.DataSource = dt;
                        lstParentResults.DataBind();
                    }
                    else
                    {
                        divQuestions.Visible = false;
                        lstParentResults.DataSource = null;
                        lstParentResults.DataBind();
                        lstParentResults.Visible = false;
                    }
                }

                //StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                //String result = sr.ReadToEnd();

                //int rr = GetIds(result);
                //objAPILogDO.strURL = url.ToString();
                //objAPILogDO.strAPIType = "Search Question";
                //objAPILogDO.strResponse = result;
                //objAPILogDO.strIPAddress = ip;
                //objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                //objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                //}
                //catch { }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindNews()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
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
                url.Append("NEWS");
                url.Append("&pageNo=1");
                url.Append("&maxResultPerPage=");
                url.Append(50);

                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(url.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> NewsIds = new List<string>();

                    int totalResults = 0;
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                        for (int i = 0; i < 3 && i < totalResults; i++)
                        {
                            NewsIds.Add(jsonResponse.responseJson.searchResultNewsSets[i].UId.ToString());
                        }
                    }

                    var result = string.Join(",", NewsIds);
                    dt.Clear();
                    objDONewsListing.NewsIdList = Convert.ToString(result);
                    dt = objDANewsListing.GetDataTableNews(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsListingUS);
                    if (dt.Rows.Count > 0)
                    {
                        divNews.Visible = true;
                        lstParentNewsResults.Visible = true;
                        lstParentNewsResults.DataSource = dt;
                        lstParentNewsResults.DataBind();
                    }
                    else
                    {
                        divNews.Visible = false;
                        lstParentNewsResults.DataSource = null;
                        lstParentNewsResults.DataBind();
                        lstParentNewsResults.Visible = false;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindBlogs()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
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
                url.Append("BLOG");
                url.Append("&maxResultPerPage=");
                url.Append(50);

                using (WebClient wc = new WebClient())
                {
                    var content = wc.DownloadString(url.ToString());
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    List<string> BlogIds = new List<string>();

                    int totalResults = 0;
                    if (jsonResponse.responseJson != null)
                    {
                        totalResults = Convert.ToInt32(jsonResponse.responseJson.totalResultCount.Value);
                        for (int i = 0; i < 3 && i < totalResults; i++)
                        {
                            BlogIds.Add(jsonResponse.responseJson.searchResultBlogSets[i].UId.ToString());
                        }
                    }

                    var result = string.Join(",", BlogIds);
                    dt.Clear();
                    objblogdo.BlogsIdList = Convert.ToString(result);
                    dt = objblogda.GetDataTableBlogs(objblogdo, DA_NewBlogs.Blog.GetBlogListing);
                    if (dt.Rows.Count > 0)
                    {
                        divBlogs.Visible = true;
                        lstParentBlogsResults.Visible = true;
                        lstParentBlogsResults.DataSource = dt;
                        lstParentBlogsResults.DataBind();
                    }
                    else
                    {
                        divBlogs.Visible = false;
                        lstParentBlogsResults.DataSource = null;
                        lstParentBlogsResults.DataBind();
                        lstParentBlogsResults.Visible = false;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindUsers()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objpost.CurrentPage = Convert.ToInt32(1);
                objpost.CurrentPageSize = Convert.ToInt32(3);
                if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                    objpost.intUserType = hdnTempUserId.Value;
                objpost.strSearch = srch;
                dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetAllStudentDetails);
                if (dt.Rows.Count > 0)
                {
                    divUsers.Visible = true;
                    lstParentUsersResults.Visible = true;
                    lstParentUsersResults.DataSource = dt;
                    lstParentUsersResults.DataBind();
                    //dvPage.Visible = true;
                    //BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
                }
                else
                {
                    divUsers.Visible = false;
                    lstParentUsersResults.Visible = false;
                    lstParentUsersResults.DataSource = null;
                    lstParentUsersResults.DataBind();
                    //dvPage.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindGroups()
    {
        try
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                objpost.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objpost.CurrentPage = Convert.ToInt32(1);
                objpost.CurrentPageSize = Convert.ToInt32(3);
                objpost.strSearch = srch;
                //if (hdnTempUserId.Value != "" && hdnTempUserId.Value != null)
                //    objpost.strCityType = hdnTempUserId.Value;

                dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetGroupSearchDetails);
                if (dt.Rows.Count > 0)
                {
                    //lblMessage.Visible = false;
                    divGroups.Visible = true;
                    lnkGroupsResults.Visible = true;
                    lstParentGroupsResults.DataSource = dt;
                    lstParentGroupsResults.DataBind();
                }
                else
                {
                    //lblMessage.Visible = true;
                    divGroups.Visible = false;
                    lnkGroupsResults.Visible = false;
                    lstParentGroupsResults.DataSource = null;
                    lstParentGroupsResults.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
    }

    protected void BindMoreCases()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("Research_SearchResult_S.aspx?srch=" + srch, false);
    }

    protected void BindMoreQuestions()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("AllQuestionDetails.aspx?srch=" + srch, false);
    }

    protected void BindMoreNews()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("AllNewsListing.aspx?srch=" + srch, false);
    }

    protected void BindMoreBlogs()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("AllBlogs.aspx?srch=" + srch, false);
    }

    protected void BindMoreUsers()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("SearchPeople.aspx?srch=" + srch, false);
    }

    protected void BindMoreGroups()
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("SearchGroup.aspx?srch=" + srch, false);
    }

    protected void lstParentCasesResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostCaseID = (HiddenField)e.Item.FindControl("hdnPostCaseID");

        if (e.CommandName == "CaseDetails")
        {
            Response.Redirect("Research-Case-Details.aspx?CTid=1&cId=" + hdnPostCaseID.Value, false);
        }
    }

    protected void lstParentArticlesResults_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
    {
        HiddenField hdnArticleId = e.Item.FindControl("hdnArticleId") as HiddenField;
        HiddenField hdnJournalId = e.Item.FindControl("hdnJournalId") as HiddenField;
        LinkButton lnkArtTItle = e.Item.FindControl("lnkArtTItle") as LinkButton;
        if (e.CommandName == "ArticleDetails")
        {
            Response.Redirect("ArticleDetails.aspx?JrnlId=" + hdnJournalId.Value + "&ArtId=" + hdnArticleId.Value);
        }
        else if (e.CommandName == "Download")
        {
            HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
            string strURL = "~\\Articles\\" + hdnFilePath.Value;
            string filename = lnkArtTItle.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }

    protected void lstParentArticlesResults_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
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

    protected void lstParentResults_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtlst = new DataTable();
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        Label lblLikePost = (Label)e.Item.FindControl("lblLikePost");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        Label lblAnswer = (Label)e.Item.FindControl("lblAnswer");
        LinkButton lnkLikebtn = (LinkButton)e.Item.FindControl("lnkLikebtn");
        HiddenField intAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        objDOQAPosting.intAddedBy = Convert.ToInt32(intAddedBy.Value);
        dtlst = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);

        if (dtlst.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dtlst.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dtlst.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dtlst.Rows[0]["TotalShare"]);

            lblLikePost.Text = Convert.ToString(TotalLike) + (TotalLike == 1 ? " Like" : " Likes");
            if (dtlst.Rows[0]["LikeUserId"].ToString() != "")
                lnkLikebtn.CssClass = "active-toogle on-flag";
            lblShare.Text = Convert.ToString(TotalShare) + " Shared";
            lblAnswer.Text = Convert.ToString(TotalReply) + (TotalReply == 1 ? " Answer" : " Answers");
        }
        else
        {
            lblLikePost.Text = "0 Likes";
            lblShare.Text = "0 Shared";
            lblAnswer.Text = "0 Answers";
        }
    }

    protected void lstParentResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");

        if (e.CommandName == "QuestionDetails")
        {
            Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value, false);
        }
    }

    protected void lstParentNewsResults_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtNews = new DataTable();
        HiddenField hdnNewsID = (HiddenField)e.Item.FindControl("hdnNewsID");
        Label lblNewsDescription = (Label)e.Item.FindControl("lblNewsDescription");

        string detail = null;
        string noHTML = null;
        string detail_noHTML = null;

        dtNews.Clear();
        objDONewsListing.ID = Convert.ToInt32(hdnNewsID.Value);
        dtNews = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleNewsRecord);
        detail = Convert.ToString(dtNews.Rows[0]["Content"]);
        noHTML = Regex.Replace(detail, @"<[^>]+>|&nbsp;", "").Trim();
        detail_noHTML = Regex.Replace(noHTML, @"\s{2,}", " ");

        if (dtNews.Rows.Count > 0)
        {
            lblNewsDescription.Text = detail_noHTML;
        }
        else
        {
            lblNewsDescription.Text = "";
        }
    }

    protected void lstParentNewsResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnNewsID = (HiddenField)e.Item.FindControl("hdnNewsID");

        if (e.CommandName == "NewsDetails")
        {
            Response.Redirect("News-Details.aspx?NewsId=" + hdnNewsID.Value, false);
        }
    }

    protected void lstParentBlogsResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostBlogID = (HiddenField)e.Item.FindControl("hdnPostBlogID");

        if (e.CommandName == "BlogDetails")
        {
            Response.Redirect("BlogsComments.aspx?intBlogId=" + hdnPostBlogID.Value, false);
        }
    }

    protected void lstParentUsersResults_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        ImageButton imgprofile = (ImageButton)e.Item.FindControl("imgprofile");
        Label lblFriends = (Label)e.Item.FindControl("lblFriends");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
        HiddenField hdnintUserTypeID = (HiddenField)e.Item.FindControl("hdnintUserTypeID");
        HiddenField hdnstrOthers = (HiddenField)e.Item.FindControl("hdnstrOthers");
        Label lblName = (Label)e.Item.FindControl("lblName");

        if (imgprofile.ImageUrl == "" || imgprofile.ImageUrl == null || imgprofile.ImageUrl == "CroppedPhoto/")
        {
            imgprofile.ImageUrl = "images/profile-photo.png";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.ImageUrl = "images/profile-photo.png";
            }
        }

        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            if (hdnintUserTypeID.Value == "6")
                lblName.Text = Convert.ToString(hdnstrOthers.Value);

            if (Convert.ToString(ViewState["UserID"]) == Convert.ToString(Session["ExternalUserId"]))
            {
                //objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
                //objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                //DataTable dtReq = new DataTable();
                //dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
                //if (dtReq.Rows.Count > 0)
                //{
                //    if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 0)
                //    {
                //        //btnsendreq.Visible = true;
                //        //lnkDisConnect.Visible = false;
                //        //btnRecmnd.Visible = false;
                //        //if (dtReq.Rows[0]["Accepts"].ToString() == "2")
                //        //    btnsendreq.Text = "Accept Invitation";

                //    }
                //    else if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 1)
                //    {
                //        //btnsendreq.Visible = false;
                //        //lnkDisConnect.Visible = true;
                //        //btnRecmnd.Visible = true;
                //        //conn_user_icon.Visible = true;
                //    }
                //    else if (Convert.ToInt32(dtReq.Rows[0]["IsAccepted"]) == 2)
                //    {
                //        //btnsendreq.Visible = true;
                //        //lnkDisConnect.Visible = false;
                //        //btnRecmnd.Visible = false;
                //    }
                //}
                //else
                //{
                //    //btnRecmnd.Visible = false;
                //    //lnkDisConnect.Visible = false;
                //}
                DataTable dtfrnd = new DataTable();
                objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);

                if (Convert.ToString(dtfrnd.Rows.Count) != "" && Convert.ToString(dtfrnd.Rows.Count) != null)
                {
                    if (Convert.ToString(dtfrnd.Rows[0]["Accepted"]) == "1")
                    {
                        lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friend";
                    }
                    else
                    {
                        lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friends";
                    }
                }
                else
                {
                    lblFriends.Text = "0 friends";
                }
            }
            else if (Convert.ToInt32(Session["ExternalUserId"]) == Convert.ToInt32(hdnRegistrationId.Value))
            {
                //btnsendreq.Visible = false;
                //btnRecmnd.Visible = false;
                //lnkDisConnect.Visible = false;

                DataTable dtfrnd = new DataTable();
                objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);
                if (Convert.ToString(dtfrnd.Rows.Count) != "" && Convert.ToString(dtfrnd.Rows.Count) != null)
                {
                    if (Convert.ToString(dtfrnd.Rows[0]["Accepted"]) == "1")
                    {
                        lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friend";
                    }
                    else
                    {
                        lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friends";
                    }
                }
                else
                {
                    lblFriends.Text = "0 friends";
                }
            }
            else
            {
                objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                DataTable dtReq = new DataTable();
                dtReq = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);
                if (dtReq.Rows.Count > 0)
                {
                    //objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                    //objdoreg.InvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                    //    DataTable dt = new DataTable();
                    //    dt = objdareg.GetExistsRequest(objdoreg, DA_Registrationdetails.RegistrationDetails.SingleRecord);

                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 0)
                    //        {
                    //            //btnsendreq.Visible = true;
                    //            //lnkDisConnect.Visible = false;
                    //            //btnRecmnd.Visible = false;
                    //        }
                    //        else if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 1)
                    //        {
                    //            //btnsendreq.Visible = false;
                    //            //lnkDisConnect.Visible = true;
                    //            //btnRecmnd.Visible = true;
                    //            //conn_user_icon.Visible = true;
                    //        }
                    //        else if (Convert.ToInt32(dt.Rows[0]["IsAccepted"]) == 2)
                    //        {
                    //            //btnsendreq.Visible = true;
                    //            //lnkDisConnect.Visible = false;
                    //            //btnRecmnd.Visible = false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //btnsendreq.Visible = true;
                    //        //lnkDisConnect.Visible = false;
                    //        //btnRecmnd.Visible = false;
                    //    }
                    //}
                    //else
                    //{
                    //    //btnRecmnd.Visible = false;
                    //    //lnkDisConnect.Visible = false;
                    //}
                    DataTable dtfrnd = new DataTable();
                    objDoProfile.RegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                    dtfrnd = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetConnCount);
                    if (Convert.ToString(dtfrnd.Rows.Count) != "" && Convert.ToString(dtfrnd.Rows.Count) != null)
                    {
                        if (Convert.ToString(dtfrnd.Rows[0]["Accepted"]) == "1")
                        {
                            lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friend";
                        }
                        else
                        {
                            lblFriends.Text = Convert.ToString(dtfrnd.Rows[0]["Accepted"]) + " friends";
                        }
                    }
                    else
                    {
                        lblFriends.Text = "0 friends";
                    }
                }
            }
        }
    }

    protected void lstParentUsersResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");

        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
    }

    protected void lstParentGroupsResults_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        //HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        //LinkButton btnjoinreq = dataItem.FindControl("btnjoinreq") as LinkButton;
        //LinkButton btnDelete = dataItem.FindControl("btnDelete") as LinkButton;
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblGroupMember = (Label)e.Item.FindControl("lblGroupMember");
        HtmlControl Grid = (HtmlControl)e.Item.FindControl("Grid");
        DataRowView drv = (DataRowView)e.Item.DataItem;

        //if (ViewState["check"] != null)
        //{
        //    if (ViewState["hdnId"] != null)
        //    {
        //        if (ViewState["hdnId"].ToString() == hdnId.Value)
        //        {
        //            Grid.Visible = false;
        //        }
        //    }
        //}
        //ViewState["hdnId"] = hdnId.Value;

        if (string.IsNullOrEmpty(drv["GroupMembers"].ToString()))
        {
            lblGroupMember.Text = "1 member";
        }
        else
        {
            int i = Convert.ToInt32(drv["GroupMembers"].ToString());
            lblGroupMember.Text = (i + 1).ToString() + " members";
        }
        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/groupPhoto.jpg";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/" + imgprofile.Src);
            if (!File.Exists(imgPathPhysical))
            {
                imgprofile.Src = "images/groupPhoto.jpg";
            }
        }
    }

    protected void lstParentGroupsResults_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");

        if (e.CommandName == "Details")
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + hdnId.Value);
        }
    }

    protected void lnkResults_Click(Object sender, EventArgs e)
    {
        BindMoreQuestions();
    }

    protected void lnkArticleResults_Click(Object sender, EventArgs e)
    {
        string srch = Convert.ToString(Request.QueryString["srch"]);
        Response.Redirect("AllArticles.aspx?srch=" + srch, false);
    }

    protected void lnkNewsResults_Click(Object sender, EventArgs e)
    {
        BindMoreNews();
    }

    protected void lnkBlogResults_Click(Object sender, EventArgs e)
    {
        BindMoreBlogs();
    }

    protected void lnkCasesResults_Click(Object sender, EventArgs e)
    {
        BindMoreCases();
    }

    protected void lnkUsersResults_Click(Object sender, EventArgs e)
    {
        BindMoreUsers();
    }

    protected void lnkGroupsResults_Click(Object sender, EventArgs e)
    {
        BindMoreGroups();
    }

    protected void lnkResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreQuestions();
    }

    protected void lnkNewsResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreNews();
    }

    protected void lnkBlogsResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreBlogs();
    }

    protected void lnkCasesResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreCases();
    }

    protected void lnkUsersResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreUsers();
    }

    protected void lnkGroupsResultsBottom_Click(Object sender, EventArgs e)
    {
        BindMoreGroups();
    }

    #region Classes
    public class _responseJson1
    {
        public responseJson responseJson { get; set; }
    }

    public class responseJson
    {
        public string cursorId { get; set; }
        public searchResultDocumentSets[] searchResultDocumentSets;//{ get; set; }
        public string sortBy { get; set; }
        public int totalResultCount { get; set; }
        public string esTurnAroundTime { get; set; }
    }

    public class searchResultDocumentSets
    {
        public string docType { get; set; }
        public documentes[] documentes { get; set; }
        public string searchCount; // { get; set; }
        public string bookmark { get; set; }
        public Int32 commentCount { get; set; }
        public string displayContent { get; set; }
        public Int32 docUid { get; set; }
        public Int32 downloadCount { get; set; }
        public string judgeTagCount { get; set; }
        public string likes { get; set; }
        public string profTagCount { get; set; }
        public string rating { get; set; }
        public string score { get; set; }
        public Int32 shareCount { get; set; }
        public string studentTagCount { get; set; }
        public string title { get; set; }
        public string uploadBy { get; set; }
        //public string uploaderName { get; set; }
        public string uploadDt { get; set; }
        public string weightage { get; set; }
        public string uploadByName { get; set; }
        public string tagCnt { get; set; }
        public string appellant { get; set; }
        public string court { get; set; }
        public string citation { get; set; }
        public string citedBy { get; set; }
        public string year { get; set; }
        public string subject { get; set; }
        public string ownerName { get; set; }
        public string judgeNames { get; set; }
    }

    public class documentes
    {
        public string bookmark { get; set; }
        public Int32 commentCount { get; set; }
        public string displayContent { get; set; }
        public string docType { get; set; }
        public Int32 docUid { get; set; }
        public Int32 downloadCount { get; set; }
        public string judgeTagCount { get; set; }
        public string likes { get; set; }
        public string profTagCount { get; set; }
        public string rating { get; set; }
        public string score { get; set; }
        public Int32 shareCount { get; set; }
        public string studentTagCount { get; set; }
        public string title { get; set; }
        public string uploadBy { get; set; }
        //public string uploaderName { get; set; }
        public string uploadDt { get; set; }
        public string weightage { get; set; }
        public string uploadByName { get; set; }
        public string tagCnt { get; set; }
        public string appellant { get; set; }
        public string court { get; set; }
        public string citation { get; set; }
        public string citedBy { get; set; }
        public string year { get; set; }
        public string subject { get; set; }
        public string ownerName { get; set; }
        //public Int32 docUid { get; set; }
    }
    #endregion
}

