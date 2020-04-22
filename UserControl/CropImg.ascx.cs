using System;
using System.Data;
using DA_SKORKEL;
using System.IO;
using System.Drawing;
using System.Web;

public partial class CropImg : System.Web.UI.UserControl
{
    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
        }
    }

    protected void GetProfileDetails(int UserId)
    {
        //images/profile-photo.png
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
                    ViewState["imgPathPhysical"] = imgPathPhysical;
                    if (File.Exists(imgPathPhysical))
                    {
                        newimg.Src = @"../CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString();
                       // newimg.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();//1220201420618PM
                        btnDeleteimg.Visible = true;
                    }
                    else
                    {
                        newimg.Src = @"../images/profile-photo.png";
                    }
                }
                catch
                {
                    newimg.Src = @"../images/profile-photo.png";
                }
            }
            else
            {
                newimg.Src = @"../images/profile-photo.png";
            }
        }
    }

    #region For Crop Image

    protected void btnDeleteimg_Click(object sender, EventArgs e)
    {
       string paths= ViewState["imgPathPhysical"].ToString() ;
       if (File.Exists(paths))
       {
           File.Delete(paths);
           newimg.Src = @"../images/profile-photo.png";
           btnDeleteimg.Visible = false;
           ViewState["imgPathPhysical"] = "";
       }
        
    }

    protected void uploadImage_Click(object sender, EventArgs e)
    {
        btnDeleteimg.Visible = false;
        lblmsg.Text = "";
        newimg.Visible = true;
        string error = hdnErrorMsg.Value;
        string fileName = hdnPhotoName.Value;
        string filePath = hdnPhotoPath.Value;
        if (fileName!="")
        {
            if (error == "")
            {
                uploadImage.Style.Add("display", "none");
                fileup1.Style.Add("display", "none");
                SaveImg.Style.Add("display", "block");
                ViewState["filename"] = filePath;
                Session["Photofile"] = filePath;
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + filePath);
                ViewState["imgPathPhysical"] = imgPathPhysical;
                newimg.Src = @"../CroppedPhoto/" + filePath;
                imgViewImg.Visible = false;
            }
            else
            {
                lblmsg.Text = error;
            }
        }
        else
        {
            lblmsg.Text = "Please select image.";
        }
         hdnErrorMsg.Value=string.Empty;
         hdnPhotoName.Value = string.Empty;
         hdnPhotoPath.Value = string.Empty;
    }

    protected void SaveImg_Click(object sender, EventArgs e)
    {
        try
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
                     HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["filename"].ToString());
                //Create a new image from the specified location to                
                //specified height and width                
                Bitmap bmp = new Bitmap(w, h, image.PixelFormat);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, new Rectangle(0, 0, w, h),
                new Rectangle(x, y, w, h),
                GraphicsUnit.Pixel);

                //Save the file and reload to the control
                ViewState["Cropfilename"] = "Crp_" + Convert.ToString(ViewState["filename"]);
                bmp.Save(HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["Cropfilename"].ToString(), image.RawFormat);
                cropedImage.Visible = true;
                cropedImage.ImageUrl = "~\\CroppedPhoto\\" + ViewState["Cropfilename"].ToString();
                newimg.Src = "";
                SaveImg.Style.Add("display", "none");
                btnSave.Style.Add("display", "block");
                btnCancel.Style.Add("display", "block");
            }
        }
        catch
        { }
    }

    #endregion

    public void clearPhoto()
    {
        newimg.Src = "";
        cropedImage.ImageUrl = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        uploadImage.Style.Add("display", "block");
        fileup1.Style.Add("display", "block");
        btnSave.Style.Add("display", "none");
        btnCancel.Style.Add("display", "none");
        cropedImage.ImageUrl = "";
        GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Thread.Sleep(30000);
        DO_Registrationdetails objdoreg = new DO_Registrationdetails();
        DA_Registrationdetails objdareg = new DA_Registrationdetails();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (Convert.ToString(ViewState["Cropfilename"]) != null && Convert.ToString(ViewState["Cropfilename"]) != "")
            objdoreg.PhotoPath = Convert.ToString(ViewState["Cropfilename"]);
        else if (Convert.ToString(ViewState["filename"]) != null && Convert.ToString(ViewState["filename"]) != "")
            objdoreg.PhotoPath = Convert.ToString(ViewState["filename"]);
        else
            return;

        objdoreg.IpAddress = ip;
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdareg.AddEditDel_Profile(objdoreg, DA_Registrationdetails.RegistrationDetails.UpdateImg);
        Response.Redirect("~/Home.aspx?ActiveStatus=P");
    }

}