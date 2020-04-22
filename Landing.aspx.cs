using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using DA_SKORKEL;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.SessionState;
using System.Web.UI;

public partial class Landing : System.Web.UI.Page
{

    private oAuthLinkedIn _oauth = new oAuthLinkedIn();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();
    CryptoGraphy objEncrypt = new CryptoGraphy();

    string GoogleClientId = ConfigurationManager.AppSettings["GoogleClientId"];
    string GoogleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
    string GoogleRedirectUrl = ConfigurationManager.AppSettings["GoogleRedirectUrl"];
    string GoogleMailUrl = ConfigurationManager.AppSettings["GoogleMailUrl"];


    protected void Page_Init(object sender, EventArgs e)
    {
        Session["RegType"] = null;
        Session["ExternalUserId"] = null;
        //this.Form.DefaultButton = Login1.FindControl("btnLogins").UniqueID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string oauth_token = Request.QueryString["oauth_token"];
        string oauth_verifier = Request.QueryString["oauth_verifier"];
        if (oauth_token != null && oauth_verifier != null)
        {
            Application["oauth_token"] = oauth_token;
            Application["oauth_verifier"] = oauth_verifier;
            Response.Redirect("LinkedInAccountDetails.aspx?oauth_verifier=" + oauth_verifier + "");
        }
        //divSuccess.Visible = false;
        Label FailureText = (Label)Login1.FindControl("FailureText");
        FailureText.Visible = false;
        if (!IsPostBack)
        {
            //Label FailureText = (Label)Login1.FindControl("FailureText");
            FailureText.Visible = false;
            //divSuccess.Visible = false;
            if (Request.Browser.IsMobileDevice)
                Session.Timeout = 100000;
            else
                //Session.Timeout = 1200;
                Session.Timeout = 86400;

            if (Page.Session["LoginName"] != null)
            {
                //Response.Redirect("Home.aspx?ActiveStatus=P"); 
            }

            string login = "";
            try
            {
                login = Convert.ToString(Request.QueryString["Login"]);
            }
            catch
            {
                login = "";
            }
            if (Request.Cookies["myScrlCookie"] != null)
            {
                CryptoGraphy objdcryptt = new CryptoGraphy();
                HttpCookie myScrlCookie = new HttpCookie("myScrlCookie");
                myScrlCookie = Request.Cookies.Get("myScrlCookie");
                Login1.UserName = myScrlCookie.Values["UserName"].ToString();
                TextBox txtbox = (TextBox)Login1.FindControl("Password");
                txtbox.Attributes["Value"] = myScrlCookie.Values["Password"].ToString();
                LoginUser();
            }
            else
            {
                TextBox txtboxPassword = (TextBox)Login1.FindControl("Password");
                TextBox txtboxUserName = (TextBox)Login1.FindControl("UserName");
                txtboxUserName.Text = string.Empty;
                txtboxPassword.Text = string.Empty;
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

            if (Login1.RememberMeSet == true)
            {
                HttpCookie myScrlCookie = new HttpCookie("myScrlCookie");
                Response.Cookies.Remove("myScrlCookie");
                //Add UserID Cookie
                myScrlCookie.Values.Add("UserID", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
                myScrlCookie.Values.Add("UserName", Login1.UserName.ToString());
                myScrlCookie.Values.Add("Password", Login1.Password.ToString());
                DateTime dtxpiry = DateTime.Now.AddDays(15);

                myScrlCookie.Expires = dtxpiry;
                myScrlCookie.HttpOnly = false;
                Response.Cookies.Add(myScrlCookie);
            }

            UserSession.UserInfo UInfo = new UserSession.UserInfo();
            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
            Session.Add("UserTypeId", TypeId);
            Session.Add("UInfo", UInfo);
            Session.Add("LoginName", LoginName);
            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
            objLogin.intRegistartionID = Convert.ToInt32(dt.Rows[0]["intRegistrationId"]);
            objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Login);

            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ip == null)
                        ip = Request.ServerVariables["REMOTE_ADDR"];
                    String url = APIURL + "userLogin.action?" +
                                "loginUid=" + dt.Rows[0]["intUserTypeID"] + dt.Rows[0]["intRegistrationId"] +
                                "&loginId=" + dt.Rows[0]["intRegistrationId"] +
                                "&password=" + objLogin.Password +
                                "&loginTime=" + DateTime.Now +
                                "&sessionId=" + HttpContext.Current.Session.SessionID;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = url;
                        objAPILogDO.strAPIType = "User Login";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }

            Response.Redirect("Home.aspx?ActiveStatus=P");
            //Response.Redirect("Signup3.aspx");
        }
        else
        {
            divLogin.Style.Add("display", "block");
            Login1.FailureText = "Invalid email or password.";
            openLoginPopup();

        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {

        LoginUser();

    }

    #region Gmail login Code

    protected void btnGoogleLogin_Click(object sender, CommandEventArgs e)
    {

        string AuthenticationUrl = "https://accounts.google.com/o/oauth2/auth?redirect_uri=" + GoogleRedirectUrl
            + "&response_type=code&client_id=" + GoogleClientId
            + "&scope=https://www.googleapis.com/auth/plus.login+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile&approval_prompt=force&access_type=offline";

        Response.Redirect(AuthenticationUrl);
    }

    protected String GeneratePassword()
    {
        int minPassSize = 6;
        int maxPassSize = 12;
        StringBuilder stringBuilder = new StringBuilder();
        char[] charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()".ToCharArray();
        int newPassLength = new Random().Next(minPassSize, maxPassSize);
        char character;
        Random rnd = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < newPassLength; i++)
        {
            character = charArray[rnd.Next(0, (charArray.Length - 1))];
            stringBuilder.Append(character);

        }

        return stringBuilder.ToString();
    }

    private void SendMail(string To, string UName, string CustPassword)
    {
        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailFrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = To;
            //  MailTo = "ychoudhari@myuberall.com";
            SmtpClient clientip = new SmtpClient(mailServer);

            clientip.Port = Convert.ToInt32(Port);
            NetworkCredential cre = new NetworkCredential(username, Password);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = cre;
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                string Topic = string.Empty;
                string Description = string.Empty;
                string empid = string.Empty;

                string str = "<p> Hi , </p>";
                str += "<p> We have created a new Skorkel Account for you. </p>";
                str += "<p> Your account details are: <br /> Email id for login: " + UName + " <br /> Password:" + CustPassword + "</p>";
                str += "<br /> <br /><p><a href='http://115.124.123.151/Landing.aspx' >www.skorkel.com</a> </p>";
                System.Net.Mail.MailMessage mm2 = new System.Net.Mail.MailMessage();
                mm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(str, null, "text/html");
                mm2.To.Clear();
                mm2.CC.Clear();
                mm2.Bcc.Clear();
                mm2.To.Add(MailTo);
                mm2.Subject = "Skorkel - Account created";
                mm2.AlternateViews.Add(htmlView);
                mm2.IsBodyHtml = true;
                clientip.Send(mm2);
                mm2.To.Clear();
                mm2.CC.Clear();
                mm2.Bcc.Clear();
            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected bool CustomerLogin(string login, string pass)
    {
        DA_Login Log = new DA_Login();

        DataTable dt = new DataTable();
        objLogin.Username = login;
        objLogin.Password = pass;

        dt = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GmailFBLogin);
        if (dt.Rows.Count > 0)
        {
            //if (login == "")
            //{
            UserSession.UserInfo UInfo = new UserSession.UserInfo();
            string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
            UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
            UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
            int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
            Session.Add("UserTypeId", TypeId);

            Session.Add("UInfo", UInfo);
            Session.Add("LoginName", LoginName);
            Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
            //Response.Redirect("~/Profile/SkorkelSearch.aspx?Content=" + txtSearchScorkel.Text.Trim());
            //}

            return true;
        }
        else
        {
            return false;

        }

    }

    private static string GetFullname(string first, string last)
    {
        var _first = first ?? "";
        var _last = last ?? "";
        if (string.IsNullOrEmpty(_first) || string.IsNullOrEmpty(_last))
            return "";
        return _first + " " + _last;
    }

    #endregion

    #region LinkedIn login Code


    protected void LoginUser()
    {
        string strPasswordmd5 = "";
        DataTable dt = new DataTable();
        objLogin.Username = Login1.UserName;
        string pass = hdnEncpass.Value;
        TextBox txtboxPass = (TextBox)Login1.FindControl("Password");
        TextBox txtboxUser = (TextBox)Login1.FindControl("UserName");

        if (Request.Cookies["myScrlCookie"] != null && objLogin.Username == Request.Cookies.Get("myScrlCookie").Values["UserName"].ToString())
        {
            CryptoGraphy objdcryptt = new CryptoGraphy();
            HttpCookie myScrlCookie = new HttpCookie("myScrlCookie");
            myScrlCookie = Request.Cookies.Get("myScrlCookie");
            Login1.UserName = myScrlCookie.Values["UserName"].ToString();
            TextBox txtbox = (TextBox)Login1.FindControl("Password");
            pass = myScrlCookie.Values["Password"].ToString();
        }

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
                if (Login1.RememberMeSet == true)
                {
                    HttpCookie myScrlCookie = new HttpCookie("myScrlCookie");
                    //Response.Cookies.Remove("myScrlCookie");
                    myScrlCookie.Values.Add("UserID", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
                    myScrlCookie.Values.Add("UserName", Login1.UserName.ToString());
                    myScrlCookie.Values.Add("Password", Login1.Password.ToString());
                    DateTime dtxpiry = DateTime.Now.AddDays(15);

                    myScrlCookie.Expires = dtxpiry;
                    myScrlCookie.HttpOnly = false;
                    Response.Cookies.Add(myScrlCookie);
                }

                UserSession.UserInfo UInfo = new UserSession.UserInfo();
                string LoginName = Convert.ToString(dt.Rows[0]["LoginName"]);
                UInfo.UserName = Convert.ToString(dt.Rows[0]["vchrUserName"]);
                UInfo.UserID = Convert.ToInt64(dt.Rows[0]["intRegistrationId"]);
                int TypeId = Convert.ToInt32(dt.Rows[0]["intUserTypeID"]);
                Session.Add("UserTypeId", TypeId);
                Session.Add("UInfo", UInfo);
                Session.Add("LoginName", LoginName);
                Session.Add("ExternalUserId", Convert.ToString(dt.Rows[0]["intRegistrationId"]));
                //Added On 16 March 2020
                Session.Add("isScorePrivate", dt.Rows[0]["bitIsScorePrivate"]);
                objLogin.intRegistartionID = Convert.ToInt32(dt.Rows[0]["intRegistrationId"]);
                objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Login);

                if (ISAPIURLACCESSED != "0")
                {
                    try
                    {
                        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ip == null)
                            ip = Request.ServerVariables["REMOTE_ADDR"];
                        String url = APIURL + "userLogin.action?" +
                                    "loginUid=" + dt.Rows[0]["intUserTypeID"] + dt.Rows[0]["intRegistrationId"] +
                                    "&loginId=" + dt.Rows[0]["intRegistrationId"] +
                                    "&password=" + objLogin.Password +
                                    "&loginTime=" + DateTime.Now +
                                    "&sessionId=" + HttpContext.Current.Session.SessionID;

                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                        myRequest1.Method = "GET";
                        if (ISAPIResponse != "0")
                        {
                            WebResponse myResponse1 = myRequest1.GetResponse();
                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url;
                            objAPILogDO.strAPIType = "User Login";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                        }
                    }
                    catch { }
                }
                if (Convert.ToString(dt.Rows[0]["logintimestamp"]) != null && Convert.ToString(dt.Rows[0]["logintimestamp"]) != "")
                {
                    Response.Redirect("Home.aspx?ActiveStatus=P");
                }
                else
                {
                    Response.Redirect("SignUp3.aspx");
                }

                //Response.Redirect("SignUp3.aspx");
            }
            else
            {
                // Remeber me cookie Removed
                TextBox txtbox = (TextBox)Login1.FindControl("Password");
                txtbox.Text = string.Empty;
                Response.Cookies["myScrlCookie"].Expires = DateTime.Now.AddDays(-1);
                HttpCookie myScrlAppCookie = new HttpCookie("myScrlAppCookie");

                myScrlAppCookie.Value = "logout";

                myScrlAppCookie.HttpOnly = false;
                Response.Cookies.Add(myScrlAppCookie);

                divLogin.Style.Add("display", "block");
                //Login1.FailureText = "Invalid user.";


                Label FailureText = (Label)Login1.FindControl("FailureText");
                FailureText.Visible = true;
                FailureText.Text = "Invalid email or password.";
                openLoginPopup();
                //divSuccess.Style.Add("display", "block");
            }
        }
        else
        {
            divLogin.Style.Add("display", "block");
            //Login1.FailureText = "Invalid user.";
            //divSuccess.Style.Add("display", "block");
            Label FailureText = (Label)Login1.FindControl("FailureText");
            FailureText.Visible = true;
            FailureText.Text = "Invalid email or password.";
            openLoginPopup();
        }
    }
    private void openLoginPopup()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "$(document).ready(function() {openLoginPopup();});", true);

    }
    protected void btnLinkedInLogin_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string authLink = _oauth.AuthorizationLinkGet();
            Application["reuqestToken"] = _oauth.Token;
            Application["reuqestTokenSecret"] = _oauth.TokenSecret;
            Application["oauthLink"] = authLink;

            Session["Session_OToken"] = _oauth.Token;
            Session["Session_OTokenSecret"] = _oauth.TokenSecret;

            Response.Redirect(authLink);

        }
        catch (Exception ex)
        { ex.Message.ToString(); }
    }

    #endregion




}