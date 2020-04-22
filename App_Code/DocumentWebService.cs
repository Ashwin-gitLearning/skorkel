using DA_SKORKEL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using SqlConn;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DocumentWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DocumentWebService : System.Web.Services.WebService
{

    public DocumentWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public Documents[] GetDocumentIndexes(string CaseId, string TagTypeIds, string FlagNo, string GroupId, string CommentUserId)
    {
        int id = 0;
        if (CommentUserId == "")
        {
            id = Convert.ToInt32(Session["ExternalUserId"]);
        }
        else
        {
            id = Convert.ToInt32(CommentUserId);
        }

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_GetDocumentIndexesearch", conn);
        //da.SelectCommand = new SqlCommand("Scrl_GetDocumentIndexesearchDemo", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = FlagNo;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.VarChar, 200).Value = id;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.Int).Value = CaseId;
        da.SelectCommand.Parameters.Add("@TagTypeIds", SqlDbType.VarChar, 200).Value = TagTypeIds;
        da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.VarChar, 200).Value = GroupId.Trim();
        da.Fill(dt);
        co.CloseConnection(conn);

        List<Documents> obj = new List<Documents>();
        Int32 Nospan = 1;
        foreach (DataRow dr in dt.Rows)
        {
            //if ((obj.Any(z => z.start == Convert.ToInt32(dr["StartIndex"]) && z.end == Convert.ToInt32(dr["EndIndex"]))) == false)
            //{
            //    if ((obj.Any(z => z.start <= Convert.ToInt32(dr["StartIndex"]) && z.end >= Convert.ToInt32(dr["EndIndex"]))) == false)
            //    {
            obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString(), NoSpan = Nospan, MaxCount = Convert.ToInt32(dr["Maxcount"]) });
            Nospan++;
            //    }
            //}
        }

        return obj.ToArray();
    }

    public List<Documents> GetCall(DataTable dt)
    {
        Int32 StartIndex = 0, EndIndex = 0;
        string dtAdded = "";
        List<Documents> obj = new List<Documents>();
        Int32 ExtraLenght = 0, EL = 0, ELCount = 0;
        int EI = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (StartIndex == 0)
            {
                StartIndex = Convert.ToInt32(dr["StartIndex"]);
                if (Convert.ToInt32(dr["StartIndex"]) == 0)
                {
                    StartIndex = 1;
                }
                EndIndex = Convert.ToInt32(dr["EndIndex"]);
                dtAdded = Convert.ToString(dr["dtAddedOn"]);
                ExtraLenght = Convert.ToInt32(dr["ExtraLength"]);
                EL = Convert.ToInt32(dr["ExtraLength"]);
                ELCount = Convert.ToInt32(dr["ExtraLength"]);
                obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString() });
            }
            else
            {
                StartIndex = Convert.ToInt32(dr["StartIndex"]);
                if (Convert.ToInt32(dr["StartIndex"]) == 0)
                {
                    StartIndex = 1;
                }
                EndIndex = Convert.ToInt32(dr["EndIndex"]);
                dtAdded = Convert.ToString(dr["dtAddedOn"]);
                ExtraLenght = Convert.ToInt32(dr["ExtraLength"]);

                if (obj.Any(z => z.start >= StartIndex && z.end >= EndIndex) == true)
                {
                    if (obj.Any(z => z.start >= EndIndex && z.end <= EndIndex) == true)
                    {
                        obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString() });
                    }
                    else if (obj.Any(z => z.start >= EndIndex == true && z.start >= StartIndex) == true && obj.Any(z => z.start <= StartIndex) == false)
                    {
                        obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString() });
                    }
                }
                else if (obj.Any(z => z.start <= StartIndex && z.end <= EndIndex) == true)
                {
                    if (obj.Any(z => z.start <= StartIndex && z.end <= StartIndex) == true && obj.Any(z => z.end >= StartIndex) == false)
                    {
                        obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString() });
                    }
                }
                else if (ExtraLenght == 1)
                {
                    obj.Add(new Documents() { start = Convert.ToInt32(dr["StartIndex"]), end = Convert.ToInt32(dr["EndIndex"]), imgtype = Convert.ToInt32(dr["TagType"]), extralength = Convert.ToInt32(dr["ExtraLength"]), rownum = Convert.ToInt32(dr["RowNum"]), name = dr["Name"].ToString() });
                }

            }
        }


        return obj.OrderBy(o => o.start).ToList();
    }

    [WebMethod(EnableSession = true)]
    public string GetLink(string CaseId, string StartIndex, string FlagNo, string GroupId)
    {
        string url = "";
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_GetDocumentIndexesLink", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = FlagNo;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = Convert.ToInt32(Session["ExternalUserId"]);
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.Int).Value = CaseId;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.Int).Value = StartIndex;
        da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.VarChar, 200).Value = GroupId.Trim();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            url = dt.Rows[0][0].ToString();
        }
        co.CloseConnection(conn);
        return url;
    }

    [WebMethod(EnableSession = true)]
    public string saveChatboxQuestion(string question, string type)
    {
        string chatbotType = "";
        DO_Registrationdetails objdoreg = new DO_Registrationdetails();
        DA_Registrationdetails objdareg = new DA_Registrationdetails();

        objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
        objdoreg.Question = question;
        objdoreg.Type = type;
        DataSet ds = objdareg.checkScorePulic(objdoreg, DA_Registrationdetails.RegistrationDetails.saveChatbotQuestion);
        if (ds.Tables[0].Rows.Count > 0)
        {
            switch (Convert.ToString(ds.Tables[0].Rows[0]["Action"]).ToLower())
            {
                case "inserted":
                    SendMailToAdmin(Convert.ToString(ds.Tables[0].Rows[0]["nvchrQuestion"]), Convert.ToString(ds.Tables[0].Rows[0]["Email"]), Convert.ToString(ds.Tables[0].Rows[0]["Name"]), Convert.ToString(ds.Tables[0].Rows[0]["nvchrType"]));
                    chatbotType = Convert.ToString(ds.Tables[0].Rows[0]["nvchrType"]);
                    break;
            }
        }
        return chatbotType;
    }

    [WebMethod(EnableSession = true)]
    public string GetPdfPathForWhatsApp(string UserId, string ContentID, string hdnDivContent, string ip, string hdnMarkMaxCount, string lblCaseTitle)
    {
        string directory = string.Empty;
        string fileName = string.Empty;
        StringBuilder text = new StringBuilder(hdnDivContent);

        #region GetContentForPDF Method Code
        StringBuilder UserURL = new StringBuilder(ConfigurationManager.AppSettings["APIURL"]);
        UserURL.Append("downloadDocument.action?");
        UserURL.Append("downloadByUId=");
        UserURL.Append(UserId);
        UserURL.Append("&downloadedParentId=");
        UserURL.Append(ContentID);
        UserURL.Append("&downloadType=");
        UserURL.Append("free");

        DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
        DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();
        try
        {
            if (ConfigurationManager.AppSettings["ISAPIURLACCESSED"] == "1")
            {
                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(UserURL.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                if (ConfigurationManager.AppSettings["ISAPIResponse"] != "0")
                {
                    StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                    String result = sr.ReadToEnd();
                    objAPILogDO.strURL = UserURL.ToString();
                    objAPILogDO.intAddedBy = Convert.ToInt32(UserId);
                    objAPILogDO.strAPIType = "Share On WhatsApp";
                    objAPILogDO.strResponse = result;
                    objAPILogDO.strIPAddress = ip;
                    objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                }
            }

            //string texta = hdnDivContent.Value;

            text = text.Replace("&quot;", "").Replace("<br>", "<br />").Replace("<br >", "<br />");
            int countCdiv = CountStringOccurrences(text.ToString(), "</div>");
            int countdiv = CountStringOccurrences(text.ToString(), "<div");
            if (countCdiv == 0 && countdiv == 0)
            {
                int txtL = text.Length;
                text = text.Insert(txtL, "</div>");
                text = text.Insert(0, "<div>");
            }

            if (Convert.ToInt32(hdnMarkMaxCount) > 0)
            {
                for (int i = 0; i <= Convert.ToInt32(hdnMarkMaxCount); i++)
                {
                    string spanOpen = "<span" + i;
                    string replace = "<span";
                    string pattern = spanOpen;
                    string result = Regex.Replace(text.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                    string spanClose = "</span" + i;
                    replace = "</span";
                    pattern = spanClose;
                    result = Regex.Replace(result.ToString(), pattern, replace, RegexOptions.IgnoreCase);
                    text = new StringBuilder(result);

                }
                text = text.Replace("class=\"preced\"", "style=\"background-color: #ffeca0;\"").Replace("class=\"rediod\"", "style=\"background-color: #AECF00;\"").Replace("class=\"issuss\"", "style=\"background-color: #faa69d;\"").Replace("span0", "span").Replace("span1", "span").Replace("span2", "span").Replace("span3", "span").Replace("span4", "span").Replace("span5", "span");
                text = text.Replace("class=\"factss\"", "style=\"background-color: #9dc6fa;\"").Replace("class=\"highls\"", "style=\"background-color: Lime;\"").Replace("class=\"orbits\"", "style=\"background-color: #f8adef;\"").Replace("class=\"ImpPss\"", "style=\"background-color: #b0ffd2;\"");
            }
        }
        catch
        {

        }
        #endregion
        #region CreatePdf() Method Code
        try
        {
            directory = HttpContext.Current.Server.MapPath("~\\SharingDoc\\");

            Random generator = new Random();
            int r = generator.Next(1, 1000000);
            string s = r.ToString().PadLeft(6, '0');

            //DateTime dt = System.DateTime.Now;
            //string timestam = Convert.ToString(dt.Day) + Convert.ToString(dt.Month) + Convert.ToString(dt.Year) + s;
            //fileName = "CaseDocument_" + timestam + ".pdf";
            fileName = "CaseDocument_" + ContentID + ".pdf";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var msOutput = new FileStream(directory + fileName, FileMode.Create))
            {
                using (var document = new Document(PageSize.A4, 30, 30, 30, 30))
                {
                    using (var writer = PdfWriter.GetInstance(document, msOutput))
                    {
                        document.Open();



                        var example_css = @".body{font-family: 'Times New Roman'}";
                        Font contentFont = FontFactory.GetFont("Times New Roman", 16.0f, iTextSharp.text.Font.BOLD);
                        Paragraph para = new Paragraph(lblCaseTitle, contentFont);
                        para.Alignment = Element.ALIGN_CENTER;
                        document.Add(para);
                        Paragraph para1 = new Paragraph(" ", contentFont);
                        document.Add(para1);
                        document.HtmlStyleClass = @".IContent{font-family: Times New Roman;text-align: justify;font-size: 22px;}";

                        using (TextReader reader = new StringReader(Convert.ToString(text)))
                        {
                            //Parse the HTML and write it to the document
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);
                        }
                        document.Close();
                    }
                    #endregion
                }
            }
            addWatermarkInsidePDF(fileName);
        }
        catch (Exception Ex)
        {
            fileName = string.Empty;
            Ex.Message.ToString();
        }
        return fileName;
    }
    public static int CountStringOccurrences(string text, string pattern)
    {
        // Loop through all instances of the string 'text'.
        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(pattern, i)) != -1)
        {
            i += pattern.Length;
            count++;
        }
        return count;
    }
    public string SendMailToAdmin(string question, string questionerEmail, string questionerName, string type)
    {
        string _ret = string.Empty;
        try
        {
            //DataTable dtRecord = new DataTable();
            string body = null;
            string subject = string.Empty;
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = ConfigurationManager.AppSettings["adminMail"];
            string MailURLFinal = MailURL + "/Landing.aspx";
            string Content = string.Empty;

            switch (type.ToLower())
            {
                case "feedback":
                    Content = "<b>" + questionerName + " </b> have following suggestions/feedback for you.";
                    break;
                case "query":
                    Content = "<b>" + questionerName + " </b> have facing following technical issue.";
                    break;
                case "faq":
                    Content = "<b>" + questionerName + " </b> have following queries.";
                    break;
            }

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            //body = body.Replace("{UserName}", UserName);
            body = body.Replace("{UserName}", "Skorkel");
            body = body.Replace("{Content}", "<span style='font-size:14px;color:#373A36;'>" + Content + "</span> <br/><br/>" +
                                "<b>Message:</b><br/> " + question +
                                "<br/><br/><span style='font-weight:bold;font-size:14px;color:#373A36;'></span><br/> " +
                                "<b>User Details:</b> <br />" +
                                "<b>Name:</b> " + questionerName + "<br />" +
                                "<b>Email:</b> " + questionerEmail + "<br/><br/><br/><br/>" +
                                "Login URL:" + "&nbsp;&nbsp;<a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Login</a>");

            subject = "" + type + " raised by " + questionerName + "";
            EmailSender.SendEmail(MailTo, subject, body);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return _ret;
    }
    private void addWatermarkInsidePDF(string fileName)
    {
        string sourceFilePath = Server.MapPath("~\\SharingDoc\\") + fileName;

        byte[] bytes = File.ReadAllBytes(sourceFilePath);
        var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/watermark_img.png"));
        img.ScaleToFit(375, 700);
        img.Alignment = iTextSharp.text.Image.UNDERLYING;
        img.SetAbsolutePosition(100, 200);
        PdfContentByte waterMark;
        using (MemoryStream stream = new MemoryStream())
        {
            PdfReader reader = new PdfReader(bytes);
            using (PdfStamper stamper = new PdfStamper(reader, stream))
            {
                int pages = reader.NumberOfPages;
                for (int i = 1; i <= pages; i++)
                {
                    waterMark = stamper.GetUnderContent(i);
                    waterMark.AddImage(img);
                }
            }
            bytes = stream.ToArray();
        }
        File.WriteAllBytes(sourceFilePath, bytes);
    }
}



public class Documents
{
    public int start;
    public int end;
    public int imgtype;
    public int extralength;
    public int rownum;
    public string name;
    public int NoSpan;
    public int MaxCount;
}
