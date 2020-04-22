<%@ WebHandler Language="C#" Class="DownloadFile" %>

using System;
using System.Web;
using System.Data;

public class DownloadFile : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            string strURL = request.QueryString["path"];
            string filename = HttpContext.Current.Server.MapPath(strURL);
            if (request.QueryString["filename"] != null && request.QueryString["filename"] != "") {
                filename = request.QueryString["filename"];
            }
            System.Net.WebClient req = new System.Net.WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            response.ContentType = "application/octet-stream";
            byte[] data = req.DownloadData(HttpContext.Current.Server.MapPath(strURL));
            response.BinaryWrite(data);
            response.End();
        }
        catch (Exception ex) {
            ex.Message.ToString();

        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}