using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SA_News_Details : System.Web.UI.Page
{
    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/SuperAdminLanding.aspx", true);
            }
            BindNewsDetails();
        }
    }

    protected void BindNewsDetails()
    {
        int PostId = Convert.ToInt32(Request.QueryString["PostId"]);
        objDONewsListing.ID = PostId;
        dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsDtls);
        if (dt.Rows.Count > 0)
        {
            lblNewsHeading.Text = dt.Rows[0]["Title"].ToString();
            lblNewsDetails.Text = dt.Rows[0]["Content"].ToString();
            //lstParentQADetails.DataSource = dt;
            //lstParentQADetails.DataBind();
        }
    }
}