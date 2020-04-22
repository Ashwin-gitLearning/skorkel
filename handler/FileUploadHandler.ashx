<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;

public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            string docPath = string.Empty;
            string Error = string.Empty;
            string fileName = string.Empty;
            HttpFileCollection files = context.Request.Files;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string key in files)
            {
                HttpPostedFile file = files[key];
                fileName = file.FileName;

                string ext = System.IO.Path.GetExtension(file.FileName);
                string exten = ext.Trim().ToLower();
                //exten == ".jpg" || exten == ".jpeg" || exten == ".png" || exten == ".gif" || exten == ".bmp" || exten == ".tif" || 
                if (exten == "ASF" || exten == "MPJPEG" || exten == "TS" || exten == "AVI" || exten == ".FLV" || exten == ".WAV" || exten == ".x-msvideo" || exten == ".webm" || exten == ".mp3" || exten == ".mp4" || file.ContentLength > 5*1024*1024)
                {
                    //docPath = "Only PDF or DOC/DOCX file Support, Max File Size 5MB";
                    docPath = "Video files not supported, Max File Size 5MB";
                    context.Response.Write(docPath);
                    return;
                }
                else
                {
                    docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(file.FileName).ToString();
                    file.SaveAs(context.Server.MapPath("~\\UploadDocument\\" + docPath));
                    context.Session["fileName"] = fileName;
                }
            }

            context.Response.Write(docPath + ":" + fileName);
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