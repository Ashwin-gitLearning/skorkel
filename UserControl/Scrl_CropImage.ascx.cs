using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

public partial class UserControl_Scrl_CropImage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //cropedImage.ImageUrl = "~\\CroppedPhoto\\3262013115205AM.jpg";      
        //cropedImage.Visible = true;
    }

    #region For Moadal Popup Crop Image
    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        string documentPath = "";
        if (uploaderImg.HasFile)
        {
            int FileLength = uploaderImg.PostedFile.ContentLength;

            documentPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(uploaderImg.FileName).ToString();
            ViewState["filename"] = documentPath;
            uploaderImg.SaveAs(Server.MapPath("~\\UploadedPhoto\\" + documentPath));
            cropbox.Src = "~\\UploadedPhoto\\" + documentPath;
            ModalCropImage.Show();
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {

        if (this.IsPostBack)
        {

            //Get the Cordinates                
            int x = Convert.ToInt32(X.Value);
            int y = Convert.ToInt32(Y.Value);
            int w = Convert.ToInt32(W.Value);
            int h = Convert.ToInt32(H.Value);

            //Load the Image from the location
            System.Drawing.Image image = Bitmap.FromFile(
                  HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadedPhoto\\" + ViewState["filename"].ToString());
            //Create a new image from the specified location to                
            //specified height and width                
            Bitmap bmp = new Bitmap(w, h, image.PixelFormat);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(image, new Rectangle(0, 0, w, h),
            new Rectangle(x, y, w, h),
            GraphicsUnit.Pixel);

            //Save the file and reload to the control
            bmp.Save(HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["filename"].ToString(), image.RawFormat);
            cropedImage.Visible = true;

            cropedImage.ImageUrl = "~\\CroppedPhoto\\" + ViewState["filename"].ToString();

            ModalCropImage.Hide();


        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalCropImage.Hide();
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        ModalCropImage.Hide();
    }
    #endregion

    #region For Crop Image
    protected void uploadImage_Click(object sender, EventArgs e)
    {
        string documentPath = "";
        lblerror.Text = "";
        if (fileup1.HasFile)
        {
            string name = fileup1.FileName;
            string ext = System.IO.Path.GetExtension(name);
            if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org")
            {
                lblerror.Visible = true;
                lblerror.Text = "File format not supported.Image should be '.jpg or .jpeg or .png or .gif'";
                lblerror.CssClass = "RedErrormsg";              
                return;

            }
            else
            {
                uploadImage.Style.Add("display", "none");
                SaveImg.Style.Add("display", "block");
                int FileLength = fileup1.PostedFile.ContentLength;

                documentPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(fileup1.FileName).ToString();
                ViewState["filename"] = documentPath;
                //Session["Photofile"] = documentPath;
                fileup1.SaveAs(Server.MapPath("~\\UploadedPhoto\\" + documentPath));
                newimg.Src = "~\\UploadedPhoto\\" + documentPath;
            }
        }
    }

    protected void SaveImg_Click(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            //Get the Cordinates   
            if (!string.IsNullOrEmpty(X.Value))
            {
                int x = Convert.ToInt32(X.Value);
                int y = Convert.ToInt32(Y.Value);
                int w = Convert.ToInt32(W.Value);
                int h = Convert.ToInt32(H.Value);

                //Load the Image from the location
                System.Drawing.Image image = Bitmap.FromFile(
                      HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadedPhoto\\" + ViewState["filename"].ToString());
                //Create a new image from the specified location to                
                //specified height and width                
                Bitmap bmp = new Bitmap(w, h, image.PixelFormat);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, new Rectangle(0, 0, w, h),
                new Rectangle(x, y, w, h),
                GraphicsUnit.Pixel);

                //Save the file and reload to the control
                bmp.Save(HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["filename"].ToString(), image.RawFormat);
                cropedImage.Visible = true;
                Session["Photofile"] = Convert.ToString(ViewState["filename"]);
                cropedImage.ImageUrl = "~\\CroppedPhoto\\" + ViewState["filename"].ToString();
                newimg.Src = "";
                uploadImage.Style.Add("display", "block");
                SaveImg.Style.Add("display", "none");
            }

            else
            {
                newimg.Src = "";
                uploadImage.Style.Add("display", "block");
                SaveImg.Style.Add("display", "none");                
            }
        }
    }
    #endregion

    public void clearPhoto()
    {
        newimg.Src = "";
        cropedImage.ImageUrl = "";
    }
}