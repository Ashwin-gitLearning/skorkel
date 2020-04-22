<%@ WebHandler Language="C#" Class="UploadDocument" %>

using System;
using System.Web;

public class UploadDocument : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {


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

                if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif")
                {                    
                    return;
                }else
                
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 1000000); //for ints
                    docName = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "")  + "-"+rInt + System.IO.Path.GetExtension(file.FileName).ToString();
                    file.SaveAs(context.Server.MapPath("~\\UploadDocument\\" + docName));
                    //ViewState["docPath"] = docPath;
                }
                
                //fileName = context.Server.MapPath("~/uploads/" + fileName);
                //file.SaveAs(fileName);
            }
            context.Response.Write(docName);
        }


        //int FileLength = upload.PostedFile.ContentLength;
        //string ext = System.IO.Path.GetExtension(this.upload.PostedFile.FileName);

        //if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif")
        //{
        //    lblMessage.Text = "File format not supported.";
        //    lblMessage.CssClass = "RedErrormsg";
        //    return;
        //}
        //if (FileLength <= 3145728)
        //{
        //    docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(upload.FileName).ToString();
        //    upload.SaveAs(Server.MapPath("~\\UploadDocument\\" + docPath));
        //    ViewState["docPath"] = docPath;
        //}
        //else
        //{
        //    lblMessage.Text = "File size should be less than or equal to 3MB";
        //    lblMessage.CssClass = "RedErrormsg";
        //    return;
        //}
        
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}