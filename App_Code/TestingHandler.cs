using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Testing Handler to restrict file downloading based on if user session doesn't exist.
/// for instance my User session will be Session["User"]
/// </summary>
public class TestingHandler:IHttpHandler,IReadOnlySessionState
{
	
    bool IHttpHandler.IsReusable
    {
        get { return false; }
    }

    void IHttpHandler.ProcessRequest(HttpContext context)
    {

        if (context.Session["UserIfo"] == null)
            context.Response.Redirect("~/Admin/CustomError.aspx?isError=False");

        //var filExtension = GettingExtension(context.Request.RawUrl);
        //if (filExtension == ".css")
        //    context.Response.Redirect("~/Admin/CustomError.aspx?isError=False");
        //context.Response.ClearContent();
        //context.Response.ClearHeaders();
        //context.Response.ContentType = MIMEType.Get(filExtension);
        //context.Response.AddHeader("Content-Disposition", "attachment");
        //context.Response.WriteFile(context.Request.RawUrl);
        //context.Response.Flush();

    }
    public string GettingExtension(string rawUrl)
    {
       return rawUrl.Substring(rawUrl.LastIndexOf(".", System.StringComparison.Ordinal));
    }
}