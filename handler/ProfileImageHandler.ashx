<%@ WebHandler Language="C#" Class="ProfileImageHandler" %>

using System;
using System.Web;

public class ProfileImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                string ExtImage = System.IO.Path.GetExtension(file.FileName).TrimStart(".".ToCharArray()).ToLower();

                if ((ExtImage != "jpeg") && (ExtImage != "jpg") && (ExtImage != "png") && (ExtImage != "gif") && (ExtImage != "bmp"))
                {
                    docPath = "Please select image.";
                    context.Response.Write(docPath);
                    return;
                }
                else
                {
                    docPath = Convert.ToString(context.Session["UserId"]) + "_" + DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(file.FileName).ToString();
                    file.SaveAs(context.Server.MapPath("~\\CroppedPhoto\\" + docPath));
                }
            }
            context.Response.Write(docPath + ":" + fileName);
        }
        else
        {
            context.Response.Write("Please select image.");
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