using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class ViewLogs : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
        }

        if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
            ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

        HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
        masterlbl.InnerText = "View Logs";

        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        BindAllLogs();
    }

    protected void BindAllLogs()
    {

        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            objLog.CurrentPage = hdnCurrentPage.Value;
            objLog.CurrentPageSize = hdnTotalItem.Value;
            objLog.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtSearch = objLogD.GetDatatable(objLog, DA_Logdetails.LogDetails.GetLog);

            if (dtSearch.Rows.Count > 0)
            {
                if (dtSearch.Rows.Count > 3)
                    dvHeight.Style.Add("display", "none");
                lstViewLog.DataSource = dtSearch;
                lstViewLog.DataBind();
            }
            else
            {
                lstViewLog.DataSource = null;
                lstViewLog.DataBind();
            }
        }
        else
        {
            lstViewLog.DataSource = null;
            lstViewLog.DataBind();
        }
    }

    protected void lstViewLog_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtchild = new DataTable();
        HiddenField hdnLogId = (HiddenField)e.Item.FindControl("hdnLogId");
        Label lblDeletedOn = (Label)e.Item.FindControl("lblDeletedOn");
        ListView lstChildViewLog = (ListView)e.Item.FindControl("lstChildViewLog");

        objLog.dtDeletedOn = Convert.ToString(lblDeletedOn.Text.Trim());
        objLog.intAddedBy = Convert.ToInt32(Session["ExternalUserId"]);
        objLog.intDeletedBy = Convert.ToInt32(Session["ExternalUserId"]);
        dtchild = objLogD.GetDatatable(objLog, DA_Logdetails.LogDetails.GetLogBydate);

        if (lblDeletedOn.Text == DateTime.Today.ToString("dd MMM yyyy"))
        {
            lblDeletedOn.Text = "Today";
        }
        else if (lblDeletedOn.Text == DateTime.Today.AddDays(-1).ToString("dd MMM yyyy"))
        {
            lblDeletedOn.Text = "Yesterday";
        }

        if (dtchild.Rows.Count > 0)
        {
            lstChildViewLog.DataSource = dtchild;
            lstChildViewLog.DataBind();
        }
        else
        {
            lstChildViewLog.DataSource = "";
            lstChildViewLog.DataBind();
        }
    }

    protected void lstViewLog_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        ListView lstChildViewLog = e.Item.FindControl("lstChildViewLog") as ListView;
        lstChildViewLog.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lstChildViewLog_ItemCommand);
        lstChildViewLog.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lstChildViewLogMyTag_ItemDataBound);
    }

    protected void lstChildViewLogMyTag_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField strAction = (HiddenField)e.Item.FindControl("hdnstrAction");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        Label lblnotificationname = (Label)e.Item.FindControl("lblnotificationname");
        LinkButton lnkName = (LinkButton)e.Item.FindControl("lnkName");
        Label lblComment = (Label)e.Item.FindControl("lblComment");
        HiddenField hdnstrActionName = (HiddenField)e.Item.FindControl("hdnstrActionName");
        Label lbldtlIcons = (Label)e.Item.FindControl("lbldtlIcons");
        Image Iconimg = (Image)e.Item.FindControl("iconImage");
        Label txtIcon = (Label)e.Item.FindControl("txtIcon");

        if (strAction.Value == "Group")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group";
            //lblComment.Visible = true;
            //lblComment.Text =  Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value+"'";
            lblnotificationname.Text = "deleted by";
        }else
        if (strAction.Value == "Group Event")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Event";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Q&A")
        {
            Iconimg.Visible = false;
            txtIcon.Visible = true;
            txtIcon.Text = " ?";
            lbldtlIcons.Text = "Question";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Q&A Ans")
        {
            Iconimg.Visible = false;
            txtIcon.Visible = true;
            txtIcon.Text = " ?";
            lbldtlIcons.Text = "Question Answer";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Blog")
        {
            Iconimg.ImageUrl = "~/images/file.svg";
            lbldtlIcons.Text = "Blog";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Blog Comment")
        {
            Iconimg.ImageUrl = "~/images/file.svg";
            lbldtlIcons.Text = "Blog Comment";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Forum")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Forum";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Wall Post")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Wall Post";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Organisation Group Wall Post")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Wall Post";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Wall Post Comment")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Wall Post Comment";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Job")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Job";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Bookmark")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "Bookmark";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Document")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "Document";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "MyDownload")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "MyDownload";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Organisation Event")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Event";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Forum")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Forum";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Event")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Event";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Poll")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Poll";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Forum")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Forum";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Wall Comment Post")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Wall Comment Post";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Job")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Job";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group folder")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group folder";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group Doc")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group Doc";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Group")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Group";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Organisation Poll")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Poll";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Folder")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Folder";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Doc")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Doc";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Poll")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Poll";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Group Document")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Document";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Group Folder")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Folder";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Wall Post Comment")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "Wall Post Comment";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Wall Post")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "Wall Post";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Wall Post Comment")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Wall Post Comment";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else if (strAction.Value == "Organisation Wall Post")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Organisation Wall Post";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "User Document")
        {
            Iconimg.ImageUrl = "~/images/profile-photo.png";
            lbldtlIcons.Text = "User Document";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        else
        if (strAction.Value == "Group Poll Comment")
        {
            Iconimg.ImageUrl = "~/images/groupPhoto.jpg";
            lbldtlIcons.Text = "Group Poll Comment";
            //lblComment.Visible = true;
            //lblComment.Text = Convert.ToString(strAction.Value) + " '" + hdnstrActionName.Value + "'";
            lblnotificationname.Text = "deleted by";
        }
        lblComment.Visible = true;
        lblComment.Text = hdnstrActionName.Value;
    }

    protected void lstChildViewLog_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField LogId = (HiddenField)e.Item.FindControl("LogId");
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
    }

}