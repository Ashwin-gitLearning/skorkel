using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DA_SKORKEL;

public partial class ArticleDetails : System.Web.UI.Page
{
    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
    string FilterText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Session["ExternalUserId"] == null)
            {
                Response.Redirect("Landing.aspx");
            }
            if (Request.QueryString["JrnlId"] != null && Request.QueryString["ArtId"] != null)
            {
                //objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
                //objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
                ValidateJournal();
                BindArticle();
                BindComments();
                GetProfileDetails(Convert.ToInt32(Session["ExternalUserId"]));
            }
        }
    }
    private void ValidateJournal()
    {
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        DataTable dtJou = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetJournal);
        if (!Convert.ToBoolean(dtJou.Rows[0]["status"]))
        {
            Response.Redirect("JournalListing.aspx");
        }
    }
    private void BindArticle()
    {
        objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.ArticleDetails);
        if (dtArt.Rows.Count > 0)
        {
            objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
            objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
            objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable artLikes = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetLikeUnlike);
            if (artLikes.Rows.Count > 0)
            {
                lnkLike.CssClass = "active-toogle on-flag";
            } else
            {
                lnkLike.CssClass = "active-toogle";
            }
            DataRow dr = dtArt.Rows[0];
            lblJournal.Text = dr["journalTitle"].ToString();
            lblLikes.Text = dr["Likes"].ToString() + (Convert.ToInt32(dr["Likes"]) != 1 ? " Likes" : " Like");
            lblComments.Text = dr["Comments"].ToString() + (Convert.ToInt32(dr["Comments"]) != 1 ? " Comments" : " Comment");
            lnkUser.Text = dr["AddedByName"].ToString();
            lnkUser.Attributes.Add("href", "profile2.aspx?RegId="+ dr["AddedByID"].ToString());
            lnkArticle.Text = dr["ArtTitle"].ToString();
            // frmPDF.Attributes.Add("src", "articles/" + dr["FilePath"]);
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            hdnArticlePath.Value ="/Articles/" + dr["FilePath"];
        }
        else
        {
            Response.Redirect("JournalListing.aspx");
        }
    }
    protected void GetProfileDetails(int UserId)
    {
        objDoProfile.RegistrationId = UserId;
        DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
        imgCurrentUser.Src = "images/profile-photo.png";
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["vchrPhotoPath"].ToString() != "" && dt.Rows[0]["vchrPhotoPath"].ToString() != string.Empty)
            {
                try
                {
                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dt.Rows[0]["vchrPhotoPath"].ToString());
                    if (File.Exists(imgPathPhysical))
                    {
                        imgCurrentUser.Src = "~\\CroppedPhoto\\" + dt.Rows[0]["vchrPhotoPath"].ToString();
                    }
                }
                catch { }
            }
        }
    }
    private void BindComments()
    {
        objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
        DataTable dtComments = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetComments);
        if (dtComments.Rows.Count > 0)
        {
            lstComments.DataSource = dtComments;
            lstComments.DataBind();
        }
        else
        {
            lstComments.DataSource = null;
            lstComments.DataBind();
        }
    }

    protected void lnkPostComments_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtComment.Text))
        {
            lblErrorComment.Visible = true;
            return;
        }
        objDOSAJor.ArticleID = objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
        objDOSAJor.Comment = txtComment.Text;
        objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        if (string.IsNullOrEmpty(hdnCommentId.Value))
        {
            objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.AddComment);
        }
        else
        {
            objDOSAJor.CommentId = Convert.ToInt32(hdnCommentId.Value);
            objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.EditComment);
        }
        Clear();
        BindComments();
        BindArticle();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "sshol", "showSuccessPopup('Success','Comment saved successfully.');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ardt", "OverlayBody();", true);
    }
    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        DeleteComment();
        Clear();
    }

    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        hdnCommentId.Value = "";
        txtComment.Text = "";
        lblErrorComment.Visible = false;
    }

    protected void lstComments_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        HtmlGenericControl lnkmani = e.Item.FindControl("lnkmani") as HtmlGenericControl;
        HtmlImage imgUser = (HtmlImage)e.Item.FindControl("imgUser");
        string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + drv["PhotoPath"]);
        if (File.Exists(imgPathPhysical))
        {
            imgUser.Src = "~\\CroppedPhoto\\" + drv["PhotoPath"].ToString();
            ViewState["imgComment"] = "~\\CroppedPhoto\\" + drv["PhotoPath"].ToString();
        }
        else
        {
            imgUser.Src = "images/profile-photo.png";
        }
        if (drv["AddedByID"].ToString() == Session["ExternalUserId"].ToString())
        {
            lnkmani.Style.Add("display", "block");
        }
    }

    protected void lstComments_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComment")
        {
            HiddenField hdnCommentIdInner = (HiddenField)e.Item.FindControl("hdnCommentIdInner");
            HiddenField hdnComment = (HiddenField)e.Item.FindControl("hdnComment");
            txtComment.Text = hdnComment.Value;
            hdnCommentId.Value = hdnCommentIdInner.Value;
            if (Request.UserAgent.Contains("Mobi"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "si", "showRight();", true);
                 txtComment.Focus();
            }
            txtComment.Focus();
        }
        else if (e.CommandName == "DeleteComment")
        {
            HiddenField hdnCommentIdInner = (HiddenField)e.Item.FindControl("hdnCommentIdInner");
            hdnCommentId.Value = hdnCommentIdInner.Value;
        }
    }
    private void DeleteComment()
    {
        objDOSAJor.CommentId = Convert.ToInt32(hdnCommentId.Value);
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.DeleteComment);
        BindComments();
        BindArticle();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.ArticleDetails);
        if (dtArt.Rows.Count > 0)
        {
            DataRow dr = dtArt.Rows[0];
            string str = @"~/articles/" + dr["FilePath"];
            string filename = lnkArticle.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + str + "&filename=" + filename);
        }
    }
    protected void lnkLike_Click(object sender, EventArgs e)
    {
        objDOSAJor.ArticleID = Convert.ToInt32(Request.QueryString["ArtId"]);
        objDOSAJor.JournalID = Convert.ToInt32(Request.QueryString["JrnlId"]);
        objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.LikeUnlike);
        BindArticle();
    }
}