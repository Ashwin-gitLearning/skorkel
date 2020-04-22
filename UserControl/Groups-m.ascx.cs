using System;
using System.Data;
using DA_SKORKEL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Net;
using System.IO;

public partial class UserControl_GroupsM : System.Web.UI.UserControl
{

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_GroupShare objGroupShareDA = new DA_GroupShare();
    DO_GroupShare objGroupShareDO = new DO_GroupShare();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_WallMessage WallMessageDO = new DO_WallMessage();
    DA_WallMessage WallMessageDA = new DA_WallMessage();
    DataTable dt = new DataTable();
    string RequestType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
                Session["intGroupId"] = Request.QueryString["GrpId"];
                GetGroupDetails();
          
            }


      
        }
    }



    protected void GetGroupDetails()
    {
        objgrp.intAddedBy = Convert.ToInt32(Convert.ToString(Session["ExternalUserId"]));
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        Session["GroupOwnerId"] = Convert.ToInt32(ViewState["intGroupId"]);

        DataSet ds = new DataSet();
        ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["GetGroupDetailsByGroupId"] = ds;

            if (ds.Tables[0].Rows[0]["strLogoPath"].ToString() != "" && ds.Tables[0].Rows[0]["strLogoPath"].ToString() != string.Empty)
            {
                    string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + Convert.ToString(ds.Tables[0].Rows[0]["strLogoPath"]));
                    if (File.Exists(imgPathPhysical))
                    {
                        imgGrp.Src = "~\\CroppedPhoto\\" + Convert.ToString(ds.Tables[0].Rows[0]["strLogoPath"]);
                    }
                    else
                    {
                        imgGrp.Src = "~/images/photo1.png";
                    }
            }
            else
                imgGrp.Src = "~/images/photo1.png";
            ViewState["GrpOwnerID"] = Convert.ToString(ds.Tables[0].Rows[0]["intRegistrationId"]);
            //lblOwner.Text = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);

            lblGroupName.Text = Convert.ToString(ds.Tables[0].Rows[0]["strGroupName"]);

           
        }

       
    }

   
}