<%@ WebHandler Language="C#" Class="CommentSearch" %>

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

public class CommentSearch : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    DO_Scrl_UserPrivacySettings objpost = new DO_Scrl_UserPrivacySettings();
    DA_Scrl_UserPrivacySettings objpostDB = new DA_Scrl_UserPrivacySettings();
    
    DataTable dt = new DataTable();

    public void ProcessRequest(HttpContext context)
    {
        string searchText = context.Request.QueryString["q"];        

        if (searchText.Contains("@"))
        {
            int j = searchText.IndexOf(@"@");

            string newString = searchText.Substring(j + 1);

            objpost.txtSearchText = newString;
            objpost.intAddedBy = Convert.ToInt32(context.Session["ExternalUserId"]);
            dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPrivacySettings.Scrl_UsePrivacySettings.SingleRecord);

            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                context.Application["ConnectedUserListOnComment"] = dt;
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["vchrPhotoPath"].ToString() == "" || dt.Rows[i]["vchrPhotoPath"].ToString() == null || dt.Rows[i]["vchrPhotoPath"].ToString() == "CroppedPhoto/")
                    {
                        dt.Rows[i]["vchrPhotoPath"] = "people-th3.jpg";
                    }

                    sb.Append(string.Format("{0},{1},{2},{3}", dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["vchrPhotoPath"].ToString(), dt.Rows[i]["Id"].ToString(), Environment.NewLine));
                }
            }


            context.Response.Write(sb.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}