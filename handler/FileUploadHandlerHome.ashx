<%@ WebHandler Language="C#" Class="FileUploadHandlerHome" %>

using System;
using System.Web;
using System.Drawing;

public class FileUploadHandlerHome : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

                string ext = System.IO.Path.GetExtension(file.FileName).ToLower();

                if (file.ContentLength == 0)
                {
                    docPath = "Cannot upload zero length file";
                    context.Response.Write(docPath);
                    return;
                }
                if (ext.Trim() != ".jpg" && ext.Trim() != ".jpeg" && ext.Trim() != ".png" && ext.Trim() != ".gif" && ext.Trim() != ".org" && ext.Trim() != ".bmp" && ext != ".mp4" && ext != ".wmv" && ext != ".flv" && ext != ".mpg" && ext != ".MP4" && ext != ".WMV" && ext != ".FLV" && ext != ".MPG" && ext != ".avi" && ext != ".AVI")
                {
                    docPath = "File format not supported.";
                    context.Response.Write(docPath);
                    return;
                }

                if (ext.Trim() == ".jpg" || ext.Trim() == ".jpeg" || ext.Trim() == ".png" || ext.Trim() == ".gif" || ext.Trim() == ".org" || ext.Trim() == ".bmp")
                {
                    if (file.ContentLength <= 3145728)
                    {
                        docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(file.FileName).ToString();
                        Image img = Image.FromStream(file.InputStream, true, true);
                        if (Array.IndexOf(img.PropertyIdList,0x112) > -1)
                        {
                            var prop = img.GetPropertyItem(0x112);
                            int val = BitConverter.ToUInt16(prop.Value, 0);
                            var rot = RotateFlipType.RotateNoneFlipNone;
                            if (val == 3 || val == 4)
                                rot = RotateFlipType.Rotate180FlipNone;
                            else if (val == 5 || val == 6)
                                rot = RotateFlipType.Rotate90FlipNone;
                            else if (val == 7 || val == 8)
                                rot = RotateFlipType.Rotate270FlipNone;

                            if (val == 2 || val == 4 || val == 5 || val == 7)
                                rot |= RotateFlipType.RotateNoneFlipX;

                            if (rot != RotateFlipType.RotateNoneFlipNone)
                                img.RotateFlip(rot);

                            img.Save(context.Server.MapPath("~\\UploadedPhoto\\" + docPath));
                        } else
                        {
                           file.SaveAs(context.Server.MapPath("~\\UploadedPhoto\\" + docPath));
                        }
                    }
                    else
                    {
                        docPath = "File size should be less than or equal to 3MB.";
                        context.Response.Write(docPath);
                        return;
                    }
                }

                if (ext == ".mp4" || ext == ".wmv" || ext == ".flv" || ext == ".mpg" || ext == ".MP4" || ext == ".WMV" || ext == ".FLV" || ext == ".MPG" || ext == ".avi" || ext == ".AVI")
                {
                    if (file.ContentLength <= 12582912)
                    {
                        docPath = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + System.IO.Path.GetExtension(file.FileName).ToString();
                        file.SaveAs(context.Server.MapPath("~\\VideoFiles\\" + docPath));
                    }
                    else
                    {
                        docPath = "Please choose a video file less than 12MB.";
                        context.Response.Write(docPath);
                        return;
                    }
                }
            }

            context.Response.Write(docPath + ":" + fileName);
        }
        else
        {
            context.Response.Write("please");
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