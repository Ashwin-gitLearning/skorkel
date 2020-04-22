using DA_SKORKEL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    DataTable dt = new DataTable();

    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();

    DO_Registrationdetails objdoreg = new DO_Registrationdetails();
    DA_Registrationdetails objdareg = new DA_Registrationdetails();

    DO_Scrl_UserSendMessage objMessageDO = new DO_Scrl_UserSendMessage();
    DA_Scrl_UserSendMessage objMessageDA = new DA_Scrl_UserSendMessage();

    DO_Networks objdonetwork = new DO_Networks();
    DA_Networks objdanetwork = new DA_Networks();

    DO_Scrl_UserRecommendation objRecmndDO = new DO_Scrl_UserRecommendation();
    DA_Scrl_UserRecommendation objRecmndDA = new DA_Scrl_UserRecommendation();

    DO_Scrl_UserGroupJoin objGrpJoinDO = new DO_Scrl_UserGroupJoin();
    DA_Scrl_UserGroupJoin objGrpJoinDA = new DA_Scrl_UserGroupJoin();

    DA_Login objLoginDB = new DA_Login();
    DO_Login objLogin = new DO_Login();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DA_Profile ObjDAprofile = new DA_Profile();
    DO_Profile objDoProfile = new DO_Profile();

    DA_MyPoints objDAPoint = new DA_MyPoints();
    DO_MyPoints objDOPoint = new DO_MyPoints();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
        //this.txtSearch.Attributes.Add("onkeypress", "button_click(this,'" + this.btnSearch.ClientID + "')");

        if (!IsPostBack)
        {
            form1.Action = QueryStringModule.Encrypt(Request.Url.Query.ToString().Replace("?", ""));
            ViewState["orgid"] = Convert.ToInt32(Request.QueryString["orgid"]);
            ViewState["RegType"] = Convert.ToString(Session["RegType"]);
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                //Newly Added
                if (Convert.ToString(Session["RegType"]) == "" || Session["RegType"] == null)
                {
                    if (Convert.ToString(ViewState["RegType"]) != "A")
                    {
                        ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    }
                    else
                    {
                        Response.Redirect("Landing.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("Landing.aspx", true);
                }
            }
            else
            {
                Response.Redirect("Landing.aspx", true);
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            lblUserName.InnerText = Convert.ToString(Session["LoginName"]);
            GetLoginUserDetails();
            BindMessages();
            BindNotificationRequest();
            GetMessageNotification();

        }

        GetNotifications();
        SetBreadcrumb();
        TotalscoreCount();
        //BindSANotifications();
    }

    //Renamed and set Breadcrum into the method only
    public void SetBreadcrumb()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToLower();
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        //return sRet;
        mdlBreadCrum1.Style.Add("display", "none");
        mdlBreadCrum2.Style.Add("display", "none");

        if (sRet == "Home.aspx" || sRet == "home.aspx")
        {
            myli_home.Attributes["class"] = "active";
            sRet = "Home";
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                //mdlBreadCrum0.Visible = true;
                objDoProfile.RegistrationId = Convert.ToInt32(Request.QueryString["RegId"]);
                DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
                if (dt.Rows.Count > 0)
                {
                    lblBread1.Text = "Home";
                    mdlBreadCrum1.Style.Add("display", "block");
                    sRet = dt.Rows[0]["NAME"].ToString();
                }
            }
        }
        else if (sRet == "profile2.aspx")
        {
            //  liDashboard.Attributes["class"] = "breadcrumb-item active";
            lblBread1.Text = "Home";
            mdlBreadCrum1.Style.Add("display", "block");
            sRet = "Build Your Score";
            myli_home.Attributes["class"] = "active";
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                objDoProfile.RegistrationId = Convert.ToInt32(Request.QueryString["RegId"]);
                DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);
                lblBread1.Text = "Home";
                mdlBreadCrum1.Style.Add("display", "block");
                if (dt.Rows.Count > 0)
                {
                    lblDashBoardMiddle2.Text = dt.Rows[0]["NAME"].ToString();
                    mdlBreadCrum2.Style.Add("display", "block");
                    sRet = "Profile";
                }
            }
        }
        //else if (sRet == "my-points.aspx")
        //{

        //    myli_scrkl.Attributes["class"] = "active";
        //    myli_Score.Attributes["class"] = "active";
        //    lblBread1.Text = "My Skorkel";
        //    mdlBreadCrum1.Style.Add("display", "block");
        //    sRet = "My Score";
        //    drodown_score.Attributes["class"] = "drop-down rotateDropDown";

        //}
        else if (sRet == "research_searchresult_s.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_rsrch.Attributes["class"] = "";
            }
            else
            {
                myli_rsrch.Attributes["class"] = "active";
            }
            sRet = "Research";
        }
        else if (sRet == "research-case-details.aspx")
        {
            lblBread1.Text = "Research";
            mdlBreadCrum1.Style.Add("display", "block");
            myli_rsrch.Attributes["class"] = "active";

            sRet = "Detail";
        }
        else if (sRet == "allblogs.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_AllBlogs.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";

            }
            else
            {
                myli_AllBlogs.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
            }
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            sRet = "Blogs";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "write-blog.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_AllBlogs.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";
            }
            else
            {
                myli_AllBlogs.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
            }
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            mdlBreadCrum2.Style.Add("display", "block");
            lblDashBoardMiddle2.Text = "Blogs";
            sRet = "Create";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "blogscomments.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_AllBlogs.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";
            }
            else
            {
                myli_AllBlogs.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
            }
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            mdlBreadCrum2.Style.Add("display", "block");
            lblDashBoardMiddle2.Text = "Blogs";
            sRet = "Detail";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "allquestiondetails.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_AllQuestDet.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";
            }
            else
            {
                myli_AllQuestDet.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
                drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
            }
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            sRet = "Q & A";

        }
        else if (sRet == "searchgroup.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_SrchGrp.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";
            }
            else
            {
                myli_SrchGrp.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
            }
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            sRet = "Groups";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }

        //        else if (sRet == "Group-Profile.aspx" || sRet == "group-profile.aspx" || sRet == "Group-Home.aspx" || sRet == "group-home.aspx" || sRet == "forum-landing-page.aspx" || sRet == "Forum-Landing-Page.aspx" || sRet == "Create-Forum.aspx" || sRet == "create-forum.aspx" || sRet == "forum-detail.aspx" || sRet == "Forum-Detail.aspx" || sRet == "uploads -docs-details.aspx" || sRet == "Uploads-Docs-Details.aspx" || sRet == "Polls-Details.aspx" || sRet == "polls-details.aspx" || sRet == "Group-Event-Main.aspx" || sRet == "group-event-main.aspx" || sRet == "groups-members.aspx" || sRet == "Groups-Members.aspx" || sRet == "Create-Poll.aspx" || sRet == "create-poll.aspx" || sRet == "Polls-Details.aspx" || sRet == "polls-details.aspx" || sRet == "Polls-VoteComment.aspx" || sRet == "polls-votecomment.aspx" || sRet == "create-uploaddocuments.aspx")

        else if (sRet == "group-profile.aspx" || sRet == "group-home.aspx" || sRet == "forum-landing-page.aspx" || sRet == "create-forum.aspx" || sRet == "forum-detail.aspx" || sRet == "uploads-docs-details.aspx" || sRet == "polls-details.aspx" || sRet == "group-event-main.aspx" || sRet == "groups-members.aspx" || sRet == "create-poll.aspx" || sRet == "create-uploaddocuments.aspx" || sRet == "polls-votecomment.aspx")
        {
            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                myli_SrchGrp.Attributes["class"] = "";
                myli_AllBlogs.Attributes["class"] = "";
                myli_qdet.Attributes["class"] = "";

                sRet = "Groups";
                drodown_interact.Attributes["class"] = "drop-down rotateDropDown";


            }
            else
            {
                //myli_AllBlogs.Attributes["class"] = "active";
                myli_qdet.Attributes["class"] = "active";
                myli_SrchGrp.Attributes["class"] = "active";
                //myli_qdet.Attributes["class"] = "active";
                if (!string.IsNullOrEmpty(Request.QueryString["GrpId"]))
                {
                    objgrp.inGroupId = Convert.ToInt32(Request.QueryString["GrpId"]);
                }
                else
                {
                    objgrp.inGroupId = -1;
                }
                DataTable dt = objgrpDB.GetDataTable(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGeneralGroupDetaiils);

                //objDoProfile.RegistrationId = Convert.ToInt32(Request.QueryString["RegId"]);
                //DataTable dt = ObjDAprofile.GetMyProfileDetails(objDoProfile, DA_Profile.Myprofile.GetAllProfileUSerDetails);

                if (dt.Rows.Count > 0)
                {
                    mdlBreadCrum1.Style.Add("display", "block");
                    lblBread1.Text = "Interact";
                    mdlBreadCrum2.Style.Add("display", "block");
                    lblDashBoardMiddle2.Text = "Groups";
                    sRet = dt.Rows[0]["strGroupName"].ToString();
                    drodown_interact.Attributes["class"] = "drop-down rotateDropDown";

                    //sRet = "Profile";
                }
                else
                {
                    mdlBreadCrum1.Style.Add("display", "block");
                    lblBread1.Text = "Interact";
                    mdlBreadCrum2.Style.Add("display", "block");
                    lblDashBoardMiddle2.Text = "Groups";
                    sRet = "";
                    drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
                }
            }
            //mdlBreadCrum1.Style.Add("display", "block");
            //lblBread1.Text = "Interact";
            //Style.Add("display", "block");
            //lblDashBoardMiddle2.Text = "Groups";
            //sRet = "";
        }
        else if (sRet == "inbox.aspx")
        {
            myli_home.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "Messaging";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "notifications_details_2.aspx" || sRet == "Notifications_Details_2.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "Notifications";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "viewactivity.aspx" || sRet == "ViewActivity.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "View Activity";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "viewlogs.aspx" || sRet == "ViewLogs.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "View Logs";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "reset-password.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            myli_home.Attributes["class"] = "active";
            sRet = "Reset Password";
        }
        else if (sRet == "searchpeople.aspx" || sRet == "SearchPeople.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "Search People";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "create_group.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Interact";
            mdlBreadCrum2.Style.Add("display", "block");
            lblDashBoardMiddle2.Text = "Groups";
            sRet = "Create Group";
            myli_qdet.Attributes["class"] = "active";
            myli_SrchGrp.Attributes["class"] = "active";
        }
        else if (sRet == "post-new-question.aspx")
        {
            myli_qdet.Attributes["class"] = "active";
            myli_AllQuestDet.Attributes["class"] = "active";
            sRet = "Create";
            lblBread1.Text = "Interact";
            lblDashBoardMiddle2.Text = "Q & A";
            mdlBreadCrum1.Style.Add("display", "block");
            mdlBreadCrum2.Style.Add("display", "block");
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "question-details-sendcontact.aspx")
        {
            sRet = "Detail";
            lblBread1.Text = "Interact";
            lblDashBoardMiddle2.Text = "Q & A";
            mdlBreadCrum1.Style.Add("display", "block");
            mdlBreadCrum2.Style.Add("display", "block");
            myli_qdet.Attributes["class"] = "active";
            myli_AllQuestDet.Attributes["class"] = "active";
            drodown_interact.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "my-tag.aspx")
        {
            myli_scrkl.Attributes["class"] = "active";
            myli_Tag.Attributes["class"] = "active";
            lblBread1.Text = "My Skorkel";
            mdlBreadCrum1.Style.Add("display", "block");
            sRet = "My Tags";
            drodown_score.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "my-documents.aspx")
        {
            myli_scrkl.Attributes["class"] = "active";
            myli_Document.Attributes["class"] = "active";
            lblBread1.Text = "My Skorkel";
            mdlBreadCrum1.Style.Add("display", "block");
            sRet = "My Documents";
            drodown_score.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "my-bookmarks.aspx")
        {
            myli_scrkl.Attributes["class"] = "active";
            myli_Bookmarks.Attributes["class"] = "active";
            lblBread1.Text = "My Skorkel";
            mdlBreadCrum1.Style.Add("display", "block");
            sRet = "My Bookmarks";
            drodown_score.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "my-saved-searches.aspx")
        {
            myli_scrkl.Attributes["class"] = "active";
            myli_Search.Attributes["class"] = "active";
            lblBread1.Text = "My Skorkel";
            mdlBreadCrum1.Style.Add("display", "block");
            sRet = "My Searches";
            drodown_score.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "allnewslisting.aspx" || sRet == "AllNewsListing.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "News";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "news-details.aspx" || sRet == "News-Details.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            mdlBreadCrum2.Style.Add("display", "block");
            lblBread1.Text = "Home";
            lblDashBoardMiddle2.Text = "News";
            if (Convert.ToString(Session["Title"]) != "" && Session["Title"] != null)
            {
                sRet = Session["Title"].ToString();
            }
            else
            {
                sRet = "";
            }
            //sRet = "News Details";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "journallisting.aspx")
        {
            myli_jour.Attributes["class"] = "active";
            myli_Previous.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Journal";
            sRet = "All Journals";
            drodown_journal.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "journaldetails.aspx")
        {
            myli_jour.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Journal";
            myli_Previous.Attributes["class"] = "active";
            sRet = "Detail";
            drodown_journal.Attributes["class"] = "drop-down rotateDropDown";

        }
        else if (sRet == "currentjournal.aspx")
        {
            myli_jour.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Journal";
            sRet = "Current Journal";
            myli_Current.Attributes["class"] = "active";
            drodown_journal.Attributes["class"] = "drop-down rotateDropDown";
        }
        else if (sRet == "articledetails.aspx")
        {
            myli_jour.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Journal";
            sRet = "Article Detail";
            drodown_journal.Attributes["class"] = "drop-down rotateDropDown";

        }
        else if (sRet == "AllJobsListing.aspx" || sRet == "alljobslisting.aspx")
        {
            myli_Jobs.Attributes["class"] = "active";
            sRet = "Jobs";
        }
        else if (sRet == "Jobs-Details.aspx" || sRet == "jobs-details.aspx")
        {
            myli_Jobs.Attributes["class"] = "active";
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Jobs";
            sRet = "Detail";
        }
        else if (sRet == "UniversalSearch.aspx" || sRet == "universalsearch.aspx")
        {
            mdlBreadCrum1.Style.Add("display", "block");
            lblBread1.Text = "Home";
            sRet = "Universal Search";
            myli_home.Attributes["class"] = "active";
        }
        else if (sRet == "FAQ.aspx" || sRet == "faq.aspx")
        {
            myli_Jobs.Attributes["class"] = "active";
            sRet = "FAQ";
        }
        else
        {
            sRet = "";
        }
        lblDashBoardName.Text = sRet;
    }

    public void GetNotifications()
    {
        BindTopNotifcations();
    }

    protected void TotalscoreCount()
    {
        objDOPoint.UserID = Convert.ToInt32(Session["ExternalUserId"]);
        ScoreService.ScoreObject scores = ScoreService.getScore(objDOPoint.UserID);
        divScoreNew.InnerText = Convert.ToString(scores.TotalScore);
    }

    protected void ImgNotification_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (lblNotifyCount.Text != "0")
        {
            dvNotification.Style.Add("display", "block");
            BindTopNotifcations();
        }
    }

    protected void BindTopNotifcations()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "500";

            DataTable dtSearch = objdanetwork.GetUserSearch(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.GetTopNotifications);

            if (dtSearch.Rows.Count > 0)
            {
                dvAllNotifi.Visible = true;
                notificationlabel.Text = "Notifications";
                dvAllNotifi.Style.Add("display", "block");
                lstNotification.DataSource = dtSearch;
                lstNotification.DataBind();
            }
            else
            {
                notificationlabel.Text = "No Notification to show";
                lstNotification.DataSource = null;
                lstNotification.DataBind();
            }
            if (dtSearch.Rows.Count >= 5)
            {
                dvAllNotifi.Style.Add("display", "block");
            }
        }
        else
        {
            lstNotification.DataSource = null;
            lstNotification.DataBind();
        }
    }

    protected void BindSANotifications()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "500";

            DataTable dtSearch = objdanetwork.GetSANotifications(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), DA_Networks.NetworkDetails.GetSANotifications);
            if (dtSearch.Rows.Count > 0)
            {
                dvAllNotifi.Style.Add("display", "block");
                lstNotification.DataSource = dtSearch;
                lblNotifyCount.Text = dtSearch.Rows[0]["Maxcount"].ToString();
                lstNotification.DataBind();
            }
            else
            {
                lstNotification.DataSource = null;
                lstNotification.DataBind();
            }
            if (dtSearch.Rows.Count >= 5)
            {
                dvAllNotifi.Style.Add("display", "block");
            }
        }
        else
        {
            lstNotification.DataSource = null;
            lstNotification.DataBind();
        }
    }

    protected void GetLoginUserDetails()
    {
        DataTable dtDetails = new DataTable();
        objRegistration.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dtDetails = objRegistrationDB.GetDataTable(objRegistration, DA_Registrationdetails.RegistrationDetails.GetApprovedStudentByInstitute);
        if (dtDetails.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dtDetails.Rows[0]["vchrPhotoPath"])))
            {
                string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString());
                if (File.Exists(imgPathPhysical))
                {
                    imgProfilePic.Src = "CroppedPhoto/" + dtDetails.Rows[0]["vchrPhotoPath"].ToString();
                }
                else
                {
                    imgProfilePic.Src = "images/profile-photo.png";
                }
            }
            else
            {
                imgProfilePic.Src = "images/profile-photo.png";
            }
        }
    }

    protected void BindMessages()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            lblInboxCount.Text = "";
            objMessageDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            DataTable dtmessage = objMessageDA.GetDataTable(objMessageDO, DA_Scrl_UserSendMessage.Scrl_UserSendMessage.AllRecords);
            if (dtmessage.Rows.Count > 0)
            {
                ViewState["MaxCount"] = dtmessage.Rows[0]["Maxcount"].ToString();
                lblInboxCount.Text = dtmessage.Rows[0]["Maxcount"].ToString();
                //lblInboxCount.Text = "";
            }
            else
            {
                lblInboxCount.Text = "0";
                //lblInboxCount.Text = "";
            }
        }
        else
        {
            lblInboxCount.Text = "0";
            //lblInboxCount.Text = "";
        }
    }

    protected void BindNotificationRequest()
    {
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "500";

        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            dt.Clear();
            objRegistration.RegistrationId = Convert.ToInt32(ViewState["UserID"]);
            dt = objRegistrationDB.GetDataTable(objRegistration, DA_Registrationdetails.RegistrationDetails.SingleRecord);
            DateTime notificaationdatetime = Convert.ToDateTime(dt.Rows[0]["notificationtimestamp"]);
            DataTable dtSearch = objdanetwork.GetNotificationCount(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.GetNotificationCount, notificaationdatetime);

            if (dtSearch.Rows.Count > 0)
            {
                hdnNotificationcount.Value = Convert.ToString(dtSearch.Rows[0]["Maxcount"]);

                if (dtSearch.Rows.Count > 0)
                {
                    lblNotifyCount.Text = dtSearch.Rows.Count.ToString();
                    //if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["intNotificationCount"])))
                    //{
                    //    int Ncount = Convert.ToInt32(Convert.ToString(dt.Rows[0]["intNotificationCount"]));
                    //    int MaxNCount = Convert.ToInt32(hdnNotificationcount.Value);

                    //    if (MaxNCount > Ncount)
                    //    {
                    //        lblNotifyCount.Text = Convert.ToString(dtSearch.Rows[0]["Maxcount"]);
                    //        lblNotifyCount.Text = Convert.ToString(MaxNCount - Ncount);
                    //    }
                    //    else
                    //    {
                    //        lblNotifyCount.Text = "0";
                    //    }
                    //}
                    //else
                    //{
                    //    lblNotifyCount.Text = "0";
                    //}
                }
                else
                {
                    lblNotifyCount.Text = string.Empty;
                }
            }
            else
            {
                lblNotifyCount.Text = "0";
                hdnNotificationcount.Value = "0";
            }
        }
    }

    protected void GetMessageNotification()
    {
        objRecmndDO.striInvitedUserId = Convert.ToString(ViewState["UserID"]);
        dt = objRecmndDA.GetDataTable(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.GetMessNotification);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["IsRead"]) != "0")
            {
                lblInboxCount.Text = Convert.ToString(dt.Rows[0]["IsRead"]);
                //lblInboxCount.Text = "";
            }
            else
            {
                lblInboxCount.Text = "0";
                //lblInboxCount.Text = "";
            }
        }
    }

    protected void BindAllRequest()
    {
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "500";

            DataTable dtSearch = objdanetwork.GetUserSearch(Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(Session["ExternalUserId"]), DA_Networks.NetworkDetails.SingleRecord);

            if (dtSearch.Rows.Count > 0)
            {
                lstNotification.DataSource = dtSearch;
                lstNotification.DataBind();
            }
            else
            {
                lstNotification.DataSource = null;
                lstNotification.DataBind();
            }
        }
        else
        {
            lstNotification.DataSource = null;
            lstNotification.DataBind();
        }
    }

    protected void lstNotification_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnReqID = (HiddenField)e.Item.FindControl("hdnReqID");
        HiddenField hdnPkId = (HiddenField)e.Item.FindControl("hdnPkId");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        Label lblEmailId = (Label)e.Item.FindControl("lblEmailId");
        //Label lblDescription = (Label)e.Item.FindControl("lblDescription");
        Label lblUserType = (Label)e.Item.FindControl("lblUserType");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        HiddenField hdnrequserid = (HiddenField)e.Item.FindControl("hdnrequserid");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        HiddenField hdnintUserTypeId = (HiddenField)e.Item.FindControl("hdnintUserTypeId");
        LinkButton lnkConfirm = (LinkButton)e.Item.FindControl("lnkConfirm");
        LinkButton lnkCancel = (LinkButton)e.Item.FindControl("lnkCancel");
        HiddenField hdnIsAccept = (HiddenField)e.Item.FindControl("hdnIsAccept");
        HiddenField hdnStrRecommendation = (HiddenField)e.Item.FindControl("hdnStrRecommendation");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");

        hdnEmailId.Value = lblEmailId.Text;
        ViewState["lblGroupName"] = lblGroupName.Text;

        if (e.CommandName == "Details")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }

        if (e.CommandName == "ShareDetails")
        {
            if (hdnTableName.Value == "Scrl_GrpShareUserStatusTbl")
            {
                Response.Redirect(hdnStrRecommendation.Value);
                return;
            }

            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + hdnrequserid.Value);
            }
        }

        if (e.CommandName == "QAShareDetails")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect(hdnStrRecommendation.Value);
            }
        }
        if (e.CommandName == "BlogShareDetails")
        {
            if (hdnRegistrationId.Value != Convert.ToString(ViewState["UserID"]))
            {
                Response.Redirect(hdnStrRecommendation.Value);
            }
        }

        if (e.CommandName == "Confirm")
        {
            if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
            {
                objRegistration.intRequestInvitaionId = Convert.ToInt32(hdnPkId.Value);
                objRegistration.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRegistration.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRegistration.IpAddress == null)
                    objRegistration.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRegistrationDB.AddEditDel_Request(objRegistration, DA_Registrationdetails.RegistrationDetails.Update);
                SendMail(e.CommandName, hdnTableName.Value);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallUserMethod();", true);
            }

            else if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
            {
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                SendMail(e.CommandName, hdnTableName.Value);
            }

            else if (hdnTableName.Value == "Scrl_UserRecommendationTbl")
            {
                objRecmndDO.intRecommendationId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Update);
                SendMail(e.CommandName, hdnTableName.Value);
                if (lblmaster.InnerText == "Home")
                {
                    Response.Redirect("Home.aspx");
                }
            }
            else if (hdnTableName.Value == "Scrl_OrgEndorsement")
            {
                objRecmndDO.intOrgEndorseId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelOrganization(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_OrgMessage.UpdateEnodorsement);
            }
            else if (hdnTableName.Value == "Scrl_OrgnisationGroupJoiningTbl")
            {
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDA.AddEditDel_Scrl_OrgnisationGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_OrgnisationGroupJoin.Update);

            }
            else if (hdnTableName.Value == "Scrl_RequestGroupJoin")
            {
                objgrp.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objgrp.inGroupId = Convert.ToInt32(hdnShareInvitee.Value);
                DataSet ds = new DataSet();
                ds = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetOtherGroupDetailsByGroupId);

                if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                        {
                            objGrpJoinDO.isAccepted = 2;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "A")
                        {
                            objGrpJoinDO.isAccepted = 1;
                        }
                    }
                }

                else if (Convert.ToString(ds.Tables[0].Rows[0]["strAccess"]) == "R")
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "0")
                        {
                            objGrpJoinDO.isAccepted = 1;
                            return;
                        }
                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "1")
                        {
                            objGrpJoinDO.isAccepted = 2;
                        }

                        else if (Convert.ToString(ds.Tables[1].Rows[0]["IsAccepted"]) == "2")
                        {
                            objGrpJoinDO.isAccepted = 0;
                        }
                    }
                }

                objGrpJoinDO.isAccepted = 1;
                objGrpJoinDO.inGroupId = Convert.ToInt32(hdnShareInvitee.Value);
                objGrpJoinDO.intInvitedUserId = Convert.ToInt32(hdnRegistrationId.Value);
                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.strIpAddress = ip;

                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                DataTable dtt = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetDataFrom);
                if (dtt.Rows.Count == 0)
                {
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                    objGrpJoinDO.intInvitedUserId = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    objGrpJoinDO.intRegistrationId = Convert.ToInt32(hdnRegistrationId.Value);
                }
                else
                {
                    objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Insert);
                }
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.UpdateGroupMember);

                SendMail(e.CommandName, hdnTableName.Value);
            }
            BindAllRequest();
            BindNotificationRequest();
            BindTopNotifcations();

            if (lblmaster.InnerText == "All Notifications")
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }

        if (e.CommandName == "RemoveNotification")
        {
            if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
            {
                objRegistration.intRequestInvitaionId = Convert.ToInt32(hdnPkId.Value);
                objRegistration.AddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRegistration.IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRegistration.IpAddress == null)
                    objRegistration.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRegistrationDB.AddEditDel_Request(objRegistration, DA_Registrationdetails.RegistrationDetails.Delete);
                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
            {
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.isAccepted = 2;
                objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.Update);
                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_UserRecommendationTbl")
            {
                objRecmndDO.intRecommendationId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Delete);
                SendMail(e.CommandName, hdnTableName.Value);
            }
            else if (hdnTableName.Value == "Scrl_OrgEndorsement")
            {
                objRecmndDO.intRecommendationId = Convert.ToInt32(hdnPkId.Value);
                objRecmndDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objRecmndDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objRecmndDO.strIpAddress == null)
                    objRecmndDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objRecmndDA.Scrl_AddEditDelRecommendations(objRecmndDO, DA_Scrl_UserRecommendation.Scrl_UserRecommendation.Delete);
            }
            else if (hdnTableName.Value == "Scrl_RequestGroupJoin")
            {
                objGrpJoinDO.intAddedBy = Convert.ToInt32(Session["ExternalUserId"].ToString());
                objGrpJoinDO.strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (objGrpJoinDO.strIpAddress == null)
                    objGrpJoinDO.strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                objGrpJoinDO.intRequestJoinId = Convert.ToInt32(hdnPkId.Value);
                objGrpJoinDA.AddEditDel_Scrl_UserGroupJoin(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.DeleteGroupMember);
                SendMail(e.CommandName, hdnTableName.Value);
            }

            BindNotificationRequest();
            BindTopNotifcations();

            if (lblmaster.InnerText == "All Notifications")
            {
                Response.Redirect("Notifications_Details_2.aspx");
            }
        }
    }

    protected void lstNotification_ItemDataBound(object sender, ListViewItemEventArgs e)
    {


        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegID");
        HiddenField hdnSANotification = (HiddenField)e.Item.FindControl("hdnSANotification");
        HiddenField hdnShareInvitee = (HiddenField)e.Item.FindControl("hdnShareInvitee");
        HiddenField hdnTableName = (HiddenField)e.Item.FindControl("hdnTableName");
        HiddenField hdnStrRecommendation = (HiddenField)e.Item.FindControl("hdnStrRecommendation");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblnotificationname = (Label)e.Item.FindControl("lblnotificationname");
        Label lblGroupName = (Label)e.Item.FindControl("lblGroupName");
        LinkButton lnkConfirm = (LinkButton)e.Item.FindControl("lnkConfirm");
        LinkButton lnkCancel = (LinkButton)e.Item.FindControl("lnkCancel");
        LinkButton lnkShareDetail = (LinkButton)e.Item.FindControl("lnkShareDetail");
        Label lnkName = (Label)e.Item.FindControl("lnkName");
        Label lblComment = (Label)e.Item.FindControl("lblComment");
        HiddenField hdnIsAccept = (HiddenField)e.Item.FindControl("hdnIsAccept");
        LinkButton lnkQAshare = (LinkButton)e.Item.FindControl("lnkQAshare");
        LinkButton lnkBlogshare = (LinkButton)e.Item.FindControl("lnkBlogshare");
        HiddenField hdnintIsAccept = (HiddenField)e.Item.FindControl("hdnintIsAccept");
        LinkButton lnkConnected = (LinkButton)e.Item.FindControl("lnkConnected");
        HtmlGenericControl SearchRept = (HtmlGenericControl)e.Item.FindControl("SearchRept");
        LinkButton hyp = (LinkButton)e.Item.FindControl("hyp");

        if (hdnTableName.Value == "Scrl_UserGroupJoiningTbl")
        {
            ViewState["lblGroupNames"] = lblGroupName.Text;
            ViewState["hyp"] = hyp.Text;
            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "group";
            if (hdnIsAccept.Value == "2")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
            }

            if (hdnIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }

            else if (hdnIsAccept.Value == "0")
            {
                lnkConfirm.Visible = true;
                lnkCancel.Visible = true;
            }

        }
        if (hdnTableName.Value == "Scrl_RequestGroupJoin")
        {
            if (ViewState["lblGroupNames"] != null)
            {
                if (ViewState["lblGroupNames"].ToString() == lblGroupName.Text && ViewState["hyp"].ToString() == hyp.Text)
                {
                    SearchRept.Style.Add("display", "none");
                }
            }

            if (hdnintIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }
            else
            {
                lnkConfirm.Visible = true;
                lnkCancel.Visible = true;
            }

            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "group";
        }
        else if (hdnTableName.Value == "Scrl_UserRequestInvitationTbl")
        {
            lblnotificationname.Text = "Request Invitation";
            if (hdnIsAccept.Value == "1")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
                lnkConnected.Visible = true;
            }
            else if (hdnIsAccept.Value == "0")
            {
                lnkConfirm.Visible = true;
                lnkCancel.Visible = true;
            }


        }
        else if (hdnTableName.Value == "Scrl_UserRecommendationTbl")
        {
            lblComment.Visible = true;
            lblComment.Text = Convert.ToString(hdnStrRecommendation.Value);
            lblnotificationname.Text = "Recommendation";
            if (hdnIsAccept.Value == "2")
            {
                lnkConfirm.Visible = false;
                lnkCancel.Visible = false;
            }

            lnkConfirm.Visible = false;
            lnkCancel.Visible = false;
        }
        else if (hdnTableName.Value == "Scrl_UserRecommendationChildTbl")
        {
            lblnotificationname.Text = "Ask for Recommendation";
        }

        else if (hdnTableName.Value == "Scrl_UserProfileWallTbl")
        {
            lblnotificationname.Text = "Wall Post";
        }

        else if (hdnTableName.Value == "Scrl_GroupShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "group";
            lnkShareDetail.Text = lblGroupName.Text;
        }
        else if (hdnTableName.Value == "Scrl_UserPostQAReplyTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "QA";
            lnkQAshare.Text = lblGroupName.Text;
        }
        else if (hdnTableName.Value == "Scrl_BlogHeadingLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "Blog";
            lnkBlogshare.Text = lblGroupName.Text;
        }
        else if (hdnTableName.Value == "SCL_SA_Notifications")
        {

            hyp.Text = "Skorkel";
            lblnotificationname.Text = "Notification";
            lblSANotification.Visible = true;


            lnkBlogshare.Text = lblGroupName.Text;

            lnkBlogshare.Attributes["class"] = "remove-hover-anchor";

            imgprofile.Src = "images/notificaion-logo.png";

        }
        else if (hdnTableName.Value == "Scrl_MicrolTagLikeShareTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "Document";
            lnkBlogshare.Text = lblGroupName.Text;
        }

        else if (hdnTableName.Value == "Scrl_OrgnisationGroupJoiningTbl")
        {
            lblnotificationname.Text = "Request to join" + " " + lblGroupName.Text + " " + "Orgnisation group";
        }
        else if (hdnTableName.Value == "Scrl_GrpShareUserStatusTbl")
        {
            DataTable dtShare = new DataTable();
            lnkConfirm.Style.Add("display", "none");
            lnkCancel.Style.Add("display", "none");

            lblnotificationname.Text = "Share" + " " + "group status link";
            lnkShareDetail.Text = lblGroupName.Text;
        }
        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/profile-photo.png";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/" + imgprofile.Src);
            if (File.Exists(imgPathPhysical))
            {

            }
            else
            {
                imgprofile.Src = "images/profile-photo.png";
            }

        }
        if (hdnRegistrationId.Value == ViewState["UserID"].ToString() && hdnTableName.Value != "SCL_SA_Notifications")
        {
            SearchRept.Style.Add("display", "none");
        }



    }

    private void SendMail(string status, string TableName)
    {
        try
        {
            DataTable dtRecord = new DataTable();
            DataTable dtGrpRecord = new DataTable();
            string body = null;
            string Name = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            string mailfrom = ConfigurationManager.AppSettings["mailfrom"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string username = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailSSL = ConfigurationManager.AppSettings["MailSSL"];
            string MailTo = "";
            if (TableName == "Ask For Recommendation" && status == "Confirm")
                MailTo = Convert.ToString(ViewState["AskForRecommMailId"]);
            else
                MailTo = hdnEmailId.Value;
            string Mailbody = "";

            dtRecord.Clear();
            objdoreg.UserName = MailTo;
            dtRecord = objdareg.GetDataTableRecord(objdoreg, DA_Registrationdetails.RegistrationDetails.UserRecordByMail);
            Name = Convert.ToString(dtRecord.Rows[0]["vchrFirstName"] + " " + dtRecord.Rows[0]["vchrLastName"]);
            dtGrpRecord.Clear();
            dtGrpRecord = objGrpJoinDA.GetDataTable(objGrpJoinDO, DA_Scrl_UserGroupJoin.Scrl_UserGroupJoin.GetSingleGroupRecord);
            SmtpClient clientip = new SmtpClient(mailServer);
            clientip.Port = Convert.ToInt32(Port);
            clientip.UseDefaultCredentials = false;
            if (MailSSL != "0")
                clientip.EnableSsl = true;

            clientip.Credentials = new System.Net.NetworkCredential(username, Password);
            string DisplayName = ConfigurationManager.AppSettings["DisplayName"];

            try
            {
                MailMessage Rmm2 = new MailMessage();
                Rmm2.IsBodyHtml = true;
                Rmm2.From = new System.Net.Mail.MailAddress(mailfrom, DisplayName);
                Rmm2.Body = Mailbody.ToString();

                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("");
                if (TableName == "Scrl_UserRequestInvitationTbl")
                {
                    if (status == "Confirm")
                    {
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{UserName}", Name);
                        body = body.Replace("{Content}", "Your friend request has been accepted by <span style='font-weight:bold;font-size:14px;color:#373A36;'>" + Session["LoginName"] + "</span>" +
                                 "<br>");
                        body = body.Replace("{RedirectURL}", MailURL);

                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                        //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Request Invitation Status</b>"
                        //    + "<br><br>" + " "
                        //    + "Your Request Invitation has been accepted by " + Session["LoginName"] + ".<br><br><br>"
                        //    + "Regards," + "<br>" + "Skorkel Team"
                        //    + "<br><br>****This is a system generated Email. Kindly do not reply****", null, "text/html");
                        //Your friend request has been accepted by[Requestor First Name][Requestor Last Name]

                        //Rmm2.Subject = "Skorkel Request Invitation Status.";
                        string MailUrlFinal = MailURL + "/Profile2.aspx?RegId=" + Convert.ToString(ViewState["UserID"]);
                        Rmm2.Subject = "Friend Request Accepted - " + Session["LoginName"];

                        FCMNotification.Send("Friend Request Accepted - " + Session["LoginName"], "Your friend request has been accepted by " + Session["LoginName"],
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailUrlFinal);
                    }
                    else if (status == "RemoveNotification")
                    {
                        return;
                    }


                }
                else if (TableName == "Scrl_UserRecommendationTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Recommendation Status";
                }

                else if (TableName == "Scrl_UserGroupJoiningTbl")
                {
                    string MailUrlFinal = MailURL + "/Group-Profile.aspx?GrpId=" + ViewState["lblGroupName"];
                    if (status == "Confirm")
                    {
                        //htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Joining Status</b>"
                        //    + "<br><br>" + " " + "Your request to join " + ViewState["lblGroupName"] + " group has been accepted by "
                        //    + Session["LoginName"] + "<br><br><br>" + "Thanks,"
                        //    + "<br>" + "Skorkel Team"
                        //    + "<br><br>****This is a system generated Email. Kindly do not reply**** ", null, "text/html");

                        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{UserName}", Name);
                        body = body.Replace("{Content}", "You are a now a member of " + "<span style='font-weight:bold;font-size:14px;color:#373A36;'>" + ViewState["lblGroupName"] + "</span>." +
                            "<br><br><a href='" + MailUrlFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>View Group</a>");
                        body = body.Replace("{RedirectURL}", MailURL);

                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    }
                    else if (status == "RemoveNotification")
                    {
                        return;
                    }
                    Rmm2.Subject = "Request to Join " + ViewState["lblGroupName"] + " Accepted";

                    FCMNotification.Send("Request to Join " + ViewState["lblGroupName"] + " Accepted", "You are a now a member of " + ViewState["lblGroupName"] + ".",
                           Convert.ToString(dtRecord.Rows[0]["intRegistrationId"]), MailUrlFinal);
                }
                else if (TableName == "Scrl_UserRecommendationChildTbl")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Asked Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Asked Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Asked Recommendation Status";
                }
                else if (TableName == "Ask For Recommendation")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask For Recommendation Status</b>" + "<br><br>" + " " + Convert.ToString(ViewState["AskForRecommName"]) + " " + "This is a replay for your asked recommendation request,which is accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Ask Recommendation Status</b>" + "<br><br>" + " " + "Your request for recommendation has not been accepted by " + Session["LoginName"] + "<br><br><br>" + "Thanks," + "<br>" + "The Skorkel Team", null, "text/html");
                    }
                    Rmm2.Subject = "Skorkel Asked Recommendation ";
                }
                else if (TableName == "Scrl_RequestGroupJoin")
                {
                    if (status == "Confirm")
                    {
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<b>Skorkel Group Invitation Status</b>"
                            + "<br><br>Your invitation of " + ViewState["lblGroupName"] + " group joining has been accepted by "
                            + Session["LoginName"] + "<br><br>"

                            + "Regards," + "<br>" + "Skorkel Team"
                            + "<br><br>****This is a system generated Email. Kindly do not reply**** ", null, "text/html");
                    }
                    else if (status == "Delete")
                    {
                        return;
                    }
                    Rmm2.Subject = "Skorkel Group Invitation Status.";
                }
                Rmm2.To.Clear();
                Rmm2.To.Add(MailTo);

                Rmm2.AlternateViews.Add(htmlView);
                Rmm2.IsBodyHtml = true;
                clientip.Send(Rmm2);
                Rmm2.To.Clear();
            }
            catch (FormatException ex)
            {
                ex.Message.ToString();
                return;
            }
            catch (SmtpException ex)
            {
                ex.Message.ToString();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        objLogin.intRegistartionID = Convert.ToInt32(Session["ExternalUserId"]);
        if (objLogin.intRegistartionID != 0)
            objLoginDB.AddAndGetLoginDetails(objLogin, DA_SKORKEL.DA_Login.Login_1.Logout);
        Response.Cookies["myScrlCookie"].Expires = DateTime.Now.AddDays(-1);

        HttpCookie myScrlAppCookie = new HttpCookie("myScrlAppCookie");

        myScrlAppCookie.Value = "logout";

        myScrlAppCookie.HttpOnly = false;
        Response.Cookies.Add(myScrlAppCookie);

        //HttpCookie reqCookies = HttpContext.Current.Request.Cookies["myScrlCookie"];
        //reqCookies.Expires = DateTime.Now;
        //Response.Cookies.Remove("myScrlCookie");
        Session.Remove("noOfmethodCalled");
        Session.Remove("isScorePrivate");
        Response.Redirect("Landing.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string txtsearch = txtSearch.Text;
        Response.Redirect("UniversalSearch.aspx?srch=" + txtsearch, false);
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string search = TextBox1.Text;
    //}

    public HtmlGenericControl BodyTag
    {
        get
        {
            return bodyelement;
        }
        set
        {
            bodyelement = value;
        }
    }

    #region To Saving chatbot question:: Added on 17 March 2020
    protected void saveChatbotQuestion()
    {
        try
        {
            string chatboxQue = string.Empty;//txtChatboxQue.Text.Trim()
            if (!string.IsNullOrEmpty(chatboxQue) && Convert.ToInt32(Session["ExternalUserId"]) != 0)
            {
                string message = string.Empty;
                objdoreg.RegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
                objdoreg.Question = chatboxQue;
                objdoreg.Type = hdnQueryType.Value.Trim();
                DataSet ds = objdareg.checkScorePulic(objdoreg, DA_Registrationdetails.RegistrationDetails.saveChatbotQuestion);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    switch (Convert.ToString(ds.Tables[0].Rows[0]["Action"]).ToLower())
                    {
                        case "inserted":
                            chatboxQue = string.Empty;
                            SendMailToAdmin(Convert.ToString(ds.Tables[0].Rows[0]["nvchrQuestion"]), Convert.ToString(ds.Tables[0].Rows[0]["Email"]), Convert.ToString(ds.Tables[0].Rows[0]["Name"]), Convert.ToString(ds.Tables[0].Rows[0]["nvchrType"]));

                            chatboxicon.Attributes.Add("class", "chatbox-container d-block");
                            inputbutton.Attributes.Add("class", "d-none");
                            chaticon.Attributes.Add("class", "far fa-comment-dots cross");

                            chatmessage.Attributes.Add("class", "m-t-5 display-blockk");

                            switch (Convert.ToString(ds.Tables[0].Rows[0]["nvchrType"]).ToLower())
                            {
                                case "feedback":
                                    message = "Thanks! We will get back to you sortly.";
                                    break;
                                case "query":
                                    message = "Thanks! We will get back to you sortly.";
                                    break;
                                case "faq":
                                    message = "Thanks! We will get back to you sortly.";
                                    break;
                                default:
                                    message = string.Empty;
                                    break;
                            }

                            break;
                        case "usernotexists":
                            break;
                        default:
                            message = string.Empty;
                            break;
                    }
                    chatmessage.InnerText = message;
                    message = string.Empty;
                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void SendMailToAdmin(string question, string questionerEmail, string questionerName, string type)
    {
        try
        {
            //DataTable dtRecord = new DataTable();
            string body = null;

            string MailURL = ConfigurationManager.AppSettings["MailURL"];
            string MailTo = ConfigurationManager.AppSettings["adminMail"];
            string MailURLFinal = MailURL + "/Landing.aspx";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            //body = body.Replace("{UserName}", UserName);
            body = body.Replace("{UserName}", "Skorkel");
            body = body.Replace("{Content}", "<span style='font-size:14px;color:#373A36;'><b>" + questionerName + "</b> wants to know about below question </span> <br/><br/>" +
                                "<b>Message:</b><br/> " + question +
                                "<br/><br/><span style='font-weight:bold;font-size:14px;color:#373A36;'></span><br/> " +
                                "<b>User Details:</b> <br />" +
                                "<b>Name:</b> " + questionerName + "<br />" +
                                "<b>Email:</b> " + questionerEmail + "<br/><br/><br/><br/>" +
                                "Login URL:" + "&nbsp;&nbsp;<a href='" + MailURLFinal + "' style ='background: #01B7BD;padding: 5px 10px; border-radius: 15px;color: #fff;text-decoration: none;'>Login</a>");

            string subject = "" + type + " raised by <b>" + questionerName + "</b>";
            //EmailSender.SendEmail(MailTo, subject, body);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    //protected void btnSend_Click(object sender, EventArgs e)
    //{
    //    //saveChatbotQuestion();
    //}
    #endregion
}
