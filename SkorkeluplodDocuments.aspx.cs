using System;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DA_SKORKEL;

public partial class SkorkeluplodDocuments : System.Web.UI.Page
{
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();

    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());
            BindTopSubjectList();
            HideShowSubject();
            SubjectTempDataTable();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

    private void BindTopSubjectList()
    {
        DataTable dtTopSub = new DataTable();
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetTopRecords);
        if (dtTopSub.Rows.Count > 0)
        {
            lstSubjCategory.DataSource = dtTopSub;
            lstSubjCategory.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
        }
        ViewState["DocId"] = null;
    }

    protected void lnkViewSubj_Click(object sender, EventArgs e)
    {
        if (lnkViewSubj.Text == "View all")
        {
            lnkViewSubj.Text = "Close";
        }
        else
        {
            lnkViewSubj.Text = "View all";
        }
        HideShowSubject();
    }

    protected void HideShowSubject()
    {
        if (lnkViewSubj.Text == "View all")
        {
            BindTopSubjectList();

        }
        else
        {
            BindSubjectList();
        }
    }

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
            lstSubjCategory.DataSource = dtSub;
            lstSubjCategory.DataBind();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.DataBind();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.UpdateDocumentByDocId);
        lblDocName.Visible = false;
        upload.Visible = true;
        try
        {
            string imgPathPhysical = Server.MapPath("~/UploadDocument/" + hdnUploadFile.Value.ToString());
            if (File.Exists(imgPathPhysical))
            {
                File.Delete(imgPathPhysical);
                lnkDelete.Style.Add("display", "none");
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }

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
                        Subjectid = (Convert.ToInt32(dtSubjCat.Rows.Count) + hdnSubCatId.Value).ToString();
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

            }
        }


    }

    protected void LstSubjCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnSubCatId = (HiddenField)e.Item.FindControl("hdnSubCatId");
        LinkButton lnkCatName = (LinkButton)e.Item.FindControl("lnkCatName");
        HtmlGenericControl SubLi = (HtmlGenericControl)e.Item.FindControl("SubLi");
        DataTable dtsub = new DataTable();
        DataTable dtlast = new DataTable();
        HiddenField hdnCountSub = (HiddenField)e.Item.FindControl("hdnCountSub");
        SubLi.Style.Add("background", "#ebeaea");
        SubLi.Style.Add("border", "1px solid #c8c8c8");
        SubLi.Style.Add("color", "#646161");

        if (ViewState["SubjectCategory"] != null)
        {
            dtsub = (DataTable)ViewState["SubjectCategory"];
            if (dtsub.Rows.Count > 0)
            {
                for (int i = 0; i < dtsub.Rows.Count; i++)
                {
                    if (hdnSubCatId.Value == dtsub.Rows[i]["intCategoryId"].ToString())
                    {
                        SubLi.Style.Add("background", "none repeat scroll 0 0 #656767 !important");
                        SubLi.Style.Add("color", "#FFFFFF !important");
                        SubLi.Style.Add("text-decoration", "none !important");
                        lnkCatName.ForeColor = System.Drawing.Color.White;
                        lnkCatName.Style.Add("color", "#FFFFFF !important");
                    }
                }
            }
            else
            {
                SubLi.Style.Add("background", "#ebeaea");
                SubLi.Style.Add("border", "1px solid #c8c8c8");
                SubLi.Style.Add("color", "#646161");

            }

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string docPath = "";
        string fname = hdnUploadFile.Value.ToString();    
   
            if (fname != "")
            {
                ViewState["docPath"] = fname;

                if (txtDocTitle.Text != "")
                {
                    objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
                }
                else
                {
                    return;
                }
                if (txtDocTitle.Text != "")
                {
                    objDoProDocs.strDescrition = txtDescrition.InnerText.Trim().Replace("'", "''");
                }
                if (ViewState["docPathNew"] != null)
                {
                    objDoProDocs.FilePath = docPath;

                }
                else
                {
                    objDoProDocs.FilePath = Convert.ToString((ViewState["docPath"]));
                }
                if (ViewState["docPathNew"] != null)
                {
                    if (lblDocName.Text != "")
                    {
                        objDoProDocs.strDocName = docPath;
                    }
                    else
                    {
                        objDoProDocs.strDocName = hdnUploadFile1.Value;
                    }
                }
                else
                {
                    objDoProDocs.strDocName = hdnUploadFile1.Value; //Convert.ToString(ViewState["strDocName"]);
                }
                objDoProDocs.intDocsSee = ddlintDocsSee.SelectedValue;
                if (ddlintDocsSee.SelectedValue == "")
                {
                    objDoProDocs.intDocsSee = "Public";
                }

                objDoProDocs.strAuthors = txtAuthors.Text.Trim().Replace("'", "''");

                if (rdDownloadYes.Checked == true)
                {
                    objDoProDocs.IsDocsDownload = "Y";
                }
                else
                {
                    objDoProDocs.IsDocsDownload = "N";
                }

                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

                objDoProDocs.intDocumentTypeID = 1;
                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                if (ViewState["DocIdEdit"] != null)
                {
                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Update);
                }
                else
                {
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Add);
                }

                ViewState["DocId"] = objDoProDocs.DocId;
                string totalSelectSubCat = Convert.ToString(ViewState["SubjectCategory"]);

                DataTable dtsub = new DataTable();
                dtsub = (DataTable)ViewState["SubjectCategory"];
                if (dtsub.Rows.Count > 0)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteTempTable);
                    }
                }
                foreach (DataRow dr in dtsub.Rows)
                {
                    //  ViewState["GrpId"] = objDO.inGroupId;
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(dr["intCategoryId"].ToString());
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                BindSubjectList();
                clear();
                BindTopSubjectList();
                Session["SubmitTime"] = DateTime.Now.ToString();
                ViewState["docPathNew"] = null;
            }
            else if (upload.HasFile)
            {
                if (ViewState["strDocName"] == null)
                {
                    int FileLength = upload.PostedFile.ContentLength;
                    string ext = System.IO.Path.GetExtension(this.upload.PostedFile.FileName);
                    if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif")
                    {
                        lblMessage.Text = "File format not supported.";
                        lblMessage.CssClass = "RedErrormsg";
                        return;
                    }
                    if (FileLength <= 3145728)
                    {
                        docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(upload.FileName).ToString();
                        upload.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
                        ViewState["docPath"] = docPath;
                    }
                    else
                    {
                        lblMessage.Text = "File size should be less than or equal to 3MB";
                        lblMessage.CssClass = "RedErrormsg";
                        return;
                    }
                }
                else if (upload.HasFile || lblDocName.Text != "")
                {

                    if (lblDocName.Text != "")
                    {
                        ViewState["docPathNew"] = lblDocName.Text;
                        docPath = lblDocName.Text;
                    }
                    else
                    {
                        int FileLength = upload.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.upload.PostedFile.FileName);

                        if (FileLength <= 3145728)
                        {
                            docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(upload.FileName).ToString();
                            upload.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
                            ViewState["docPathNew"] = docPath;

                        }

                        else
                        {
                            lblMessage.Text = "File size should be less than or equal to 3MB";
                            lblMessage.CssClass = "RedErrormsg";
                            return;
                        }
                    }
                }

                if (txtDocTitle.Text != "")
                {
                    objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
                }
                else
                {
                    return;
                }
                if (txtDocTitle.Text != "")
                {
                    objDoProDocs.strDescrition = txtDescrition.InnerText.Trim().Replace("'", "''");
                }
                if (ViewState["docPathNew"] != null)
                {
                    objDoProDocs.FilePath = docPath;

                }
                else
                {
                    objDoProDocs.FilePath = Convert.ToString((ViewState["docPath"]));
                }
                if (ViewState["docPathNew"] != null)
                {
                    if (lblDocName.Text != "")
                    {
                        objDoProDocs.strDocName = docPath;
                    }
                    else
                    {
                        objDoProDocs.strDocName = upload.FileName;
                    }
                }
                else
                {
                    objDoProDocs.strDocName = upload.FileName; //Convert.ToString(ViewState["strDocName"]);
                }

                objDoProDocs.intDocsSee = ddlintDocsSee.SelectedValue;
                if (ddlintDocsSee.SelectedValue == "")
                {
                    objDoProDocs.intDocsSee = "Public";
                }               

                objDoProDocs.strAuthors = txtAuthors.Text.Trim().Replace("'", "''");
                if (rdDownloadYes.Checked == true)
                {
                    objDoProDocs.IsDocsDownload = "Y";
                }
                else
                {
                    objDoProDocs.IsDocsDownload = "N";
                }

                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

                objDoProDocs.intDocumentTypeID = 1;
                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                if (ViewState["DocIdEdit"] != null)
                {
                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Update);
                }
                else
                {
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Add);
                }

                ViewState["DocId"] = objDoProDocs.DocId;
                string totalSelectSubCat = Convert.ToString(ViewState["SubjectCategory"]);

                DataTable dtsub = new DataTable();
                dtsub = (DataTable)ViewState["SubjectCategory"];
                if (dtsub.Rows.Count > 0)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                         objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteTempTable);
                    }
                }
                foreach (DataRow dr in dtsub.Rows)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(dr["intCategoryId"].ToString());
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                BindSubjectList();
                clear();
                BindTopSubjectList();
                Session["SubmitTime"] = DateTime.Now.ToString();
                ViewState["docPathNew"] = null;
            }
            else
            {
                lblMessage.Text = "Please Select Document.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }      
    }

    protected void clear()
    {
        ViewState["SubjectCategory"] = null;
        txtDocTitle.Text = "";
        txtAuthors.Text = "";
        lblMessage.Text = "";
        ddlintDocsSee.ClearSelection();
        rdDownloadNo.Checked = false;
        rdDownloadYes.Checked = false;
        lblDocName.Text = "";
        lnkDelete.Visible = false;
        txtDescrition.InnerText = "";
        SubjectTempDataTable();
    }

}