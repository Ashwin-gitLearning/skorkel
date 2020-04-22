using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

public partial class my_documents : System.Web.UI.Page
{
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();
    DO_ProfileDocuments DocDO = new DO_ProfileDocuments();
    DA_ProfileDocuments DocDA = new DA_ProfileDocuments();
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();

    DataTable dt = new DataTable();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divDeletesucess.Style.Add("display", "none");
            Session["SubmitTime"] = DateTime.Now.ToString();
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "My Skorkel";
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            BindDocuments();
         //   BindDocsSale();
          //  TotalDocSale();
          //  BindPublisDocs();
          //  TotalPublishDocs();
            BindSubjectList();
            ddUploader1.MaxFileSize = "5242880";
            //  TotalDocs();
        }

    }
    private void BindDocuments()
    {

        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkuploadDoc.Visible = false;
            DocDO.AddedBy = Convert.ToInt32(ViewState["RegId"]);
            DocDO.FriendId = Convert.ToInt32(Session["ExternalUserId"]);
            dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.GetPublicDocsForFriend);
        }
        else
        {
            DocDO.AddedBy = Convert.ToInt32(ViewState["UserID"]);
            dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.Getdocuments);
        }

        if (dt.Rows.Count > 0)
        {
            divEmptyResult.Style.Add("display", "none");
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
        else
        {
            divEmptyResult.Style.Add("display", "block");
            LstDocument.DataSource = null;
            LstDocument.DataBind();
        }
    }
    private void BindSubjectList()
    {
        DataTable dtSub = new DataTable();

        dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
        if (dtSub.Rows.Count > 0)
        {
            lstSubjCategory.DataTextField = "strCategoryName";
            lstSubjCategory.DataValueField = "intCategoryId";
            lstSubjCategory.DataSource = dtSub;
            lstSubjCategory.RefreshList();
        }
        else
        {
            lstSubjCategory.DataSource = null;
            lstSubjCategory.RefreshList();
        }
    }
    protected void clearDoc()
    {
        ddUploader1.Reset();
        txtDescrition.Value = "";
        txtDocTitle.Text = "";
        lblErrorMsg.Visible = false;
        lblErrorMsg.Text = "";
        uploadPopopError.Style.Add("display", "none");
        //    lblfilenamee.Text = "";
        //    lnkDeleteDoc123.Style.Add("display", "none");
        //    ViewState["Callsmoothmenu"] = "smooth";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  string FilePath = hdnFilePath.Value;
        // string DocName = hdnDocName.Value;
     //   string Error = hdnErrorMsg.Value;
        bool fileUploaded = ddUploader1.UploadFile();
        // string filename = hdnUploadFile.Value;
        if (!fileUploaded)
        {

            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = ddUploader1.ErrorMesssage;
            lblErrorMsg.CssClass = "RedErrormsg";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "docpop", "OverlayBody();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
            return;
        }
        else
        {
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = "";
        }

        if (txtDocTitle.Text != "Title")
        {
            objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
        }
        if (txtDescrition.Value != "Description")
        {
            objDoProDocs.strDescrition = txtDescrition.Value.Trim().Replace("'", "''");
        }

        objDoProDocs.FilePath = ddUploader1.UploadedFileName;
        objDoProDocs.strDocName = ddUploader1.FileName;
        objDoProDocs.intDocsSee = imgPrivate.Checked ? "Private" : "Public";

        objDoProDocs.IsDocsDownload = imgDownload.Checked ? "Y" : "N";
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
        List<KeyValuePair<string, string>> subJectValues = lstSubjCategory.GetSelectedValues();

        if (subJectValues != null && subJectValues.Count > 0)
        {
            string[] subJectCateValues = subJectValues.Select(s => (string)s.Value).ToArray();
            if (ViewState["DocIdEdit"] != null)
            {
                objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteTempTable);
            }

            //    string[] totalSubjects = hdnSubject.Value.Split(',');
            var dictionary = new Dictionary<int, int>();

            Dictionary<string, int> counts = subJectCateValues.ToArray().GroupBy(x => x)
                                          .ToDictionary(g => g.Key,
                                                        g => g.Count());

            foreach (var v in counts)
            {
                if (v.Value == 1)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 3)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 5)
                {

                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 7)
                {

                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }
                else if (v.Value == 9)
                {
                    if (ViewState["DocIdEdit"] != null)
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
                    }
                    else
                    {
                        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
                    }
                    objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
                    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
                }

            }
        }
        RedirectToDocument();
        //clearDoc();
        //BindDocuments();
        //Session["SubmitTime"] = DateTime.Now.ToString();
        //ViewState["docPathNew"] = null;
        //divDocumentUplaod.Style.Add("display", "none");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI12", "CallDocLI();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
    }
    protected void upldrFile_delete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
    }
    protected void RedirectToDocument()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=D&RegId=" + Request.QueryString["RegId"]);
        }
        Response.Redirect("my-documents.aspx");
    }
    protected void lnkCancelDoc_Click(object sender, EventArgs e)
    {
        RedirectToDocument();
        //clearDoc();
        //BindDocuments();
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //divDocumentUplaod.Style.Add("display", "none");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
    }
    protected void lnkuploadDoc_click(object sender, EventArgs e)
    {
      //  Page.Form.Attributes.Add("enctype", "multipart/form-data");
        divDocumentUplaod.Style.Add("display", "block");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallDocMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosn", "createChosen();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dopopup", "OverlayBody();", true);
    //    BindDocuments();
        clearDoc();
    }

    protected void LstDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtIsDownload = new DataTable();
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl editUserComment = (HtmlControl)e.Item.FindControl("editUserComment");
        HiddenField hdnstrFilePath = (HiddenField)e.Item.FindControl("hdnstrFilePath");
        HiddenField hdnIsDocsDownload = (HiddenField)e.Item.FindControl("hdnIsDocsDownload");
        HyperLink lblDocument1 = (HyperLink)e.Item.FindControl("lblDocument1");
        Label lblDocument = (Label)e.Item.FindControl("lblDocument");
        DataRowView itemdata = e.Item.DataItem as DataRowView;
        if (itemdata["intaddedby"].ToString() != ViewState["UserID"].ToString())
        {
            editUserComment.Style.Add("display", "none");
            if (itemdata["IsDocsDownload"].ToString() != "Y")
            {
                lblDocument.Style.Add("display", "block");
                lblDocument1.Style.Add("display", "none");
            }
        }
    }

    protected void BindDocsSale()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetDocsSaleByAddedBy);
        if (dt.Rows.Count > 0)
        {
         //   lstDocSale.DataSource = dt;
         //   lstDocSale.DataBind();
        }
    }

    protected void TotalDocSale()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalDocSale);
        if (dt.Rows.Count > 0)
        {
      //      lblSale.Text = Convert.ToString(dt.Rows[0]["TotalDocSale"]);
        }
    }

    protected void BindPublisDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetPublishDocsByAddedBy);
        if (dt.Rows.Count > 0)
        {
      //      lstPublishDocs.DataSource = dt;
       //     lstPublishDocs.DataBind();
        }
    }

    protected void TotalPublishDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalPublishDocs);
        if (dt.Rows.Count > 0)
        {
     //       lblPublishDocs.Text = Convert.ToString(dt.Rows[0]["TotalPublDocs"]);
        }
    }

    protected void TotalDocs()
    {
        objDoProDocs.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.DocumenTemp.GetTotalDocs);
        if (dt.Rows.Count > 0)
        {
      //      lblTotalDocs.Text = Convert.ToString(dt.Rows[0]["TotalDocsCount"]);
        }
    }

    protected void lnkFreeViewAll_OnClick(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        Response.Redirect("CreateProfile-Documents.aspx?updateid=1");
    }

    protected void lnkViewAll_OnClick(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        Response.Redirect("documents.aspx");
    }
    protected void BindEditSubjectList(string DocId)
    {
        DocDO.AddedBy = Convert.ToInt32(ViewState["RegId"]);
        DocDO.DocId = Convert.ToInt32(DocId);
        DataTable dtSub = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.GetDocSubcategory);
        List<string> catValues = new List<string>();
        if (dtSub.Rows.Count > 0)
        {
            for (int i = 0; i < dtSub.Rows.Count; i++)
            {
                catValues.Add(dtSub.Rows[i]["intCategoryId"].ToString());
                //if (subval == "")
                //{
                //    subval = dtSub.Rows[i]["intCategoryId"].ToString();
                //}
                //else
                //{
                //    subval += "," + dtSub.Rows[i]["intCategoryId"].ToString();
                //}
            }
            //   hdnSubject.Value = subval;

        }
        lstSubjCategory.SetValues(catValues.ToArray());

    }
    protected void LstDocument_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HiddenField hdnstrFilePath = (HiddenField)e.Item.FindControl("hdnstrFilePath");
        HiddenField hdnIsDocsDownload = (HiddenField)e.Item.FindControl("hdnIsDocsDownload");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnstrDocTitle = (HiddenField)e.Item.FindControl("hdnstrDocTitle");
        ViewState["DocId"] = Convert.ToInt32(hdnDocId.Value);
        if (e.CommandName == "EditDocs")
        {
            //PopUpCropImage.Style.Add("display", "none");
            BindEditSubjectList(hdnDocId.Value);
            //   BindEditTopSubjectList(hdnDocId.Value);
            //        divDocumentUplaod.Style.Add("display", "block");
            ViewState["DocIdEdit"] = hdnDocId.Value;
            objDoProDocs.DocId = Convert.ToInt32(hdnDocId.Value);
            DataTable dtDoc = objDAProDocs.GetDataTable(objDoProDocs, DA_ProfileDocuments.Document.SelectDoc);
            if (dtDoc.Rows.Count > 0)
            {
                txtDocTitle.Text = dtDoc.Rows[0]["strDocTitle"].ToString();
                txtDescrition.InnerText = dtDoc.Rows[0]["strDescrition"].ToString();
                imgDownload.Checked = dtDoc.Rows[0]["IsDocsDownload"].ToString() == "Y";
                imgPrivate.Checked = !(dtDoc.Rows[0]["intDocsSee"].ToString() == "Public");


                if (dtDoc.Rows[0]["strFilePath"].ToString() != "")
                {
                    ddUploader1.SetValues(dtDoc.Rows[0]["strDocName"].ToString(), dtDoc.Rows[0]["strFilePath"].ToString());
                }
                else
                {
                    //ddUploader1.
                }
                //else
                //{
                //  //  lblfilenamee.Text = "Uploaded file not found.";
                //}
            }
            //e.Item.Parent.Parent
            ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);

            HtmlControl divdoc = this.PnlDocuments.FindControl("divDocumentUplaod") as HtmlControl;
            divDocumentUplaod.Style.Add("display", "block");
        }
        if (e.CommandName == "DownloadDoc")
        {
            if (hdnIsDocsDownload.Value == "Y")
            {

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
        }

    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        if (hdnintdocIdelete.Value != "")
        {
            objDoProDocs.DocId = Convert.ToInt32(hdnintdocIdelete.Value);
            objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteDocument);
            string PathPhysical = Server.MapPath("~/UploadDocument/" + Convert.ToString(hdnfilestrFilePathe.Value));
            if (File.Exists(PathPhysical))
            {
                File.Delete(PathPhysical);
            }

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(hdnintdocIdelete.Value);
            objLog.strAction = "Document";
            objLog.strActionName = hdnstrdocDescriptiondele.Value;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 10;   // Document
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            hdnintdocIdelete.Value = "";
            hdnstrdocDescriptiondele.Value = "";
            hdnfilestrFilePathe.Value = "";
            BindDocuments();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DocI", "divCancels();", true);
        }
    }


}