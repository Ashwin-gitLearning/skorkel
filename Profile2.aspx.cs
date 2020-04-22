using DA_SKORKEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Profile2 : System.Web.UI.Page
{
    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();
    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();
    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();
    DA_Scrl_UserEducationTbl objDAEdu = new DA_Scrl_UserEducationTbl();
    DO_Scrl_UserEducationTbl objDOEdu = new DO_Scrl_UserEducationTbl();
    DO_Scrl_UserExperienceTbl objUserExpDO = new DO_Scrl_UserExperienceTbl();
    DA_Scrl_UserExperienceTbl objUserExpDA = new DA_Scrl_UserExperienceTbl();
    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();
    DO_ProfileDocuments DocDO = new DO_ProfileDocuments();
    DA_ProfileDocuments DocDA = new DA_ProfileDocuments();
    DA_CategoryMaster DAobjCategory = new DA_CategoryMaster();
    DO_CategoryMaster objCategory = new DO_CategoryMaster();
    DataTable dt = new DataTable();
    DataTable dtUpdate = new DataTable();
    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();
    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();
    DA_SASubDoc objDASASubDoc = new DA_SASubDoc();
    DO_SASubDoc objDOSASubDoc = new DO_SASubDoc();
    static string APIURL = ConfigurationManager.AppSettings["APIURL"];
    static string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    static string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string UserTypeId = string.Empty, CurrentlyWork = string.Empty;
    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();
    bool isFriend = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        ViewState["BrowserName"] = browser.Browser;

        if (!IsPostBack)
        {
            if (Request.QueryString["Photo"] != null && Request.QueryString["Photo"] != "")
            {
                divSuccess.Style.Add("display", "block");
                // Master.BodyTag.Attributes.Add("class", "add-scroller");
            }
            if ((Request.QueryString["ActiveStatus"] != null && Request.QueryString["ActiveStatus"] != "") || (Request.QueryString["Status"] != null && Request.QueryString["Status"] != ""))
            {
                if (Request.QueryString["ActiveStatus"] == "Ac" && Request.QueryString["Status"] != null && Request.QueryString["Status"] != "Update" && Request.QueryString["Status"] != "Cancel" && Request.QueryString["Status"] != "")
                {
                    divSuccess.Style.Add("display", "block");
                    Master.BodyTag.Attributes.Add("class", "remove-scroller");
                    lblSuccess.Text = "Achievements saved successfully.";
                }
                else if (Request.QueryString["Status"] == "Update")
                {
                    divSuccess.Style.Add("display", "block");
                    Master.BodyTag.Attributes.Add("class", "remove-scroller");
                    lblSuccess.Text = "Achievements updated successfully.";
                }
                else
                {
                    divSuccess.Style.Add("display", "none");
                    lblSuccess.Text = "";
                    //Master.BodyTag.Attributes.Add("class", "remove-scroller");
                }
            }

            Session["SubmitTime"] = DateTime.Now.ToString();
            //  hdnCurrentPage.Value = "1";
            //  hdnTotalItem.Value = "10";
            int LoginId = Convert.ToInt32(Session["ExternalUserId"]);
            int FrndId = 0;
            ViewState["UserID"] = LoginId;
            ViewState["RegId"] = FrndId;
            hdnUserID.Value = Convert.ToString(LoginId);
            divAchivement.Style.Add("display", "none");
            divAddskill.Style.Add("display", "none");
            divEducation.Style.Add("display", "none");
            divEditProfile.Style.Add("display", "none");
            divDocumentUplaod.Style.Add("display", "none");
            AddWorkExp.Style.Add("display", "none");
            //  lnkChangeImage.Visible = true;
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                FrndId = Convert.ToInt32(Request.QueryString["RegId"]);

                if (LoginId == FrndId)
                {
                    GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
                    FrndId = 0;
                }
                else
                {
                    lblMessCount.OnClientClick = "return false;";
                    lblMessCount.CssClass = "text_decoration_color un-anchor";
                    lnkImageEdit.Visible = false;
                    //     divEditUserProfile.Style.Add("display", "none");
                    //     lnkChangeImage.Visible = false;
                    ViewState["UserID"] = LoginId;
                    ViewState["RegId"] = FrndId;
                    GetProfileDetails(Convert.ToInt32(Request.QueryString["RegId"]));
                    //     ConnectedUserAdd();
                    //      BindConnectedUser(FrndId);
                    //     BindGroup(FrndId);
                }
            }
            else
            {
                GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
            }

            if (FrndId == 0)
            {
                //    BindPostUpdate(LoginId, 0, 1); //self wall
                //   BindConnectedUser(LoginId);
                //   hdnloginIds.Value = Convert.ToString(LoginId);
                //   BindGroup(LoginId);
                LoadEndrosAndMsg();
                TotalscoreCount();
                lnkEditProfile.Visible = true;
                lnkSubmitDocs.Visible = true;
            }
            else
            {
                //lblEndorseCount.OnClientClick = "return false;";
                lnkEditProfile.Visible = false;
                lnkSubmitDocs.Visible = false;
                if (!IsConnectedUser(FrndId, hdnUserID.Value))
                    Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"]);
                //  BindPostUpdate(LoginId, FrndId, 2);
                //  BindConnectedUser(FrndId);
                // hdnloginIds.Value = Convert.ToString(FrndId);
                // BindGroup(FrndId);
                LoadEndrosAndMsg();
                // lnkWorkex.Visible = false;
                isFriend = true;
                TotalscoreCount();
            }

            if (Request.QueryString["Endrosment"] != "" && Request.QueryString["Endrosment"] != null)
            {
                WorkExp();
                EndorseClicked();//lnkWorkex.Style.Add("border-bottom", "none");
                //lnkDocuments.Style.Add("border-bottom", "none");
                //lnkEducation.Style.Add("border-bottom", "none");
                //lnkAchievements.Style.Add("border-bottom", "none");
                //PnlDocuments.Visible = false;
                //PnlWorkex.Visible = false;
                //PnlEduction.Visible = false;
                //PnlAchivement.Visible = false;
                //LoadUserExp();
                //LoadUserAreaExp();
                //LoadUserAreaSkill();
                //LoadUserexpYear();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
                //this.ClientScript.RegisterStartupScript(this.GetType(), "navigate", "window.onload = function() {window.location.hash='#SkillsetSection';}", true);
            }
            else if (Request.QueryString["ActiveStatus"] == "H")
            {
                //       HomeLoad();
                hdnActive.Value = "H";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else if (Request.QueryString["ActiveStatus"] == "D")
            {
                hdnActive.Value = "D";
                Document();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else if (Request.QueryString["ActiveStatus"] == "W")
            {
                hdnActive.Value = "W";
                WorkExp();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else if (Request.QueryString["ActiveStatus"] == "E")
            {
                hdnActive.Value = "E";
                Education();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else if (Request.QueryString["ActiveStatus"] == "Ac")
            {
                hdnActive.Value = "AC";
                Achivement();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }
            else
            {
                hdnActive.Value = "E";
                Education();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallMessageNotification();", true);
            }

            ddUploaderSubmitDoc.URL = "handler/UploadSubDoc.ashx";
            ddUploaderSubmitDoc.MaxFileSize = "5242880";
            ddUploaderSubmitDoc.DocType = "subDoc";
            ddUploaderSubmitDoc.ShowDelete = true;

            // To check score is public or private

            if (Convert.ToBoolean(Session["isScorePrivate"]) == true)
            {
                cbScorePrivate.Checked = true;
            }
        }
        //  Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);


    }
    protected void RedirectToDocument()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=D&RegId=" + Request.QueryString["RegId"]);
        }
        Response.Redirect("Profile2.aspx?ActiveStatus=D");
    }
    protected void RedirectToEducation()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=E&RegId=" + Request.QueryString["RegId"]);
        }
        Response.Redirect("Profile2.aspx?ActiveStatus=E");
    }
    protected void RedirectToAchivements(string flag)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"]);
        }
        if (flag == "true")
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"] + "&Status=Save");
        }
        else if (flag == "update")
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"] + "&Status=Update");
        }
        else
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"] + "&Status=Cancel");
        }
    }
    protected void RedirectToWorkExp()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("Profile2.aspx?ActiveStatus=W&RegId=" + Request.QueryString["RegId"]);
        }
        Response.Redirect("Profile2.aspx?ActiveStatus=W");
    }
    protected void upldrFile_delete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
    }

    protected void btnSuccess_Click(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        Master.BodyTag.Attributes.Add("class", "");
        Response.Redirect("Profile2.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"]);
    }

    protected void btncrpSave_Click(object sender, EventArgs e)
    {
        //Thread.Sleep(30000);
        string fileName = hdnPhotoName.Value;
        string filePath = hdnPhotoPath.Value;
        if (fileName != "")
        {
            ViewState["Cropfilename"] = "Crp_" + filePath;
            string[] data = hdnFileData.Value.Split(',');
            byte[] imageBytes = Convert.FromBase64String(data[1]);
            ViewState["Cropfilename"] = "Crp_" + filePath;
            Stream st = new MemoryStream(imageBytes);
            //Bitmap bmp = new Bitmap(st);
            System.Drawing.Image img = System.Drawing.Image.FromStream(st);
            if (Array.IndexOf(img.PropertyIdList, 0x112) > -1)
            {
                var prop = img.GetPropertyItem(0x112);
                int val = BitConverter.ToUInt16(prop.Value, 0);
                var rot = RotateFlipType.RotateNoneFlipNone;
                if (val == 3 || val == 4)
                    rot = RotateFlipType.Rotate180FlipNone;
                else if (val == 5 || val == 6)
                    rot = RotateFlipType.Rotate90FlipNone;
                else if (val == 7 || val == 8)
                    rot = RotateFlipType.Rotate270FlipNone;

                if (val == 2 || val == 4 || val == 5 || val == 7)
                    rot |= RotateFlipType.RotateNoneFlipX;

                if (rot != RotateFlipType.RotateNoneFlipNone)
                    img.RotateFlip(rot);
            }
            img.Save(HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["Cropfilename"].ToString());
            //string format = "";
            //System.Drawing.Imaging.ImageFormat f = System.Drawing.Imaging.ImageFormat.Jpeg;
            //try
            //{
            //    format = data[0].Split(';')[0].Split(':')[1].ToLower();
            //}
            //catch (Exception es) { return; }
            //if (format == "image/tif" || "image/tiff" == format)
            //{
            //    f = System.Drawing.Imaging.ImageFormat.Tiff;
            //}
            //else if (format == "image/png")
            //{
            //    f = System.Drawing.Imaging.ImageFormat.Png;
            //}
            //else if (format == "image/gif")
            //{
            //    f = System.Drawing.Imaging.ImageFormat.Gif;
            //}
            //else if (format == "image/bmp")
            //{
            //    f = System.Drawing.Imaging.ImageFormat.Bmp;
            //}
            //bmp.Save(HttpContext.Current.Request.PhysicalApplicationPath + "\\CroppedPhoto\\" + ViewState["Cropfilename"].ToString(), f);
        }
        DO_Registrationdetails objdoreg = new DO_Registrationdetails();
        DA_Registrationdetails objdareg = new DA_Registrationdetails();
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (Convert.ToString(ViewState["Cropfilename"]) != null && Convert.ToString(ViewState["Cropfilename"]) != "")
            objdoreg.PhotoPath = Convert.ToString(ViewState["Cropfilename"]);
        else if (filePath != null && filePath != "")
            objdoreg.PhotoPath = Convert.ToString(ViewState["filename"]);
        else
            return;

        objdoreg.IpAddress = ip;
        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdareg.AddEditDel_Profile(objdoreg, DA_Registrationdetails.RegistrationDetails.UpdateImg);
        //GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallCloseImage", "CloseImage();", true);

        Response.Redirect("~/Profile2.aspx?Photo=true");
        //Response.Write("<script language='javascript'>window.alert('Profile image updated successfully.');window.location='Profile2.aspx';</script>");

    }
    protected void LoadUserAreaExp()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            //  divSkillEditdelete.Visible = false;
            lnkAddskill.Visible = false;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
            if (dt.Rows.Count > 0)
            {
                lstAreaExpert.Visible = true;
                lstAreaExpert.DataSource = dt;
                lstAreaExpert.DataBind();
                divexistingSkils.Style.Add("display", "block");
                //       divSkillEditdelete.Visible = false;
            }
            else
            {
                divexistingSkils.Style.Add("display", "none");
                lstAreaExpert.Visible = false;
                lstAreaExpert.DataSource = null;
                lstAreaExpert.DataBind();
                // divSkillEditdelete.Visible = false;
            }
        }
        else
        {
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
            if (dt.Rows.Count > 0)
            {
                lstAreaExpert.Visible = true;
                lstAreaExpert.DataSource = dt;
                lstAreaExpert.DataBind();
                divexistingSkils.Style.Add("display", "block");
                //       divSkillEditdelete.Visible = true;
            }
            else
            {
                divexistingSkils.Style.Add("display", "none");
                lstAreaExpert.Visible = false;
                lstAreaExpert.DataSource = null;
                lstAreaExpert.DataBind();
                //      divSkillEditdelete.Visible = false;
            }
        }

    }
    protected void lstAreaExpert_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divUserexperenceED = (HtmlControl)e.Item.FindControl("divUserexperenceED");
        HtmlControl divEndr = (HtmlControl)e.Item.FindControl("divEndr");
        // HtmlImage imgPlus = (HtmlImage)e.Item.FindControl("imgPlus");
        LinkButton lnkEndrose = (LinkButton)e.Item.FindControl("lnkEndrose");
        Label lblEndronseCount = (Label)e.Item.FindControl("lblEndronseCount");
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");

        if (ViewState["RegId"].ToString() != "0")
        {
            //   imgPlus.Visible = true;
            divEndr.Attributes["class"] = "chip endorse-chip";
            lnkEndrose.Visible = true;
        }

        if (ViewState["RegId"].ToString() == "0")
        {
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        }
        else
        {
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
        }
        objUserExpDO.intUserSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetEndronseCount);
        if (dt.Rows.Count > 0)
        {
            lblEndronseCount.Text = dt.Rows[0]["Endorse"].ToString();
        }

    }
    protected void lstAreaExpert_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkdDelete");
        Label lblstrSkillName = (Label)e.Item.FindControl("lblstrSkillName");
        // PopUpCropImage.Style.Add("display", "none");
        if (e.CommandName == "EndronseSkill")
        {
            objRecmndDO.intInvitedUserId = Convert.ToInt32(ViewState["RegId"]);
            objRecmndDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
            objRecmndDO.StrRecommendation = lblstrSkillName.Text.Replace("'", "''").Replace("\n", "<br>");
            objRecmndDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (objRecmndDO.strIpAddress == null)
                objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            objRecmndDO.intSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
            objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Add);

            try
            {
                if (objRecmndDO.intOutRecommendationId != 0)
                {

                    StringBuilder url = new StringBuilder();
                    url.Append(APIURL);
                    url.Append("endorseUserSkill.action?");
                    url.Append("endorseByUserId =");
                    url.Append(UserTypeId);
                    url.Append(objRecmndDO.intAddedBy);
                    url.Append("&endorseToUserId =");
                    url.Append(UserTypeId);
                    url.Append(objRecmndDO.intInvitedUserId);
                    url.Append("&skillId=");
                    url.Append(objRecmndDO.intOutRecommendationId);

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                    ;
                    myRequest1.Method = "GET";
                    WebResponse myResponse1 = myRequest1.GetResponse();

                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();

                    objAPILogDO.strURL = url.ToString();
                    objAPILogDO.strAPIType = "User Endorse Skill";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = objdoreg.IpAddress;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }

            }
            catch (Exception ex) { }
            //LoadUserAreaExp();
            //LoadEndrosAndMsg();
            RedirectToWorkExp();
        }

    }
    protected void lnkCancelSkill_Click(object sender, EventArgs e)
    {

        objUserExpDO.dtFromDate = DateTime.Now;
        objUserExpDO.dtToDate = DateTime.Now;
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteUserSkills);
        RedirectToWorkExp();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork2", "CallWorkLI();", true);
    }
    protected void lnkSaveSkill_Click(object sender, EventArgs e)
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (ViewState["lstAreaSkill"] != null)
        {
            lblareaskill.Text = "";
            DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillUnqId);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["strUnqId"].ToString() != "")
                {
                    objUserExpDO.dtFromDate = DateTime.Now;
                    objUserExpDO.dtToDate = DateTime.Now;
                    objUserExpDO.strUnqId = dt.Rows[0]["strUnqId"].ToString();
                    objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateSkill);
                    LoadUserAreaSkill();
                    ClearExperience();
                    LoadUserAreaExp();
                }
            }
            else
            {
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDO.strUnqId = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "");
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.UpdateSkill);
                //LoadUserAreaSkill();
                //ClearExperience();
                //LoadUserAreaExp();
            }
            RedirectToWorkExp();
            //ViewState["lstAreaSkill"] = null;
            //TotalscoreCount();
            //divAddskill.Style.Add("display", "none");
            //lstAreaExpert.Visible = true;
            //lstAreaSkill.Visible = true;
        }
        else
        {
            LoadUserAreaSkill();
            ClearExperience();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveSkill", "callSaveSkill();", true);
            lblareaskill.Text = "Please add skill.";
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void btnAreaExpSave_Click(object sender, EventArgs e)
    {
        lblareaskill.Text = "";
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];
        if (txtAreaExpert.Text.Trim() != "")
        {
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDO.strSkillName = txtAreaExpert.Text.Trim();
            objUserExpDO.strIpAddress = ip;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objUserExpDO.strUnqId = "";
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.InsertUserExpSkill);
            txtAreaExpert.Text = "";
            LoadUserAreaSkill();
        }
        else
        {
            LoadUserAreaSkill();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAddSkill", "CallAddSkill();", true);
            lblareaskill.Text = "Please enter area of expertise.";
            return;
        }
        LoadUserAreaExp();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }
    protected void lstAreaSkill_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        lblareaskill.Text = "";
        HiddenField hdnintUserSkillId = (HiddenField)e.Item.FindControl("hdnintUserSkillId");
        LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkdDelete");
        if (e.CommandName == "DeleteExp")
        {
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDO.intUserSkillId = Convert.ToInt32(hdnintUserSkillId.Value);
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.DeleteUserSkill);
            if (ViewState["Editskillarea"] == null)
            {
                LoadUserAreaSkill();
            }
            else
            {
                LoadUserAreaExp();
                LoadlstArea();
            }
            TotalscoreCount();
        }

    }
    public void LoadlstArea()
    {
        divAddskill.Style.Add("display", "block");
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillByUserId);
        if (dt.Rows.Count > 0)
        {
            ViewState["Editskillarea"] = "1";
            ViewState["lstAreaSkill"] = "1";
            lstAreaSkill.Visible = true;
            lstAreaSkill.DataSource = dt;
            lstAreaSkill.DataBind();
        }
        else
        {
            ViewState["Editskillarea"] = null;
            ViewState["lstAreaSkill"] = null;
            lstAreaSkill.Visible = false;
            lstAreaSkill.DataSource = null;
            lstAreaSkill.DataBind();
        }
    }
    protected void LoadEndrosAndMsg()
    {
        objDoProfile.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objDoProfile.ConnectRegistrationId = Convert.ToInt32(ViewState["RegId"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            objDoProfile.RegistrationId = Convert.ToInt32(Request.QueryString["RegId"]);
            DataTable dtEndorse = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetEndorseCount);
            if (dtEndorse.Rows.Count > 0)
            {
                int Endorse = Convert.ToInt32(dtEndorse.Rows[0]["Endorse"]);
                lblEndorseCount.Text = Convert.ToString(Endorse);
            }
            else
            {
                lblEndorseCount.Text = "0";
            }
        }
        else
        {
            DataTable dtEndorse = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetEndorseCount);
            if (dtEndorse.Rows.Count > 0)
            {
                int Endorse = Convert.ToInt32(dtEndorse.Rows[0]["Endorse"]);
                lblEndorseCount.Text = Convert.ToString(Endorse);
            }
            else
            {
                lblEndorseCount.Text = "0";
            }
        }
        objDoProfile.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
    }

    protected bool IsConnectedUser(int frndid, string loginId)
    {

        objdonetwork.RegistrationId = Convert.ToInt32(loginId);
        dt = objdanetwork.GetUserConnections(objdonetwork, DA_Networks.NetworkDetails.GetTopFriendList);
        if (dt.Rows.Count > 0)
        {
            ViewState["FriendList"] = dt;
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("intinviteduserid=" + frndid.ToString());
                if (dr.Count() > 0)
                    return true;
            }
        }
        return false;

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
    private void BindEditTopSubjectList(string Id)
    {
        DataTable dtTopSub = new DataTable();
        objCategory.intPostQuestionId = Convert.ToInt32(Id);
        dtTopSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.GetEditDocSubjectList);
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
    protected void TotalscoreCount()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            objDOPoint.UserID = Convert.ToInt32(ViewState["RegId"]);            
        }
        else
        {
            objDOPoint.UserID = Convert.ToInt32(ViewState["UserID"]);
        }
        ScoreService.ScoreObject scores = ScoreService.getScore(objDOPoint.UserID);

        lblMessCount.Text = Convert.ToString(scores.TotalScore);
        AScore.InnerText = Convert.ToString(scores.AScore);
        BScore.InnerText = Convert.ToString(scores.BScore);
        CScore.InnerText = Convert.ToString(scores.CScore);
        DScore.InnerText = Convert.ToString(scores.DScore);
        ViewState["Totalexp"] = null;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDegreeList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName like @Search + '%'";
                com.CommandText = "Select Top 5 strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName Like '%' + LTRIM(RTRIM(@Search)) + '%' Order By strDegreeName";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strDegreeName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }
    protected void lnkDocuments_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            //Response.Redirect("Home.aspx?RegId=" + Request.QueryString["RegId"] + "&ActiveStatus=D");
            isFriend = true;
        }
        divDocumentUplaod.Style.Add("display", "none");
        hdnActive.Value = "D";
        Document();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
    }

    protected void Document()
    {
        //  lnkDocuments.Style.Add("border-bottom", "2px solid #00b6bc");
        //lnkHome.Style.Add("border-bottom", "none");
        lnkWorkex.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link active show";
        lnkAchievements.CssClass = "nav-link";
        //lnkDocuments.CssClass = "nav-link active show";
        if (isFriend)
            lnkuploadDoc.Visible = false;
        else
            lnkuploadDoc.Visible = true;
        PnlDocuments.Visible = true;
        // PnlHome.Visible = false;
        PnlEduction.Visible = false;
        PnlWorkex.Visible = false;
        PnlAchivement.Visible = false;
        BindDocuments();
        BindSubjectList();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }
    //protected void lnkDelete_Click(object sender, EventArgs e)
    //{
    //    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteDocument);
    //    uploadDoc.Visible = true;
    //    try
    //    {
    //        string imgPathPhysical = Server.MapPath("~/UploadDocument/" + hdnUploadFile.Value.ToString());
    //        if (File.Exists(imgPathPhysical))
    //        {
    //            File.Delete(imgPathPhysical);
    //            lnkDeleteDoc123.Style.Add("display", "none");
    //            lblfilenamee.Text = "";
    //            hdnUploadFile.Value = "";
    //            hdnUploadFile1.Value = "";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    clearDoc();
    //    BindDocuments();
    //    divDeletesucess.Style.Add("display", "none");
    //}

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
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        divDocumentUplaod.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI", "CallDocLI();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallDocMiddle();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
        //  BindDocuments();
        clearDoc();
    }
    protected void lblEndorseCount_click(object sender, EventArgs e)
    {
        //PopUpCropImage.Style.Add("display", "none");
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            if (Request.QueryString["RegId"] != ViewState["UserID"].ToString())
            {
                RedirectToWorkExp();
            }
            else
            {
                EndorseClicked();
            }
        }
        else
        {
            EndorseClicked();

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork5", "CallWorkLI();", true);
            //  txtAreaExpert.Focus();
            //   ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
            //this.ClientScript.RegisterStartupScript(this.GetType(), "navigates", "window.onload = function() {window.location.hash='#SkillsetSection';}", true);
        }

    }
    protected void EndorseClicked()
    {
        // Response.Redirect("Home.aspx?Endrosment=E");
        lnkWorkex.CssClass = "nav-link active show";
        lnkDocuments.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link";
        lnkAchievements.CssClass = "nav-link";
        PnlDocuments.Visible = false;
        PnlWorkex.Visible = true;
        PnlEduction.Visible = false;
        PnlAchivement.Visible = false;
        LoadUserExp();
        LoadUserAreaExp();
        LoadUserAreaSkill();
        LoadUserexpYear();
        divAddskill.Style.Add("display", "block");
    }
    protected void LoadAchivment()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkAddachive.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        //divDeletesucess.Style.Add("display", "none");
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompititionLists(string prefixText, int count, string contextKey)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select CompName,intMilestoneId from Scrl_CompetitionMasterTbl where " + "CompName like @Search + '%'";
                //com.CommandText = "Select CompName, intMilestoneId from Scrl_CompetitionMasterTbl where " + "LTRIM(RTRIM(CompName)) like LTRIM(RTRIM(@Search)) + '%' And competitiontype=@CompType";
                //com.CommandText = "Select CompName, intMilestoneId from Scrl_CompetitionMasterTbl where " + "LTRIM(RTRIM(CompName)) Like '%' + LTRIM(RTRIM(@Search)) + '%' And Competitiontype=@CompType";
                com.CommandText = "Select Top 5 CompName, intMilestoneId from Scrl_CompetitionMasterTbl where " + "LTRIM(RTRIM(CompName)) Like '%' + LTRIM(RTRIM(@Search)) + '%' And Competitiontype=@CompType Order By CompName";
                com.Parameters.AddWithValue("@Search", prefixText.Trim());
                com.Parameters.AddWithValue("@CompType", contextKey.Trim());
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["CompName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }
    protected void Achivement()
    {
        //Master.BodyTag.Attributes.Add("class", "add-scroller");
        lnkAchievements.CssClass = "nav-link active show";
        // lnkHome.Style.Add("border-bottom", "none");
        // lnkDocuments.Style.Add("border-bottom", "none");
        if (isFriend)
            lnkAddachive.Visible = false;
        else
            lnkAddachive.Visible = true;
        lnkWorkex.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link";
        lnkDocuments.CssClass = "nav-link";
        //  PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        PnlWorkex.Visible = false;
        PnlEduction.Visible = false;
        PnlAchivement.Visible = true;
        LoadAchivment();
        LoadMilestones();
    }
    protected void Bind_ddlPosition(string Ids)
    {
        Bind_ddlPosition(Ids, null);
    }

    protected void Bind_ddlPosition(string Ids, string selectedPos)
    {
        ddlPosition.Items.Clear();
        //ddlPosition.Items.Add("Select State");
        objDOEdu.PositionIdList = Ids;
        DataTable dtt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetPositionList);

        if (dtt.Rows.Count > 0)
        {
            //divddlPosition.Visible = true;
            //ddlPosition.Visible = true;
            ddlPosition.DataSource = dtt;
            ddlPosition.DataTextField = "strPositionName";
            ddlPosition.DataValueField = "intPostionId";
            ddlPosition.DataBind();
            if (selectedPos != null)
            {
                ddlPosition.SelectedValue = selectedPos;
            }

        }
        else
        {
            ddlPosition.DataSource = null;
            ddlPosition.DataBind();
            ddlPosition.Items.Insert(0, "Position");
        }
    }

    protected void ddlMilestone_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = ddlMilestone.SelectedIndex;
        ClearAchivement();
        ddlMilestone.SelectedIndex = selectedIndex;
        if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,4,5";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Moot";
                AutoCompleteExtender33.ContextKey = "Moot";
                //lnkContinue.Visible = true;
            }
            else
            {
                lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,6";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Debate";
                AutoCompleteExtender33.ContextKey = "Debate";
            }
            else
            {
                lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "ADR";
                AutoCompleteExtender33.ContextKey = "ADR";
            }
            else
            {
                lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Publications")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                string Ids = "";
                //Bind_ddlPosition(Ids);
                divJournalName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divDescription.Visible = false;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
                hdnCompetitionType.Value = "Publications";
                AutoCompleteExtender44.ContextKey = "Publications";
            }
            else
            {
                lnkContinue.Visible = true;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "7,8,9,10,11";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCommitteeName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Committee Membership";
                AutoCompleteExtender33.ContextKey = "Committee Membership";
            }
            else
            {
                lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Student Body")
        {
            LinkButton4.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "8,9";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Student Body";
                AutoCompleteExtender33.ContextKey = "Student Body";
            }
            else
            {
                lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else
        {
            string Ids = "";
            Bind_ddlPosition(Ids);
            divCompetition.Visible = false;
            divAdditionalAwrd.Visible = false;
            divCommitteeName.Visible = false;
            divDescription.Visible = false;
            divJournalName.Visible = false;
            AutoCompleteExtender33.ContextKey = "0";
            lnkContinue.Visible = false;
            LinkButton4.Visible = false;
            lnkSaveAchivemnt.Visible = false;
            divddlPosition.Visible = false;
            ddlPosition.Visible = false;
        }
    }

    protected void lnkContinue_Click(object sender, EventArgs e)
    {
        if (ddlMilestone.SelectedItem.Text == "Publications")
        {
            divddlPosition.Visible = false;
            ddlPosition.Visible = false;
            string Ids = "";
            Bind_ddlPosition(Ids);
            divJournalName.Visible = true;
            divCompetition.Visible = false;
            divAdditionalAwrd.Visible = false;
            divCommitteeName.Visible = false;
            divDescription.Visible = false;
            hdnCompetitionType.Value = "Publications";
            AutoCompleteExtender44.ContextKey = "Publications";
        }
        else if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
            string Ids = "1,2,3,4,5";
            Bind_ddlPosition(Ids);
            divCompetition.Visible = true;
            divAdditionalAwrd.Visible = true;
            divDescription.Visible = true;
            divCommitteeName.Visible = false;
            divJournalName.Visible = false;
            hdnCompetitionType.Value = "Moot";
            AutoCompleteExtender33.ContextKey = "Moot";
        }
        else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
            string Ids = "1,2,3,6";
            Bind_ddlPosition(Ids);
            divCompetition.Visible = true;
            divAdditionalAwrd.Visible = true;
            divDescription.Visible = true;
            divCommitteeName.Visible = false;
            divJournalName.Visible = false;
            hdnCompetitionType.Value = "Debate";
            AutoCompleteExtender33.ContextKey = "Debate";
        }
        else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
            string Ids = "1,2,3";
            Bind_ddlPosition(Ids);
            divCompetition.Visible = true;
            divAdditionalAwrd.Visible = true;
            divDescription.Visible = true;
            divCommitteeName.Visible = false;
            divJournalName.Visible = false;
            hdnCompetitionType.Value = "ADR";
            AutoCompleteExtender33.ContextKey = "ADR";
        }
        else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
            string Ids = "7,8,9,10,11";
            Bind_ddlPosition(Ids);
            divDescription.Visible = true;
            divCommitteeName.Visible = true;
            divCompetition.Visible = false;
            divAdditionalAwrd.Visible = false;
            divJournalName.Visible = false;
            hdnCompetitionType.Value = "Committee Membership";
            AutoCompleteExtender33.ContextKey = "Committee Membership";
        }
        else if (ddlMilestone.SelectedItem.Text == "Student Body")
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
            string Ids = "8,9";
            Bind_ddlPosition(Ids);
            divDescription.Visible = true;
            divCompetition.Visible = false;
            divAdditionalAwrd.Visible = false;
            divCommitteeName.Visible = false;
            divJournalName.Visible = false;
            hdnCompetitionType.Value = "Student Body";
            AutoCompleteExtender33.ContextKey = "Student Body";
        }
        else
        {
            divddlPosition.Visible = true;
            ddlPosition.Visible = true;
        }
        divCtrls.Visible = true;
        lnkContinue.Visible = false;
        lnkSaveAchivemnt.Visible = true;
    }

    protected void lnkSaveAchivemnt_Click(object sender, EventArgs e)
    {
        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        int CompID = 0;
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtCompitition.Text != "" || txtCommitteeName.Text != "" || txtJournalName.Text != "" || ddlMilestone.SelectedItem.Text == "Student Body")
        {
            if (ddlMilestone.SelectedItem.Text != "Type of Milestone")
            {
                objDOEdu.intMilestoneId = Convert.ToInt32(ddlMilestone.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAch", "callSaveAch();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchi", "OverlayBody();", true);
                lblAchivmentmsg.Text = "Please select milestone.";
                return;
            }
            if (ddlPosition.SelectedItem.Text != "Position")
            {
                objDOEdu.intPostionId = Convert.ToInt32(ddlPosition.SelectedValue);
            }
            else if (ddlPosition.Visible == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAchs", "callSaveAch();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAch", "OverlayBody();", true);
                lblAchivmentmsg.Text = "Please select position.";
                return;
            }

            objDOEdu.dtDate = DateTime.Now;
            objDOEdu.strEducationAchievement = "Achievement";
            objDOEdu.strIpAddress = ip;
            //if (string.IsNullOrEmpty(lblId.Text))
            //{
            if (txtCompitition.Text.Trim() != "" && txtCompitition.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                objDOEdu.CompID = CompID;
            }
            if (txtCommitteeName.Text.Trim() != "" && txtCommitteeName.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtCommitteeName.Text.Trim();
            }
            if (txtJournalName.Text.Trim() != "" && txtJournalName.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtJournalName.Text.Trim();
                CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                objDOEdu.CompID = CompID;
            }
            objDOEdu.CompLavel = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompLavel);
            if (ddlPosition.SelectedItem.Text != "Position")
            {
                objDOEdu.strPosition = ddlPosition.SelectedItem.Text;
            }
            objDOEdu.strMilestone = ddlMilestone.SelectedItem.Text;
            objDOEdu.strAdditionalAward = txtAdditionalAwrd.Text.Trim();
            objDOEdu.strAchDescription = txtAchivDescription.InnerText.Trim();
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

            if (ViewState["hdnintAchivmentId"] == null)
            {
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.InsertAchivement);
                RedirectToAchivements("true");
            }
            else
            {
                objDOEdu.intAchivmentId = Convert.ToInt32(ViewState["hdnintAchivmentId"]);
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.UpdateAchivement);
                lnkSaveAchivemnt.Text = "Save";
                RedirectToAchivements("update");
            }
            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    String url = APIURL + "updateUserEducationDetails.action?" +
                                "uid=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                "&educationId=" + objDOEdu.intOutId +
                                "&degree=" + objDOEdu.strDegree +
                                "&institution=" + objDOEdu.strInstituteName +
                                "&year=" + objDOEdu.intYear +
                                "&educationType=" + objDOEdu.strEducationAchievement +
                                "&achievementMarked=" + 1 +
                                "&description=" + objDOEdu.strSpclLibrary;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strAPIType = "Profile Achivement";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
            //RedirectToAchivements("true");
            //LoadAchivment();
            //ClearAchivement();
            //divAchivement.Style.Add("display", "none");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi4", "CallAchLI();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchiv", "OverlayBody();", true);
            return;
        }

    }

    protected void lnkProfileSave_click(object sender, EventArgs e)
    {
        // sddsdsd.Visible = true;
        // mydiv.Style.Add("display", "block");
        // PopUpCropImage.Style.Add("display", "none");
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        //Update user details.
        objdoreg.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
        objdoreg.ModifiedBy = Convert.ToInt32(ViewState["UserID"]);

        if (txtName.Text.Trim() == "")
        {
            lblProfilemsg.Text = "Please enter name.";
            divEditProfile.Style.Add("display", "block");
            return;
        }
        if (txtName.Text.Trim() == "")
        {
            lblProfilemsg.Text = "Please enter name.";
            divEditProfile.Style.Add("display", "block");
            return;
        }
        string name = txtName.Text.Trim();
        string[] ssize = name.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (ssize.Count() > 2)
        {
            if (ssize.Count() > 3)
            {
                objdoreg.FirstName = ssize[0] + " " + ssize[1] + " " + ssize[2];
                objdoreg.LastName = ssize[3];
            }
            else
            {
                objdoreg.FirstName = ssize[0] + " " + ssize[1];
                objdoreg.LastName = ssize[2];
            }
        }
        else if (ssize.Count() < 2)
        {
            lblProfilemsg.Text = "Please enter full name";
            divEditProfile.Style.Add("display", "block");
            return;
        }
        else
        {
            objdoreg.FirstName = ssize[0];
            objdoreg.LastName = ssize[1];
        }

        if (txtGender.Text == "Male" || txtGender.Text == "male")
        {
            objdoreg.Sex = "M";
        }
        else if (txtGender.Text == "Female" || txtGender.Text == "female")
        {
            objdoreg.Sex = "F";
        }

        if (txtPhone.Text.Trim() != "")
        {
            objdoreg.Mobile = Convert.ToInt64(txtPhone.Text.Trim());
        }

        objdoreg.Languages = txtLanguage.Text.Trim().Replace("'", "''");
        objdoreg.IpAddress = ip;
        objdareg.AddEditDel_Profile(objdoreg, DA_Registrationdetails.RegistrationDetails.Update);
        divEditProfile.Style.Add("display", "none");
        GetProfileDetails(Convert.ToInt32(ViewState["UserID"]));
        lblProfilemsg.Text = "";
        Response.Redirect("Profile2.aspx?ActiveStatus=" + hdnActive.Value);
    }
    protected void ClearAchivement()
    {
        ViewState["hdnintAchivmentId"] = null;
        txtCompitition.Text = "";
        ddlPosition.SelectedIndex = 0;
        ddlMilestone.SelectedIndex = 0;
        txtAdditionalAwrd.Text = "";
        txtAchivDescription.InnerText = "";
        lblAchivmentmsg.Text = "";
        txtJournalName.Text = "";
        txtCommitteeName.Text = "";
        ViewState["Callsmoothmenu"] = "smooth";
    }
    protected void LoadMilestones()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetMilestones);

        if (dt.Rows.Count > 0)
        {
            ddlMilestone.DataSource = dt;
            ddlMilestone.DataTextField = "strMilestoneName";
            ddlMilestone.DataValueField = "intMilestoneId";
            ddlMilestone.DataBind();
            ddlMilestone.Items.Insert(0, new ListItem("Type of Milestone", "0"));
        }
        else
        {
            ddlMilestone.DataSource = null;
            ddlMilestone.DataBind();
        }

        //DataTable dtt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetPosition);

        //if (dtt.Rows.Count > 0)
        //{
        //ddlPosition.DataSource = dtt;
        //ddlPosition.DataTextField = "strPositionName";
        //ddlPosition.DataValueField = "intPostionId";
        //ddlPosition.DataBind();
        ddlPosition.Items.Insert(0, new ListItem("Position", "0"));
        //}
        //else
        //{
        //    ddlPosition.DataSource = null;
        //    ddlPosition.DataBind();
        //}

    }
    protected void aAddworkExp_click(object sender, EventArgs e)
    {
        AddWorkExp.Style.Add("display", "block");
        LoadUserExp();
        LoadUserAreaExp();
        LoadUserAreaSkill();
        LoadUserexpYear();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripte", "CallExpNumaric();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "CallWorkMiddle();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork7", "CallWorkLI();", true);
        ClearExperience();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "addWrkCli", "OverlayBody();", true);
    }
    protected void lnkWorkex_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            isFriend = true;
        }
        hdnActive.Value = "W";
        AddWorkExp.Style.Add("display", "none");
        divAddskill.Style.Add("display", "none");
        WorkExp();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork", "CallWorkLI();", true);
    }
    protected void lnkEducation_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            isFriend = true;
        }
        hdnActive.Value = "E";
        //divEducation.Style.Add("display", "none");
        Education();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);
    }
    protected void lnkCancelAchivemnt_Click(object sender, EventArgs e)
    {
        RedirectToAchivements("false");
        ClearAchivement();
        //divAchivement.Style.Add("display", "none");
        //LoadAchivment();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi1", "CallAchLI();", true);
    }
    protected void lnkAddachive_Click(object sender, EventArgs e)
    {
        divAchivement.Style.Add("display", "block");
        LoadAchivment();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi2", "CallAchLI();", true);
        ClearAchivement();
    }
    protected void lnkAchievements_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            isFriend = true;
        }
        hdnActive.Value = "Ac";
        Achivement();
        divAchivement.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi", "CallAchLI();", true);
    }
    protected void WorkExp()
    {
        if (isFriend)
        {
            aAddworkExp.Visible = false;
            lnkAddskill.Visible = false;
        }
        else
        {
            aAddworkExp.Visible = true;
            lnkAddskill.Visible = true;
        }
        lnkAchievements.CssClass = "nav-link";
        //lnkWorkex.Style.Add("border-bottom", "2px solid #00b6bc");
        //lnkHome.Style.Add("border-bottom", "none");
        //lnkDocuments.Style.Add("border-bottom", "none");
        //lnkEducation.Style.Add("border-bottom", "none");
        //lnkAchievements.Style.Add("border-bottom", "none");
        //PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        lnkDocuments.CssClass = "nav-link";
        lnkWorkex.CssClass = "nav-link active show";
        lnkEducation.CssClass = "nav-link";
        PnlWorkex.Visible = true;
        PnlEduction.Visible = false;
        //PnlEduction.Visible = false;
        PnlAchivement.Visible = false;
        LoadUserExp();
        LoadUserAreaExp();
        LoadUserAreaSkill();
        LoadUserexpYear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);

    }
    protected void lstAchivement_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divAchivementED = (HtmlControl)e.Item.FindControl("divAchivementED");
        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divAchivementED.Style.Add("display", "none");
        }

        //if (e.Item.ItemType == ListViewItemType.DataItem)
        //{
        //    DropDownList ddlItem = (DropDownList)e.Item.FindControl("ddlPosition");
        //    var rowView = e.Item.DataItem as DataRowView;
        //    //int id = (int)rowView["ID"];  // whatever
        //    // get data from id ...
        //    //ddlItem.DataSouce = data;
        //    //ddlItem.DataTextField = "Name";
        //    //ddlItem.DataValueField = "ID";
        //    //ddlItem.DataBind();
        //}
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //ddlPosition.Attributes.Add("onclick", "ddlTestClientClick(this);");
    }

    protected void lstAchivement_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int CompID = 0;
        HiddenField hdnintAchivmentId = (HiddenField)e.Item.FindControl("hdnintAchivmentId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intAchivmentId = Convert.ToInt32(hdnintAchivmentId.Value);
        string selectedPos = null;
        if (e.CommandName == "EditAch")
        {
            //   PopUpCropImage.Style.Add("display", "none");
            lnkSaveAchivemnt.Text = "Update";
            LinkButton4.Visible = true;
            LoadMilestones();
            divAchivement.Style.Add("display", "block");
            ViewState["hdnintAchivmentId"] = hdnintAchivmentId.Value;
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecordAchivemrnt);

            if (dt.Rows.Count > 0)
            {
                lnkContinue.Visible = false;
                lnkSaveAchivemnt.Visible = true;

                //txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                ddlMilestone.SelectedValue = Convert.ToString(dt.Rows[0]["intMilestoneId"]);
                txtAdditionalAwrd.Text = Convert.ToString(dt.Rows[0]["strAdditionalAward"]);
                txtAchivDescription.InnerText = Convert.ToString(dt.Rows[0]["strDescription"]);

                if (Convert.ToString(dt.Rows[0]["intPostionId"]) != "" && Convert.ToString(dt.Rows[0]["intPostionId"]) != null)
                {
                    try
                    {
                        divddlPosition.Visible = true;
                        divCtrls.Visible = true;
                        ddlPosition.Visible = true;
                        selectedPos = Convert.ToString(dt.Rows[0]["intPostionId"]);
                        //Bind_ddlPosition(ddlMilestone.SelectedValue);
                        //ddlPosition.SelectedValue = dt.Rows[0]["intPostionId"].ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
                {
                    //if (Convert.ToString(dt.Rows[0]["intPostionId"]) != "" && Convert.ToString(dt.Rows[0]["intPostionId"]) != null)
                    //{
                    //    try
                    //    {
                    //        divddlPosition.Visible = true;
                    //        divCtrls.Visible = true;
                    //        ddlPosition.Visible = true;
                    //        Bind_ddlPosition(ddlMilestone.SelectedValue);
                    //        //ddlPosition.SelectedValue = dt.Rows[0]["intPostionId"].ToString().Trim();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //    }
                    //}

                    //string eventTarget = Request.Params.Get("__EVENTTARGET").ToString();
                    //if (eventTarget == ddlPosition.ClientID)
                    //{
                    //    //BindDdlTest();
                    string Ids = "1,2,3,4,5";
                    Bind_ddlPosition(Ids, selectedPos);
                    //}

                    //divCtrls.Visible = true;
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    //divddlPosition.Visible = true;
                    //ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Moot";
                    AutoCompleteExtender33.ContextKey = "Moot";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
                {
                    string Ids = "1,2,3,6";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCtrls.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Debate";
                    AutoCompleteExtender33.ContextKey = "Debate";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
                {
                    string Ids = "1,2,3";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "ADR";
                    AutoCompleteExtender33.ContextKey = "ADR";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else if (ddlMilestone.SelectedItem.Text == "Publications")
                {
                    //string Ids = "";
                    //Bind_ddlPosition(Ids);
                    divJournalName.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divDescription.Visible = false;
                    divddlPosition.Visible = false;
                    hdnCompetitionType.Value = "Publications";
                    AutoCompleteExtender44.ContextKey = "Publications";
                    txtJournalName.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtJournalName.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
                {
                    string Ids = "7,8,9,10,11";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Committee Membership";
                    AutoCompleteExtender33.ContextKey = "Committee Membership";
                    txtCommitteeName.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else if (ddlMilestone.SelectedItem.Text == "Student Body")
                {
                    string Ids = "8,9";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divDescription.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Student Body";
                    AutoCompleteExtender33.ContextKey = "Student Body";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                    objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                    CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                }
                else
                {
                    //string Ids = "";
                    //Bind_ddlPosition(Ids);
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divDescription.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = false;
                    CompID = 0;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        }
    }

    protected void Education()
    {
        if (isFriend)
        {
            lnlAddEducation.Visible = false;
        }
        else
        {
            lnlAddEducation.Visible = true;
        }
        lnkAchievements.CssClass = "nav-link";
        //lnkEducation.Style.Add("border-bottom", "2px solid #00b6bc");
        //lnkHome.Style.Add("border-bottom", "none");
        //lnkDocuments.Style.Add("border-bottom", "none");
        //lnkWorkex.Style.Add("border-bottom", "none");
        //lnkAchievements.Style.Add("border-bottom", "none");
        //PnlHome.Visible = false;
        PnlDocuments.Visible = false;
        lnkWorkex.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link active show";
        lnkDocuments.CssClass = "nav-link";
        PnlWorkex.Visible = false;
        PnlEduction.Visible = true;
        PnlAchivement.Visible = false;
        //PnlEduction.Visible = true;
        //PnlAchivement.Visible = false;
        LoadEducation();
        LoadYear();

    }
    protected void lnkAddSkill_Click(object sender, EventArgs e)
    {
        divAddskill.Style.Add("display", "block");
        LoadlstArea();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork5", "CallWorkLI();", true);
    }
    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        //divDeletesucess.Style.Add("display", "none");
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        //if (hdnintPostId.Value != "")
        //{
        //    objstatusDO.intStatusUpdateId = Convert.ToInt32(hdnintPostId.Value);
        //    objstatusDA.AddEditDel_Scrl_UserStatusUpdateTbl(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.Delete);
        //    objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.intActionId = Convert.ToInt32(hdnintPostId.Value);
        //    objLog.strAction = "Wall Post";
        //    objLog.strActionName = hdnstrPostDescriptiondele.Value;
        //    objLog.strIPAddress = ip;
        //    objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.SectionId = 1;   // User Wall Post
        //    objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        //    BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        //    Session.Remove("PostDelete");
        //    Session.Remove("hdnPostUpdateId");
        //    Session.Remove("lblPostDescription");
        //    hdnintPostId.Value = "";
        //    hdnstrPostDescriptiondele.Value = "";
        //    divDeletesucess.Style.Add("display", "none");
        //}
        //else if (hdnintPostceIdelet.Value != "")
        //{
        //    objstatusDO.intCommentId = Convert.ToInt32(hdnintPostceIdelet.Value);
        //    DataTable dtDelete = new DataTable();
        //    dtDelete = objstatusDA.GetLikeDataTable(objstatusDO, DA_Scrl_UserStatusUpdateTbl.Scrl_UserStatusUpdateTbl.DeleteComment);
        //    if (ISAPIURLACCESSED != "0")
        //    {
        //        string UserURL = APIURL + "removeComment.action?userId =" + ViewState["UserID"] +
        //            "&commentId=" + objstatusDO.intCommentId;
        //        try
        //        {
        //            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL);
        //            myRequest1.Method = "GET";
        //            WebResponse myResponse1 = myRequest1.GetResponse();
        //            if (ISAPIResponse != "0")
        //            {
        //                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
        //                String result = sr.ReadToEnd();

        //                objAPILogDO.strURL = UserURL;
        //                objAPILogDO.strAPIType = "Wall User remove Comment";
        //                objAPILogDO.strResponse = result;
        //                objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        //                objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
        //            }
        //        }
        //        catch { }

        //    }
        //    divDeletesucess.Style.Add("display", "none");
        //    if (ViewState["RegId"] == null || ViewState["RegId"].ToString() == "0")
        //    {
        //        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), 0, 1); //self wall
        //    }
        //    else
        //    {
        //        BindPostUpdate(Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["RegId"]), 2); //frnd wall
        //    }

        //    objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.intActionId = Convert.ToInt32(hdnintPostceIdelet.Value);
        //    objLog.strAction = "Wall Post Comment";
        //    objLog.strActionName = hdnstrPostDescriptioncedel.Value;
        //    objLog.strIPAddress = ip;
        //    objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.SectionId = 1;   // User Wall Post 
        //    objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        //    hdnintPostceIdelet.Value = "";
        //    hdnstrPostDescriptioncedel.Value = "";
        //}
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
            divDeletesucess.Style.Add("display", "none");
        }
        else if (hdnintworkeIdelet.Value != "")
        {
            AddWorkExp.Style.Add("display", "none");
            objUserExpDO.intExperienceId = Convert.ToInt32(hdnintworkeIdelet.Value);
            objUserExpDO.dtFromDate = DateTime.Now;
            objUserExpDO.dtToDate = DateTime.Now;
            objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Delete);
            LoadUserExp();
            TotalscoreCount();
            hdnintworkeIdelet.Value = "";
            //    divDeletesucess.Style.Add("display", "none");
        }
        else if (hdnintedueIdelet.Value != "")
        {
            objDOEdu.intEducationId = Convert.ToInt32(hdnintedueIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Delete);
            LoadEducation();
            hdnintedueIdelet.Value = "";
        }
        else if (hdnintacheIdelet.Value != "")
        {
            objDOEdu.intAchivmentId = Convert.ToInt32(hdnintacheIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.DeleteAchivement);
            LoadAchivment();
            hdnintacheIdelet.Value = "";
            //  divDeletesucess.Style.Add("display", "none");
        }

        //hdnintPostId.Value = "";
        //hdnintPostceIdelet.Value = "";
        //hdnintacheIdelet.Value = "";
        //hdnintedueIdelet.Value = "";
        //hdnintdocIdelete.Value = "";
        //divDeletesucess.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);

    }
    protected void lstEducation_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divEducationED = (HtmlControl)e.Item.FindControl("divEducationED");
        Label lblintToMonth = (Label)e.Item.FindControl("lblintToMonth");
        Label lblintToYear = (Label)e.Item.FindControl("lblintToYear");
        Label lblPresentDay = (Label)e.Item.FindControl("lblPresentDay");
        HiddenField hdnToMonth = (HiddenField)e.Item.FindControl("hdnToMonth");

        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divEducationED.Style.Add("display", "none");
        }
        if (hdnToMonth.Value != "")
        {
            if (Convert.ToInt32(hdnToMonth.Value) == DateTime.Now.Month)
            {
                lblintToMonth.Visible = false;
                lblintToYear.Visible = false;
                lblPresentDay.Text = "Present Day";
                // chkEducation.Checked = true;
            }
        }
        else
        {
            lblintToMonth.Visible = false;
            lblintToYear.Visible = false;
            lblPresentDay.Text = "Present Day";
            // chkEducation.Checked = true;
        }

    }
    protected void GetProfileDetails(int UserId)
    {
        objDoProfile.RegistrationId = UserId;
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["vchrPhotoPath"].ToString() != "" && dt.Rows[0]["vchrPhotoPath"].ToString() != string.Empty)
            {
                try
                {

                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString());
                    if (File.Exists(imgPathPhysical))
                    {
                        imgUser.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        ViewState["imgComment"] = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        hdnImageProfile.Value = "CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString();
                        //imgUserm.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                    }
                    else
                    {
                        imgUser.Src = "images/profile-photo.png";
                        //imgUserm.Src = "images/profile-photo.png";
                        ViewState["imgComment"] = "images/profile-photo.png";
                        hdnImageProfile.Value = "images/profile-photo.png";
                    }


                }
                catch (Exception ex)
                {
                    imgUser.Src = "images/profile-photo.png";
                    ViewState["imgComment"] = "images/profile-photo.png";
                    hdnImageProfile.Value = "images/profile-photo.png";
                    //imgUserm.Src = "images/profile-photo.png";
                }
            }
            else
            {
                imgUser.Src = "images/profile-photo.png";
                ViewState["imgComment"] = "images/profile-photo.png";
                hdnImageProfile.Value = "images/profile-photo.png";
                //imgUserm.Src = "images/profile-photo.png";
            }

            lblUserProfName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            ViewState["LoginName"] = lblUserProfName.Text;

            bool IsPrivate = Convert.ToBoolean(dt.Rows[0]["bitIsScorePrivate"]);
            if (IsPrivate == true && UserId != Convert.ToInt32(Session["ExternalUserId"]))
            {
                liscore.Attributes.Add("class", "d-none");
                divScoreCard.Attributes.Add("class", "d-none");
            }
            else
            {
                liscore.Attributes.Add("class", "d-block");
                divScoreCard.Attributes.Add("class", "d-block");
            }

            liScorePrivate.Attributes.Add("class", "d-none");
            if (UserId == Convert.ToInt32(Session["ExternalUserId"]))
            {
                liScorePrivate.Attributes.Add("class", "d-block");
            }

            if (Request.QueryString["RegId"] == null)
            {
                Session["LoginName"] = lblUserProfName.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallProfilename", "CallProfilename();", true);
            }
            //lblUserProfNameM.Text = Convert.ToString(dt.Rows[0]["NAME"]);
        }
    }
    protected void lstEducation_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintEducationId = (HiddenField)e.Item.FindControl("hdnintEducationId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intEducationId = Convert.ToInt32(hdnintEducationId.Value);
        if (e.CommandName == "EditEdu")
        {
            //  PopUpCropImage.Style.Add("display", "none");
            divEducation.Style.Add("display", "block");
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                ViewState["intEducationId"] = dt.Rows[0]["intEducationId"].ToString();

                txtInstitute.Text = Convert.ToString(dt.Rows[0]["strInstituteName"]);
                txtTitle.Text = Convert.ToString(dt.Rows[0]["strDegree"]);
                string yearFromvalue = Convert.ToString(dt.Rows[0]["intYear"]);
                string yearTovalue = Convert.ToString(dt.Rows[0]["intToYear"]);
                int toMonth = Convert.ToInt16(dt.Rows[0]["intToMonth"]);
                int intToYear = Convert.ToInt16(dt.Rows[0]["intToYear"]);

                //if (DateTime.Now.Month == toMonth && ((DateTime.Now.Year - 1) == intToYear))
                if (DateTime.Now.Month == toMonth && ((DateTime.Now.Year) == intToYear))
                {
                    chkEducation.Checked = true;
                }

                txtPercentile.Text = Convert.ToString(dt.Rows[0]["student_percentile"]);
                txtEducationTodt.SelectedIndex = txtEducationTodt.Items.IndexOf(txtEducationTodt.Items.FindByValue(yearTovalue));
                txtEducationFromdt.SelectedIndex = txtEducationFromdt.Items.IndexOf(txtEducationFromdt.Items.FindByValue(yearFromvalue));
                txtEduDescription.InnerText = Convert.ToString(dt.Rows[0]["strSpclLibrary"]);
                SelectEducationMonth(dt);

                if (chkEducation.Checked)
                {
                    txtEducationTodt.Enabled = false;
                    ddlToMonth.Enabled = false;

                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallEducationMiddle();", true);
        }
    }
    public void SelectEducationMonth(DataTable dtt)
    {
        if (dtt.Rows[0]["intMonth"].ToString() == "1")
        {
            ddlFromMonth.Value = "Jan";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "2")
        {
            ddlFromMonth.Value = "Feb";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "3")
        {
            ddlFromMonth.Value = "Mar";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "4")
        {
            ddlFromMonth.Value = "Apr";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "5")
        {
            ddlFromMonth.Value = "May";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "6")
        {
            ddlFromMonth.Value = "Jun";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "7")
        {
            ddlFromMonth.Value = "Jul";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "8")
        {
            ddlFromMonth.Value = "Aug";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "9")
        {
            ddlFromMonth.Value = "Sep";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "10")
        {
            ddlFromMonth.Value = "Oct";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "11")
        {
            ddlFromMonth.Value = "Nov";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "12")
        {
            ddlFromMonth.Value = "Dec";
        }
        else
        {
            ddlFromMonth.Value = "Jan";
        }

        if (dtt.Rows[0]["intToMonth"].ToString() == "1")
        {
            ddlToMonth.SelectedValue = "Jan";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "2")
        {
            ddlToMonth.SelectedValue = "Feb";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "3")
        {
            ddlToMonth.SelectedValue = "Mar";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "4")
        {
            ddlToMonth.SelectedValue = "Apr";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "5")
        {
            ddlToMonth.SelectedValue = "May";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "6")
        {
            ddlToMonth.SelectedValue = "Jun";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "7")
        {
            ddlToMonth.SelectedValue = "Jul";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "8")
        {
            ddlToMonth.SelectedValue = "Aug";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "9")
        {
            ddlToMonth.SelectedValue = "Sep";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "10")
        {
            ddlToMonth.SelectedValue = "Oct";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "11")
        {
            ddlToMonth.SelectedValue = "Nov";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "12")
        {
            ddlToMonth.SelectedValue = "Dec";
        }
        else
        {
            ddlToMonth.SelectedValue = "Jan";
        }

    }
    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName like @Search + '%'";
                com.CommandText = "Select Top 5 strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName Like '%' + LTRIM(RTRIM(@Search)) + '%' Order By strInstituteName";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strInstituteName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;

            }

        }

    }

    protected void LoadUserexpYear()
    {
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
        if (dt.Rows.Count > 0)
        {
            txtFromYear.DataSource = dt;
            txtFromYear.DataTextField = "intYear";
            txtFromYear.DataValueField = "intYear";
            txtFromYear.DataBind();
            txtFromYear.Items.Insert(0, new ListItem("Year"));

            txtToYear.DataSource = dt;
            txtToYear.DataTextField = "intYear";
            txtToYear.DataValueField = "intYear";
            txtToYear.DataBind();
            txtToYear.Items.Insert(0, new ListItem("Year"));

        }

    }
    protected void lnlSaveExp_Click(object sender, EventArgs e)
    {
        lblWrkMessage.Text = "";
        if (ViewState["hdnintExperienceId"] == null)
        {
            if (txtInstituteName.Text != "")
            {
                string ukey1 = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "");
                ViewState["UnqKey"] = ukey1;
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                if (Validations.validateJavaScriptInput(txtInstituteName.Text))
                {
                    objUserExpDO.strCompanyName = txtInstituteName.Text.Trim();

                }
                else
                {
                    lblWrkMessage.Visible = true;
                    lblWrkMessage.Text = "Company Name is not valid.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkPopup", "OverlayBody();", true);
                    //  ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Company Name is not valid');</script>", false);
                    return;
                }

                if (Validations.validateJavaScriptInput(txtDegree.Text))
                {
                    objUserExpDO.strDesignation = txtDegree.Text.Trim();

                }
                else
                {
                    lblWrkMessage.Visible = true;
                    lblWrkMessage.Text = "Position is not valid.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "worpopup", "OverlayBody();", true);
                    // ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Position is not valid');</script>", false);
                    return;
                }


                objUserExpDO.intToYear = Convert.ToInt32(txtToYear.Text.Trim());
                objUserExpDO.intFromYear = Convert.ToInt32(txtFromYear.Text.Trim());

                if (fromMonth.Value == "Jan")
                {
                    objUserExpDO.inFromtMonth = 1;
                }
                else if (fromMonth.Value == "Feb")
                {
                    objUserExpDO.inFromtMonth = 2;
                }
                else if (fromMonth.Value == "Mar")
                {
                    objUserExpDO.inFromtMonth = 3;
                }
                else if (fromMonth.Value == "Apr")
                {
                    objUserExpDO.inFromtMonth = 4;
                }
                else if (fromMonth.Value == "May")
                {
                    objUserExpDO.inFromtMonth = 5;
                }
                else if (fromMonth.Value == "Jun")
                {
                    objUserExpDO.inFromtMonth = 6;
                }
                else if (fromMonth.Value == "Jul")
                {
                    objUserExpDO.inFromtMonth = 7;
                }
                else if (fromMonth.Value == "Aug")
                {
                    objUserExpDO.inFromtMonth = 8;
                }
                else if (fromMonth.Value == "Sep")
                {
                    objUserExpDO.inFromtMonth = 9;
                }
                else if (fromMonth.Value == "Oct")
                {
                    objUserExpDO.inFromtMonth = 10;
                }
                else if (fromMonth.Value == "Nov")
                {
                    objUserExpDO.inFromtMonth = 11;
                }
                else if (fromMonth.Value == "Dec")
                {
                    objUserExpDO.inFromtMonth = 12;
                }

                if (toMonth.SelectedValue == "Jan")
                {
                    objUserExpDO.intToMonth = 1;
                }
                else if (toMonth.SelectedValue == "Feb")
                {
                    objUserExpDO.intToMonth = 2;
                }
                else if (toMonth.SelectedValue == "Mar")
                {
                    objUserExpDO.intToMonth = 3;
                }
                else if (toMonth.SelectedValue == "Apr")
                {
                    objUserExpDO.intToMonth = 4;
                }
                else if (toMonth.SelectedValue == "May")
                {
                    objUserExpDO.intToMonth = 5;
                }
                else if (toMonth.SelectedValue == "Jun")
                {
                    objUserExpDO.intToMonth = 6;
                }
                else if (toMonth.SelectedValue == "Jul")
                {
                    objUserExpDO.intToMonth = 7;
                }
                else if (toMonth.SelectedValue == "Aug")
                {
                    objUserExpDO.intToMonth = 8;
                }
                else if (toMonth.SelectedValue == "Sep")
                {
                    objUserExpDO.intToMonth = 9;
                }
                else if (toMonth.SelectedValue == "Oct")
                {
                    objUserExpDO.intToMonth = 10;
                }
                else if (toMonth.SelectedValue == "Nov")
                {
                    objUserExpDO.intToMonth = 11;
                }
                else if (toMonth.SelectedValue == "Dec")
                {
                    objUserExpDO.intToMonth = 12;
                }

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.intToMonth = DateTime.Now.Month;
                }

                if (Convert.ToInt32(txtToYear.Text.Trim()) < Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    lblWrkMessage.Visible = true;
                    lblWrkMessage.Text = "From date should not be greater than to date.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkup", "OverlayBody();", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from year To to year.');</script>", false);
                    return;
                }
                else if (Convert.ToInt32(txtToYear.Text.Trim()) == Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    if (Convert.ToInt32(objUserExpDO.intToMonth) < Convert.ToInt32(objUserExpDO.inFromtMonth))
                    {
                        lblWrkMessage.Visible = true;
                        lblWrkMessage.Text = "From date should not be greater than to date.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkupopup", "OverlayBody();", true);
                        //  ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                        return;
                    }
                }


                objUserExpDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.strDescription = txtDescription.InnerText;
                objUserExpDO.strIpAddress = ip;
                objUserExpDO.strUnqId = ViewState["UnqKey"].ToString();

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.bitAtPresent = true;
                    CurrentlyWork = null;
                }
                else
                {
                    objUserExpDO.bitAtPresent = false;
                    CurrentlyWork = Convert.ToString(objUserExpDO.dtToDate);
                }

                objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Insert);
                if (objUserExpDO.intOutId == 0)
                {
                    dvSourceLinkErrorText.InnerText = "Work experience already added.";
                    return;
                }
                else
                {
                    dvSourceLinkErrorText.InnerText = "";
                }

                objUserExpDO.intAddedBy = objUserExpDO.intOutId;

                if (ISAPIURLACCESSED == "1")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("updateWorkExpDetails.action?");
                        url.Append("uid=");
                        url.Append(Convert.ToInt32(ViewState["UserID"]));
                        url.Append("expId =");
                        url.Append(objUserExpDO.intOutId);
                        url.Append("&designation =");
                        url.Append(objUserExpDO.strDesignation);
                        url.Append("&company=");
                        url.Append(objUserExpDO.strCompanyName);
                        url.Append("&workFrom=");
                        url.Append(objUserExpDO.dtFromDate);
                        url.Append("&workTo=");
                        url.Append(CurrentlyWork);
                        url.Append("&details=");
                        url.Append(objUserExpDO.strDescription);
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());

                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url.ToString();
                            objAPILogDO.strAPIType = "Profile Work Experience";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }

                    catch { }
                }
            }

        }
        else
        {
            if (txtInstituteName.Text != "")
            {
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objUserExpDO.intExperienceId = Convert.ToInt32(ViewState["hdnintExperienceId"]);
                objUserExpDO.strCompanyName = txtInstituteName.Text.Trim();
                objUserExpDO.strDesignation = txtDegree.Text.Trim();
                objUserExpDO.intToYear = Convert.ToInt32(txtToYear.Text.Trim());
                objUserExpDO.intFromYear = Convert.ToInt32(txtFromYear.Text.Trim());

                if (fromMonth.Value == "Jan")
                {
                    objUserExpDO.inFromtMonth = 1;
                }
                else if (fromMonth.Value == "Feb")
                {
                    objUserExpDO.inFromtMonth = 2;
                }
                else if (fromMonth.Value == "Mar")
                {
                    objUserExpDO.inFromtMonth = 3;
                }
                else if (fromMonth.Value == "Apr")
                {
                    objUserExpDO.inFromtMonth = 4;
                }
                else if (fromMonth.Value == "May")
                {
                    objUserExpDO.inFromtMonth = 5;
                }
                else if (fromMonth.Value == "Jun")
                {
                    objUserExpDO.inFromtMonth = 6;
                }
                else if (fromMonth.Value == "Jul")
                {
                    objUserExpDO.inFromtMonth = 7;
                }
                else if (fromMonth.Value == "Aug")
                {
                    objUserExpDO.inFromtMonth = 8;
                }
                else if (fromMonth.Value == "Sep")
                {
                    objUserExpDO.inFromtMonth = 9;
                }
                else if (fromMonth.Value == "Oct")
                {
                    objUserExpDO.inFromtMonth = 10;
                }
                else if (fromMonth.Value == "Nov")
                {
                    objUserExpDO.inFromtMonth = 11;
                }
                else if (fromMonth.Value == "Dec")
                {
                    objUserExpDO.inFromtMonth = 12;
                }

                if (toMonth.SelectedValue == "Jan")
                {
                    objUserExpDO.intToMonth = 1;
                }
                else if (toMonth.SelectedValue == "Feb")
                {
                    objUserExpDO.intToMonth = 2;
                }
                else if (toMonth.SelectedValue == "Mar")
                {
                    objUserExpDO.intToMonth = 3;
                }
                else if (toMonth.SelectedValue == "Apr")
                {
                    objUserExpDO.intToMonth = 4;
                }
                else if (toMonth.SelectedValue == "May")
                {
                    objUserExpDO.intToMonth = 5;
                }
                else if (toMonth.SelectedValue == "Jun")
                {
                    objUserExpDO.intToMonth = 6;
                }
                else if (toMonth.SelectedValue == "Jul")
                {
                    objUserExpDO.intToMonth = 7;
                }
                else if (toMonth.SelectedValue == "Aug")
                {
                    objUserExpDO.intToMonth = 8;
                }
                else if (toMonth.SelectedValue == "Sep")
                {
                    objUserExpDO.intToMonth = 9;
                }
                else if (toMonth.SelectedValue == "Oct")
                {
                    objUserExpDO.intToMonth = 10;
                }
                else if (toMonth.SelectedValue == "Nov")
                {
                    objUserExpDO.intToMonth = 11;
                }
                else if (toMonth.SelectedValue == "Dec")
                {
                    objUserExpDO.intToMonth = 12;
                }

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.intToMonth = DateTime.Now.Month;
                }

                objUserExpDO.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.strDescription = txtDescription.InnerText;
                objUserExpDO.strIpAddress = ip;
                objUserExpDO.strUnqId = ViewState["UnqKey"].ToString();

                if (chkAtPresent.Checked == true)
                {
                    objUserExpDO.bitAtPresent = true;
                    CurrentlyWork = null;
                }
                else
                {
                    objUserExpDO.bitAtPresent = false;
                    CurrentlyWork = Convert.ToString(objUserExpDO.dtToDate);
                }

                if (Convert.ToInt32(txtToYear.Text.Trim()) < Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    lblWrkMessage.Visible = true;
                    lblWrkMessage.Text = "From date should not be greater than to date.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkupopp", "OverlayBody();", true);
                    return;
                }
                else if (Convert.ToInt32(txtToYear.Text.Trim()) == Convert.ToInt32(txtFromYear.Text.Trim()))
                {
                    if (Convert.ToInt32(objUserExpDO.intToMonth) <= Convert.ToInt32(objUserExpDO.inFromtMonth))
                    {
                        lblWrkMessage.Visible = true;
                        lblWrkMessage.Text = "From date should not be greater than to date.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkuopup", "OverlayBody();", true);
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                        return;
                    }
                }

                objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                objUserExpDO.dtFromDate = DateTime.Now;
                objUserExpDO.dtToDate = DateTime.Now;
                objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Update);

                if (objUserExpDO.intOutId == 0)
                {
                    dvSourceLinkErrorText.InnerText = "Work experience already added.";
                    return;
                }
                else
                {
                    dvSourceLinkErrorText.InnerText = "";
                }

                objUserExpDO.intAddedBy = objUserExpDO.intOutId;

                if (ISAPIURLACCESSED == "1")
                {
                    try
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append(APIURL);
                        url.Append("updateWorkExpDetails.action?");
                        url.Append("uid=");
                        url.Append(Convert.ToInt32(ViewState["UserID"]));
                        url.Append("expId =");
                        url.Append(objUserExpDO.intOutId);
                        url.Append("&designation =");
                        url.Append(objUserExpDO.strDesignation);
                        url.Append("&company=");
                        url.Append(objUserExpDO.strCompanyName);
                        url.Append("&workFrom=");
                        url.Append(objUserExpDO.dtFromDate);
                        url.Append("&workTo=");
                        url.Append(CurrentlyWork);
                        url.Append("&details=");
                        url.Append(objUserExpDO.strDescription);
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url.ToString();
                            objAPILogDO.strAPIType = "Profile Work Experience";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }

                    catch { }
                }

            }
        }
        //LoadUserExp();
        //TotalscoreCount();
        //ClearExperience();
        //AddWorkExp.Style.Add("display", "none");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork8", "CallWorkLI();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveB", "callSaveExp();", true);
        RedirectToWorkExp();

    }
    protected string CalculatePersonalPoint(int UserId)
    {
        string Total = string.Empty;
        try
        {
            String url = APIURL + "getUserScore.action?"
                               + "uId=" + Convert.ToString(UserId);
            if (ISAPIURLACCESSED == "1")
            {
                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(Convert.ToString(url));
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();

                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();
                string data = Convert.ToString(ViewState["url"]);
                _responseJson1 my = JsonConvert.DeserializeObject<_responseJson1>(result);
                StringBuilder sb = new StringBuilder();

                //      ResearchDataTable();
                if (my.responseJson != null)
                {
                    try
                    {
                        int issueScore = 0, factScore = 0, importantParagraphScore = 0, orbiterDictumScore = 0, precedentScore = 0, ratioDecidenciScore = 0, summaryScore = 0;
                        if (my.responseJson.issueScore != null)
                            issueScore = Convert.ToInt32(my.responseJson.issueScore);
                        if (my.responseJson.factScore != null)
                            factScore = Convert.ToInt32(my.responseJson.factScore);
                        if (my.responseJson.importantParagraphScore != null)
                            importantParagraphScore = Convert.ToInt32(my.responseJson.importantParagraphScore);
                        if (my.responseJson.orbiterDictumScore != null)
                            orbiterDictumScore = Convert.ToInt32(my.responseJson.orbiterDictumScore);
                        if (my.responseJson.precedentScore != null)
                            precedentScore = Convert.ToInt32(my.responseJson.precedentScore);
                        if (my.responseJson.ratioDecidenciScore != null)
                            ratioDecidenciScore = Convert.ToInt32(my.responseJson.ratioDecidenciScore);
                        if (my.responseJson.summaryScore != null)
                            summaryScore = Convert.ToInt32(my.responseJson.summaryScore);

                        Total = (issueScore + factScore + importantParagraphScore + orbiterDictumScore + precedentScore + ratioDecidenciScore + summaryScore).ToString();

                    }
                    catch
                    {
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return Total;
    }
    protected void LoadUserExp()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            //     lnkuploadDoc.Visible = false;
            aAddworkExp.Visible = false;
            objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            DataTable dtExp = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.AllRecords);
            if (dtExp.Rows.Count > 0)
            {
                lstWorkExperience.DataSource = dtExp;
                lstWorkExperience.DataBind();
            }
            else
            {
                lstWorkExperience.DataSource = null;
                lstWorkExperience.DataBind();
            }
        }
        else
        {
            DataTable dtExp = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.AllRecords);
            if (dtExp.Rows.Count > 0)
            {
                lstWorkExperience.DataSource = dtExp;
                lstWorkExperience.DataBind();
            }
            else
            {
                lstWorkExperience.DataSource = null;
                lstWorkExperience.DataBind();
            }
        }
    }
    protected void lstWorkExperience_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintExperienceId = (HiddenField)e.Item.FindControl("hdnintExperienceId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objUserExpDO.intExperienceId = Convert.ToInt32(hdnintExperienceId.Value);
        if (e.CommandName == "EditExp")
        {
            //  PopUpCropImage.Style.Add("display", "none");
            AddWorkExp.Style.Add("display", "block");
            DataTable dtuser = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.SingleRecord);

            try
            {
                if (dtuser.Rows.Count > 0)
                {
                    ViewState["UnqKey"] = Convert.ToString(dtuser.Rows[0]["strUnqId"]);
                    ViewState["hdnintExperienceId"] = hdnintExperienceId.Value;
                    txtInstituteName.Text = Convert.ToString(dtuser.Rows[0]["strCompanyName"]);
                    txtDegree.Text = Convert.ToString(dtuser.Rows[0]["strDesignation"]);
                    txtDescription.InnerText = Convert.ToString(dtuser.Rows[0]["strDescription"]);

                    if (string.IsNullOrEmpty(Convert.ToString(dtuser.Rows[0]["bitAtPresent"])))
                        chkAtPresent.Checked = false;
                    else
                        if (Convert.ToBoolean(dtuser.Rows[0]["bitAtPresent"]) == true)
                    {
                        chkAtPresent.Checked = true;
                        txtToYear.Enabled = false;
                        toMonth.Enabled = false;


                    }
                    else
                    {
                        chkAtPresent.Checked = false;
                        txtToYear.Enabled = true;
                        toMonth.Enabled = true;

                    }

                    SelectMonth(dtuser);
                    txtFromYear.Text = Convert.ToString(dtuser.Rows[0]["intFromYear"]);
                    txtToYear.Text = Convert.ToString(dtuser.Rows[0]["intToYear"]);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallWorkMiddle();", true);
            LoadUserAreaSkill();
        }
    }

    public void SelectMonth(DataTable dtt)
    {
        if (dtt.Rows[0]["inFromtMonth"].ToString() == "1")
        {
            fromMonth.Value = "Jan";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "2")
        {
            fromMonth.Value = "Feb";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "3")
        {
            fromMonth.Value = "Mar";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "4")
        {
            fromMonth.Value = "Apr";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "5")
        {
            fromMonth.Value = "May";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "6")
        {
            fromMonth.Value = "Jun";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "7")
        {
            fromMonth.Value = "Jul";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "8")
        {
            fromMonth.Value = "Aug";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "9")
        {
            fromMonth.Value = "Sep";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "10")
        {
            fromMonth.Value = "Oct";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "11")
        {
            fromMonth.Value = "Nov";
        }
        else if (dtt.Rows[0]["inFromtMonth"].ToString() == "12")
        {
            fromMonth.Value = "Dec";
        }
        else
        {
            fromMonth.Value = "Jan";
        }

        if (dtt.Rows[0]["intToMonth"].ToString() == "1")
        {
            toMonth.SelectedValue = "Jan";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "2")
        {
            toMonth.SelectedValue = "Feb";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "3")
        {
            toMonth.SelectedValue = "Mar";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "4")
        {
            toMonth.SelectedValue = "Apr";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "5")
        {
            toMonth.SelectedValue = "May";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "6")
        {
            toMonth.SelectedValue = "Jun";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "7")
        {
            toMonth.SelectedValue = "Jul";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "8")
        {
            toMonth.SelectedValue = "Aug";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "9")
        {
            toMonth.SelectedValue = "Sep";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "10")
        {
            toMonth.SelectedValue = "Oct";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "11")
        {
            toMonth.SelectedValue = "Nov";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "12")
        {
            toMonth.SelectedValue = "Dec";
        }
        else
        {
            toMonth.SelectedValue = "Jan";
        }

    }
    protected void lstWorkExperience_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divUserexperenceED = (HtmlControl)e.Item.FindControl("divUserexperenceED");
        if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        {
            divUserexperenceED.Style.Add("display", "none");
        }
    }
    protected void ClearExperience()
    {
        lblWrkMessage.Text = "";
        txtToYear.SelectedIndex = 0;
        txtInstituteName.Text = "";
        txtFromYear.SelectedIndex = 0;
        txtDescription.InnerText = "";
        txtDegree.Text = "";
        fromMonth.Value = "Month";
        toMonth.SelectedValue = "Month";
        chkAtPresent.Checked = false;
        //lblareaskill.Text = "";
        toMonth.Enabled = true;
        txtToYear.Enabled = true;
        ViewState["Callsmoothmenu"] = "smooth";
    }
    protected void LoadUserAreaSkill()
    {
        objUserExpDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objUserExpDA.GetDataTable(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.GetSkillWithoudUkey);
        if (dt.Rows.Count > 0)
        {
            ViewState["lstAreaSkill"] = dt;
            lstAreaSkill.Visible = true;
            lstAreaSkill.DataSource = dt;
            lstAreaSkill.DataBind();
        }
        else
        {
            lstAreaSkill.Visible = false;
            lstAreaSkill.DataSource = null;
            lstAreaSkill.DataBind();
            return;
        }

    }
    protected void lnlCancel_Click(object sender, EventArgs e)
    {
        RedirectToWorkExp();
        //AddWorkExp.Style.Add("display", "none");
        //ClearExperience();
        //LoadUserAreaSkill();
        //LoadUserExp();
        //LoadUserAreaExp();
        //LoadUserAreaSkill();
        //LoadUserexpYear();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallWork1", "CallWorkLI();", true);

    }
    protected void lnlAddEducation_Click(object sender, EventArgs e)
    {
        divEducation.Style.Add("display", "block");
        LoadEducation();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", " CallEducationMiddle();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation5", "CallEduLI();", true);
        clearEducation();
    }
    protected void lnkSaveEducation_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtTitle.Text != "")
        {
            if (Validations.validateJavaScriptInput(txtTitle.Text))
            {
                objDOEdu.strDegree = txtTitle.Text.Trim().Replace("'", "''");
                objDOEdu.intDegreeId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetDeegreeid);
            }
            else
            {
                lblMessage.Text = "Degree is not valid.";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Degree is not valid');</script>", false);
                return;
            }
        }
        else
            return;

        if (txtInstitute.Text != "")
        {
            if (Validations.validateJavaScriptInput(txtInstitute.Text))
            {
                objDOEdu.strInstituteName = txtInstitute.Text.Trim().Replace("'", "''");
                objDOEdu.intInstituteId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetInstituteid);
            }
            else
            {
                lblMessage.Text = "Institute Name is not valid.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopu12p", "OverlayBody();", true);
                //  ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Institute Name is not valid');</script>", false);
                return;
            }

        }
        else
            return;

        if (txtDescription.InnerText != "Description")
            objDOEdu.strSpclLibrary = txtEduDescription.InnerText.Trim();
        if (txtEducationFromdt.Text.Trim() != "Year")
            objDOEdu.intYear = Convert.ToInt32(txtEducationFromdt.Text.Trim());
        else
            return;
        if (txtEducationTodt.Text.Trim() != "Year")
            objDOEdu.intToYear = Convert.ToInt32(txtEducationTodt.Text.Trim());
        else
            return;

        if (ddlFromMonth.Value == "Jan")
        {
            objDOEdu.intMonth = 1;
        }
        else if (ddlFromMonth.Value == "Feb")
        {
            objDOEdu.intMonth = 2;
        }
        else if (ddlFromMonth.Value == "Mar")
        {
            objDOEdu.intMonth = 3;
        }
        else if (ddlFromMonth.Value == "Apr")
        {
            objDOEdu.intMonth = 4;
        }
        else if (ddlFromMonth.Value == "May")
        {
            objDOEdu.intMonth = 5;
        }
        else if (ddlFromMonth.Value == "Jun")
        {
            objDOEdu.intMonth = 6;
        }
        else if (ddlFromMonth.Value == "Jul")
        {
            objDOEdu.intMonth = 7;
        }
        else if (ddlFromMonth.Value == "Aug")
        {
            objDOEdu.intMonth = 8;
        }
        else if (ddlFromMonth.Value == "Sep")
        {
            objDOEdu.intMonth = 9;
        }
        else if (ddlFromMonth.Value == "Oct")
        {
            objDOEdu.intMonth = 10;
        }
        else if (ddlFromMonth.Value == "Nov")
        {
            objDOEdu.intMonth = 11;
        }
        else if (ddlFromMonth.Value == "Dec")
        {
            objDOEdu.intMonth = 12;
        }
        else
        {
            objDOEdu.intMonth = 1;
        }

        if (ddlToMonth.SelectedValue == "Jan")
        {
            objDOEdu.intToMonth = 1;
        }
        else if (ddlToMonth.SelectedValue == "Feb")
        {
            objDOEdu.intToMonth = 2;
        }
        else if (ddlToMonth.SelectedValue == "Mar")
        {
            objDOEdu.intToMonth = 3;
        }
        else if (ddlToMonth.SelectedValue == "Apr")
        {
            objDOEdu.intToMonth = 4;
        }
        else if (ddlToMonth.SelectedValue == "May")
        {
            objDOEdu.intToMonth = 5;
        }
        else if (ddlToMonth.SelectedValue == "Jun")
        {
            objDOEdu.intToMonth = 6;
        }
        else if (ddlToMonth.SelectedValue == "Jul")
        {
            objDOEdu.intToMonth = 7;
        }
        else if (ddlToMonth.SelectedValue == "Aug")
        {
            objDOEdu.intToMonth = 8;
        }
        else if (ddlToMonth.SelectedValue == "Sep")
        {
            objDOEdu.intToMonth = 9;
        }
        else if (ddlToMonth.SelectedValue == "Oct")
        {
            objDOEdu.intToMonth = 10;
        }
        else if (ddlToMonth.SelectedValue == "Nov")
        {
            objDOEdu.intToMonth = 11;
        }
        else if (ddlToMonth.SelectedValue == "Dec")
        {
            objDOEdu.intToMonth = 12;
        }
        else
        {
            objDOEdu.intToMonth = 1;
        }

        objDOEdu.intToYear = Convert.ToInt32(txtEducationTodt.Text);
        if (chkAtPresent.Checked == true)
        {
            objDOEdu.intToMonth = DateTime.Now.Month;
        }

        if (Convert.ToInt32(objDOEdu.intToYear) < Convert.ToInt32(objDOEdu.intYear))
        {
            lblMessage.Text = "From date should not be greater than to date.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "edu13popup", "OverlayBody();", true);
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "YearAlert", "showSuccessPopup('Error','Please check from year To to year.')",true);
            return;
        }
        else if (Convert.ToInt32(objDOEdu.intToYear) == Convert.ToInt32(objDOEdu.intYear))
        {
            if (Convert.ToInt32(objDOEdu.intToMonth) <= Convert.ToInt32(objDOEdu.intMonth))
            {
                lblMessage.Text = "From date should not be greater than to date.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopup13", "OverlayBody();", true);
                // ScriptManager.RegisterStartupScript(this, typeof(Page), "MonthAlert", "showSuccessPopup('Error','Please check from month To to month.')", false);
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Please check from month To to month.');</script>", false);
                return;
            }
        }

        if (txtPercentile.Text != null && txtPercentile.Text != "")
        {
            objDOEdu.stud_percentile = Convert.ToDecimal(txtPercentile.Text);
        }

        objDOEdu.strEducationAchievement = "Education";
        objDOEdu.strIpAddress = ip;
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDOEdu.dtDate = DateTime.Now;

        if (ViewState["intEducationId"] == null)
        {
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Insert);
        }
        else
        {
            objDOEdu.intEducationId = Convert.ToInt32(ViewState["intEducationId"]);
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Update);
            objDOEdu.intOutId = 1;
        }
        if (objDOEdu.intOutId == 0)
        {
            lblMessage.Text = "Record Already Inserted..!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopup", "OverlayBody();", true);
            return;
        }

        string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
        if (ISAPIURLACCESSED != "0")
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("updateUserEducationDetails.action?");
                url.Append("uid=");
                url.Append(UserTypeId + Convert.ToInt32(ViewState["UserID"]));
                url.Append("&educationId=");
                url.Append(objDOEdu.intOutId);
                url.Append("&degree=");
                url.Append(objDOEdu.strDegree);
                url.Append("&institution=");
                url.Append(objDOEdu.strInstituteName);
                url.Append("&year=");
                url.Append(objDOEdu.intYear);
                url.Append("&educationType=");
                url.Append(objDOEdu.strEducationAchievement);
                url.Append("&achievementMarked=");
                url.Append(1);
                url.Append("&description=");
                url.Append(objDOEdu.strSpclLibrary);

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());


                myRequest1.Method = "GET";
                if (ISAPIResponse != "0")
                {
                    WebResponse myResponse1 = myRequest1.GetResponse();
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strAPIType = "Profile Education";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }
            catch { }
        }
        RedirectToEducation();
        //clearEducation();
        //LoadEducation();
        //divEducation.Style.Add("display", "none");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation5", "CallEduLI();", true);
    }
    protected void chkAtPresent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAtPresent.Checked == true)
        {
            txtToYear.SelectedIndex = 1;// txtToYear.Items.Count - 1;
            // txtToYear.Text = DateTime.Now.Year.ToString();
            txtToYear.Enabled = false;
            int i = DateTime.Now.Month;
            toMonth.SelectedIndex = i;
            toMonth.Enabled = false;
        }
        else
        {
            txtToYear.SelectedIndex = 0;
            txtToYear.Enabled = true;
            toMonth.Enabled = true;
            toMonth.SelectedIndex = 0;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkExCur", "OverlayBody();", true);
    }
    protected void chkEducation_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkEducation.Checked == true)
        {
            // txtEducationTodt.SelectedValue = DateTime.Now.Year.ToString();
            // txtEducationFromdt.SelectedIndex = txtEducationTodt.Items.Count - 1;
            txtEducationTodt.SelectedIndex = 1;//txtEducationTodt.Items.Count - 1;
            txtEducationTodt.Enabled = false;
            int i = DateTime.Now.Month;
            ddlToMonth.SelectedIndex = i;
            ddlToMonth.Enabled = false;
        }
        else
        {
            txtEducationTodt.SelectedIndex = 0;
            txtEducationTodt.Enabled = true;
            ddlToMonth.Enabled = true;
            ddlToMonth.SelectedIndex = 0;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchiv", "OverlayBody();", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  string FilePath = hdnFilePath.Value;
        // string DocName = hdnDocName.Value;
        string Error = hdnErrorMsg.Value;
        bool fileUploaded = ddUploader1.UploadFile();
        // string filename = hdnUploadFile.Value;
        if (!fileUploaded)
        {

            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = ddUploader1.ErrorMesssage;
            lblErrorMsg.CssClass = "RedErrormsg";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
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
        if (txtDescription.Value != "Description")
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
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
        else
        {
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
        BindSubjectList();
        txtDescrition.Value = "";
        txtDocTitle.Text = "";
        lblErrorMsg.Visible = false;
        lblErrorMsg.Text = "";
        //    lblfilenamee.Text = "";
        //    lnkDeleteDoc123.Style.Add("display", "none");
        //    ViewState["Callsmoothmenu"] = "smooth";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    }
    protected void lnkCancelEducation_Click(object sender, EventArgs e)
    {
        RedirectToEducation();
        //clearEducation();
        //divEducation.Style.Add("display", "none");
        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation1", "CallEduLI();", true);
        //LoadEducation();
    }

    protected void LoadEducation()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnlAddEducation.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }

        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }
        }
    }
    protected void LoadYear()
    {
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
        if (dt.Rows.Count > 0)
        {
            txtEducationFromdt.DataSource = dt;
            txtEducationFromdt.DataTextField = "intYear";
            txtEducationFromdt.DataValueField = "intYear";
            txtEducationFromdt.DataBind();
            txtEducationFromdt.Items.Insert(0, new ListItem("Year"));

            txtEducationTodt.DataSource = dt;
            txtEducationTodt.DataTextField = "intYear";
            txtEducationTodt.DataValueField = "intYear";
            txtEducationTodt.DataBind();
            txtEducationTodt.Items.Insert(0, new ListItem("Year"));
        }

    }

    protected void lnkSubDocCancel_Click(object sender, EventArgs e)
    {
        ClearSubmitDoc();
        docModal1.Style.Add("display", "none");
        Master.BodyTag.Attributes.Add("class", "");
    }

    protected void lnkSubDoc_Click(object sender, EventArgs e)
    {
        docModal1.Style.Add("display", "block");
        Master.BodyTag.Attributes.Add("class", "remove-scroller");

    }

    protected void lnkSubDocSave_Click(object sender, EventArgs e)
    {
        if (!ddUploaderSubmitDoc.UploadFile())
        {
            lblErrorUpload.Text = "Please upload document.";
            lblErrorUpload.Visible = true;
            return;
        }
        ddUploaderSubmitDoc.SetValues(ddUploaderSubmitDoc.FileName, ddUploaderSubmitDoc.UploadedFileName);
        if (!chkConfirm.Checked)
        {
            lblErrorConf.Text = "Please confirm.";
            lblErrorConf.Visible = true;
            return;
        }
        objDOSASubDoc.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOSASubDoc.FilePath = ddUploaderSubmitDoc.UploadedFileName;
        objDOSASubDoc.FileName = ddUploaderSubmitDoc.FileName;
        objDASASubDoc.AddEditSubDocs(objDOSASubDoc, DA_SASubDoc.Case.AddArticle);
        ClearSubmitDoc();
        docModal1.Style.Add("display", "none");
        //Master.BodyTag.Attributes.Add("class", "");
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "sshol", "showSuccessPopup('Success','Verification document submitted successfully.');", true);
    }

    private void ClearSubmitDoc()
    {
        //txtTitle.Text = "";
        lblErrorUpload.Text = "";
        lblErrorUpload.Visible = false;
        lblErrorConf.Text = "";
        lblErrorConf.Visible = false;
        ddUploaderSubmitDoc.Reset();
        chkConfirm.Checked = true;

    }

    protected void clearEducation()
    {
        lblMessage.Text = "";
        LoadYear();
        ViewState["intEducationId"] = null;
        txtEducationFromdt.SelectedValue = "Year";
        txtEducationTodt.SelectedValue = "Year";
        txtEduDescription.InnerText = "";
        ddlToMonth.SelectedValue = "Month";
        ddlFromMonth.Value = "Month";
        txtTitle.Text = "";
        txtInstitute.Text = "";
        chkEducation.Checked = false;
        txtEducationTodt.SelectedIndex = 0;
        txtEducationTodt.Enabled = true;
        ddlToMonth.Enabled = true;
        ddlToMonth.SelectedIndex = 0;
        ViewState["Callsmoothmenu"] = "smooth";
    }
    public class _responseJson1
    {
        public responseJson responseJson { get; set; }
    }
    public class responseJson
    {
        public string responseId { get; set; }
        public searchResultDocumentSets[] searchResultDocumentSets;  //{ get; set; }
        public string dateOfCalculation { get; set; }
        public string factCount { get; set; }
        public string factScore { get; set; }
        public string importantParagraphScore { get; set; }
        public string importantParagraphCount { get; set; }
        public string issueCount { get; set; }
        public string issueScore { get; set; }
        public string orbiterDictumCount { get; set; }
        public string orbiterDictumScore { get; set; }
        public string precedentCount { get; set; }
        public string precedentScore { get; set; }
        public string ratioDecidenciCount { get; set; }
        public string ratioDecidenciScore { get; set; }
        public string summaryCount { get; set; }
        public string summaryScore { get; set; }

    }
    public class searchResultDocumentSets
    {
        public string docType { get; set; }
        public documents[] documents { get; set; }
        public string searchCount { get; set; }
    }
    public class documents
    {
        public string dateOfCalculation { get; set; }
        public string factCount { get; set; }
        public string factScore { get; set; }
        public string importantParagraphScore { get; set; }
        public string issueCount { get; set; }
        public string issueScore { get; set; }
        public string orbiterDictumCount { get; set; }
        public string orbiterDictumScore { get; set; }
        public string precedentCount { get; set; }
        public string precedentScore { get; set; }
        public string ratioDecidenciCount { get; set; }
        public string ratioDecidenciScore { get; set; }
        public string summaryCount { get; set; }
        public string summaryScore { get; set; }
    }

    protected void cbScorePrivate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            objdoreg.isScorePrivate = 0;
            if (cbScorePrivate.Checked)
            {
                objdoreg.isScorePrivate = 1;
            }

            objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataSet ds = objdareg.checkScorePulic(objdoreg, DA_Registrationdetails.RegistrationDetails.isScorePrivate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                bool isScoreVal = Convert.ToBoolean(ds.Tables[0].Rows[0][1]);
                Session["isScorePrivate"] = ds.Tables[0].Rows[0][1];
                if (isScoreVal)
                {
                    cbScorePrivate.Checked = true;
                }
                else
                {
                    cbScorePrivate.Checked = false;
                }
            }
        }
        catch (Exception)
        {

        }
    }
}