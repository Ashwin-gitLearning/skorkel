using System;
using System.Data;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class write_blog : System.Web.UI.Page
{
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DataTable dt = new DataTable();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    static string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Blog";
            SubjectTempDataTable();
            SubjectSearchTempDataTable();
            RemoveTempSub();
            BindSubjectList();

            if (!string.IsNullOrEmpty(Request.QueryString["BlogId"]))
            {
                ViewState["intBlogId"] = Request.QueryString["BlogId"];
                LoadEditBlogs();
                BindEditSubjectList();
                BindSubjectLists();
            }
            txtTitle.Focus();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitBlogTime"];
    }

    protected void RemoveTempSub()
    {
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.RemoveTempSubCat);
    }

    protected void LoadEditBlogs()
    {
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsDetails);
        if (dtSub.Rows.Count > 0)
        {
            txtTitle.Text = Convert.ToString(dtSub.Rows[0]["strBlogHeading"]);
            CKDescription.InnerText = Convert.ToString(dtSub.Rows[0]["strBlogTitle"]);
        }
    }

    private void BindEditSubjectList()
    {
        DataTable dtSub = new DataTable();
        DataSet ds = new DataSet();
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetRelatedSubject);
        ViewState["SaveSubCatname"] = dtSub;
    }

    private void BindSubjectLists()
    {
        string[] body;
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetRelatedSubjects);
        List<string> catValues = new List<string>();
        if (dtSub.Rows.Count > 0)
        {
            if (dtSub.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSub.Rows)
                {
                    catValues.Add(dr["intCategoryId"].ToString() );
                }
                txtSubjectList.SetValues(catValues.ToArray());
                //for (int i = 0; i < body.Length; i++)
                //{
                //    string selectedvalue = body[i];
                //    if (selectedvalue != "")
                //    {
                //        foreach (ListItem item in txtSubjectList.Items)
                //        {
                //            if (item.Value == selectedvalue)
                //            {
                //                item.Selected = true;
                //            }
                //        }
                //    }
                //}

            }
            else
            {

            }
       }
    }

    #region Main Context

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

    private void BindSubjectList()
    {
        DataTable dtSub = new DataTable();

        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            txtSubjectList.DataSource = dtSub;
            txtSubjectList.DataValueField = "intCategoryId";
            txtSubjectList.DataTextField = "strCategoryName";
            txtSubjectList.DataBind();
        }
        
    }

    protected void LstSubjCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Subject Category")
        {
            HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
            LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
            HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");

            string Subjectid = "0", SubjectCat = string.Empty;
            DataTable dtSubjCat = new DataTable();
            DataTable dtsub = new DataTable();

            dtSubjCat = (DataTable)ViewState["SubjectCategory"];
            DataRow rwSubj = dtSubjCat.NewRow();

            if (ViewState["SubjectCategory"] != null)
            {
                DataTable dtContent = (DataTable)ViewState["SubjectCategory"];
                if (dtContent.Rows.Count > 0)
                {
                    if (lnkCatName.Text.Contains("'"))
                        lnkCatName.Text = lnkCatName.Text.Replace("'", "''");
                    for (int i = 0; i < dtContent.Rows.Count; i++)
                    {
                        string AutoSugestName = Convert.ToString(dtContent.Rows[i]["strCategoryName"]);
                        bool contains = lnkCatName.Text.IndexOf(AutoSugestName, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (contains == true)
                        {
                            dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                            //to check whethere the column is newly added 
                            if (Convert.ToInt32(ViewState["MId"]) > 0)
                                dtSubjCat.Rows.Remove(dtContent.Rows[i]);
                            ViewState["SubjectCategory"] = dtSubjCat;
                            SubLi.Style.Add("background", "#ebeaea");
                            SubLi.Style.Add("border", "1px solid #c8c8c8");
                            SubLi.Style.Add("color", "#646161");
                            lnkCatName.Style.Add("color", "#646161");
                            return;
                        }
                    }
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["MId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["MId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                }
                else
                {
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["MId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["MId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectCategory"];
                        ViewState["SubjectCategory"] = dtSubjCat;
                    }
                }
            }

            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                SubLi.Style.Add("background", "none repeat scroll 0 0 #656767 !important");
                SubLi.Style.Add("color", "#FFFFFF !important");
                SubLi.Style.Add("text-decoration", "none !important");
                lnkCatName.ForeColor = System.Drawing.Color.White;
                lnkCatName.Style.Add("color", "#FFFFFF !important");
            }
            else
            {
                SubLi.Style.Add("background", "#ebeaea");
                SubLi.Style.Add("border", "1px solid #c8c8c8");
                SubLi.Style.Add("color", "#646161");
                lnkCatName.Style.Add("color", "#646161");
            }
        }
    }

    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");
        DataTable dtsub = new DataTable();

        if (hdnCountSub.Value == "1")
        {
            SubLi.Style.Add("background", "none repeat scroll 0 0 #656767 !important");
            SubLi.Style.Add("color", "#FFFFFF !important");
            SubLi.Style.Add("text-decoration", "none !important");
            lnkCatName.ForeColor = System.Drawing.Color.White;
            lnkCatName.Style.Add("color", "#FFFFFF !important");
        }
        else
        {
            SubLi.Style.Add("background", "#ebeaea");
            SubLi.Style.Add("border", "1px solid #c8c8c8");
            SubLi.Style.Add("color", "#646161");
        }

        dtsub = (DataTable)ViewState["SaveSubCatname"];
        if (dtsub != null)
        {
            ViewState["SubjectCategory"] = dtsub;
        }
    }
    #endregion

    protected void lnkSubmitBlog_Click(object sender, EventArgs e)
    {
            if (!string.IsNullOrEmpty(CKDescription.InnerText.Trim()))
            {
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];

                if (ViewState["intBlogId"] == null)
                {
                    objblogdo.strIPAddress = ip;
                    objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                    //Bug 9 HTML Validations
                    objblogdo.strBlogHeading = Validations.validateHtmlInput(Convert.ToString(txtTitle.Text.Replace("'", "''").Trim()));
                    string editor = CKDescription.InnerText.Trim().Replace(",", "''").Replace("<p>", "<br>").Replace("</p>", "");
                    string noHTML = Regex.Replace(editor, @"<[^>]+>|&nbsp;", "").Trim();
                    objblogdo.strBlogTitle = Regex.Replace(noHTML, @"\s{2,}", " ");
                    string PhotoPath = "";
                    if (UploadPics.HasFile)
                    {
                        string name = UploadPics.FileName;
                        string ext = System.IO.Path.GetExtension(name);

                        if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }
                        int FileLength = UploadPics.PostedFile.ContentLength;
                        if (FileLength <= 3145728)
                        {
                            PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(UploadPics.FileName).ToString();
                            UploadPics.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
                            objblogdo.strBlogPicture = PhotoPath;
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "image size should be less than or equal to 3MB.'";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }
                    }
                    objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.Add);

                    if (!string.IsNullOrEmpty(Convert.ToString(objblogdo.intOutId)) || objblogdo.intOutId != 0)
                    {
                        ViewState["intOutId"] = objblogdo.intOutId;
                    // string totalMembers = hdnsubject.Value;
                    //  string[] members = null;
                    List<KeyValuePair<string, string>> subJectValues = txtSubjectList.GetSelectedValues();

                    if (subJectValues != null && subJectValues.Count > 0)
                    {
                        string[] members = subJectValues.Select(s => (string)s.Value).ToArray();
                        for (int i = 0; i < members.Length; i++)
                        {
                            if (Convert.ToString(members.GetValue(i)) != "")
                            {
                                int ID = Convert.ToInt32(members.GetValue(i));
                                objblogdo.intBlogId = Convert.ToInt32(ViewState["intOutId"]);
                                objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objblogdo.intSubjectCategoryId = ID;
                                objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.ADDSelectedSubCatId);
                            }
                        }
                    }
                    clear();
                    lblSuccess.Text = "Blog Added Successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg1", "CallOnRequestSucess();", true);
                    divSuccess.Style.Add("display", "block");
                    }
                    //              if (ISAPIURLACCESSED != "0")
                    //              {
                    //                  try
                    //                  {

                    //StringBuilder url = new StringBuilder();
                    //url.Append(APIURL);
                    //url.Append("addBlog.action?");
                    //url.Append("blogId =");
                    //url.Append(objblogdo.intBlogId);
                    //url.Append("&userId=");
                    //url.Append(objblogdo.intAddedBy);
                    //url.Append("&userName=" + null);
                    //url.Append("&insertDt=");
                    //url.Append(DateTime.Now);
                    //url.Append("&content=");
                    //url.Append(objblogdo.strBlogTitle);
                    //url.Append("&title=");
                    //url.Append(objblogdo.strBlogHeading);


                    //                      HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    //                      myRequest1.Method = "GET";
                    //                      WebResponse myResponse1 = myRequest1.GetResponse();
                    //                      if (ISAPIResponse != "0")
                    //                      {
                    //                          StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    //                          String result = sr.ReadToEnd();
                    //                          objAPILogDO.strURL = url.ToString();
                    //                          objAPILogDO.strAPIType = "Write Blog";
                    //                          objAPILogDO.strResponse = result;
                    //                          objAPILogDO.strIPAddress = ip;
                    //                          objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    //                          objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    //                      }
                    //                  }
                    //                  catch { }
                    //              }
                    if (ISAPIURLACCESSED != "0")
                    {
                        try
                        {
                            StringBuilder url = new StringBuilder();
                            url.Append(APIURL);
                            url.Append("addBlog?");
                            url.Append("blogId=");
                            url.Append(Convert.ToInt32(ViewState["intOutId"]));
                            url.Append("&userId=");
                            url.Append(objblogdo.intAddedBy);
                            url.Append("&insertDt=");
                            url.Append(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                            url.Append("&title=");
                            url.Append(objblogdo.strBlogHeading);
                            url.Append("&content=");
                            url.Append(objblogdo.strBlogTitle);
                            url.Append("&scope=");
                            url.Append("ALL");
                            url.Append("&userName=" + null);

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                            myRequest1.Method = "GET";
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            //if (ISAPIResponse != "0")
                            //{
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();
                                objAPILogDO.strURL = url.ToString();
                                objAPILogDO.strAPIType = "Write Blog";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            //}
                        }
                        catch { }
                    }
                }
                else
                {
                    objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                    objblogdo.strIPAddress = ip;
                    objblogdo.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
                    objblogdo.strBlogHeading = Convert.ToString(txtTitle.Text.Replace("'", "''").Trim());
                    string editor = CKDescription.InnerText.Trim().Replace(",", "''").Replace("<p>", "<br>").Replace("</p>", "");
                    string noHTML = Regex.Replace(editor, @"<[^>]+>|&nbsp;", "").Trim();
                    objblogdo.strBlogTitle = Regex.Replace(noHTML, @"\s{2,}", " ");
                    string PhotoPath = "";

                    if (UploadPics.HasFile)
                    {
                        string name = UploadPics.FileName;
                        string ext = System.IO.Path.GetExtension(name);
                        if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }

                        int FileLength = UploadPics.PostedFile.ContentLength;
                        if (FileLength <= 3145728)
                        {
                            PhotoPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(UploadPics.FileName).ToString();
                            UploadPics.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + PhotoPath));
                            objblogdo.strBlogPicture = PhotoPath;
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "image size should be less than or equal to 3MB.'";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }
                    }

                    objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.Update);

                    if (!string.IsNullOrEmpty(Convert.ToString(objblogdo.intOutId)) || objblogdo.intOutId != 0)
                    {
                        objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.DeleteSelectedSubCatId);
                        //  string totalMembers = hdnsubject.Value;
                        // string[] members = totalMembers.Split(',');
                    List<KeyValuePair<string, string>> subJectValues = txtSubjectList.GetSelectedValues();

                    if (subJectValues != null && subJectValues.Count > 0)
                    {
                        string[] members = subJectValues.Select(s => (string)s.Value).ToArray();
                        for (int i = 0; i < members.Length; i++)
                        {
                            if (Convert.ToString(members.GetValue(i)) != "")
                            {
                                int ID = Convert.ToInt32(members.GetValue(i));
                                objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
                                objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objblogdo.intSubjectCategoryId = ID;
                                objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.ADDSelectedSubCatId);
                            }
                        }
                    }
                    clear();
                    lblSuccess.Text = "Blog Update successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg1", "CallOnRequestSucess();", true);
                    divSuccess.Style.Add("display", "block");
                    }

                    if (ISAPIURLACCESSED != "0")
                    {
                        try
                        {
                            StringBuilder url = new StringBuilder();
						    url.Append(APIURL);
                            url.Append("updateBlog?");
                            url.Append("blogId =");
                            url.Append(objblogdo.intBlogId);
                            url.Append("&userId =");
						    url.Append(objblogdo.intAddedBy);
						    url.Append("&insertDt=");
                            url.Append(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                            url.Append("&title=");
                            url.Append(objblogdo.strBlogHeading);
                            url.Append("&content=");
                            url.Append(objblogdo.strBlogTitle);
                            url.Append("&scope=");
                            url.Append("ALL");
                            url.Append("&userName=" + null);

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                            myRequest1.Method = "GET";
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            //if (ISAPIResponse != "0")
                            //{
                                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                                String result = sr.ReadToEnd();

                                objAPILogDO.strURL = url.ToString();
                                objAPILogDO.strAPIType = "Update Blog";
                                objAPILogDO.strResponse = result;
                                objAPILogDO.strIPAddress = ip;
                                objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                            //}
                        }
                        catch { }
                    }
                }
            }
            else
            {
                lblSuMess.Text = "Please enter blog.";
                lblSuMess.ForeColor = System.Drawing.Color.Red;
                divSuccess.Style.Add("display", "none");
            }
    }

    protected void clear()
    {
        txtSubjectList.RefreshList();
        hdnsubject.Value = "";
        SubjectTempDataTable();
        txtTitle.Text = "";
        CKDescription.InnerText = string.Empty;
        lblSuMess.Text = "";
        BindSubjectList();
        Session["SubmitTime"] = DateTime.Now.ToString();
        ViewState["intBlogId"] = null;
    }

    #region Search Context

    protected void SubjectSearchTempDataTable()
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
        ViewState["SubjectSearchCategory"] = dtSubjCat;
    }

    protected void lstSerchSubjCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Subject Category")
        {
            HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
            LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
            HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");

            string Subjectid = "0", SubjectCat = string.Empty;
            DataTable dtSubjCat = new DataTable();
            DataTable dtsub = new DataTable();

            dtSubjCat = (DataTable)ViewState["SubjectSearchCategory"];
            DataRow rwSubj = dtSubjCat.NewRow();

            if (ViewState["SubjectSearchCategory"] != null)
            {
                DataTable dtContent = (DataTable)ViewState["SubjectSearchCategory"];
                if (dtContent.Rows.Count > 0)
                {
                    if (lnkCatName.Text.Contains("'"))
                        lnkCatName.Text = lnkCatName.Text.Replace("'", "''");
                    for (int i = 0; i < dtContent.Rows.Count; i++)
                    {
                        string AutoSugestName = Convert.ToString(dtContent.Rows[i]["strCategoryName"]);
                        bool contains = lnkCatName.Text.IndexOf(AutoSugestName, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (contains == true)
                        {
                            dtSubjCat = (DataTable)ViewState["SubjectSearchCategory"];
                            //to check whethere the column is newly added 
                            if (Convert.ToInt32(ViewState["SearchMId"]) > 0)
                                dtSubjCat.Rows.Remove(dtContent.Rows[i]);
                            ViewState["SubjectSearchCategory"] = dtSubjCat;
                            SubLi.Style.Add("background", "#ebeaea");
                            SubLi.Style.Add("border", "1px solid #c8c8c8");
                            SubLi.Style.Add("color", "#646161");
                            lnkCatName.Style.Add("color", "#646161");
                            return;
                        }
                    }
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["SearchMId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectSearchCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["SearchMId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectSearchCategory"];
                        ViewState["SubjectSearchCategory"] = dtSubjCat;
                    }
                }
                else
                {
                    if (dtSubjCat.Rows.Count <= 0)
                    {
                        ViewState["SearchMId"] = hdnSubCatId.Value;
                        rwSubj["intCategoryId"] = hdnSubCatId.Value;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();
                        dtSubjCat.Rows.Add(rwSubj);
                        ViewState["SubjectSearchCategory"] = dtSubjCat;
                    }
                    else
                    {
                        Subjectid = hdnSubCatId.Value.ToString();
                        ViewState["SearchMId"] = Subjectid;
                        rwSubj["intCategoryId"] = Subjectid;
                        rwSubj["strCategoryName"] = lnkCatName.Text.Trim();

                        for (int i = 0; i < dtSubjCat.Rows.Count; i++)
                        {
                            if (SubjectCat == dtSubjCat.Rows[i]["strCategoryName"].ToString())
                                return;
                        }
                        dtSubjCat.Rows.Add(rwSubj);
                        dtSubjCat = (DataTable)ViewState["SubjectSearchCategory"];
                        ViewState["SubjectSearchCategory"] = dtSubjCat;
                    }
                }
            }

            dtsub = (DataTable)ViewState["SubjectSearchCategory"];
            if (dtsub.Rows.Count > 0)
            {
                SubLi.Style.Add("background", "none repeat scroll 0 0 #656767 !important");
                SubLi.Style.Add("color", "#FFFFFF !important");
                SubLi.Style.Add("text-decoration", "none !important");
                lnkCatName.ForeColor = System.Drawing.Color.White;
                lnkCatName.Style.Add("color", "#FFFFFF !important");
            }
            else
            {
                SubLi.Style.Add("background", "#ebeaea");
                SubLi.Style.Add("border", "1px solid #c8c8c8");
                SubLi.Style.Add("color", "#646161");
                lnkCatName.Style.Add("color", "#646161");
            }
        }
    }

    protected void lstSerchSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();

        if (dtsub.Rows.Count > 0)
        {
            lnkCatName.ForeColor = System.Drawing.Color.White;
        }
        else
        {
            SubLi.Style.Add("background", "#ebeaea");
            SubLi.Style.Add("border", "1px solid #c8c8c8");
            SubLi.Style.Add("color", "#646161");
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objblogda.AddEditDel_NewBlog(objblogdo, DA_NewBlogs.Blog.DeleteContext);
        BindBlog();
        SubjectSearchTempDataTable();
    }

    private void BindBlog()
    {
        DataTable dtSub = new DataTable();
        String BlogId = "", ID = string.Empty;

        if (!string.IsNullOrEmpty(Convert.ToString(ViewState["SubjectSearchCategory"])))
        {
            DataTable SubCat = new DataTable();
            String SubCatId = string.Empty;
            SubCat = (DataTable)ViewState["SubjectSearchCategory"];
            foreach (DataRow dr in SubCat.Rows)
            {
                if (string.IsNullOrEmpty(ID))
                    ID = Convert.ToString(dr["intCategoryId"].ToString());
                else
                    ID += "," + Convert.ToString(dr["intCategoryId"].ToString());
            }
        }

        objblogdo.ID = ID;
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetSearchResult);

        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            BlogId = Convert.ToString(dtSub.Rows[i]["intBlogId"]);
            Session["intBlogId"] += "," + BlogId;
        }
        Response.Redirect("AllBlogs.aspx");
    }

    protected void onClick_sucess(object sender, EventArgs e)
    {
        Response.Redirect("AllBlogs.aspx");
    }

}