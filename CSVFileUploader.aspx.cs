using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DA_SKORKEL;
using System.Text;
using System.Net;
//using Microsoft.VisualBasic.FileIO;

public partial class CSVFileUploader : System.Web.UI.Page
{
    //DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    //DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();
    Random rd = new Random();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
    }
    
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string dwnload = "~/Downloads/SqlExport.csv";        
        string filePath = dwnload;
        
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Dummy.csv\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();        
    }

    //File Upload Control click event
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //Creating objects of datatable  
        DataTable tblcsv = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtCheck = new DataTable();
        DataTable dt3 = new DataTable();

        //Creating columns  
        tblcsv.Columns.Add("FirstName");
        tblcsv.Columns.Add("LastName");
        tblcsv.Columns.Add("Email");
        string root = Server.MapPath("/FilesUploads/");
        //string subdir = @"C:\Temp\Mahesh";
        // If directory does not exist, create it. 
        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
        }
        //Getting full file path of Uploaded file  
        string CSVFilePath = null;
        CSVFilePath = Server.MapPath("~/FilesUploads/") + Path.GetFileName(fileUpload.PostedFile.FileName);

        if (fileUpload.HasFile)
        // Call a helper method routine to save the file.
        {
            fileUpload.SaveAs(CSVFilePath);
            string ReadCSV = File.ReadAllText(CSVFilePath);
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        count++;
                    }
                }
            }

            //Getting datatable without header
            dt = tblcsv.Clone();

            foreach (DataRow drtableOld in tblcsv.AsEnumerable().Skip(1))
            {
                dt.ImportRow(drtableOld);
            }

            DataColumn myColumn = new DataColumn();
            myColumn.DataType = System.Type.GetType("System.String");
            myColumn.AllowDBNull = false;
            myColumn.Caption = "vchrActive";
            myColumn.ColumnName = "vchrActive";
            myColumn.DefaultValue = "Y";

            dt.Columns.Add(myColumn);

            DataColumn myColumnpwd = new DataColumn();
            myColumnpwd.DataType = System.Type.GetType("System.String");
            myColumnpwd.AllowDBNull = false;
            myColumnpwd.Caption = "vchrPassword";
            myColumnpwd.ColumnName = "vchrPassword";
            //myColumnpwd.DefaultValue = pwd;

            dt.Columns.Add(myColumnpwd);
            dt3 = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objLogin.Username = Convert.ToString(dt.Rows[i]["Email"]).Replace("\r", null);
                dtCheck.Clear();
                dtCheck = objLoginDB.GetDataSet(objLogin, DA_SKORKEL.DA_Login.Login_1.GetUserMailCheck);
                if (dtCheck.Rows.Count > 0)
                {
                    //lblMsgs.Text = "Email id already exist.";
                    //txtUname.Text = "";
                    //return;
                }
                else
                {
                    string pwd = null;
                    pwd = CreateRandomPassword(10);
                    dt.Rows[i]["vchrPassword"] = pwd;
                    dt3.ImportRow(dt.Rows[i]);                    
                }                
            }

            //Calling insert Functions  
            InsertCSVRecords(dt3);
        }
        else
        // Notify the user that a file was not uploaded.
        {
            lblError.Visible = true;
            lblError.Text = "You did not specify a file to upload.";
        }
    }

    //Getting random password
    private string CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        string numbers = "0123456789";
        char[] chars = new char[passwordLength];
        //Random rd = new Random();

        for (int i = 0; i < passwordLength-2; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        for (int i = 0; i < 2; i++)
        {
            chars[passwordLength-2+i] = numbers[rd.Next(0, numbers.Length)];
        }


        return new string(chars);
    }

    //Function to Insert Records  
    private void InsertCSVRecords(DataTable csvdt)
    {
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //bool IsSuccessSave = false;
        //SqlTransaction transaction = null;
        DataTable dtInsrt = new DataTable();
        dtInsrt = csvdt.Clone();

        DataColumn myColumn = new DataColumn();
        myColumn.DataType = System.Type.GetType("System.Int32");
        myColumn.AllowDBNull = false;
        myColumn.Caption = "intUserTypeID";
        myColumn.ColumnName = "intUserTypeID";
        myColumn.DefaultValue = 1;

        DataColumn myColumndtAddedOn = new DataColumn();
        myColumndtAddedOn.DataType = System.Type.GetType("System.DateTime");
        myColumndtAddedOn.AllowDBNull = false;
        myColumndtAddedOn.Caption = "dtAddedOn";
        myColumndtAddedOn.ColumnName = "dtAddedOn";
        myColumndtAddedOn.DefaultValue = DateTime.Now;

        dtInsrt.Columns.Add(myColumn);
        dtInsrt.Columns.Add(myColumndtAddedOn);

        for (int i = 0; i < csvdt.Rows.Count; i++)
        {
            CryptoGraphy objEncrypt = new CryptoGraphy();
            
            dtInsrt.ImportRow(csvdt.Rows[i]);
            dtInsrt.Rows[i]["Email"] = Convert.ToString(dtInsrt.Rows[i]["Email"]).Replace("\r", null);
            dtInsrt.Rows[i]["vchrPassword"] = objEncrypt.Encrypt(Convert.ToString(csvdt.Rows[i]["vchrPassword"]));
        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        
        using (SqlBulkCopy objbulk = new SqlBulkCopy(conn))
        { 
            //assigning Destination table name    
            objbulk.DestinationTableName = "Scrl_RegistrationTbl";
            //Mapping Table column    

            objbulk.ColumnMappings.Add("FirstName", "vchrFirstName");
            objbulk.ColumnMappings.Add("LastName", "vchrLastName");
            objbulk.ColumnMappings.Add("Email", "vchrUserName");
            objbulk.ColumnMappings.Add("vchrActive", "vchrActive");
            objbulk.ColumnMappings.Add("vchrPassword", "vchrPassword");
            objbulk.ColumnMappings.Add("intUserTypeID", "intUserTypeID");
            objbulk.ColumnMappings.Add("dtAddedOn", "dtAddedOn");
            //dtAddedOn
            //Inserting Datatable Records to DataBase    
            conn.Open();
            //transaction.Commit();
            //IsSuccessSave = true;
            objbulk.WriteToServer(dtInsrt);
            conn.Close();
            
            for (int i = 0; i < csvdt.Rows.Count; i++)
            {
                SendMail(csvdt.Rows[i]["Email"].ToString().Replace("\r", null), csvdt.Rows[i]["vchrPassword"].ToString(), csvdt.Rows[i]["FirstName"].ToString() + " " + csvdt.Rows[i]["LastName"].ToString());

                DataTable dtRecord = new DataTable();
                dtRecord.Clear();
                objdoreg.UserName = csvdt.Rows[i]["Email"].ToString().Replace("\r", null);
                dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);
                //string id = "id=" + dtRecord.Rows[0]["intRegistrationId"].ToString();
                //dt = objLoginDB.GetDataTable(objLogin, DA_SKORKEL.DA_Login.Login_1.UserDetails);

                if (ISAPIURLACCESSED == "1")
                {
                    StringBuilder UserURL = new StringBuilder();
                    UserURL.Append(APIURL);
                    UserURL.Append("registerUser.action?");
                    UserURL.Append("uid=");
                    UserURL.Append(Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]));
                    UserURL.Append("&userId=");
                    UserURL.Append(Convert.ToString(dtRecord.Rows[0]["vchrUserName"]));
                    UserURL.Append("&password=");
                    UserURL.Append(Convert.ToString(dtRecord.Rows[0]["vchrPassword"]));
                    UserURL.Append("&firstName=");
                    UserURL.Append(Convert.ToString(dtRecord.Rows[0]["vchrFirstName"]));
                    UserURL.Append("&lastName=");
                    UserURL.Append(Convert.ToString(dtRecord.Rows[0]["vchrLastName"]));
                    UserURL.Append("&userType=STUDENT");
                    UserURL.Append("&userContextIds=" + null);
                    UserURL.Append("&friendUserIds=" + null);
                    UserURL.Append("&lawRelated=" + null);

                    try
                    {
                        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                        myRequest1.Method = "GET";
                        WebResponse myResponse1 = myRequest1.GetResponse();

                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();

                        objAPILogDO.strURL = UserURL.ToString();
                        objAPILogDO.strAPIType = "Student";
                        objAPILogDO.strResponse = result;

                        if (ip == null)
                            objAPILogDO.strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                    catch (Exception ex)
                    { ex.Message.ToString(); }
                }
            }
            lblError.Visible = true;
            lblError.Text = "User's list uploaded successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
        }
    }

    //Sending Emails
    private void SendMail(string UsersMail, string UsersPwd, string UserName)
    {
        try
        {
            //DataTable dtRecord = new DataTable();
            string body = null;

            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = UsersMail;
            string MailURLFinal = MailURL + "/Landing.aspx";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{Content}", "<span style='font-size:14px;color:#373A36;'>Your email: " + UsersMail +
                   "</span> and password: " + UsersPwd +
                   "<span style='font-weight:bold;font-size:14px;color:#373A36;'></span><br/> Please login and reset password on first login. <br /><br/>Login URL:" +
                   "&nbsp;&nbsp;<a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Login</a>");

            string subject = "Welcome, " + UserName + " - Login Details";
            EmailSender.SendEmail(MailTo, subject, body);

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}