using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;
using System.Net.Mail;
using System.Net.Mime;
using System.Data.SqlClient;

public static class EmailSender
{

    public static void SendEmail(string MailTo, string subject, string body)
    {

        try
        {
            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string Mailbody = "";
            body = body.Replace("{RedirectURL}", MailURL);
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            clientip.Credentials = new System.Net.NetworkCredential(username, Password);
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                Rmm2.Body = Mailbody.ToString();
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);
                Rmm2.Subject = subject;
                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                //clientip.SendAsync(Rmm2, null);
                //Rmm2.To.Clear();
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
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    public static void SendEmailWithAttachment(string MailTo, string subject, string body, string fileName)
    {

        try
        {
            Attachment data = new Attachment(HttpContext.Current.Server.MapPath("~\\tmp\\") + fileName, MediaTypeNames.Application.Octet);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string Mailbody = "";
            body = body.Replace("{RedirectURL}", MailURL);
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;
            clientip.Credentials = new System.Net.NetworkCredential(username, Password);
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            try
            {
                using (MailMessage Rmm2 = new MailMessage())
                {
                    Rmm2.IsBodyHtml = true;
                    Rmm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                    Rmm2.Body = Mailbody.ToString();
                    Rmm2.Attachments.Add(data);
                    Rmm2.To.Clear();
                    Rmm2.To.Add(MailTo);
                    Rmm2.Subject = subject;
                    Rmm2.AlternateViews.Add(htmlView);
                    Rmm2.IsBodyHtml = true;
                    clientip.Send(Rmm2);
                }
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
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}