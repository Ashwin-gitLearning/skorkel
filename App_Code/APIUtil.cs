using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for APIUtil
/// </summary>
public class APIUtil
{

    static string APIURL = ConfigurationManager.AppSettings["APIURL"];
    static string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    static string MailURL = ConfigurationManager.AppSettings["MailURL"];


    public static int getArticleWordCount(string filePath)
    {
        if (ISAPIURLACCESSED != "0")
        {
            try
            {

                StringBuilder url = new StringBuilder();
                url.Append(APIURL);
                url.Append("pdfWordCount?");
                url.Append("url=");
                url.Append(MailURL + "/Articles/" + filePath);

                HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());
                myRequest1.Method = "GET";
                WebResponse myResponse1 = myRequest1.GetResponse();
                StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                String result = sr.ReadToEnd();
                dynamic res = JsonConvert.DeserializeObject(result);
                dynamic res2 = JsonConvert.DeserializeObject(Convert.ToString(res.responseJson));
                return Convert.ToInt32(res2.wordCount);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        return 0;
    }
}