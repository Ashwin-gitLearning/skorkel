<%@ WebHandler Language="C#" Class="UploadArticle" %>

using System;
using System.Web;
using System.IO;

public class UploadArticle : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {


        if (context.Request.Files.Count > 0)
        {
            string docName = string.Empty; ;
            HttpFileCollection files = context.Request.Files;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string key in files)
            {
                HttpPostedFile file = files[key];
                string fileName = file.FileName;
                string ext = System.IO.Path.GetExtension(file.FileName);
                if (ext.Trim() != ".pdf")
                    docName = "File format not supported.";
                else
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 1000000); //for ints
                    docName = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "")  + "-"+rInt + System.IO.Path.GetExtension(file.FileName).ToString();
                    string directory = context.Server.MapPath("~\\Articles\\");
                    if(!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    file.SaveAs(directory+ docName);
                }
            }
            context.Response.Write(docName);
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