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
using Newtonsoft.Json;

public static class FCMNotification
{

    public static void Send(String Title, String Body, String UserID, String URL)
    {

        try
        {
            WebRequest tRequest = WebRequest.Create(ConfigurationManager.AppSettings["FirebaseAPI"]);
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FCMAuthKey"]));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", ConfigurationManager.AppSettings["FCMSenderId"]));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/user_id_"+ UserID,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = Body,
                    title = Title,
                    badge = 1
                },
                data = new
                {
                    url = URL
                }
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}