using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ViewActivity : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_ViewActivity objdoactivity = new DO_ViewActivity();
    DA_ViewActivity objdaactivity = new DA_ViewActivity();

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
            masterlbl.InnerText = "View Activity";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            BindAllActivity();
        }
    }

    protected void BindAllActivity()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objdoactivity.CurrentPage = hdnCurrentPage.Value;
            objdoactivity.CurrentPageSize = hdnTotalItem.Value;
            objdoactivity.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtSearch = objdaactivity.GetDatatable(objdoactivity, DA_ViewActivity.Activity.GetActivityDate);

            if (dtSearch.Rows.Count > 0)
            {
                if (dtSearch.Rows.Count > 3)
                    dvHeight.Style.Add("display", "none");
                lstViewActivity.DataSource = dtSearch;
                lstViewActivity.DataBind();
            }
            else
            {                
                lstViewActivity.DataSource = null;
                lstViewActivity.DataBind();
            }
        }
        else
        {
            lstViewActivity.DataSource = null;
            lstViewActivity.DataBind();
        }
    }

    protected void lstViewActivity_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtMain = new DataTable();
        DataTable dtchild = new DataTable();

        HiddenField hdnId = (HiddenField)e.Item.FindControl("hdnId");
        Label lblAddedOn = (Label)e.Item.FindControl("lblAddedOn");
        ListView lstChildViewActivity = (ListView)e.Item.FindControl("lstChildViewActivity");

        objdoactivity.ActivityDate = Convert.ToString(lblAddedOn.Text.Trim());
        objdoactivity.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        dtchild = objdaactivity.GetDatatable(objdoactivity, DA_ViewActivity.Activity.GetAllActivityByDate);

        if (lblAddedOn.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblAddedOn.Text = "Today";
        }
        else if (lblAddedOn.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblAddedOn.Text = "Yesterday";
        }

        if (dtchild.Rows.Count > 0)
        {
            lstChildViewActivity.DataSource = dtchild;
            lstChildViewActivity.DataBind();
        }
        else
        {
            lstChildViewActivity.DataSource = "";
            lstChildViewActivity.DataBind();
        }
    }

    protected void lstViewActivity_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChildViewActivity = e.Item.FindControl("lstChildViewActivity") as ListView;
        lstChildViewActivity.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChildViewActivity_ItemCommand);
        lstChildViewActivity.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChildMyTag_ItemDataBound);
    }

    protected void lstChildMyTag_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        HiddenField hdnstrActivity = (HiddenField)e.Item.FindControl("hdnstrActivity");
        Label lblnotificationname = (Label)e.Item.FindControl("lblnotificationname");
        Label lblnotificationaction = (Label)e.Item.FindControl("lblnotificationaction");
        LinkButton lnkName = (LinkButton)e.Item.FindControl("lnkName");
        Label lblComment = (Label)e.Item.FindControl("lblComment");
        Image Iconimg = (Image)e.Item.FindControl("iconImage");
        Label txtIcon = (Label)e.Item.FindControl("txtIcon");
        HtmlGenericControl divPostBody = (HtmlGenericControl)e.Item.FindControl("divPostBody");
        Label lblCommentUser = (Label)e.Item.FindControl("lblCommentUser");

        if (hdnTableName.Value == "Scrl_NewBlogsTbl")
        {
            
            lblnotificationname.Text = "Blog <b>" + Convert.ToString(hdnstrActivity.Value) + "</b> shared by" ;
            Iconimg.ImageUrl = "~/images/file.svg";
        }

        if (hdnTableName.Value == "Scrl_QuestionAnsTbl")
        {
            lblnotificationname.Text = "Question <b>" + Convert.ToString(hdnstrActivity.Value) + "</b> shared by";
            Iconimg.Visible = false;
            txtIcon.Visible = true;
            txtIcon.Text = " ?";
            //Iconimg.ImageUrl = "~/images/QuestionIcon.jpg";
        }
        
        if (hdnTableName.Value == "FriendDecline")
        {
            divPostBody.Visible = false;
            //lblCommentUser.Visible = true;
            lblCommentUser.Text =  Convert.ToString(hdnstrActivity.Value);
            lblCommentUser.Font.Bold = true;
            lblnotificationname.Text = "<b>" + lblCommentUser.Text + "</b>" +" Request invitation rejected by";
            Iconimg.ImageUrl = "~/images/profile-photo.png";
        }


        if (hdnTableName.Value == "GroupDecline")
        {
            divPostBody.Visible = false;
            string[] str = Convert.ToString(hdnstrActivity.Value).Split(':');
            //lblCommentUser.Visible = true;            
            lblCommentUser.Text = str[0];
            lblCommentUser.Font.Bold = true;
            lblnotificationname.Text = string.Format("<b>" + lblCommentUser.Text + "</b>" + " Request to join <span style='font-weight: bold;'>{0} Group</span>  rejected by", str[1]);//"Request to join " + str[1] + " rejected by";
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
        }
    }

    protected void lstChildViewActivity_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPkId = (HiddenField)e.Item.FindControl("hdnPkId");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");

        if (e.CommandName == "Details")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        BindAllActivity();
    }

    protected void lstChildViewActivity_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }
}