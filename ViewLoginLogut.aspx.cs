using System;
using System.Web.UI.HtmlControls;
using DA_SKORKEL;
using System.Data;

public partial class ViewLoginLogut : System.Web.UI.Page
{
    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "View Login/Logout";

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "10";
            BindAllLogs();
        }
    }

    protected void BindAllLogs()
    {

            objLogin.intRegistartionID = Convert.ToInt32(ViewState["UserID"]);
            dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GetLoginLogout);
            lstViewLog.DataSource = dt;
            lstViewLog.DataBind();

    }

}