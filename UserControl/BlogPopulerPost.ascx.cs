using System;
using System.Data;
using DA_SKORKEL;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Web.UI;

public partial class UserControl_BlogPopulerPost : System.Web.UI.UserControl
{
    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DataTable dt = new DataTable();
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

            BindPosts();
        }
        
    }
    private void BindPosts()
    {
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetAllBlogsPost);
        if (dtSub.Rows.Count > 0)
        {
            RepPopPost.DataSource = dtSub;
            RepPopPost.DataBind();
        }
        else
        {
            RepPopPost.DataSource = null;
            RepPopPost.DataBind();
            lnlView.Visible = false;
        }
    }
   
    protected void RepPopPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Linkhead")
        {
            HiddenField hdnintBlogId = (HiddenField)e.Item.FindControl("hdnintBlogId");
            //BlogsComments.aspx?intBlogId=56
            Response.Redirect("BlogsComments.aspx?intBlogId=" + hdnintBlogId.Value);
        }

    }

    protected void lnkview_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/AllBlogs.aspx");GetBlogss
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMyAwesomeUserControl", "$('#divDeletesucess').hide();$('#divSuccess').hide(); ", true);
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dtSub = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetBlogss);
        if (dtSub.Rows.Count > 0)
        {
            lnlView.Visible = false;
            RepPopPost.DataSource = dtSub;
            RepPopPost.DataBind();
        }
        else
        {
            RepPopPost.DataSource = null;
            RepPopPost.DataBind();
            lnlView.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
        
        if (Request.UserAgent.Contains("Mobi"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Acsi", "showRight();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Asi", "setActive();", true);

        }
        divShomore.Attributes["class"] = "text-center";
    }
}