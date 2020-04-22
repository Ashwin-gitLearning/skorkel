using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Text.RegularExpressions;

public partial class Polls_VoteComment : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DO_Scrl_UserPollTbl objDOPoll = new DO_Scrl_UserPollTbl();
    DA_Scrl_UserPollTbl objDAPoll = new DA_Scrl_UserPollTbl();

    DO_Scrl_UserGroupDetailTbl objDO = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objDA = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_UserGroupDetailTbl objgrp = new DO_Scrl_UserGroupDetailTbl();
    DA_Scrl_UserGroupDetailTbl objgrpDB = new DA_Scrl_UserGroupDetailTbl();

    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();

    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();

    string APIURL = ConfigurationManager.AppSettings["APIURL"];
    string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    string UserTypeId = string.Empty, polOptionId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        lblMessageVote.Visible = false;
        lblMessage.Visible = false;
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Groups";

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }
            if (Request.QueryString["PollId"] != "" && Request.QueryString["PollId"] != null)
            {
                ViewState["intPollId"] = Request.QueryString["PollId"];
            }

            hdnCurrentPage.Value = "1";
            hdnTotalItem.Value = "5";
            BindPoll();
            pnlBarChart.Visible = true;
            objDOPoll.intAddedBy =Convert.ToInt32(ViewState["UserID"]);
            objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
            DataTable dtbar = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetVoteComent);
            if (dtbar.Rows.Count > 0)
            {
                divBarChart.Style.Add("display", "block");
            }
            else
            {
                divcommrnt.Value = "1";
            }
            AccessModulePermisssion();
            BindBarChart();
            CheckForGroupMember();
            BindComments();
            GetUserType();
        }

        BindBarChart();
    }

    protected void AccessModulePermisssion()
    {
        objDO.intUserId = Convert.ToInt32(ViewState["UserID"]);
        objDO.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.inGroupId = Convert.ToInt32(ViewState["intGroupId"]);
        objgrp.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        DataSet dschk = objgrpDB.GetDataSet(objgrp, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.ViewGrpAssignUser);
        DataTable dtRoleAP = objDA.GetDataTable(objDO, DA_Scrl_UserGroupDetailTbl.Scrl_UserGroupDetailTbl.GetGrpRoleRequestPermission);

        if (dschk.Tables[3].Rows.Count > 0)
        {
            if (dschk.Tables[3].Rows[0][0].ToString() == ViewState["UserID"].ToString())
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
                return;
            }
        }

        if (dtRoleAP.Rows.Count > 0)
        {
            if (dtRoleAP.Rows[0]["IsAccepted"].ToString() != "0" && dtRoleAP.Rows[0]["IsAccepted"].ToString() != "2")
            {
                DivHome.Style.Add("display", "block");
                DivForumTab.Style.Add("display", "block");
                DivUploadTab.Style.Add("display", "block");
                DivPollTab.Style.Add("display", "block");
                DivEventTab.Style.Add("display", "block");
                DivMemberTab.Style.Add("display", "block");
            }
            else
            {
                Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
            }
        }
        else
        {
            Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
        }
    }

    protected void GetUserType()
    {
        if (Convert.ToString(ViewState["FlagUser"]) == "1")
            UserTypeId = "S";
        else if (Convert.ToString(ViewState["FlagUser"]) == "2")
            UserTypeId = "P";
        else if (Convert.ToString(ViewState["FlagUser"]) == "3")
            UserTypeId = "LI";
        else if (Convert.ToString(ViewState["FlagUser"]) == "4")
            UserTypeId = "L";
        else if (Convert.ToString(ViewState["FlagUser"]) == "5")
            UserTypeId = "J";
        else if (Convert.ToString(ViewState["FlagUser"]) == "6")
            UserTypeId = "TP";
        else if (Convert.ToString(ViewState["FlagUser"]) == "7")
            UserTypeId = "LF";
    }

    protected void BindBarChart()
    {
        Color[] barColors = new Color[11]{
       Color.Purple,
       Color.SteelBlue,
       Color.Aqua,
       Color.Yellow,
       Color.Navy,
       Color.Green,
       Color.Blue,
       Color.Red,
        Color.Purple,
       Color.SteelBlue,
       Color.Aqua
   };

        objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);

        DataSet Dtall = objDAPoll.GetDataTableDS(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.BindChartall);
        string[] x = new string[Dtall.Tables.Count];
        int[] y = new int[Dtall.Tables.Count];


        for (int i = 0; i < Dtall.Tables.Count; i++)
        {
            x[i] = Dtall.Tables[i].Rows[0]["strOption"].ToString();
            y[i] = Convert.ToInt32(Dtall.Tables[i].Rows[0]["OpCount"]);            
        }

        //Chart1.Series[0].Points.DataBindXY(x, y);
        //Chart1.Series[0].ChartType = SeriesChartType.Column;
        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        //Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        //Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        //Chart1.ChartAreas["ChartArea1"].AxisX.LineWidth = 0;
        //Chart1.ChartAreas["ChartArea1"].AxisY.LineWidth = 0;
        //Chart1.Series[0]["PixelPointWidth"] = "20";
        //Chart1.ChartAreas["ChartArea1"].AxisY.Enabled = AxisEnabled.False;
        //Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsEndLabelVisible = false;

        //Chart1.Series[0].Label = "#PERCENT";
        //for (int i = 0; i < Dtall.Tables.Count; i++)
        //{
        //    Chart1.Series[0].Points[i].Color = barColors[i%barColors.Length];
        //}

        hdnBarOptions.Value = String.Join("%@%", x);
        hdnBarValues.Value = String.Join("%@%", y);

    }

    #region Vote Poll

    protected void BindPoll()
    {
        objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
        dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.SingleRecord);
        if (dt.Rows.Count > 0)
        {
           
            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["dtPollExpireDate"])))
            {
                if (Convert.ToDateTime(Convert.ToString(dt.Rows[0]["dtPollExpireDate"])) <= DateTime.Today)
                {
                    if (Convert.ToDateTime(Convert.ToString(dt.Rows[0]["strPollExpireTime"])) < Convert.ToDateTime(DateTime.Now.ToShortTimeString()))
                    {

                        lnkVote.Visible = false;
                        lblMessageVote.Text = "Voting date is expired.";
                        lblMessageVote.CssClass = "RedErrormsg";
                        lblMessageVote.Visible = true;
                        pnlBarChart.Visible = true;
                        BindBarChart();
                        divBarChart.Style.Add("display", "block");
                    }
                }
            }

            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["strVoteType"])))
            {
                ViewState["strVoteType"] = Convert.ToString(dt.Rows[0]["strVoteType"]);
            }


            lstPoll.DataSource = dt;
            lstPoll.DataBind();
        }
        else
        {
            lstPoll.DataSource = null;
            lstPoll.DataBind();
        }
    }

    protected void CheckForGroupMember()
    {
        if (Convert.ToString(ViewState["strVoteType"]) == "Group Member")
        {
            dt.Clear();
            objDOPoll.intGroupId = Convert.ToInt32(ViewState["intGroupId"]);
            objDOPoll.intRegistrationId = Convert.ToInt32(Session["ExternalUserId"]);
            dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.PollGroupMemberOnly);
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                lnkVote.Visible = false;
                lblMessageVote.Text = "Only group member(s) can vote this poll.";
                lblMessageVote.CssClass = "RedErrormsg";
                lblMessageVote.Visible = true;
                return;
            }
        }
    }

    protected void lstPoll_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblVoters = (Label)e.Item.FindControl("lblVoters");
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        Label lblExpiredt = (Label)e.Item.FindControl("lblExpiredt");
        HtmlControl divHorizontal = (HtmlControl)e.Item.FindControl("divHorizontal");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lnkDeleteEvent = (LinkButton)e.Item.FindControl("lnkDelete1");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit1");
        string UserID = Convert.ToString(ViewState["UserID"]);
        HtmlGenericControl spanEditDelete = (HtmlGenericControl)e.Item.FindControl("spanEditDelete");

        if (hdnRegistrationId.Value == UserID)
        {
            spanEditDelete.Visible = true;
            lnkDeleteEvent.Visible = true;
            lnkEdit.Visible = true;
        }
        else
        {
            lnkDeleteEvent.Visible = false;
            lnkEdit.Visible = false;
        }

        if (lblDescription.Text == "")
        { lblDescription.Visible = false; }

        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/profile-photo.png";
        }

        if (lblExpiredt.Text == "")
        {
            lblExpiredt.Text = "Never";
        }

        if (String.IsNullOrEmpty(lblVoters.Text))
            lblVoters.Text = "0 Votes";

        Panel pnlRadio = (Panel)e.Item.FindControl("pnlRadio");
        Panel pnlCheckBox = (Panel)e.Item.FindControl("pnlCheckBox");
        HiddenField hdnVotingPattern = (HiddenField)e.Item.FindControl("hdnVotingPattern");

        objDOPoll.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
        

        if (hdnVotingPattern.Value == "Single")
        {
            pnlRadio.Visible = true;
            HtmlControl tr1 = (HtmlControl)e.Item.FindControl("tr1");
            HtmlControl tr2 = (HtmlControl)e.Item.FindControl("tr2");
            HtmlControl tr3 = (HtmlControl)e.Item.FindControl("tr3");
            HtmlControl tr4 = (HtmlControl)e.Item.FindControl("tr4");
            HtmlControl tr5 = (HtmlControl)e.Item.FindControl("tr5");
            HtmlControl tr6 = (HtmlControl)e.Item.FindControl("tr6");
            HtmlControl tr7 = (HtmlControl)e.Item.FindControl("tr7");
            HtmlControl tr8 = (HtmlControl)e.Item.FindControl("tr8");
            HtmlControl tr9 = (HtmlControl)e.Item.FindControl("tr9");
            HtmlControl tr10 = (HtmlControl)e.Item.FindControl("tr10");

            RadioButton rdbQ1 = (RadioButton)e.Item.FindControl("rdbQ1");
            RadioButton rdbQ2 = (RadioButton)e.Item.FindControl("rdbQ2");
            RadioButton rdbQ3 = (RadioButton)e.Item.FindControl("rdbQ3");
            RadioButton rdbQ4 = (RadioButton)e.Item.FindControl("rdbQ4");
            RadioButton rdbQ5 = (RadioButton)e.Item.FindControl("rdbQ5");
            RadioButton rdbQ6 = (RadioButton)e.Item.FindControl("rdbQ6");
            RadioButton rdbQ7 = (RadioButton)e.Item.FindControl("rdbQ7");
            RadioButton rdbQ8 = (RadioButton)e.Item.FindControl("rdbQ8");
            RadioButton rdbQ9 = (RadioButton)e.Item.FindControl("rdbQ9");
            RadioButton rdbQ10 = (RadioButton)e.Item.FindControl("rdbQ10");

            if (string.IsNullOrEmpty(rdbQ1.Text))
                tr1.Visible = false;
            if (string.IsNullOrEmpty(rdbQ2.Text))
                tr2.Visible = false;
            if (string.IsNullOrEmpty(rdbQ3.Text))
                tr3.Visible = false;
            if (string.IsNullOrEmpty(rdbQ4.Text))
                tr4.Visible = false;
            if (string.IsNullOrEmpty(rdbQ5.Text))
                tr5.Visible = false;
            if (string.IsNullOrEmpty(rdbQ6.Text))
                tr6.Visible = false;
            if (string.IsNullOrEmpty(rdbQ7.Text))
                tr7.Visible = false;
            if (string.IsNullOrEmpty(rdbQ8.Text))
                tr8.Visible = false;
            if (string.IsNullOrEmpty(rdbQ9.Text))
                tr9.Visible = false;
            if (string.IsNullOrEmpty(rdbQ10.Text))
                tr10.Visible = false;
        }
        else if (hdnVotingPattern.Value == "Multiple")
        {
            pnlCheckBox.Visible = true;
            HtmlControl tr11 = (HtmlControl)e.Item.FindControl("tr11");
            HtmlControl tr12 = (HtmlControl)e.Item.FindControl("tr12");
            HtmlControl tr13 = (HtmlControl)e.Item.FindControl("tr13");
            HtmlControl tr14 = (HtmlControl)e.Item.FindControl("tr14");
            HtmlControl tr15 = (HtmlControl)e.Item.FindControl("tr15");
            HtmlControl tr16 = (HtmlControl)e.Item.FindControl("tr16");
            HtmlControl tr17 = (HtmlControl)e.Item.FindControl("tr17");
            HtmlControl tr18 = (HtmlControl)e.Item.FindControl("tr18");
            HtmlControl tr19 = (HtmlControl)e.Item.FindControl("tr19");
            HtmlControl tr20 = (HtmlControl)e.Item.FindControl("tr20");

            CheckBox chkQ1 = (CheckBox)e.Item.FindControl("chkQ1");
            CheckBox chkQ2 = (CheckBox)e.Item.FindControl("chkQ2");
            CheckBox chkQ3 = (CheckBox)e.Item.FindControl("chkQ3");
            CheckBox chkQ4 = (CheckBox)e.Item.FindControl("chkQ4");
            CheckBox chkQ5 = (CheckBox)e.Item.FindControl("chkQ5");
            CheckBox chkQ6 = (CheckBox)e.Item.FindControl("chkQ6");
            CheckBox chkQ7 = (CheckBox)e.Item.FindControl("chkQ7");
            CheckBox chkQ8 = (CheckBox)e.Item.FindControl("chkQ8");
            CheckBox chkQ9 = (CheckBox)e.Item.FindControl("chkQ9");
            CheckBox chkQ10 = (CheckBox)e.Item.FindControl("chkQ10");

            if (string.IsNullOrEmpty(chkQ1.Text))
                tr11.Visible = false;
            if (string.IsNullOrEmpty(chkQ2.Text))
                tr12.Visible = false;
            if (string.IsNullOrEmpty(chkQ3.Text))
                tr13.Visible = false;
            if (string.IsNullOrEmpty(chkQ4.Text))
                tr14.Visible = false;
            if (string.IsNullOrEmpty(chkQ5.Text))
                tr15.Visible = false;
            if (string.IsNullOrEmpty(chkQ6.Text))
                tr16.Visible = false;
            if (string.IsNullOrEmpty(chkQ7.Text))
                tr17.Visible = false;
            if (string.IsNullOrEmpty(chkQ8.Text))
                tr18.Visible = false;
            if (string.IsNullOrEmpty(chkQ9.Text))
                tr19.Visible = false;
            if (string.IsNullOrEmpty(chkQ10.Text))
                tr20.Visible = false;
        }

        DataTable dtbar = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetVoteComent);
        if (dtbar.Rows.Count > 0)
        {
            pnlRadio.Visible = false;
            pnlCheckBox.Visible = false;
            lnkVote.Visible = false;
        }
       
    }

    protected void lstPoll_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        Label lnkPollName= (Label)e.Item.FindControl("lnkPollName");
        ViewState["lnkPollName"] = lnkPollName.Text;
        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
        else if (e.CommandName == "DeletePoll")
        {
            BindBarChart();
            BindComments();
            divDeletesucess.Style.Add("display", "block");
        }
        else if (e.CommandName == "Edit Poll")
        {
            Response.Redirect("create-poll.aspx?GrpId=" + ViewState["intGroupId"] + "&PollId=" + Convert.ToInt32(ViewState["intPollId"]));
        }
    }

    protected void lnkVote_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        for (int i = 0; i < lstPoll.Items.Count; i++)
        {
            HiddenField hdnVotingPattern = lstPoll.Items[i].FindControl("hdnVotingPattern") as HiddenField;

            objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);

            if (hdnVotingPattern.Value == "Single")
            {
                RadioButton rdbQ1 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ1");
                RadioButton rdbQ2 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ2");
                RadioButton rdbQ3 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ3");
                RadioButton rdbQ4 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ4");

                RadioButton rdbQ5 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ5");
                RadioButton rdbQ6 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ6");
                RadioButton rdbQ7 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ7");
                RadioButton rdbQ8 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ8");
                RadioButton rdbQ9 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ9");
                RadioButton rdbQ10 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ10");

                if (rdbQ1.Checked == false && rdbQ2.Checked == false && rdbQ3.Checked == false && rdbQ4.Checked == false && rdbQ5.Checked == false && rdbQ6.Checked == false && rdbQ7.Checked == false && rdbQ8.Checked == false && rdbQ9.Checked == false && rdbQ10.Checked == false)
                {
                    lblMessageVote.Text = "Please Select Option";
                    lblMessageVote.ForeColor = System.Drawing.Color.Red;
                    lblMessageVote.Visible = true;
                    return;
                }
                if (rdbQ1.Checked == true)
                {
                    objDOPoll.bitOption1 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption1 = false;

                if (rdbQ2.Checked == true)
                {
                    objDOPoll.bitOption2 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption2 = false;

                if (rdbQ3.Checked == true)
                {
                    objDOPoll.bitOption3 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption3 = false;

                if (rdbQ4.Checked == true)
                {
                    objDOPoll.bitOption4 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption4 = false;

                if (rdbQ5.Checked == true)
                {
                    objDOPoll.bitOption5 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption5 = false;

                if (rdbQ6.Checked == true)
                {
                    objDOPoll.bitOption6 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption6 = false;

                if (rdbQ7.Checked == true)
                {
                    objDOPoll.bitOption7 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption7 = false;

                if (rdbQ8.Checked == true)
                {
                    objDOPoll.bitOption8 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption8 = false;

                if (rdbQ9.Checked == true)
                {
                    objDOPoll.bitOption9 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption9 = false;

                if (rdbQ10.Checked == true)
                {
                    objDOPoll.bitOption10 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption10 = false;

                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];

                objDOPoll.strIpAddress = ip;
                objDOPoll.intAddedBy = Convert.ToInt32(ViewState["UserID"]);


                objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.InsertOptions);
                if (objDOPoll.intPollOutId == 1)
                {
                    lblMessageVote.Text = "Vote saved successfully.";
                    lblMessageVote.ForeColor = System.Drawing.Color.Green;
                    lblMessageVote.Visible = true;
                    if (ISAPIURLACCESSED == "1")
                    {
                        try
                        {
                            String url = APIURL + "createUserForPol.action?" +
                                            "userId=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                            "&polId=POL" + objDOPoll.intPollId +
                                            "&polOptionId=" + polOptionId;

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                            myRequest1.Method = "GET";
                            WebResponse myResponse1 = myRequest1.GetResponse();

                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url;
                            objAPILogDO.strAPIType = "Group Poll Vote";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                        }
                        catch
                        {

                        }
                    }

                }
                else
                {
                    lblMessageVote.Text = "You have already voted for this poll.";
                    lblMessageVote.ForeColor = System.Drawing.Color.Red;
                    lblMessageVote.Visible = true;
                }

            }
            else if (hdnVotingPattern.Value == "Multiple")
            {
                CheckBox chkQ1 = (CheckBox)lstPoll.Items[i].FindControl("chkQ1");
                CheckBox chkQ2 = (CheckBox)lstPoll.Items[i].FindControl("chkQ2");
                CheckBox chkQ3 = (CheckBox)lstPoll.Items[i].FindControl("chkQ3");
                CheckBox chkQ4 = (CheckBox)lstPoll.Items[i].FindControl("chkQ4");
                CheckBox chkQ5 = (CheckBox)lstPoll.Items[i].FindControl("chkQ5");
                CheckBox chkQ6 = (CheckBox)lstPoll.Items[i].FindControl("chkQ6");
                CheckBox chkQ7 = (CheckBox)lstPoll.Items[i].FindControl("chkQ7");
                CheckBox chkQ8 = (CheckBox)lstPoll.Items[i].FindControl("chkQ8");
                CheckBox chkQ9 = (CheckBox)lstPoll.Items[i].FindControl("chkQ9");
                CheckBox chkQ10 = (CheckBox)lstPoll.Items[i].FindControl("chkQ10");

                if (chkQ1.Checked == false && chkQ2.Checked == false && chkQ3.Checked == false && chkQ4.Checked == false && chkQ5.Checked == false && chkQ6.Checked == false && chkQ7.Checked == false && chkQ8.Checked == false && chkQ9.Checked == false && chkQ10.Checked == false)
                {
                    lblMessageVote.Text = "Please Select Option";
                    lblMessageVote.ForeColor = System.Drawing.Color.Red;
                    lblMessageVote.Visible = true;
                    return;
                }

                if (chkQ1.Checked == true)
                {
                    objDOPoll.bitOption1 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption1 = false;

                if (chkQ2.Checked == true)
                {
                    objDOPoll.bitOption2 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption2 = false;

                if (chkQ3.Checked == true)
                {
                    objDOPoll.bitOption3 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption3 = false;

                if (chkQ4.Checked == true)
                {
                    objDOPoll.bitOption4 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption4 = false;

                if (chkQ5.Checked == true)
                {
                    objDOPoll.bitOption5 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption5 = false;

                if (chkQ6.Checked == true)
                {
                    objDOPoll.bitOption6 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption6 = false;

                if (chkQ7.Checked == true)
                {
                    objDOPoll.bitOption7 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption7 = false;

                if (chkQ8.Checked == true)
                {
                    objDOPoll.bitOption8 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption8 = false;

                if (chkQ9.Checked == true)
                {
                    objDOPoll.bitOption9 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption9 = false;

                if (chkQ10.Checked == true)
                {
                    objDOPoll.bitOption10 = true;
                    polOptionId = "true";
                }
                else
                    objDOPoll.bitOption10 = false;

                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null)
                    ip = Request.ServerVariables["REMOTE_ADDR"];

                objDOPoll.strIpAddress = ip;
                objDOPoll.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

                objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.InsertOptions);
                if (objDOPoll.intPollOutId == 1)
                {
                    if (ISAPIURLACCESSED == "1")
                    {
                        try
                        {
                            String url = APIURL + "createUserForPol.action?" +
                                            "userId=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                            "&polId=POL" + objDOPoll.intPollId +
                                            "&polOptionId=" + polOptionId;

                            HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                            myRequest1.Method = "GET";
                            WebResponse myResponse1 = myRequest1.GetResponse();

                            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                            String result = sr.ReadToEnd();

                            objAPILogDO.strURL = url;
                            objAPILogDO.strAPIType = "Group Poll Vote";
                            objAPILogDO.strResponse = result;
                            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                            objAPILogDO.strIPAddress = ip;
                            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);

                        }
                        catch
                        {

                        }
                    }
                    lblMessageVote.Text = "Vote saved successfully.";
                    lblMessageVote.ForeColor = System.Drawing.Color.Green;
                    lblMessageVote.Visible = true;
                }
                else
                {
                    lblMessageVote.Text = "You have already voted for this poll.";
                    lblMessageVote.ForeColor = System.Drawing.Color.Red;
                    lblMessageVote.Visible = true;
                }
            }

        }
        Clear();
        pnlBarChart.Visible = true;
        lnkVote.Visible = false;
        lblMessageVote.Visible = false;
        BindBarChart();
        divBarChart.Style.Add("display", "block");
        divcommrnt.Value = "0";
        BindPoll();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSuccess", "openPollSuccessPopup('Success', '"+ lblMessageVote.Text + "')", true);
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "block");
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    private void Clear()
    {
        for (int i = 0; i < lstPoll.Items.Count; i++)
        {
            HiddenField hdnVotingPattern = lstPoll.Items[i].FindControl("hdnVotingPattern") as HiddenField;
            objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);

            if (hdnVotingPattern.Value == "Single")
            {
                RadioButton rdbQ1 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ1");
                RadioButton rdbQ2 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ2");
                RadioButton rdbQ3 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ3");
                RadioButton rdbQ4 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ4");
                RadioButton rdbQ5 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ5");
                RadioButton rdbQ6 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ6");
                RadioButton rdbQ7 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ7");
                RadioButton rdbQ8 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ8");
                RadioButton rdbQ9 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ9");
                RadioButton rdbQ10 = (RadioButton)lstPoll.Items[i].FindControl("rdbQ10");
                rdbQ1.Checked = false;
                rdbQ2.Checked = false; rdbQ3.Checked = false; rdbQ4.Checked = false;
                rdbQ5.Checked = false; rdbQ6.Checked = false; rdbQ7.Checked = false;
                rdbQ8.Checked = false; rdbQ9.Checked = false; rdbQ10.Checked = false;
            }
            else if (hdnVotingPattern.Value == "Multiple")
            {
                CheckBox chkQ1 = (CheckBox)lstPoll.Items[i].FindControl("chkQ1");
                CheckBox chkQ2 = (CheckBox)lstPoll.Items[i].FindControl("chkQ2");
                CheckBox chkQ3 = (CheckBox)lstPoll.Items[i].FindControl("chkQ3");
                CheckBox chkQ4 = (CheckBox)lstPoll.Items[i].FindControl("chkQ4");
                CheckBox chkQ5 = (CheckBox)lstPoll.Items[i].FindControl("chkQ5");
                CheckBox chkQ6 = (CheckBox)lstPoll.Items[i].FindControl("chkQ6");
                CheckBox chkQ7 = (CheckBox)lstPoll.Items[i].FindControl("chkQ7");
                CheckBox chkQ8 = (CheckBox)lstPoll.Items[i].FindControl("chkQ8");
                CheckBox chkQ9 = (CheckBox)lstPoll.Items[i].FindControl("chkQ9");
                CheckBox chkQ10 = (CheckBox)lstPoll.Items[i].FindControl("chkQ10");

                chkQ1.Checked = false; chkQ2.Checked = false; chkQ3.Checked = false;
                chkQ4.Checked = false; chkQ5.Checked = false; chkQ6.Checked = false;
                chkQ7.Checked = false; chkQ8.Checked = false; chkQ9.Checked = false;
                chkQ10.Checked = false;
            }
        }
    }
    #endregion

    #region Poll Comments
    protected void BindComments()
    {
        dvPage.Visible = false;
        objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
        dt = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.BindChildList);

        if (dt.Rows.Count > 0)
        {
            lstPollsComment.Visible = true;
            lstPollsComment.DataSource = dt;
            lstPollsComment.DataBind();
            totalComments.Visible = true;
            totalComments.InnerText = Convert.ToInt32(dt.Rows.Count) + ((dt.Rows.Count == 1) ? " Comment" : " Comments");
        }
        else
        {
            lstPollsComment.DataSource = null;
            lstPollsComment.DataBind();
            lstPollsComment.Visible = false;
            totalComments.Visible = false;
        }
    }

    protected void lnkPost_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (!string.IsNullOrEmpty(txtComment.InnerHtml))
        {
            objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
            objDOPoll.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            //objDOPoll.strComment = txtComment.InnerHtml;

            string editor = txtComment.InnerText.Trim().Replace("<p>", "<br>").Replace("</p>", "");
            string noHTML = Regex.Replace(editor, @"<[^>]+>|&nbsp;", "").Trim();
            objDOPoll.strComment = Regex.Replace(noHTML, @"\s{2,}", " ");

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];
            objDOPoll.strIpAddress = ip;

            if (ViewState["EditComment"] == null)
            {
                objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.InsertComment);
                BindComments();
                lblMessage.Text = "Comment posted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;
            }
            else
            {
                objDOPoll.intCommentId =Convert.ToInt32(ViewState["EditComment"]);
                objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.UpdateComment);
                BindComments();
                lblMessage.Text = "Comment update successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;
            }
            ViewState["EditComment"] = null;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSuccess", "openPollSuccessPopup('Success', '" + lblMessage.Text + "')", true);
            lblMessage.Visible = false;
            txtComment.InnerText = "";
            BindBarChart();
        }
    }

    protected void lnkShowResult_Click(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        lblMessage.Text = "";
        lblMessageVote.Text = "";
        lblMessageVote.Visible = false;
        lblMessage.Visible = false;
        if (dvShowComments.Style["display"] == "none")
            dvShowComments.Style.Add("display", "block");
        else
            dvShowComments.Style.Add("display", "none");
    }

    protected void lstPollsComment_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlImage imgprofile = (HtmlImage)e.Item.FindControl("imgprofile");
        HiddenField hdnimgprofile = e.Item.FindControl("hdnimgprofile") as HiddenField;

        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        LinkButton lnkDeleteEvent = (LinkButton)e.Item.FindControl("lnkDeletes");
        LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdits");
        string UserID = Convert.ToString(ViewState["UserID"]);
        HtmlGenericControl divEditDelete = (HtmlGenericControl)e.Item.FindControl("divEditDelete");

        if (hdnRegistrationId.Value == UserID)
        {
            lnkDeleteEvent.Visible = true;
            lnkEdit.Visible = true;
            divEditDelete.Visible = true;
        }
        else
        {
            lnkDeleteEvent.Visible = false;
            lnkEdit.Visible = false;
        }

        if (imgprofile.Src == "" || imgprofile.Src == null || imgprofile.Src == "CroppedPhoto/")
        {
            imgprofile.Src = "images/profile-photo.png";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.Src = "images/profile-photo.png";
            }
        }
    }

    protected void lstPollsComment_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        HiddenField hdnRegistrationId = (HiddenField)e.Item.FindControl("hdnRegistrationId");
        HiddenField hdnintCommentId = (HiddenField)e.Item.FindControl("hdnintCommentId");
        Label lblName=(Label)e.Item.FindControl("lblName");
        ViewState["hdnintCommentId"] = hdnintCommentId.Value;
        ViewState["commentname"] = lblName.Text;
        if (e.CommandName == "Details")
        {
            Response.Redirect("Home.aspx?RegId=" + hdnRegistrationId.Value);
        }
        else if (e.CommandName == "DeletePoll")
        {
            BindBarChart();
            divDeletesucess.Style.Add("display", "block");
        }
        else if (e.CommandName == "Edit Poll")
        {
            objDOPoll.intCommentId = Convert.ToInt32(hdnintCommentId.Value);
            ViewState["EditComment"] = hdnintCommentId.Value;
            DataTable dtComment = objDAPoll.GetDataTable(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.GetCommentById);
            if (dtComment.Rows.Count > 0)
            {
                txtComment.InnerText = dtComment.Rows[0]["strComment"].ToString();
                txtComment.Focus();
            }
        }
    }

    #endregion

    #region Paging For All

    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {

        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {

            Int64 DisplayPage = 10;

            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);

            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;    // ((40 /10) - 1) * 10

            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            if (totalPage < EndPage)
            {
                EndPage = totalPage;
            }

            if (totalPage == 1)
            {
                lnkPrevious.Visible = false;
                lnkFirst.Visible = false;

                lnkNext.Visible = false;
                lnkLast.Visible = false;

                rptDvPage.DataSource = null;
                rptDvPage.DataBind();
            }
            else
            {
                DataTable dtPage = new DataTable();
                DataColumn PageNo = new DataColumn();
                PageNo.DataType = System.Type.GetType("System.String");
                PageNo.ColumnName = "intPageNo";
                dtPage.Columns.Add(PageNo);

                for (Int64 i = StartPage; i <= EndPage; i++)
                {
                    dtPage.Rows.Add(i.ToString());
                }

                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();


                if (CurrentPage > DisplayPage)
                {
                    lnkFirst.Visible = true;
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (StartPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                    lnkFirst.Visible = false;
                }
                if (totalPage > EndPage)
                {
                    lnkNext.Visible = true;
                    hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPage.Value = totalPage.ToString();
                    lnkLast.Visible = true;
                }
                else
                {
                    lnkNext.Visible = false;
                    lnkLast.Visible = false;
                }
            }
        }

    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnNextPage.Value;
        BindComments();
    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        BindComments();
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnLastPage.Value;
        BindComments();
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = hdnPreviousPage.Value;
        BindComments();
    }

    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Enabled = false;
                BindComments();
            }
        }
    }

    protected void rptDvPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                if (hdnCurrentPage.Value == lnkPageLink.Text)
                {
                    lnkPageLink.Enabled = false;
                }

                else
                {
                    lnkPageLink.Enabled = true;
                }
            }
        }
    }

    #endregion

    #region Tabs
    protected void lnkProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Profile.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkUploadTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    #endregion

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        lblMessageVote.Text = "";
        lblMessageVote.Visible = false;
        if (hdnDeletePostQuestionID.Value == "")
        {
            objDOPoll.intPollId = Convert.ToInt32(ViewState["intPollId"]);
            objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.Delete);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["intPollId"]);
            objLog.strAction = "Group Poll";
            objLog.strActionName = ViewState["lnkPollName"].ToString();
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 7;   // Group Poll
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            Response.Redirect("Polls-Details.aspx?DeletePoll=true&GrpId=" + ViewState["intGroupId"]);
        }
        else
        {

            objDOPoll.intCommentId = Convert.ToInt32(hdnDeletePostQuestionID.Value);
            objDAPoll.AddEditDel_Scrl_UserPollTbl(objDOPoll, DA_Scrl_UserPollTbl.Scrl_UserPollTbl.DeleteComment);

            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
                ip = Request.ServerVariables["REMOTE_ADDR"];

            objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.intActionId = Convert.ToInt32(ViewState["intPollId"]);
            objLog.strAction = "Group Poll Comment";
            objLog.strActionName = hdnstrQuestionDescription.Value;
            objLog.strIPAddress = ip;
            objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
            objLog.SectionId = 7;   // Group Poll
            objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
            BindComments();
            BindBarChart();
            ViewState["hdnintCommentId"] = null;
            divDeletesucess.Style.Add("display", "none");
        }
        hdnDeletePostQuestionID.Value = "";
    }


    protected void lnkDeleteCancel_Click(object sender, EventArgs e)
    {
        BindBarChart();
        divDeletesucess.Style.Add("display","none");
    }

}