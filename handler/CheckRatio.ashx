<%@ WebHandler Language="C#" Class="CheckRatio" %>

using System;
using System.Web;
using System.Data;
using DA_SKORKEL;

public class CheckRatio : IHttpHandler {

    DA_Ratio objDARatio = new DA_Ratio();
    DO_Ratio objDORatio = new DO_Ratio();

    public void ProcessRequest(HttpContext context)
    {      
        objDORatio.CaseId = Convert.ToInt32(context.Request.QueryString["ContentId"]);
        objDORatio.intTagType = Convert.ToInt32(context.Request.QueryString["TagType"]);
        objDORatio.ContentTypeID = Convert.ToInt32(context.Request.QueryString["ContentTypeId"]);
        
        DataTable dtRatio = new DataTable();

        dtRatio = objDARatio.GetDataTable(objDORatio, DA_Ratio.Ratio.GetRatioByCaseId);
        string datatext = "";
        if (dtRatio.Rows.Count > 0)
        {
             datatext = "1";
                                  
            //datatext = dtSummary.Rows[0]["strSummaryText"].ToString();
            //ContentId = Convert.ToInt32(dtSummary.Rows[0]["intContentId"]);
            context.Response.Write(datatext);            
        }
        else
        {
            datatext = "0";
            
            context.Response.Write(datatext);
        }        
        
    }
    
    //public void ProcessRequest (HttpContext context) {
    //    context.Response.ContentType = "text/plain";
    //    context.Response.Write("Hello World");
    //}
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}