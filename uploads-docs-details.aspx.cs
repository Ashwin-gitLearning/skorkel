using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Spire.Doc;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net;


public partial class uploads_docs_details : System.Web.UI.Page
{
    DO_ProfileDocuments DocDO = new DO_ProfileDocuments();
    DA_ProfileDocuments DocDA = new DA_ProfileDocuments();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    DO_GroupUploadDoc objUploadDoc = new DO_GroupUploadDoc();
    DA_GroupUploadDoc objUploadDocDA = new DA_GroupUploadDoc();

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dvPopup.Style.Add("display", "none");
            divDeletesucess.Style.Add("display", "none");
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }
            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";
            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            AccessModulePermisssion();
            GetFolderDetails();
            GetOuterDocuments();
            hdnUptFolderName.Value = "Save";
        }
        lnlBack.Visible = false;
        divCancelPoll.Style.Add("display", "none");
    }

    protected void AccessModulePermisssion()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);

        if (dschk.Tables[3].Rows.Count > 0)
        {
            if (dschk.Tables[3].Rows[0][0].ToString() == ViewState["UserID"].ToString())
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
                return;
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
            }
        }
        else
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
        }
    }

    protected void GetFolderDetails()
    {
        DocDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetAllFolderDetail);
        if (dt.Rows.Count > 0)
        {
            lstFolderDetails.DataSource = dt;
            lstFolderDetails.DataBind();
        }
        else
        {
            lstFolderDetails.DataSource = null;
            lstFolderDetails.DataBind();
        }
    }

    #region AssignRole

    protected void GetAccessModuleDetails()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        dt = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpModuleDetailsAcces);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ModuleName = Convert.ToString(dt.Rows[i]["strModuleName"]);

                switch (ModuleName)
                {
                    case "Wall": DivHome.Style.Add("display", "block");
                        break;
                    case "Uploads": DivUploadTab.Style.Add("display", "block");
                        break;
                    case "Events": DivEventTab.Style.Add("display", "block");
                        break;
                    case "Discussion": DivForumTab.Style.Add("display", "block");
                        break;
                    case "Polls": DivPollTab.Style.Add("display", "block");
                        break;
                    case "Members": DivMemberTab.Style.Add("display", "block");
                        break;
                }
            }

        }

    }

    #endregion

    protected void lstFolderDetails_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtFolder = new DataTable();
        HiddenField hdnFolderId = (HiddenField)e.Item.FindControl("hdnFolderId");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        HtmlControl dropdownMenuLink = (HtmlControl)e.Item.FindControl("dropdownMenuLink");
        HiddenField hdnstrIsPublic = (HiddenField)e.Item.FindControl("hdnstrIsPublic");
        HiddenField hdnstrFolderDescription = (HiddenField)e.Item.FindControl("hdnstrFolderDescription");
        LinkButton lbllblFolderName = (LinkButton)e.Item.FindControl("lblFolderName");
        ImageButton imgBtn = (ImageButton)e.Item.FindControl("imgBtn");
        Label lblTotalDocs = (Label)e.Item.FindControl("lblTotalDocs");
        HtmlGenericControl tdfolder = (HtmlGenericControl)e.Item.FindControl("tdfolder");

        if (hdnstrIsPublic.Value == "Private")
        {
            if (Convert.ToInt32(ViewState["UserID"]) != Convert.ToInt32(hdnRegistrationId.Value))
            {
                tdfolder.Visible = false;
            }
        }

        string ff = hdnRegistrationId.Value;
        string UserID = Convert.ToString(ViewState["UserID"]);
        if (hdnRegistrationId.Value == UserID)
        {
            lnkEdit.Visible = true;
            lnkDelete.Visible = true;
            dropdownMenuLink.Visible = true;
        }
        else
        {
            dropdownMenuLink.Visible = false;
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
        }

        DocDO.intFolderId = Convert.ToInt32(hdnFolderId.Value);
        dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetTotalDocs);
        if (dt.Rows.Count > 0)
        {

            string TotalDocs = Convert.ToString(dt.Rows[0]["TotalDocs"]);
            if (TotalDocs == "0")
            {
                DocDO.intParentId = Convert.ToInt32(hdnFolderId.Value);
                dtFolder = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetTotalFolder);
                if (dtFolder.Rows.Count > 0)
                {
                    string TotalFolder = Convert.ToString(dtFolder.Rows[0]["TotalFolder"]);
                    if (TotalFolder == "0")
                    {
                        lblTotalDocs.Text = "(0)";
                    }
                    else
                    {
                        lblTotalDocs.Text = "(" + TotalFolder + ")";
                    }
                }
            }
            else
            {
                lblTotalDocs.Text = "(" + TotalDocs + ")";
            }
        }

    }

    protected void lstFolderDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        GetOuterDocuments();
        dvPopup.Style.Add("display", "none");
        HiddenField hdnFolderId = (HiddenField)e.Item.FindControl("hdnFolderId");
        HiddenField hdnFolderName = (HiddenField)e.Item.FindControl("hdnFolderName");

        HiddenField hdnstrIsPublic = (HiddenField)e.Item.FindControl("hdnstrIsPublic");
        HiddenField hdnstrFolderDescription = (HiddenField)e.Item.FindControl("hdnstrFolderDescription");

        ViewState["intFolderId"] = hdnFolderId.Value;
        ViewState["FolderName"] = hdnFolderName.Value;
        divFolderSuccess.Style.Add("display", "none");


        if (e.CommandName == "EditFolder")
        {
            dvPopup.Style.Add("display", "block");
            txtDirectoryName.Text = hdnFolderName.Value;
            hdnUptFolderName.Value = "Update";
            hdnFolderEditId.Value = hdnFolderId.Value;
            txtDescription.InnerText = hdnstrFolderDescription.Value;
            if (hdnstrIsPublic.Value == "Private")
            {
                rbPrivate.Checked = true;
                rbPublic.Checked = false;
            }
            else
            {
                rbPrivate.Checked = false;
                rbPublic.Checked = true;
            }
        }

        else if (e.CommandName == "DeleteFolder")
        {
            dvPopup.Style.Add("display", "none");
            hdnFolderEditId.Value = hdnFolderId.Value;
            ViewState["hdnFolderEditId"] = Convert.ToInt32(hdnFolderEditId.Value);
            ViewState["hdnFolderName"] = hdnFolderName.Value;
            ViewState["folders"] = "folders";
            divDeletesucess.Style.Add("display", "block");
        }
        else
        {
            ViewState["intFolderIdIn"] = hdnFolderId.Value;
            if (ViewState["FolderName"] != null && ViewState["First"] == null)
            {
                lblFirst.Visible = true;
                lnkFirst.Visible = true;
                lnkFirst.Text = Convert.ToString(ViewState["FolderName"]);
                ViewState["First"] = lnkFirst.Text;
                ViewState["FirstFolderId"] = hdnFolderId.Value;

            }
            if (ViewState["FolderName"] != null && Convert.ToString(ViewState["First"]) != lnkFirst.Text && Convert.ToString(ViewState["Second"]) != lnkSecond.Text && ViewState["Second"] == null)
            {
                lblSecond.Visible = true;
                lnkSecond.Visible = true;
                lnkSecond.Text = Convert.ToString(ViewState["FolderName"]);
                ViewState["Second"] = lnkSecond.Text;
                ViewState["SecondFolderId"] = hdnFolderId.Value;
            }
            if (ViewState["FolderName"] != null && Convert.ToString(ViewState["Second"]) != lnkSecond.Text && Convert.ToString(ViewState["First"]) != lnkFirst.Text)
            {
                lnkThird.Visible = true;
                lnkThird.Text = Convert.ToString(ViewState["FolderName"]) + " >>";
                ViewState["Third"] = lnkThird.Text;
                ViewState["ThirdFolderId"] = hdnFolderId.Value;
            }
            GetDocumentsDetails();
        }
    }

    protected void lstFolderDocs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnUploadDocId = (HiddenField)e.Item.FindControl("hdnUploadDocId");
        Label lblFolderName = (Label)e.Item.FindControl("lblFolderName");
        if (e.CommandName == "FolderDetails")
        {
            divCancelPoll.Style.Add("display", "none");
            int UploadDocId = Convert.ToInt32(hdnUploadDocId.Value);
            ViewState["UploadDocId"] = UploadDocId;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~\\GroupDocuments\\"));
            foreach (DirectoryInfo f in dir.GetDirectories())
                if (f.Name.Contains(lblFolderName.Text))
                    Session["Path"] = f.FullName;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GetOuterDocuments();
        if (ViewState["FolderName"] != null && ViewState["First"] == null)
        {
            Response.Redirect("~/Create-UploadDocuments.aspx?FolderID=" + ViewState["intFolderId"] + "&GrpId=" + ViewState["intGroupId"] + "&first=" + ViewState["First"]);
        }
        if (ViewState["FolderName"] != null && Convert.ToString(ViewState["First"]) != lnkFirst.Text && Convert.ToString(ViewState["Second"]) != lnkSecond.Text && ViewState["Second"] == null)
        {
            Response.Redirect("~/Create-UploadDocuments.aspx?FolderID=" + ViewState["intFolderId"] + "&GrpId=" + ViewState["intGroupId"] + "&first=" + ViewState["First"] + "&second=" + ViewState["Second"]);
        }
        if (ViewState["FolderName"] != null && Convert.ToString(ViewState["Second"]) != lnkSecond.Text && Convert.ToString(ViewState["First"]) != lnkFirst.Text)
        {
            Response.Redirect("~/Create-UploadDocuments.aspx?FolderID=" + ViewState["intFolderId"] + "&GrpId=" + ViewState["intGroupId"] + "&first=" + ViewState["First"] + "&second=" + ViewState["Second"] + "&third=" + ViewState["Third"]);
        }
        Response.Redirect("~/Create-UploadDocuments.aspx?FolderID=" + ViewState["intFolderId"] + "&GrpId=" + ViewState["intGroupId"]);
    }

    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        GetFolderDetails();
        GetOuterDocuments();
        divDeletesucess.Style.Add("display", "none");
        divCancelPoll.Style.Add("display", "none");
        dvPopup.Style.Add("display", "block");
    }

    protected void GetDocumentsDetails()
    {
        DocDO.intFolderId = Convert.ToInt32(ViewState["intFolderId"]);
        DocDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetDocsDetailsByFolderId);
        if (dt.Rows.Count > 0)
        {
            pnlLstDocument.Visible = true;
            pnlFolderDetails.Visible = true;
            LstDocument.DataSource = dt;
            LstDocument.DataBind(); GetChildFolder();
        }
        else
        {
            pnlFolderDetails.Visible = true;
            pnlLstDocument.Visible = false;
            LstDocument.DataSource = null;
            LstDocument.DataBind();
            GetChildFolder();
        }
    }

    protected void LstDocument_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        divFolderSuccess.Style.Add("display", "none");
        popup1.Style.Add("display", "none");
        GetFolderDetails();
        dvPopup.Style.Add("display", "none");
        HiddenField hdnUploadDocsId = (HiddenField)e.Item.FindControl("hdnUploadDocsId");
        HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
        HiddenField hdnstrDocTitle = (HiddenField)e.Item.FindControl("hdnstrDocTitle");
        ViewState["DocTitle"] = hdnstrDocTitle.Value;
        ViewState["Filepath"] = hdnFilePath.Value;
        ViewState["hdnUploadDocsId"] = hdnUploadDocsId.Value;
        LinkButton hrp_DocPath = (LinkButton)e.Item.FindControl("hrp_DocPath");
        if (e.CommandName == "EditDocs")
        {
            Response.Redirect("Create-UploadDocuments.aspx?DocId=" + hdnUploadDocsId.Value + "&GrpId=" + Request.QueryString["GrpId"]);
        }
        if (e.CommandName == "DeleteDocs")
        {
            divDeletesucess.Style.Add("display", "block");
        }

        if (e.CommandName == "OpenDocDown")
        {
            string strURL = "~\\UploadDocument\\" + ViewState["Filepath"].ToString();
            try
            {
                Response.Redirect("handler/DownloadFile.ashx?path=" + strURL);
                DivDownload.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                DivDownload.Style.Add("display", "none");
                ex.Message.ToString();
            }
        }

        if (e.CommandName == "OpenDoc")
        {
            lblTitles.Text = hdnstrDocTitle.Value;
            divdisp1.InnerHtml = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            try
            {
                Document document = new Document();
                string ext = System.IO.Path.GetExtension(hdnFilePath.Value);
                if (ext.Trim() == ".doc" || ext.Trim() == ".docx" || ext.Trim() == ".txt")
                {
                    
                    string path = Request.PhysicalApplicationPath + "\\UploadDocument\\" + hdnFilePath.Value.ToString();
                    document.LoadFromFile(path);
                    string GetText = document.GetText();
                    string Doc = TextToHtml(GetText);
                    if (!string.IsNullOrEmpty(Doc))
                    {
                        divdisp1.InnerHtml = GetText.ToString();
                        ViewState["UploadDocsId"] = hdnUploadDocsId.Value;
                    }
                    popup1.Style.Add("display", "block");
                }
                else if (ext.Trim() == ".xls" || ext.Trim() == ".xlsx")
                {
                    //Get file name of selected file
                    string filePath = Request.PhysicalApplicationPath + "UploadDocument\\" + hdnFilePath.Value.ToString();

                    //Open the connection with excel file based on excel version
                    OleDbConnection con = null;
                    // filePath=@"C:\Users\usil-61\Downloads\190713021313-ploadAndReadExcelfile\UploadAndReadExcelfile\UploadExcel\Userfiles\Updated_API_IO_Design.xlsx";
                    if (ext == ".xls")
                    {
                        con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;");

                    }
                    else if (ext == ".xlsx")
                    {
                        con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                        //con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                    }
                    con.Open();
                    //Get the list of sheet available in excel sheet
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //Get first sheet name
                    string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();
                    //Select rows from first sheet in excel sheet and fill into dataset
                    OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", con);
                    OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                    DataSet ExcelDataSet = new DataSet();
                    ExcelAdapter.Fill(ExcelDataSet);
                    con.Close();
                    //Bind the dataset into gridview to display excel contents
                    GridView1.DataSource = ExcelDataSet;
                    GridView1.DataBind();

                    popup1.Style.Add("display", "block");
                }
                else if (ext.Trim() == ".pdf")
                {
                    string fileName = Request.PhysicalApplicationPath + "UploadDocument\\" + hdnFilePath.Value.ToString();

                    //string FilePath = fileName;
                    //WebClient User = new WebClient();
                    //Byte[] FileBuffer = User.DownloadData(FilePath);
                    //if (FileBuffer != null)
                    //{
                    //    Response.ContentType = "application/pdf";
                    //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    //    Response.BinaryWrite(FileBuffer);
                    //}

                    StringBuilder text = new StringBuilder();
                    PdfReader pdfReader = new PdfReader(fileName);
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                        currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                        text.Append(currentText);
                       
                    }
                    divdisp1.InnerHtml = text.ToString();
                    pdfReader.Close();
                    popup1.Style.Add("display", "block");
                }
                else
                {
                    DivDownload.Style.Add("display", "block");
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Closepopup();", hdnstrDocTitle.Value.ToString()), true);
                }

            }
            catch (Exception ex)
            {
                DivDownload.Style.Add("display", "block");
                ex.Message.ToString();
                System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Closepopup();", hdnstrDocTitle.Value.ToString()), true);

            }

        }
    }

    protected void LstDocument_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HiddenField hdnintDocsSee = (HiddenField)e.Item.FindControl("hdnintDocsSee");
        HiddenField hdnintDocumentTypeID = (HiddenField)e.Item.FindControl("hdnintDocumentTypeID");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
        HtmlControl dropdownMenuLink = (HtmlControl)e.Item.FindControl("dropdownMenuLink");
        HtmlControl tdDocs = (HtmlControl)e.Item.FindControl("tdDocs");
        Label divDocType = (Label)e.Item.FindControl("divDocType");
        HtmlControl divDoctTypeOuter = (HtmlControl)e.Item.FindControl("divDoctTypeOuter");
        if (hdnintDocsSee.Value == "Private")
        {
            if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
                tdDocs.Visible = false;
        }
        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            dropdownMenuLink.Visible = false;
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
        }
        if (Convert.ToInt32(hdnintDocumentTypeID.Value) == 1)
        {
            divDoctTypeOuter.Visible = true;
            divDocType.Text = "Case";
        }
        else if (Convert.ToInt32(hdnintDocumentTypeID.Value) == 3)
        {
            divDoctTypeOuter.Visible = true;
            divDocType.Text = "Article";
        }
        else if (Convert.ToInt32(hdnintDocumentTypeID.Value) == 4)
        {
            divDoctTypeOuter.Visible = true;
            divDocType.Text = "Note";
        }
        else if (Convert.ToInt32(hdnintDocumentTypeID.Value) == 5)
        {
            divDoctTypeOuter.Visible = true;
            divDocType.Text = "Other";
        }
        else
        {
            divDoctTypeOuter.Visible = false;
        }
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        //ViewState["intFolderId"] = null;
        //ViewState["hdnFolderName"] = null;
        //divDeletesucess.Style.Add("display", "none");
        //GetFolderDetails();
        //GetOuterDocuments();
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + Request.QueryString["GrpId"]);
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        if (ViewState["hdnFolderName"] != null)
        {
            lblEditSuccess.Visible = false;
            lblSuccess.Visible = false;
            lblDelSuccess.Visible = true;
            DocDO.intFolderId = Convert.ToInt32(ViewState["hdnFolderEditId"]);
            DocDA.AddEditDel_GroupDocument(DocDO, DA_ProfileDocuments.GropDocument.DeleteDocFolder);

            string PathPhysical = Server.MapPath("~/GroupDocuments/" + Convert.ToString(ViewState["hdnFolderName"]));
            if (Directory.Exists(PathPhysical))
            {
                Directory.Delete(PathPhysical);
            }
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["hdnFolderEditId"]);
            objLog.strAction = "Group Folder";
            objLog.strActionName = ViewState["hdnFolderName"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 5;   // Group Folder
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            GetFolderDetails();
            GetOuterDocuments();
            if (ViewState["FirstFolderId"] != null)
            {
                ViewState["intFolderId"] = ViewState["FirstFolderId"];
                GetDocumentsDetails(); GetChildFolder();
            }
            else
            {
                if (ViewState["SecondFolderId"] != null)
                {
                    ViewState["intFolderId"] = ViewState["SecondFolderId"];
                    GetDocumentsDetails(); GetChildFolder();
                }
                else
                {
                    if (ViewState["ThirdFolderId"] != null)
                    {
                        ViewState["intFolderId"] = ViewState["ThirdFolderId"];
                        GetDocumentsDetails(); GetChildFolder();
                    }
                    else
                    {
                        GetFolderDetails(); GetOuterDocuments();
                    }
                }
            }

            ViewState["intFolderId"] = null;
            ViewState["hdnFolderName"] = null;
            divDeletesucess.Style.Add("display", "none");

        }
        else
        {

            DocDO.UploadDocId = Convert.ToInt32(ViewState["hdnUploadDocsId"]);
            DocDA.AddEditDel_GroupDocument(DocDO, DA_ProfileDocuments.GropDocument.DeleteDocumentByDocId);
            string PathPhysical = Server.MapPath("~/UploadDocument/" + Convert.ToString(ViewState["Filepath"]));
            if (File.Exists(PathPhysical))
            {
                File.Delete(PathPhysical);
            }

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["hdnUploadDocsId"]);
            objLog.strAction = "Group Document";
            objLog.strActionName = Convert.ToString(ViewState["DocTitle"]);
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 6;   // Group Document
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            if (ViewState["intFolderIdIn"] != null)
            {
                GetDocumentsDetails(); GetChildFolder();
            }
            else
            {
                GetFolderDetails(); GetOuterDocuments();
            }
            divDeletesucess.Style.Add("display", "none");
            ViewState["intFolderIdIn"] = null;
        }

    }

    //protected void LoadDocLike()
    //{
    //    objUploadDoc.intUploadDocId = Convert.ToInt32(ViewState["UploadDocsId"]);
    //    objUploadDoc.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
    //    objUploadDoc.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //    DataTable dtt = objUploadDocDA.GetGroupDocDataTable(objUploadDoc, DA_GroupUploadDoc.GroupUploadDoc.GetLikeComment);
    //    if (dtt.Rows.Count != 0)
    //    {
    //        lnkLikePost.Text = dtt.Rows[0]["CountLike"].ToString();
    //        lblDocComment.Text = dtt.Rows[0]["CountComment"].ToString();
    //        if (dtt.Rows[0]["LikeUserId"].ToString() == ViewState["UserID"].ToString())
    //        {
    //            lnkLike.Text = "Unlike";
    //        }
    //        else
    //        {
    //            lnkLike.Text = "Like";
    //        }
    //    }
    //}

    //protected void LoadDocComment()
    //{
    //    objUploadDoc.intUploadDocId = Convert.ToInt32(ViewState["UploadDocsId"]);
    //    objUploadDoc.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
    //    DataTable dtComment = objUploadDocDA.GetGroupDocDataTable(objUploadDoc, DA_GroupUploadDoc.GroupUploadDoc.GetComment);
    //    if (dtComment.Rows.Count > 0)
    //    {
    //        RepPopPost.DataSource = dtComment;
    //        RepPopPost.DataBind();
    //    }
    //    else
    //    {
    //        RepPopPost.DataSource = null;
    //        RepPopPost.DataBind();
    //    }
    //}

    public static string TextToHtml(string text)
    {
        text = HttpUtility.HtmlEncode(text);
        return text;
    }

    protected void GetChildFolder()
    {
        if (ViewState["intFolderId"] != null && Convert.ToString(ViewState["intFolderId"]) != "")
        {
            DocDO.intParentId = Convert.ToInt32(ViewState["intFolderId"]);
            dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetChildFolderNameByParentID);
            ViewState["childFolder"] = "yes";
        }
        else
        {
            DocDO.intFolderId = Convert.ToInt32(ViewState["intFolderId"]);
            dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetChildFolderName);
        }

        if (dt.Rows.Count > 0)
        {
            lstFolderDetails.DataSource = dt;
            lstFolderDetails.DataBind();
        }
        else
        {
            pnlFolderDetails.Visible = false;
        }
        ViewState["FolderId"] = null;
    }

    protected void GetChildFileDoc()
    {
        DocDO.intFolderId = Convert.ToInt32(ViewState["intFolderId"]);
        DocDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.GetDocsDetailsByFolderId);
        if (dt.Rows.Count > 0)
        {
            pnlLstDocument.Visible = true;
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
        else
        {
            pnlLstDocument.Visible = false;
            LstDocument.DataSource = null;
            LstDocument.DataBind();
        }
    }

    protected void GetOuterDocuments()
    {
        DocDO.intGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
        dt = DocDA.GetGrouDocumetDataTable(DocDO, DA_ProfileDocuments.GropDocument.Getdocuments);
        if (dt.Rows.Count > 0)
        {
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
        else
        {
            LstDocument.DataSource = null;
            LstDocument.DataBind();
        }
    }

    protected void lnkPopupOK_Click(object sender, EventArgs e)
    {
        var folderName = txtDirectoryName.Text.Trim().Replace("'", "''").Replace("/", "").Replace(".", "").Replace(":", "");
        string s = Convert.ToString(Session["Path"]);
        string strpath;
        if (s == "" || s == null)
        {
            try
            {
                strpath = Server.MapPath("~\\GroupDocuments\\" + folderName + "\\");
                Directory.CreateDirectory(strpath);
            }
            catch
            {
                lblMess.Text = "File name can't contain special character.";
                return;
            }
        }
        else
        {
            strpath = s + "\\" + folderName + "\\";
            Directory.CreateDirectory(strpath);
        }

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            DocDO.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        if (ViewState["intFolderId"] != null && Convert.ToString(ViewState["intFolderId"]) != "")
        {
            DocDO.IsFolder = "Y";
            DocDO.intParentId = Convert.ToInt32(ViewState["intFolderId"]);
            ViewState["ParentId"] = DocDO.intParentId;
        }
        DocDO.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DocDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DocDO.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        DocDO.strFolderName = Convert.ToString(folderName);
        DocDO.strFolderDescription = txtDescription.InnerText.Trim();

        if (rbPrivate.Checked == true)
        {
            DocDO.strIsPublic = "Private";
        }
        else
        {
            DocDO.strIsPublic = "Public";
        }

        if (hdnUptFolderName.Value == "Save")
        {
            lblEditSuccess.Visible = false;
            lblSuccess.Visible = true;
            lblDelSuccess.Visible = false;
            DocDA.AddEditDel_GroupDocument(DocDO, DA_ProfileDocuments.GropDocument.InsertFolderName);
            ViewState["FolderId"] = DocDO.DocId;
            txtDirectoryName.Text = "";
            txtDescription.InnerText = "";
            GetFolderDetails();
            GetOuterDocuments();
            GetChildFolder();
            if (Convert.ToString(ViewState["childFolder"]) == "yes")
            {
                GetChildFileDoc();
            }
            pnlFolderDetails.Visible = true;
            pnlLstDocument.Visible = true;
            dvPopup.Style.Add("display", "none");
            divFolderSuccess.Style.Add("display", "block");
        }
        else if (hdnUptFolderName.Value == "Update")
        {
            lblEditSuccess.Visible = true;
            lblSuccess.Visible = false;
            lblDelSuccess.Visible = false;
            DocDO.intFolderId = Convert.ToInt32(hdnFolderEditId.Value);
            DocDA.AddEditDel_GroupDocument(DocDO, DA_ProfileDocuments.GropDocument.RenameFolder);
            dvPopup.Style.Add("display", "none");
            divFolderSuccess.Style.Add("display", "block");

            GetFolderDetails();
            GetOuterDocuments();
            hdnUptFolderName.Value = "Save";
            txtDirectoryName.Text = "";
            txtDescription.InnerText = "";
        }

    }

    #region Tabs

    protected void lnkProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkUploadTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        int GrpId = Convert.ToInt32(Request.QueryString["GrpId"]);
        lblFirst.Visible = false;
        lnkFirst.Visible = false;
        lnkSecond.Visible = false;
        lnkThird.Visible = false;
        lblSecond.Visible = false;
        ViewState["First"] = null;
        ViewState["Second"] = null;
        ViewState["Third"] = null;
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + GrpId);
    }

    protected void lnkSecond_Click(object sender, EventArgs e)
    {

        lnkThird.Visible = false;
        ViewState["intFolderId"] = ViewState["SecondFolderId"];
        GetDocumentsDetails();
    }

    protected void lnkThird_Click(object sender, EventArgs e)
    {
        ViewState["intFolderId"] = ViewState["ThirdFolderId"];
        GetDocumentsDetails();
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        divFolderSuccess.Style.Add("display", "none");
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"] + "", true);
    }

    protected void lnkConnDisconn_Click(object sender, EventArgs e)
    {
        lblEditSuccess.Visible = false;
        lblSuccess.Visible = false;
        lblDelSuccess.Visible = true;
        DocDO.intFolderId = Convert.ToInt32(ViewState["hdnFolderEditId"]);
        DocDA.AddEditDel_GroupDocument(DocDO, DA_ProfileDocuments.GropDocument.DeleteDocFolder);

        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.intActionId = Convert.ToInt32(ViewState["hdnFolderEditId"]);
        objLog.strAction = "Group Folder";
        objLog.strActionName = ViewState["hdnFolderName"].ToString();
        objLog.strIPAddress = ip;
        objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        objLog.SectionId = 5;   // Group Folder
        objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);

        divCancelPoll.Style.Add("display", "none");
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"] + "", true);
    }

    protected void RepPopPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HiddenField hdnCommentID = (HiddenField)e.Item.FindControl("hdnCommentID");


    }

    protected void RepPopPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

        }


    }

    protected void lnkLinkPost_OnClick(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];


        objUploadDoc.intUploadDocId = Convert.ToInt32(ViewState["UploadDocsId"]);
        objUploadDoc.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objUploadDoc.strIpAddress = ip;
        objUploadDoc.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objUploadDocDA.AddEditDel_GroupUpDocument(objUploadDoc, DA_GroupUploadDoc.GroupUploadDoc.InsertLike);
        //LoadDocComment(); LoadDocLike();
        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Opendiv('{0}');", ViewState["DocTitle"].ToString()), true);
    }

    protected void lnkPostComent_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        objUploadDoc.strComment = Session["File"].ToString();
        objUploadDoc.intUploadDocId = Convert.ToInt32(ViewState["UploadDocsId"]);
        objUploadDoc.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objUploadDoc.strIpAddress = ip;
        objUploadDoc.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objUploadDocDA.AddEditDel_GroupUpDocument(objUploadDoc, DA_GroupUploadDoc.GroupUploadDoc.InsertComment);
        //LoadDocComment(); LoadDocLike();
        Session.Remove("File");
        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Opendiv('{0}');", ViewState["DocTitle"].ToString()), true);
    }

    protected void lblDocComment_Click(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Opendiv('{0}');", ViewState["DocTitle"].ToString()), true);
    }

    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        string strURL = "~\\UploadDocument\\" + ViewState["Filepath"].ToString();
        try
        {
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL);
            DivDownload.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            DivDownload.Style.Add("display", "none");
            ex.Message.ToString();
        }

    }

    protected void lnkCloseDownload_OnClick(object sender, EventArgs e)
    {
        DivDownload.Style.Add("display", "none");
        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("Closepopup();", "ss"), true);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvPopup.Style.Add("display", "none");
        GetFolderDetails();
        GetOuterDocuments();
    }
    
}
