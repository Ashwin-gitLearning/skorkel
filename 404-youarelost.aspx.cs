using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _404_youarelost : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //string oauth_token = Request.QueryString["oauth_token"];
        //string oauth_verifier = Request.QueryString["oauth_verifier"];
        //if (oauth_token != null && oauth_verifier != null)
        //{
        //    Application["oauth_token"] = oauth_token;
        //    Application["oauth_verifier"] = oauth_verifier;
        //    Response.Redirect("LinkedInAccountDetails.aspx?oauth_verifier=" + oauth_verifier + "");
        //}
        ////divSuccess.Visible = false;
        //Label FailureText = (Label)Login1.FindControl("FailureText");
        //FailureText.Visible = false;
        //if (!IsPostBack)
        //{
        //    //Label FailureText = (Label)Login1.FindControl("FailureText");
        //    FailureText.Visible = false;
        //    //divSuccess.Visible = false;
        //    if (Request.Browser.IsMobileDevice)
        //        Session.Timeout = 100000;
        //    else
        //        //Session.Timeout = 1200;
        //        Session.Timeout = 86400;

        //    if (Page.Session["LoginName"] != null)
        //    {
        //        //Response.Redirect("Home.aspx?ActiveStatus=P"); 
        //    }

        //    string login = "";
        //    try
        //    {
        //        login = Request.QueryString["Login"].ToString();
        //    }
        //    catch
        //    {
        //        login = "";
        //    }
        //    if (Request.Cookies["myScrlCookie"] != null)
        //    {
        //        CryptoGraphy objdcryptt = new CryptoGraphy();
        //        HttpCookie myScrlCookie = new HttpCookie("myScrlCookie");
        //        myScrlCookie = Request.Cookies.Get("myScrlCookie");
        //        Login1.UserName = myScrlCookie.Values["UserName"].ToString();
        //        TextBox txtbox = (TextBox)Login1.FindControl("Password");
        //        txtbox.Attributes["Value"] = myScrlCookie.Values["Password"].ToString();
        //        LoginUser();
        //    }
        //    else
        //    {
        //        TextBox txtboxPassword = (TextBox)Login1.FindControl("Password");
        //        TextBox txtboxUserName = (TextBox)Login1.FindControl("UserName");
        //        txtboxUserName.Text = string.Empty;
        //        txtboxPassword.Text = string.Empty;
        //    }
        //}
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Landing.aspx");
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //LoginUser();
    }       

    private void openLoginPopup()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "$(document).ready(function() {openLoginPopup();});", true);
    }

}