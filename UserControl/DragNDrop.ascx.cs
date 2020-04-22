using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_DragNDrop : System.Web.UI.UserControl
{
    public string ErrorMesssage { get; set; }
    public string UploadedFileName { get; set; }
    public string FileName { get; set; }
    public event EventHandler Delete = null;
    public string URL { get; set; }
    public string MaxFileSize { get; set; }
    public string DocType { get; set; }
    public bool ShowDelete { get; set; }
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            lnkDelete.Style.Add("display", "none");
            hdnURL.Value = URL;
            if(!string.IsNullOrEmpty(MaxFileSize))
                hdnFileSize.Value = MaxFileSize;
            if (!string.IsNullOrEmpty(DocType))
                hdnDocType.Value = DocType;
            hdnDeleteVisible.Value = ShowDelete.ToString();
        }
        else
        {
            if (hdnFileSelected.Value!="")
            {
                FileName = hdnUploadFile1.Value.ToString();
                UploadedFileName = hdnUploadFile.Value.ToString();
            }
        }
    }
    public bool UploadFile()
    {
        string docPath = "";
        FileName = hdnUploadFile1.Value.ToString();
        UploadedFileName = hdnUploadFile.Value.ToString();
        if (UploadedFileName != "")
        {
            return true;
        }
        else if (upload.HasFile)
        {

            int FileLength = upload.PostedFile.ContentLength;
            string ext = System.IO.Path.GetExtension(this.upload.PostedFile.FileName);
            string exten = ext.Trim().ToLower();
            if (exten == ".jpg" || exten == ".jpeg" || exten == ".png" || exten == ".gif")
            {
                ErrorMesssage = "File format not supported.";
                return false;
            }
            if (FileLength <= 3145728)
            {
                Random r = new Random();
                int rInt = r.Next(0, 1000000); //for ints
                docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + "-"+rInt + System.IO.Path.GetExtension(upload.FileName).ToString();
                upload.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
                UploadedFileName = docPath;
                hdnUploadFile.Value = docPath;
                hdnUploadFile1.Value = upload.FileName;
                lblfilenamee.Text = upload.FileName;
                upload.Visible = false;
                lnkDelete.Style.Add("display", "block");
            }
            else
            {
                ErrorMesssage = "File size should be less than or equal to 3 MB";
                return false;
            }

        }
        else
        {
            ErrorMesssage = "Please select document.";
            return false;
        }
        return true;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = "~/UploadDocument/";
            string dt = hdnDocType.Value.ToLower();
            if (!string.IsNullOrEmpty(dt) && dt == "article")
                filePath = "~/Articles/";
            if (!string.IsNullOrEmpty(dt) && dt == "resume")
                filePath = "~/Resumes/";
            if (!string.IsNullOrEmpty(dt) && dt == "subDoc")
                filePath = "~/SubDoc/";
            string imgPathPhysical = Server.MapPath(filePath + hdnUploadFile.Value.ToString());
            if (File.Exists(imgPathPhysical))
            {
                File.Delete(imgPathPhysical);
              //  Reset();
            }
            Reset();
        }
        catch (Exception ex) { ex.Message.ToString(); }
        if (this.Delete != null)
            this.Delete(this, e);
    }

    public void Reset()
    {
        UploadedFileName = "";
        hdnUploadFile.Value = "";
        hdnUploadFile1.Value = "";
        hdnFileSelected.Value = "";
        lblfilenamee.Text = "";
        ErrorMesssage = "";
        upload.Visible = true;
        lnkDelete.Style.Add("display", "none");
        uploadDragDrop.Style.Add("display", "block");
    }
    public void SetValues(string fileName, string UploadedFile)
    {
        UploadedFileName = UploadedFile;
        hdnUploadFile.Value = UploadedFile;
        hdnUploadFile1.Value = fileName;
        lblfilenamee.Text = fileName;
        hdnFileSelected.Value = "Uploaded";
        ErrorMesssage = "";
        upload.Visible = false;
        if (hdnDeleteVisible.Value == null || hdnDeleteVisible.Value == "")
            hdnDeleteVisible.Value = "false";
        if (Convert.ToBoolean(hdnDeleteVisible.Value))
            lnkDelete.Style.Add("display", "inline");
        else
           lnkDelete.Style.Add("display", "none");
        uploadDragDrop.Style.Add("display", "none");
    }
}