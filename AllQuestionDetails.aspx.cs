using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;

public partial class AllQuestionDetails : System.Web.UI.Page
{
    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DA_Scrl_UserQAPosting objDAQAPosting = new DA_Scrl_UserQAPosting();
    DO_Scrl_UserQAPosting objDOQAPosting = new DO_Scrl_UserQAPosting();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl myObject;
        myObject = (HtmlContainerControl)Master.FindControl("PostQuest");
        myObject.Visible = true;
        divSuccess.Style.Add("display", "none");
        if (!IsPostBack)
        {
            divSuccess.Style.Add("display", "none");
            if (Request.QueryString["DeleteQuest"] != null && Request.QueryString["DeleteQuest"] != "")
            {
                divSuccess.Style.Add("display", "block");
                lblSuccess.Text = "Successfully deleted.";
                Master.BodyTag.Attributes.Add("class", "remove-scroller");
            }
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/Landing.aspx", true);
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Q&A";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"]);
            SubjectSearchTempDataTable();

            if (!string.IsNullOrEmpty(Request.QueryString["MP"]))
            {
                ViewState["MP"] = "MP";
                lnkRecentBlogs.Attributes.Add("class", "activeq");
                //BindMostPopular();
            }
            else
            {
                lnkAllBlog.Attributes.Add("class", "activeq");
                //BindQADetails();
            }
            BindQADetails();
            BindSearchSubjectList();
            //HideShowSubject();
        }

    }

    protected void BindQADetails()
    {
        if (Convert.ToString(Request.QueryString["srch"]) != null && Convert.ToString(Request.QueryString["srch"]) != "" && (!Page.IsPostBack))
        {
            string srch = Convert.ToString(Request.QueryString["srch"]);
            txtSearchQuestion.Text = srch;
        }
        
        String ID = string.Empty;
        objDOQAPosting.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOQAPosting.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);

        List<KeyValuePair<string, string>> subJectValues = lstSerchSubjCategory.GetSelectedValues();
        string[] subJectCateValues = subJectValues.Select(s => s.Value).ToArray();

        if (txtSearchQuestion.Text != "" || subJectCateValues.Count() > 0)
        {
            objDOQAPosting.strSearch = txtSearchQuestion.Text.Trim().Replace("'", "''");
            if (subJectCateValues.Count() > 0)
            {
                ID = String.Join(",", subJectCateValues);
            }

            ViewState["SearchQA"] = txtSearchQuestion.Text.Trim().Replace("'", "''");
            ViewState["SearchQAID"] = ID;

            objDOQAPosting.ID = ID;
            if (ViewState["MP"] != null)
            {
                objDOQAPosting.MP = "MP";
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
            }
            else
            {
                objDOQAPosting.MP = "";
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
            }
        }
        else
        {
            //if (ViewState["MP"] != null)
            //{
            //    dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetMostLike);
            //}
            //else
            //{
            //    dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetQueAnsDetailss);
            //}
            if (ViewState["MP"] != null)
            {
                objDOQAPosting.MP = "MP";
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
            }
            else
            {
                objDOQAPosting.MP = "";
                dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
            }

        }

        if (dtt.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lblmsg.Visible = false;
            lstParentQADetails.DataSource = dtt;
            lstParentQADetails.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtt.Rows[0]["Maxcount"]));

        }
        else
        {
            lblmsg.Visible = true;
            lstParentQADetails.DataSource = null;
            lstParentQADetails.DataBind();
            lstParentQADetails.Visible = false;
            dvPage.Visible = false;
        }
        //txtSearchQuestion.Text = "";
        hdnSubject.Value = "";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void lstParentQADetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl divEditDeleteList = (HtmlGenericControl)e.Item.FindControl("divEditDeleteList");
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        Panel pnlAttachFile = (Panel)e.Item.FindControl("pnlAttachFile");
        Label lblAttachDocs = (Label)e.Item.FindControl("lblAttachDocs");
        ListView lstSubjCategory = (ListView)e.Item.FindControl("lstSubjCategory");
        HtmlGenericControl liSubjCategory = (HtmlGenericControl)e.Item.FindControl("liSubjCategory");
        HiddenField intAddedBy = (HiddenField)e.Item.FindControl("hdnAddedBy");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        DataTable dtChildContext = new DataTable();

        if (Convert.ToInt32(ViewState["UserID"]) == Convert.ToInt32(intAddedBy.Value))
        {
            lnkDelete.Visible = true;
            lnkEdit.Visible = true;
            divEditDeleteList.Visible = true;
        }

        if (hdnPostQuestionID.Value != "" && hdnPostQuestionID.Value != null)
        {
            DataTable dtsub = new DataTable();
            objCategory.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
            dtsub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetCatNameByPostQuestionId);
            if (dtsub.Rows.Count > 0)
            {
                lstSubjCategory.DataSource = dtsub;
                lstSubjCategory.DataBind();
            }
            else
            {
                lstSubjCategory.DataSource = null;
                lstSubjCategory.DataBind();
                liSubjCategory.Visible = false;
            }
        }
        Label btnLike = (Label)e.Item.FindControl("btnLike");
        HtmlGenericControl liLike = (HtmlGenericControl)e.Item.FindControl("liLike");
        Label lblTotallike = (Label)e.Item.FindControl("lblTotallike");
        Label lblreply = (Label)e.Item.FindControl("lblreply");
        Label lblShare = (Label)e.Item.FindControl("lblShare");
        LinkButton lnkLike = (LinkButton)e.Item.FindControl("lnkLike");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);
        objDOQAPosting.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetTotalLikeByById);
        if (dt.Rows.Count > 0)
        {
            int TotalLike = Convert.ToInt32(dt.Rows[0]["TotalLike"]);
            int TotalReply = Convert.ToInt32(dt.Rows[0]["TotalReply"]);
            int TotalShare = Convert.ToInt32(dt.Rows[0]["TotalShare"]);

           // int LikeUserId = Convert.ToInt32(dt.Rows[0]["LikeUserId"]);

            lblTotallike.Text = Convert.ToString(TotalLike) + (TotalLike==1? " Like" : " Likes");
            if (dt.Rows[0]["LikeUserId"].ToString() != "")
                liLike.Attributes["class"] = "active on-flag list-inline-item d-inline-flex align-items-center";
            lblreply.Text = Convert.ToString(TotalReply) + (TotalReply == 1 ? " Answer" : " Answers");
            lblShare.Text = Convert.ToString(TotalShare) +" Shared";

        }
        else
        {
            lblTotallike.Text = "0 Likes";
            lblreply.Text = "0 Answers";
            lblShare.Text = "0 Shared";
        }
        lblTotallike.ToolTip = "View Likes";
        DataTable dtLike = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetLikeUser);
        if (dtLike.Rows.Count > 0)
        {
            for (int i = 0; i < dtLike.Rows.Count; i++)
            {
                if (lblTotallike.ToolTip != "View Likes")
                    lblTotallike.ToolTip += Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
                else
                    lblTotallike.ToolTip = Convert.ToString(dtLike.Rows[i]["UserName"]) + Environment.NewLine;
            }
        }
    }

    protected void lstParentQADetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        LinkButton Label1 = (LinkButton)e.Item.FindControl("Label1");
        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnPostQuestionID.Value);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (e.CommandName == "QADetails")
        {
            if (ViewState["MP"] == null)
            {
                Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value);
            }
            else
            {
                Response.Redirect("Question-Details-SendContact.aspx?PostQAId=" + hdnPostQuestionID.Value + "&MP=" + Convert.ToString(ViewState["MP"]));
            }
        }
        else if (e.CommandName == "Delete QA")
        {
            ViewState["hdnPostQuestionID"] = hdnPostQuestionID.Value;
            ViewState["Questiona"] = Label1.Text;
            divDeletesucess.Style.Add("display", "block");
        }
        else if (e.CommandName == "Edit QA")
        {
            Response.Redirect("post-new-question.aspx?PostQAId=" + hdnPostQuestionID.Value);
        }
        BindQADetails();
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objDOQAPosting.intPostQuestionId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
        objDAQAPosting.AddEditDel_Scrl_UserQueAnsPostingTbl(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.DeleteQA);

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnPostQuestionID"]);
        objLog.strAction = "Q&A";
        objLog.strActionName = hdnstrQuestionDescription.Value;
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 25;   // Q&A Delete
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("removeQuestion?");
                url.Append("queId=");
                url.Append(Convert.ToInt32(hdnDeletePostQuestionID.Value));
                url.Append("&userId=");
                url.Append(Convert.ToInt32(ViewState["UserID"]));

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();

                objAPILogDO.strURL = url.ToString();
                objAPILogDO.strAPIType = "Deleting Question";
                objAPILogDO.strResponse = result;
                objAPILogDO.strIPAddress = ip;
                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
            }
            catch { }
        }

        BindQADetails();

        divDeletesucess.Style.Add("display", "none");
        hdnDeletePostQuestionID.Value = "";
    }

    protected void lstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnPostQuestionID = (HiddenField)e.Item.FindControl("hdnPostQuestionID");
        HiddenField hdnPostQueAnsnID = (HiddenField)e.Item.FindControl("hdnPostQueAnsnID");

        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");

        SubLi.Attributes.Add("class", "#A1A1A1");
    }

    protected void chkRecent_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        ViewState["SearchQA"] = null;
        ViewState["MP"] = null;
        sortFilterText.InnerText = lnkAllBlog.Text;
        BindQADetails();
    }

    protected void chkMostPopular_CheckedChanged(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        ViewState["SearchQA"] = null;
        sortFilterText.InnerText = lnkRecentBlogs.Text;
        ViewState["MP"] = "MP";
        BindQADetails();
        //ViewState["MP"] = "MP";
    }

    protected void BindMostPopular()
    {
        objDOQAPosting.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOQAPosting.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        dtt = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetMostLike);
        if (dtt.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lstParentQADetails.DataSource = dtt;
            lstParentQADetails.DataBind();
            dvPage.Visible = false;
            lblmsg.Visible = false;
        }
        else
        {
            dvPage.Visible = false;
            lstParentQADetails.Visible = false;
            lblmsg.Visible = true;
        }
    }

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
            lstSerchSubjCategory.DataTextField = "strCategoryName";
            lstSerchSubjCategory.DataValueField = "intCategoryId";
            lstSerchSubjCategory.RefreshList();
            
            //lstSerchSubjCategory.DataBind();
        }
        else
        {
            lstSerchSubjCategory.DataSource = null;
            lstSerchSubjCategory.RefreshList();
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
                string[] totalSubjects = Convert.ToString(ViewState["SubjectSearchCategory"]).Split(',');
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

                        if (hdnSubCatId.Value == Convert.ToString(ID))
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

    //protected void HideShowSubject()
    //{
    //    if (lnkViewSubj.Text == "View all")
    //    {
    //        BindTopSubjectList();
    //    }
    //    else
    //    {
    //        BindSearchSubjectList();
    //    }
    //}

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
        ViewState["DocId"] = null;
    }

    //protected void lnkViewSubj_Click(object sender, EventArgs e)
    //{
    //    ViewState["SubjectSearchCategory"] = hdnSubject.Value;
    //    viewAllsub();
    //}

    //protected void viewAllsub()
    //{
    //    divDeletesucess.Style.Add("display", "none");
    //    ViewState["Update"] = "Update";
    //    if (lnkViewSubj.Text == "View all")
    //    {
    //        lnkViewSubj.Text = "Close";
    //    }
    //    else
    //    {
    //        lnkViewSubj.Text = "View all";
    //    }
    //    HideShowSubject();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallTop();", true);
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindQADetails();
       // txtSearchQuestion.Text = "";
        divDeletesucess.Style.Add("display", "none");
    }

    protected void btnTag_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallTop();", true);
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindTags();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallTagSelections", " CallTagSelections();", true);
        divDeletesucess.Style.Add("display", "none");
    }

    public void BindTags()
    {
        String ID = string.Empty;
        objDOQAPosting.strSearch = txtSearchQuestion.Text.Trim().Replace("'", "''");
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

        ViewState["SearchQA"] = txtSearchQuestion.Text.Trim().Replace("'", "''");
        ViewState["SearchQAID"] = ID;

        objDOQAPosting.ID = ID;
       DataTable datat = objDAQAPosting.GetDataTable(objDOQAPosting, DA_Scrl_UserQAPosting.QuetionAns.GetSearchResults);
       if (datat.Rows.Count > 0)
        {
            lstParentQADetails.Visible = true;
            lblmsg.Visible = false;
            lstParentQADetails.DataSource = datat;
            lstParentQADetails.DataBind();
            dvPage.Visible = true;
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(datat.Rows[0]["Maxcount"]));

        }
        else
        {
            lblmsg.Visible = true;
            lstParentQADetails.DataSource = null;
            lstParentQADetails.DataBind();
            lstParentQADetails.Visible = false;
            dvPage.Visible = false;
        }

    }

    protected void btnAllQuestion_Click(object sender, EventArgs e)
    {
        BindQADetails();
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
                  StartPage ++;
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
        divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindQADetails();
            }
            else
            {
                BindQADetails();
            }
        }
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = "1";
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindQADetails();
        }
        else
        {
            BindQADetails();
        }
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        hdnCurrentPage.Value = hdnLastPage.Value;
        if (Convert.ToString(ViewState["ViewAll"]) == "1")
        {
            BindQADetails();
        }
        else
        {
            BindQADetails();
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            if (Convert.ToString(ViewState["ViewAll"]) == "1")
            {
                BindQADetails();
            }
            else
            {
                BindQADetails();
            }
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
                if (Convert.ToString(ViewState["ViewAll"]) == "1")
                {
                    BindQADetails();
                }
                else
                {
                    BindQADetails();
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

    #endregion


}