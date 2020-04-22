<%@ WebHandler Language="C#" Class="GetSummaryDetails" %>

using System;
using System.Web;
using System.Data;
using DA_SKORKEL;

public class GetSummaryDetails : IHttpHandler
{

    DA_ContentSummary objSummaryDb = new DA_ContentSummary();
    DO_ContentSummary objSummary = new DO_ContentSummary();

    DO_Comment objcomment = new DO_Comment();
    DA_Comment dbcomment = new DA_Comment();

    public void ProcessRequest(HttpContext context)
    {
        string TagType = Convert.ToString(context.Request.QueryString["TagType"]);
        if (TagType == "Summary")
        {
            objSummary.ContentId = Convert.ToInt32(context.Request.QueryString["ContentId"]);
            objSummary.addedby = Convert.ToInt32(context.Request.QueryString["AddedBy"]);
            DataTable dtSummary = new DataTable();
            dtSummary = objSummaryDb.GetDataTable(objSummary, DA_ContentSummary.ContentSummary.SingleRecord);
            if (dtSummary.Rows.Count > 0)
            {
                string datatext = " ";
                int ContentId = 0;
                datatext = dtSummary.Rows[0]["strSummaryText"].ToString();
                ContentId = Convert.ToInt32(dtSummary.Rows[0]["intContentId"]);
                context.Response.Write(datatext);
            }
            else
            {
                context.Response.Write("");
            }
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