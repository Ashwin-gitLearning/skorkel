using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DA_SKORKEL;

public partial class SuperAdminLanding : System.Web.UI.Page
{
    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();
    CryptoGraphy objEncrypt = new CryptoGraphy();

    protected void Page_Init(object sender, EventArgs e)
    {
        Session["RegType"] = null;
        Session["ExternalUserId"] = null;
        //this.Form.DefaultButton = Login1.FindControl("btnLogins").UniqueID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string login = "";
            try
            {
                login =Convert.ToString(Request.QueryString["Login"]);
            }
            catch
            {
                login = "";
            }
            if (Request.Cookies["myScrlCookieSA"] != null)
            {
                HttpCookie myScrlCookieSA = new HttpCookie("myScrlCookieSA");
                myScrlCookieSA = Request.Cookies.Get("myScrlCookieSA");
                Login1.UserName = myScrlCookieSA.Values["UserName"].ToString();
                TextBox txtbox = (TextBox)Login1.FindControl("Password");
                txtbox.Attributes["Value"] = myScrlCookieSA.Values["Password"].ToString();

                DataTable dt = new DataTable();
                objLogin.Username = Login1.UserName;
                objLogin.Password = txtbox.Attributes["Value"];
                dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.UserLogin);

                if (dt.Rows.Count > 0)
                {
                    if (login == "")
                    {
                        UserSession.UserInfo UInfo = new UserSession.UserInfo();
                        string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
                        UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
                        UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
                        int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
                        string RegistrationType = Convert.ToString(dt.Rows[0]["RegistartionType"]);

                        Session.Add("RegType", RegistrationType);
                        Session.Add("UserTypeId", TypeId);
                        Session.Add("UInfo", UInfo);
                        Session.Add("LoginName", LoginName);
                        Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
                        
                    }
                }
            }
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string strPasswordmd5 = "";
        DataTable dt = new DataTable();
        objLogin.Username = Login1.UserName;
        string Password = objEncrypt.Encrypt(Login1.Password);
        objLogin.Password = Password;
        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.UserLoginMD5);
        if (dt.Rows.Count > 0)
        {
            string strPassword = objEncrypt.Decrypt(Convert.ToString(dt.Rows[0]["vchrPassword"]));
            DataTable dtmd5 = new DataTable();
            objLogin.Password = strPassword;
            dtmd5 = objLoginDB.GetDataSet(objLogin, DA_Login.Login_1.GetMD5);
            if (dtmd5.Rows.Count > 0)
            {
                strPasswordmd5 = Convert.ToString(dtmd5.Rows[0]["strPasswordMD5"]);
            }

            UserSession.UserInfo UInfo = new UserSession.UserInfo();
            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
            string RegistrationType = Convert.ToString(dt.Rows[0]["RegistartionType"]);

            Session.Add("RegType", RegistrationType);
            Session.Add("UserTypeId", TypeId);
            Session.Add("UInfo", UInfo);
            Session.Add("LoginName", LoginName);
            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
            objLogin.intRegistartionID = Convert.ToInt32(dt.Rows[0]["intRegistrationId"]);
            objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Login);

            Response.Redirect("SA_JournalListing.aspx");
            //Response.Redirect("SA_Testing.aspx");
        }
        else
        {
            Login1.FailureText = "Invalid user.";
            divLogin.Style.Add("display", "block");
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string strPasswordmd5 = "";
        DataTable dt = new DataTable();
        objLogin.Username = Login1.UserName;
        string pass = hdnEncpass.Value;

        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.UserLoginMD5);//UserLogin);
        if (dt.Rows.Count > 0)
        {
            string strPassword = objEncrypt.Decrypt(Convert.ToString(dt.Rows[0]["vchrPassword"]));
            DataTable dtmd5 = new DataTable();
            objLogin.Password = strPassword;
            dtmd5 = objLoginDB.GetDataSet(objLogin, DA_Login.Login_1.GetMD5);
            if (dtmd5.Rows.Count > 0)
            {
                strPasswordmd5 = Convert.ToString(dtmd5.Rows[0]["strPasswordMD5"]);
            }
            if (strPasswordmd5 == pass)
            {
                UserSession.UserInfo UInfo = new UserSession.UserInfo();
                string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
                UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
                UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
                int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
                string RegistrationType = Convert.ToString(dt.Rows[0]["RegistartionType"]);

                Session.Add("RegType", RegistrationType);
                Session.Add("UserTypeId", TypeId);
                Session.Add("UInfo", UInfo);
                Session.Add("LoginName", LoginName);
                Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
                objLogin.intRegistartionID = Convert.ToInt32(dt.Rows[0]["intRegistrationId"]);
                objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Login);

                //Response.Redirect("SA_Testing.aspx");
                Response.Redirect("SA_JournalListing.aspx");
            }
            else
            {
                Login1.FailureText = "Invalid user.";
                divLogin.Style.Add("display", "block");
            }
        }
        else
        {
            Login1.FailureText = "Invalid user.";
            divLogin.Style.Add("display", "block");
        }
    }
    
 }