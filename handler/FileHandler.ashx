<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System.Text;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.SessionState;

public class FileHandler : IHttpHandler ,IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        try
        {
            string searchText = context.Request.QueryString["q"];
            context.Session["File"] = searchText;
        }
        catch (Exception ex) { ex.Message.ToString(); }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}