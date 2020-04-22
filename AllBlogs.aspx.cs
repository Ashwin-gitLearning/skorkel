using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;

public partial class AllBlogs : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();
    DataTable dtCount = new DataTable();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl myObject;
        myObject = (HtmlContainerControl)Master.FindControl("blogWrite");
        myObject.Visible = true;
        divSuccess.Style.Add("display", "none");
        if (!IsPostBack)
        {
            divSuccess.Style.Add("display", "none");
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/Landing.aspx", true);
            }
            
            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"]);

            if (Request.QueryString["DeleteBlog"] != null && Request.QueryString["DeleteBlog"] != "")
            {
                divSuccess.Style.Add("display", "block");
                lblSuccess.Text = "Successfully deleted.";
                Master.BodyTag.Attributes.Add("class", "remove-scroller");
            }

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Blog";
            divDeletesucess.Style.Add("display", "none");
            BindSearchSubjectList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
            ViewState["RecentBlogs"] = "R";
            if (!string.IsNullOrEmpty(Convert.ToString(Session["intBlogId"])))
            {
                BindSearchResult();
            }
            else
            if (Request.QueryString["Blogtype"] == "R" || Request.QueryString["Blogtype"] == null)
            {
                ViewState["RecentBlogs"] = "R";
                BindBlog(); //BindRecentBlog();
            }
            else 
            if (Request.QueryString["Blogtype"] == "A")
            {
                ViewState["RecentBlogs"] = "A";
                BindBlog();
            }          
        }
    }

    private void BindBlog()
    {
        if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "" && (!Page.IsPostBack))
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
            txtblogsearch.Text = srch;
        }

        DataTable dtSub = new DataTable();
        String ID = string.Empty;
        List<KeyValuePair<string, string>> subJectValues = lstSerchSubjCategory.GetSelectedValues();
        string[] subJectCateValues = subJectValues.Select(s => s.Value).ToArray();

        if ((txtblogsearch.Text != "" && txtblogsearch.Text != "Search") || subJectCateValues.Count() > 0)
        {
            objblogdo.strSearch = txtblogsearch.Text.Trim().Replace("'", "''");           
            //string[] totalSubjects = hdnSubject.Value.Split(',');
            var dictionary = new Dictionary<int, int>();

            if (subJectCateValues.Count() > 0)
            {
                ID = String.Join(",", subJectCateValues);
                //Dictionary<string, int> counts = subJectCateValues.GroupBy(x => x)
                //                              .ToDictionary(g => g.Key,
                //                                            g => g.Count());

                //foreach (var v in counts)
                //{
                //    if (v.Value == 1)
                //    {
                //        if (string.IsNullOrEmpty(ID))
                //            ID = Convert.ToString(v.Key);
                //        else
                //            ID += "," + Convert.ToString(v.Key);
                //    }
                //    else if (v.Value == 3)
                //    {
                //        if (string.IsNullOrEmpty(ID))
                //            ID = Convert.ToString(v.Key);
                //        else
                //            ID += "," + Convert.ToString(v.Key);
                //    }
                //    else if (v.Value == 5)
                //    {
                //        if (string.IsNullOrEmpty(ID))
                //            ID = Convert.ToString(v.Key);
                //        else
                //            ID += "," + Convert.ToString(v.Key);
                //    }
                //    else if (v.Value == 7)
                //    {
                //        if (string.IsNullOrEmpty(ID))
                //            ID = Convert.ToString(v.Key);
                //        else
                //            ID += "," + Convert.ToString(v.Key);
                //    }
                //    else if (v.Value == 9)
                //    {
                //        if (string.IsNullOrEmpty(ID))
                //            ID = Convert.ToString(v.Key);
                //        else
                //            ID += "," + Convert.ToString(v.Key);
                //    }
                //}
            }

            ViewState["SearchBlogs"] = txtblogsearch.Text.Trim().Replace("'", "''");
            ViewState["SearchBlogsID"] = ID;
            if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
            {
                objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            }

            objblogdo.ID = ID;
            objblogdo.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            objblogdo.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetSearchResult);

        }
        else
        {
            //if (ViewState["SearchBlogs"] != null)
            //{
            //    objblogdo.strSearch =Convert.ToString(ViewState["SearchBlogs"]);
            //    objblogdo.ID = Convert.ToString(ViewState["SearchBlogsID"]);
            //    objblogdo.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
            //    objblogdo.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
            //    dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetSearchResult);
            //}
            //else
            //{
                objblogdo.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
                objblogdo.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
                objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
                {
                    dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogss);
                } else
                {
                    dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsDate);
                }               
            //}
        }

        if (dtSub.Rows.Count > 0)
        {
            lstAllBlogs.Visible = true;
            lblmsg.Visible = false;
            lstAllBlogs.DataSource = dtSub;
            lstAllBlogs.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtSub.Rows[0]["Maxcount"]));
        }
        else
        {
            lstAllBlogs.DataSource = null;
            lstAllBlogs.DataBind();
            dvPage.Visible = false;
            lblmsg.Visible = true;
            lstAllBlogs.Visible = false;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    //protected void BindRecentBlog()
    //{
    //    ViewState["RecentBlogs"] = "R";
    //    lnkAllBlog.Style.Add("border-bottom", "0");
    //    lnkRecentBlogs.Style.Add("border-bottom", "2px solid #00b6bc");
    //    DataTable dtSub = new DataTable();
    //    objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //    objblogdo.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
    //    objblogdo.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
    //    dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsDate);
    //    if (dtSub.Rows.Count > 0)
    //    {
    //        lstAllBlogs.Visible = true;
    //        lblmsg.Visible = false;
    //        lstAllBlogs.DataSource = dtSub;
    //        lstAllBlogs.DataBind();
    //        dvPage.Visible = true;
    //        BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtSub.Rows[0]["Maxcount"]));
    //    }
    //    else
    //    {
    //        lstAllBlogs.DataSource = null;
    //        lstAllBlogs.DataBind();
    //        dvPage.Visible = false;
    //        lblmsg.Visible = true;
    //        lstAllBlogs.Visible = false;
    //    }
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
    //}
    
    private void BindSearchResult()
    {
        DataTable dtSrch = new DataTable();
        objblogdo.ID = Convert.ToString(Session["intBlogId"]);
        dtSrch = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetBlogsById);
        if (dtSrch.Rows.Count > 0)
        {
            lstAllBlogs.Visible = true;
            lstAllBlogs.DataSource = dtSrch;
            lstAllBlogs.DataBind();
            dvPage.Visible = false;
        }
        else
        {
            lstAllBlogs.DataSource = null;
            lstAllBlogs.DataBind();
            dvPage.Visible = false;
            lstAllBlogs.Visible = false;
        }
        Session["intBlogId"] = "";
    }

    protected void lstAllBlogs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintBlogId = (HiddenField)e.Item.FindControl("hdnintBlogId");
        HiddenField hdnAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");
        LinkButton lnkBlogHeading = (LinkButton)e.Item.FindControl("lnkBlogHeading");
        if (e.CommandName == "BlogsDetails")
        {
            if (Convert.ToString(ViewState["RecentBlogs"]) == "R")
            {
                Response.Redirect("BlogsComments.aspx?intBlogId=" + hdnintBlogId.Value + "&Blogtype=" + Convert.ToString(ViewState["RecentBlogs"]) + "&hdnAddedBy=" + Convert.ToString(hdnAddedBy.Value));
            }
            else
            {
                Response.Redirect("BlogsComments.aspx?intBlogId=" + hdnintBlogId.Value + "&Blogtype=" + Convert.ToString(ViewState["RecentBlogs"]) + "&hdnAddedBy=" + Convert.ToString(hdnAddedBy.Value));
            }
        }
        else if (e.CommandName == "Delete Blog")
        {
            ViewState["hdnintBlogId"] = hdnintBlogId.Value;
            ViewState["lnkBlogHeading"] = lnkBlogHeading.Text;
            divDeletesucess.Style.Add("display", "block");
        }
        else if (e.CommandName == "Edit Blog")
        {
            Response.Redirect("write-blog.aspx?BlogId=" + hdnintBlogId.Value);
        }
    }

    protected void lstAllBlogs_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl dvPic = (HtmlGenericControl)e.Item.FindControl("dvPic");
        Label lblDate = (Label)e.Item.FindControl("lblDate");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        HiddenField intAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton btnBlogLike = e.Item.FindControl("btnBlogLike") as LinkButton;
        HiddenField hdnBlogLikeUserId = e.Item.FindControl("hdnBlogLikeUserId") as HiddenField;

        //Session["SubmitTime"] = DateTime.Now.ToString();
        //if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        //{
        //    ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
        //}
        //else
        //{
        //    Response.Redirect("~/Landing.aspx", true);
        //}

        if (lblDate.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblDate.Text = "a few moments ago";
        }
        else 
        if (lblDate.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblDate.Text = "Yesterday";
        }

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(intAddedBy.Value))
        {
            lnkDelete.Visible = true;
            lnkEdit.Visible = true;
            divEditDeleteList.Visible = true;
        }

        if (hdnBlogLikeUserId.Value == ViewState["UserID"].ToString())
        {
            btnBlogLike.CssClass = "active-toogle on-flag";
        }
    }

    protected void lnkSubmitBlog_Click(object sender, EventArgs e)
    {
        Response.Redirect("write-blog.aspx");
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objblogdo.intBlogId = Convert.ToInt32(ViewState["hdnintBlogId"]);
        objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.DeleteBlog);

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnintBlogId"]);
        objLog.strAction = "Blog";
        objLog.strActionName =Convert.ToString(ViewState["lnkBlogHeading"]);
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
                url.Append(Convert.ToInt32(ViewState["hdnintBlogId"]));
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

        if (Convert.ToString(ViewState["RecentBlogs"]) == "R")
        {
            BindBlog(); //RecentBlogs();
        }
        else
        {
            BindBlog();
        }
        divDeletesucess.Style.Add("display", "none");
    }
    
    #region Search Context
    //protected void lstSerchSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
    //    LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
    //    HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
    //    DataTable dtsub = new DataTable();
    //    string ID = "";
    //    SubLi.Attributes.Add("class", "unselectBlogLi");
    //    if (ViewState["SubjectSearchCategory"] != null)
    //    {
    //        if (Convert.ToString(ViewState["SubjectSearchCategory"]) != "")
    //        {
    //            string[] totalSubjects = Convert.ToString(ViewState["SubjectSearchCategory"]).Split(',');
    //            var dictionary = new Dictionary<int, int>();

    //            if (totalSubjects.Count() > 0)
    //            {
    //                Dictionary<string, int> counts = totalSubjects.GroupBy(x => x)
    //                                              .ToDictionary(g => g.Key,
    //                                                            g => g.Count());

    //                foreach (var v in counts)
    //                {
    //                    if (v.Value == 1)
    //                    {
    //                        if (string.IsNullOrEmpty(ID))
    //                            ID = Convert.ToString(v.Key);
    //                        else
    //                            ID += "," + Convert.ToString(v.Key);
    //                    }
    //                    else if (v.Value == 3)
    //                    {
    //                        if (string.IsNullOrEmpty(ID))
    //                            ID = Convert.ToString(v.Key);
    //                        else
    //                            ID += "," + Convert.ToString(v.Key);
    //                    }
    //                    else if (v.Value == 5)
    //                    {
    //                        if (string.IsNullOrEmpty(ID))
    //                            ID = Convert.ToString(v.Key);
    //                        else
    //                            ID += "," + Convert.ToString(v.Key);
    //                    }
    //                    else if (v.Value == 7)
    //                    {
    //                        if (string.IsNullOrEmpty(ID))
    //                            ID = Convert.ToString(v.Key);
    //                        else
    //                            ID += "," + Convert.ToString(v.Key);
    //                    }
    //                    else if (v.Value == 9)
    //                    {
    //                        if (string.IsNullOrEmpty(ID))
    //                            ID = Convert.ToString(v.Key);
    //                        else
    //                            ID += "," + Convert.ToString(v.Key);
    //                    }

    //                    if (hdnSubCatId.Value == Convert.ToString(ID))
    //                    {
    //                        SubLi.Attributes.Add("class", "selectBlogLi");
    //                        lnkCatName.Attributes.Add("class", "selectBlogcat");
    //                    }
    //                    ID = "";
    //                }
    //            }
    //        }
    //    }
    //}

    private void BindSearchSubjectList()
    {
        DataTable dtSub = new DataTable();
        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSerchSubjCategory.DataSource = dtSub;
            lstSerchSubjCategory.DataTextField = "strCategoryName";
            lstSerchSubjCategory.DataValueField = "intCategoryId";
            
            //lstSerchSubjCategory.DataBind();
            lstSerchSubjCategory.RefreshList();
            ViewState["dtSubjectCategory"] = dtSub;

            //lstSubjCategory.DataTextField = "strCategoryName";
            //lstSubjCategory.DataValueField = "intCategoryId";
            //lstSubjCategory.DataSource = dtSub;
            //lstSubjCategory.RefreshList();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.RefreshList();
            
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindBlog();
        //lstSerchSubjCategory.DataSource = (DataTable)ViewState["dtSubjectCategory"];
        //lstSerchSubjCategory.DataTextField = "strCategoryName";
        //lstSerchSubjCategory.DataValueField = "intCategoryId";
        //lstSerchSubjCategory.RefreshList();
        
        //txtblogsearch.Text = "";
        hdnSubject.Value = "";
    }

    protected void lnkAllBlog_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        ViewState["SearchBlogs"] = null;
        ViewState["SearchBlogsID"] = null;
        ViewState["RecentBlogs"] = "A";
        //lnkAllBlog.Style.Add("border-bottom", "2px solid #00b6bc");
        //lnkRecentBlogs.Style.Add("border-bottom", "0");
        sortFilterText.InnerText = lnkAllBlog.Text;
        BindBlog();
        //txtblogsearch.Text = "";
        hdnSubject.Value = "";

    }

    protected void lnkRecentBlogs_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        ViewState["SearchBlogs"] = null;
        ViewState["SearchBlogsID"] = null;
        ViewState["RecentBlogs"] = "R";
        //lnkAllBlog.Style.Add("border-bottom", "0");
        //lnkRecentBlogs.Style.Add("border-bottom", "2px solid #00b6bc");
        sortFilterText.InnerText = lnkRecentBlogs.Text;
        BindBlog(); //RecentBlogs();
        //txtblogsearch.Text = "";
        hdnSubject.Value = "";
    }

    //protected void RecentBlogs()
    //{
    //    hdnCurrentPage.Value = "1";
    //    DataTable dtSub = new DataTable();
    //    objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //    objblogdo.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
    //    objblogdo.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
    //    dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsDate);
    //    if (dtSub.Rows.Count > 0)
    //    {
    //        lblmsg.Visible = false;
    //        lstAllBlogs.Visible = true;
    //        dvPage.Visible = true;
    //        lstAllBlogs.DataSource = dtSub;
    //        lstAllBlogs.DataBind();
    //        BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtSub.Rows[0]["Maxcount"]));
    //    }
    //    else
    //    {
    //        lstAllBlogs.DataSource = null;
    //        lstAllBlogs.DataBind();
    //        lstAllBlogs.Visible = false;
    //        dvPage.Visible = false;
    //        lblmsg.Visible = true;
    //    }

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
    //}

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

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            BindBlog();
            //if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
            //{
            //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //    {
            //        BindBlog();
            //    }
            //    else
            //    {
            //        BindBlog();
            //    }
            //}
            //else
            //{
            //    if (ViewState["SearchBlogs"] != null)
            //    {
            //        BindBlog();
            //    }
            //    else
            //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //    {
                    
            //        BindRecentBlog();
            //    }
            //    else
            //    {
            //        BindRecentBlog();
            //    }
            //}
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        BindBlog();
        //if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
        //{
        //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
        //    {
        //        BindBlog();
        //    }
        //    else
        //    {
        //        BindBlog();
        //    }
        //}
        //else
        //{
        //    if (ViewState["SearchBlogs"] != null)
        //    {
        //        BindBlog();
        //    }
        //    else
        //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
        //    {
        //        BindRecentBlog();
        //    }
        //    else
        //    {
        //        BindRecentBlog();
        //    }
        //}
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = hdnLastPage.Value;
        BindBlog();
        //if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
        //{
        //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
        //    {
        //        BindBlog();
        //    }
        //    else
        //    {
        //        BindBlog();
        //    }
        //}
        //else
        //{
        //    if (ViewState["SearchBlogs"] != null)
        //    {
        //        BindBlog();
        //    }
        //    else
        //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
        //    {
        //        BindRecentBlog();
        //    }
        //    else
        //    {
        //        BindRecentBlog();
        //    }
        //}
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            BindBlog();
            //if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
            //{
            //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //    {
            //        BindBlog();
            //    }
            //    else
            //    {
            //        BindBlog();
            //    }
            //}
            //else
            //{
            //    if (ViewState["SearchBlogs"] != null)
            //    {
            //        BindBlog();
            //    }
            //    else
            //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //    {
            //        BindRecentBlog();
            //    }
            //    else
            //    {
            //        BindRecentBlog();
            //    }
            //}
        }
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
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
                BindBlog();

                //if (Convert.ToString(ViewState["RecentBlogs"]) == "A")
                //{
                //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
                //    {
                //        BindBlog();
                //    }
                //    else
                //    {
                //        BindBlog();
                //    }
                //}
                //else if (Convert.ToString(ViewState["RecentBlogs"]) == "R")
                //{
                //    if (ViewState["SearchBlogs"] != null)
                //    {
                //        BindBlog();
                //    }else
                //    if (Convert.ToString(ViewState["ViewAll"]) == "1")
                //    {
                //        BindRecentBlog();
                //    }
                //    else
                //    {
                //        BindRecentBlog();
                //    }

                //}

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

    #endregion

}