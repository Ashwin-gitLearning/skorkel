<%@ WebHandler Language="C#" Class="Serach" %>

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
using System.IO;



public class Serach : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    DO_Scrl_UserPostUpdateTbl objpost = new DO_Scrl_UserPostUpdateTbl();
    DA_Scrl_UserPostUpdateTbl objpostDB = new DA_Scrl_UserPostUpdateTbl();
    DataTable dt = new DataTable();

    public void ProcessRequest(HttpContext context)
    {
        string searchText = context.Request.QueryString["q"];

        objpost.intRegistrationId = Convert.ToInt32(context.Session["ExternalUserId"]);
        if (searchText != "")
        {
            objpost.strSearch = searchText.Trim().Replace("'", "''");
        }
        else
        {
            objpost.strSearch = "";
        }
        dt = objpostDB.GetDataTable(objpost, DA_Scrl_UserPostUpdateTbl.Scrl_UserPostUpdateTbl.GetUnionallSearchDetails);

        StringBuilder sb = new StringBuilder();

        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["vchrPhotoPath"].ToString() == "" || dt.Rows[i]["vchrPhotoPath"].ToString() == null || dt.Rows[i]["vchrPhotoPath"].ToString() == "CroppedPhoto/")
                {
                    //Newly Added
                    if (dt.Rows[i]["Header"].ToString() == "Group")
                    {
                        dt.Rows[i]["vchrPhotoPath"] = "images/groupPhoto.jpg";
                    }
                    else
                    {
                        dt.Rows[i]["vchrPhotoPath"] = "images/profile-photo.png";
                    }
                }
                else
                {
                    string imgPathPhysical = System.Web.HttpContext.Current.Server.MapPath("~/CroppedPhoto/" + dt.Rows[i]["vchrPhotoPath"]);
                    if ((System.IO.File.Exists(imgPathPhysical))==false)
                    {
                        if (dt.Rows[i]["Header"].ToString() == "Group")
                        {
                            dt.Rows[i]["vchrPhotoPath"] = "images/groupPhoto.jpg";
                        }
                        else
                        {
                            dt.Rows[i]["vchrPhotoPath"] = "images/profile-photo.png";
                        }
                        //dt.Rows[i]["vchrPhotoPath"] = "profile-photo.png";
                    } else
                    {
                           dt.Rows[i]["vchrPhotoPath"] = "CroppedPhoto/" + dt.Rows[i]["vchrPhotoPath"];
                    }
                }

                sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["vchrPhotoPath"].ToString(), dt.Rows[i]["HeaderNo"].ToString(), dt.Rows[i]["strPostDescription"].ToString(), dt.Rows[i]["MaxAllCount"].ToString(), dt.Rows[i]["Id"].ToString(), dt.Rows[i]["Header"].ToString(), Environment.NewLine));
            }
        }
        else
        {
            sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "No Record Found.", "profile-photo.png", "No Record", "", "1", "1", "No Record", Environment.NewLine));
        }

        context.Response.Write(sb.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}