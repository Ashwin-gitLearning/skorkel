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

public partial class UserControl_BlogRecentComments : System.Web.UI.UserControl
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
        ViewState["intBlogId"] = Convert.ToInt32(Request.QueryString["intBlogId"]);
        DataTable dtSub = new DataTable();
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objblogdo.intBlogId = Convert.ToInt32(ViewState["intBlogId"]);
        dtSub = objblogda.GetDataTableComment(objblogdo, DA_NewBlogs.BlogCommnet.GetTop4ByBlog);
        if (dtSub.Rows.Count > 0)
        {
            RepComments.DataSource = dtSub;
            RepComments.DataBind();
        }
        else
        {
            RepComments.DataSource = null;
            RepComments.DataBind();
        }
    }
    protected void RepComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    { }
}