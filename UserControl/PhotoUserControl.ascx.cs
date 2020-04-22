using System;
using System.Drawing;
using DA_SKORKEL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
public partial class UserControl_PhotoUserControl : System.Web.UI.UserControl
{
    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            imCropped.Visible = false;
            GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
        }
    }

    protected void GetProfileDetails(int UserId)
    {
        DataTable dt;
        objDoProfile.RegistrationId = UserId;
        dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["vchrPhotoPath"].ToString() != "" && dt.Rows[0]["vchrPhotoPath"].ToString() != string.Empty)
            {
                try
                {
                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString());
                    if (File.Exists(imgPathPhysical))
                    {
                        ImFullImage.ImageUrl = @"../CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        // newimg.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();//1220201420618PM
                    }
                    else
                    {
                        ImFullImage.ImageUrl = "images/profile-photo.png";
                    }
                }
                catch
                {
                    ImFullImage.ImageUrl = "images/profile-photo.png";
                }
            }
            else
            {
                ImFullImage.ImageUrl = "images/profile-photo.png";
            }
        }
    }
    protected void btnUploadPhoto_Click(object sender, EventArgs e)
    {
        string documentPath = "";
        if (fupPhoto.HasFile)
        {
            if (fupPhoto.FileBytes.Length == 0)
                return;

            string ExtImage = System.IO.Path.GetExtension(fupPhoto.FileName).TrimStart(".".ToCharArray()).ToLower();
            if ((ExtImage != "jpeg") && (ExtImage != "jpg") && (ExtImage != "png") && (ExtImage != "gif") && (ExtImage != "bmp"))
            {
                return;
            }

            int FileLength = fupPhoto.PostedFile.ContentLength;

            documentPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(fupPhoto.FileName).ToString();
            Session["UploadedFileName"] = documentPath;
            fupPhoto.SaveAs(Server.MapPath("~\\CroppedPhoto\\" + Session["UploadedFileName"]));

            string MainImg = Request.ApplicationPath + @"/CroppedPhoto/" + Session["UploadedFileName"];

            ImFullImage.Visible = true;
            divCropImage.Visible = true;
            btnCrop.Visible = true;
            imCropped.Visible = false;
            ImFullImage.ImageUrl = MainImg;
        }
    }

    public void CropImage(string path, int X, int Y, int Width, int Height)
    {
        try
        {

            string outputFileName = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(Session["UploadedFileName"]).Replace("-", ""));
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromFile(path);
            Bitmap bitmap = new Bitmap(imageToBeResized, Width, Height);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            byte[] image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            System.IO.FileStream fs = new System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            fs.Write(image, 0, image.Length);
            imCropped.ImageUrl = outputFileName.Replace("%", " ");
            //using (MemoryStream memory = new MemoryStream())
            //{
            //    using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
            //    {
            //        using (Bitmap bmpCropped = new Bitmap(Width, Height))
            //        {
            //            using (Graphics g = Graphics.FromImage(bmpCropped))
            //            {
            //                Rectangle rectDestination = new Rectangle(0, 0, bmpCropped.Width, bmpCropped.Height);
            //                Rectangle rectCropArea = new Rectangle(X, Y, Width, Height);
            //                // g.DrawImage(img, rectDestination, rectCropArea, GraphicsUnit.Pixel);
            //                string SaveTo = Server.MapPath(".") + @"\CroppedPhoto\" + Convert.ToString(Session["UploadedFileName"]).Replace("-", "");
            //                bmpCropped.Save(SaveTo);
            //                string CroppedImg = Request.ApplicationPath + @"/CroppedPhoto/" + Convert.ToString(Session["UploadedFileName"]).Replace("-", "");
            //                imCropped.ImageUrl = CroppedImg;
            //            }
            //        }

            //        //using (Bitmap bmpCropped = new Bitmap(Width, Height))
            //        //{
            //        //    bmpCropped.Save(memory, ImageFormat.Jpeg);
            //        //    byte[] bytes = memory.ToArray();
            //        //    fs.Write(bytes, 0, bytes.Length);
            //        //   // string CroppedImg = Request.ApplicationPath + @"/CroppedPhoto/" + Convert.ToString(Session["UploadedFileName"]).Replace("-", "");
            //        //    imCropped.ImageUrl = outputFileName;
            //        //}
            //    }
            //}
          
        }
        catch (Exception excep)
        {
            excep.Message.ToString();
        }
    }
    protected void btnCrop_Click(object sender, EventArgs e)
    {
        int x, y, w, h;
        if (!int.TryParse(hfX.Value, out x))
        {
            //Set default x value
            x = 0;
        }

        if (!int.TryParse(hfY.Value, out y))
        {
            //Set default y value
            y = 0;
        }

        if (!int.TryParse(hfHeight.Value, out h))
        {
            //Set default height value
            h = 0;
        }

        if (!int.TryParse(hfWidth.Value, out w))
        {
            //Set default width value
            w = 0;
        }


        CropImage(Server.MapPath(".") + @"/CroppedPhoto/" + Session["UploadedFileName"], x, y, w, h);
        ImFullImage.Visible = false;
        btnCrop.Visible = false;
        imCropped.Visible = true;
        btnSave.Visible = true;
        Session["ImageCrop"] = "Yes";

        //System.IO.File.Delete(Server.MapPath(".") + @"/CroppedPhoto/" + Session["UploadedFileName"]);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DO_Registrationdetails objdoreg = new DO_Registrationdetails();
        DA_Registrationdetails objdareg = new DA_Registrationdetails();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (Convert.ToString(Session["UploadedFileName"]).Replace("-", "") != null && Convert.ToString(Session["UploadedFileName"]).Replace("-", "") != "")
            objdoreg.PhotoPath = Convert.ToString(Session["UploadedFileName"]).Replace("-", "");
        else       
            return;
        objdoreg.IpAddress = ip;
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdareg.AddEditDel_Profile(objdoreg, DA_Registrationdetails.RegistrationDetails.UpdateImg);
        Response.Redirect("Home.aspx?ActiveStatus=P");
    }
}